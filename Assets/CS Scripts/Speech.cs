using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Speech : MonoBehaviour {
	[SerializeField]
	private float scaleSpeed                               = 0.5f;
	[SerializeField]
	private float messageSpeed                             = 0.01f;
	private GameObject background                          = null;
	private GameObject title                               = null;
	private GameObject message                             = null;
	private GameObject arrow                               = null;
	private Text titleText                                 = null;
	private Text messageText                               = null;
	private List<NameValueAction<string, string>> messages = new List<NameValueAction<string, string>>();
	private bool scalingUp                                 = false;
	private bool scalingDown                               = false;
	private NameValueAction<string, string> currentMessage = new NameValueAction<string, string>("", "");

	public bool Active {
		get {
			return this.background.activeSelf;
		}
	}

	public bool WritingSubject {
		get {
			return this.messageText.text != this.currentMessage.Value;
		}
	}

	void Start () {
		this.background  = GameObject.Find("Speech background");
		this.title       = GameObject.Find("Speech title");
		this.message     = GameObject.Find("Speech message");
		this.arrow       = GameObject.Find("Speech arrow");
		this.titleText   = this.title.GetComponent<Text>();
		this.messageText = this.message.GetComponent<Text>();
		this.HideComponents();
	}

	void Update () {
		this.UpdateSpeech();
	}

	public void AddMessage (string title, string message, Action onComplete = null) {
		this.messages.Add(new NameValueAction<string, string>(title, message, onComplete));

		if (this.messages.Count == 1) {
			this.Advance();
		}
	}

	public void Advance () {
		if (this.scalingUp || this.scalingDown) {
			return;
		}

		if (this.WritingSubject) {
			this.messageText.text = this.currentMessage.Value;
			CancelInvoke("WriteMessage");
		} else 
		if (this.background.transform.localScale.x == 1.0f) {
			this.title.SetActive(false);
			this.message.SetActive(false);
			this.arrow.SetActive(false);
			this.titleText.text = this.messageText.text = this.currentMessage.Value = "";
			this.scalingDown    = true;
		}
		if (this.messages.Count > 0 && this.background.transform.localScale.x == 0.0f) {
			this.background.SetActive(true);
			this.scalingUp = true;
		}
	}

	private void HideComponents () {
		this.background.transform.localScale = new Vector3(0.0f, this.background.transform.localScale.y, this.background.transform.localScale.z);
		this.background.SetActive(false);
		this.title.SetActive(false);
		this.message.SetActive(false);
		this.arrow.SetActive(false);
	}

	private void WriteMessage () {
		if (this.currentMessage.Value.Length > 0) {
			this.messageText.text += this.currentMessage.Value[ this.messageText.text.Length ];
		}

		if (this.messageText.text == this.currentMessage.Value) {
			CancelInvoke("WriteMessage");
		}
	}

	private void UpdateSpeech () {
		if (this.scalingUp && this.background.transform.localScale.x < 1.0f) {
			float nextScaleX = Mathf.Min(this.background.transform.localScale.x + this.scaleSpeed * Time.deltaTime, 1.0f);
			this.background.transform.localScale = new Vector3(nextScaleX, this.background.transform.localScale.y, this.background.transform.localScale.z);

			if (nextScaleX == 1.0f) {
				this.title.SetActive(true);
				this.message.SetActive(true);
				this.arrow.SetActive(true);
				this.scalingUp      = false;
				this.currentMessage = this.messages.First();
				this.titleText.text = this.currentMessage.Name;
				this.messages.Remove(this.currentMessage);
				InvokeRepeating("WriteMessage", 0.0f, this.messageSpeed);
			}
		} else 
		if (this.scalingDown && this.background.transform.localScale.x > 0.0f) {
			float nextScaleX = Mathf.Max(this.background.transform.localScale.x - this.scaleSpeed * Time.deltaTime, 0.0f);
			this.background.transform.localScale = new Vector3(nextScaleX, this.background.transform.localScale.y, this.background.transform.localScale.z);

			if (nextScaleX == 0.0f) {
				this.scalingDown = false;
				this.Advance();

				if (this.messages.Count == 0) {
					this.background.SetActive(false);
				}

				this.currentMessage.InvokeAction();
			}
		}
	}
}
