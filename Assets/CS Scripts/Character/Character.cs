using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	public float InitialDirection             = 0.0f;
	public string SpeechName                  = "";
	public ushort WalkSpeed                   = 4;
	public ushort RunSpeed                    = 10;
	public byte LongIdleWaitCount             = 3;
	public bool Moving                        = false;
	public bool Running                       = false;
	public bool FootstepSound                 = true;

	[SerializeField]
	private AudioClip[] audioClips            = null;
	private Animator animator                 = null;
	private UIImageFade colorFilterFadeScript = null;
	private Speech speechScript               = null;
	private GameObject collidedInspect        = null;
	private GameObject savedCollidedInspect   = null;
	private byte idleCount                    = 0;

	public float Direction {
		get {
			return this.transform.rotation.eulerAngles.y;
		}
		set {
			if (!this.Crouching) {
				this.transform.rotation = Quaternion.Euler(0.0f, value, 0.0f);
			}
		}
	}

	public bool Thinking {
		get {
			return AnimatorUtils.IsPlaying(this.animator, "SThink") || 
			       AnimatorUtils.IsPlaying(this.animator, "Think");
		}
	}

	public bool Crouching {
		get {
			return AnimatorUtils.IsPlaying(this.animator, "StartCrouch") || 
			       AnimatorUtils.IsPlaying(this.animator, "Crouch")      || 
			       AnimatorUtils.IsPlaying(this.animator, "StopCrouch");
		}
	}

	public bool HasCollidedInspect {
		get {
			return this.collidedInspect != null;
		}
	}

	void Start () {
		this.Direction             = this.InitialDirection;
		this.animator              = this.GetComponent<Animator>();
		this.colorFilterFadeScript = GameObject.Find("Color filter").GetComponent<UIImageFade>();
		this.speechScript          = GameObject.Find("Speech").GetComponent<Speech>();

		if (this.transform.parent != null) {
			var colliderEventsScript                   = this.transform.parent.gameObject.GetComponent<ColliderEvents>();
			colliderEventsScript.OnTriggerEnter2DEvent = OnTriggerEnter2D;
			colliderEventsScript.OnTriggerExit2DEvent  = OnTriggerExit2D;
		}
	}

	void Update () {
		this.UpdateMovement();
		this.UpdateAnimation();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Inspect") {
			this.collidedInspect = collider.gameObject;
		} else 
		if (collider.gameObject.tag == "Entry") {
			this.collidedInspect = collider.gameObject;
			this.EnterIfTrigger();
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject == this.collidedInspect) {
			this.collidedInspect = null;
		}
	}

	public void Action () {
		if (!this.HasCollidedInspect || this.Crouching) {
			return;
		}

		this.savedCollidedInspect = this.collidedInspect;
		var collidedInspectScript = this.savedCollidedInspect.GetComponent<Inspect>();

		if (collidedInspect.tag == "Inspect") {
			if (collidedInspectScript.CrouchType) {
				AnimatorUtils.PlayOnce(this.animator, "StartCrouch");
			} else {
				collidedInspectScript.StartSpeech(this.speechScript, this.SpeechName + ":");
			}
		} else 
		if (collidedInspect.tag == "Entry") {
			var entryScript = collidedInspect.GetComponent<Entry>();

			if (entryScript != null && entryScript.Enabled && !entryScript.IsTrigger) {
				this.EnterRoom(entryScript);
			}
		}
	}

	public void PlayStopCrouchAnimation () {
		AnimatorUtils.PlayOnce(this.animator, "StopCrouch");
	}

	private void PlayFootstepSound () {
		if (this.FootstepSound && this.audioClips != null && this.audioClips.Length > 0) {
			AudioSource.PlayClipAtPoint(this.audioClips[0], new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z));
		}
	}

	private void IncreaseIdleCount () {
		if (this.LongIdleWaitCount == 0) {
			return;
		}

		this.idleCount++;

		if (this.idleCount >= this.LongIdleWaitCount) {
			AnimatorUtils.PlayOnce(this.animator, "SThink");
		}
	}

	private void InvokeCrouchedAction () {
		if (this.savedCollidedInspect.gameObject.tag == "Inspect") {
			var collidedInspectScript = this.savedCollidedInspect.GetComponent<Inspect>();
			collidedInspectScript.StartSpeech(this.speechScript, this.SpeechName + ":", this.PlayStopCrouchAnimation);
		}
	}

	private void EnterIfTrigger () {
		var entryScript = this.collidedInspect.GetComponent<Entry>();

		if (entryScript == null || !entryScript.Enabled || !entryScript.IsTrigger) {
			return;
		}

		this.EnterRoom(entryScript);
	}

	private void EnterRoom (Entry entryScript) {
		if (this.colorFilterFadeScript != null) {
			this.colorFilterFadeScript.Opacity = 1.0f;
		}

		GS.Set("NextRoomEntryIndex", entryScript.NextRoomEntryIndex);
		SceneManager.LoadScene(entryScript.NextScene);
	}

	private void UpdateMovement () {
		if (this.Crouching || !this.Moving) {
			return;
		}

		float translationRads = MathUtils.ToRads(this.Direction + 90.0f);
		float translationX, translationY;

		if (this.Moving && this.Running) {
			translationX = -Mathf.Cos(translationRads) * this.RunSpeed * Time.deltaTime;
			translationY =  Mathf.Sin(translationRads) * this.RunSpeed * Time.deltaTime;
		} else {
			translationX = -Mathf.Cos(translationRads) * this.WalkSpeed * Time.deltaTime;
			translationY =  Mathf.Sin(translationRads) * this.WalkSpeed * Time.deltaTime;
		}

		if (this.transform.parent == null) {
			this.transform.position += new Vector3(translationX, translationY, 0.0f);
		} else {
			this.transform.parent.gameObject.transform.position += new Vector3(translationX, translationY, 0.0f);
		}
	}

	private void UpdateAnimation () {
		if (this.Crouching || (!this.Moving && this.Thinking)) {
			this.idleCount = 0;

			return;
		}

		if (this.Moving && this.Running) {
			this.idleCount = 0;
			AnimatorUtils.PlayOnce(this.animator, "Run");
		} else 
		if (this.Moving) {
			this.idleCount = 0;
			AnimatorUtils.PlayOnce(this.animator, "Walk");
		} else {
			AnimatorUtils.PlayOnce(this.animator, "Idle");
		}
	}
}
