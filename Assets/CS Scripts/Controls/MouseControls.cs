using UnityEngine;
using System.Collections;

public class MouseControls : MonoBehaviour {
	public float RunDistance          = 2.0f;

	[SerializeField]
	private GameObject character      = null; // Player
	private Character characterScript = null;

	public GameObject Character {
		get {
			return this.character;
		}
		set {
			this.character       = value;
			this.characterScript = (value == null) ? null : value.GetComponent<Character>() ;
		}
	}

	void Start () {
		this.Character = this.character;
	}
	
	void Update () {
		if (this.characterScript == null) {
			return;
		}

		if (!Input.GetMouseButton(0)) {
			this.characterScript.Moving = this.characterScript.Running = false;

			return;
		}

		var targetPoint                = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
		targetPoint.z                  = 0.0f;
		this.characterScript.Direction = 360.0f - (MathUtils.AngleBetween(this.character.transform.position, targetPoint) + 270.0f);
		this.characterScript.Moving    = true;
		this.characterScript.Running   = Vector3.Distance(this.character.transform.position, targetPoint) >= this.RunDistance;
	}
}
