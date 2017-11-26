using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Image))]
public class UIImageFade : MonoBehaviour {
	public event Action OnComplete = null;

	[SerializeField]
	private float speed            = 1.0f;
	private Image uiImage          = null;
	private float targetOpacity    = 0.0f;

	public Image UIImage {
		get {
			return this.uiImage;
		}
	}

	public float Opacity {
		get {
			return this.uiImage.color.a;
		}
		set {
			if (value >= 0.0f && value <= 1.0f) {
				var color          = this.uiImage.color;
				color.a            = this.targetOpacity = value;
				this.uiImage.color = color;
			}
		}
	}

	void Awake () {
		this.uiImage = this.GetComponent<Image>();
	}
	
	void Update () {
		this.UpdateFade();
	}

	public void FadeIn () {
		var color          = this.uiImage.color;
		color.a            = 0.0f;
		this.uiImage.color = color;
		this.targetOpacity = 1.0f;
	}

	public void FadeOut () {
		var color          = this.uiImage.color;
		color.a            = 1.0f;
		this.uiImage.color = color;
		this.targetOpacity = 0.0f;
	}

	private void UpdateFade () {
		var color = this.uiImage.color;

		if (color.a == this.targetOpacity) {
			return;
		}

		if (color.a < this.targetOpacity) {
			color.a = Mathf.Min(color.a + this.speed * Time.deltaTime, this.targetOpacity);
		} else {
			color.a = Mathf.Max(color.a - this.speed * Time.deltaTime, this.targetOpacity);
		}

		this.uiImage.color = color;

		if (color.a == this.targetOpacity && this.OnComplete != null) {
			this.OnComplete();
		}
	}
}
