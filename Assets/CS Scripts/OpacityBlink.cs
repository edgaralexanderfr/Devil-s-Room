using UnityEngine;
using System.Collections;

public class OpacityBlink : MonoBehaviour {
	[Range(0.0f, 1.0f)]
	public float Min          = 0.0f;
	[Range(0.0f, 1.0f)]
	public float Max          = 1.0f;
	[Range(0.0f, 1.0f)]
	public float Speed        = 1.0f;

	private Renderer renderer = null;
	private sbyte direction   = -1;

	void Start () {
		this.renderer = this.GetComponent<Renderer>();

		if (this.Min > this.Max) {
			float min = this.Min;
			Min       = this.Max;
			Max       = min;
		}
	}
	
	void Update () {
		var color = this.renderer.material.color;
		color.a  += (this.Speed * this.direction * Time.deltaTime);

		if (color.a < this.Min) {
			color.a   = this.Min;
			this.direction = 1;
		} else 
		if (color.a > this.Max) {
			color.a   = this.Max;
			this.direction = -1;
		}

		this.renderer.material.color = color;
	}
}
