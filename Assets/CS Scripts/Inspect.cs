using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inspect : MonoBehaviour {
	[SerializeField]
	private List<string> messages   = new List<string>();
	[SerializeField]
	private bool crouchType         = false;
	private AudioSource audioSource = null;

	public bool CrouchType {
		get {
			return this.crouchType;
		}
	}

	void Start () {
		this.audioSource = this.GetComponent<AudioSource>();
	}

	public void StartSpeech (Speech speechScript, string title, Action onComplete = null) {
		if (this.audioSource != null) {
			this.audioSource.Play();
		}

		if (this.messages.Count < 1) {
			return;
		}

		int i;

		for (i = 0; i < this.messages.Count - 1; i++) {
			speechScript.AddMessage(title, this.messages[ i ], null);
		}

		speechScript.AddMessage(title, this.messages[ i ], onComplete);
	}
}
