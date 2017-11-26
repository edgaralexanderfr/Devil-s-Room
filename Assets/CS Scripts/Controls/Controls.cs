using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Controls : MonoBehaviour {
	[SerializeField]
	private GameObject character      = null; // Player
	[SerializeField]
	private GameObject speech         = null;
	private GameObject coordinates    = null; // Debug coordinates
	private Text coordinatesText      = null;
	private Character characterScript = null;
	private Speech speechScript       = null;

	public GameObject Character {
		get {
			return this.character;
		}
		set {
			this.character       = value;
			this.characterScript = (value == null) ? null : value.GetComponent<Character>() ;
		}
	}

	public GameObject Speech {
		get {
			return this.speech;
		}
		set {
			this.speech       = value;
			this.speechScript = (value == null) ? null : value.GetComponent<Speech>() ;
		}
	}

	void Start () {
		this.Character   = this.character;
		this.Speech      = this.speech;
		this.coordinates = GameObject.Find("Coordinates");

		if (this.coordinates != null) {
			this.coordinatesText = this.coordinates.GetComponent<Text>();
		}
	}
	
	void Update () {
		if (this.characterScript == null) {
			return;
		}

		if (this.speechScript != null && this.speechScript.Active) {
			// Player can't move or do anything else but interact with the current speech...
			this.characterScript.Moving = false;

			if (Input.GetButtonDown("Action")) {
				this.speechScript.Advance();
			}

			return;
		}

		if (Input.GetButtonDown("Action")) {
			this.characterScript.Action();
		}

		this.characterScript.Running = Input.GetButton("Run");
		var targetPoint              = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

		if (Vector3.Distance(Vector3.zero, targetPoint) < 0.5f) {
			this.characterScript.Moving = false;
		} else {
			this.characterScript.Direction = 360.0f - (MathUtils.AngleBetween(Vector3.zero, targetPoint) + 270.0f);
			this.characterScript.Moving    = true;
		}

		if (this.coordinatesText != null) {
			if (this.character.transform.parent != null) {
				this.coordinatesText.text = "X: " + this.character.transform.parent.gameObject.transform.position.x + Environment.NewLine + 
											"Y: " + this.character.transform.parent.gameObject.transform.position.y + Environment.NewLine + 
											"Z: " + this.character.transform.position.z + Environment.NewLine + 
											"A: " + this.character.transform.rotation.eulerAngles.y;
			} else {
				this.coordinatesText.text = "X: " + this.character.transform.position.x + Environment.NewLine + 
											"Y: " + this.character.transform.position.y + Environment.NewLine + 
											"Z: " + this.character.transform.position.z + Environment.NewLine + 
											"A: " + this.character.transform.rotation.eulerAngles.y;
			}
		}
	}
}
