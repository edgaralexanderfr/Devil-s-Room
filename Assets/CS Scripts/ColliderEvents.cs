using UnityEngine;
using System;
using System.Collections;

public class ColliderEvents : MonoBehaviour {
	public Action<Collider2D> OnTriggerEnter2DEvent = null;
	public Action<Collider2D> OnTriggerExit2DEvent  = null;

	void OnTriggerEnter2D (Collider2D collider) {
		if (this.OnTriggerEnter2DEvent != null) {
			this.OnTriggerEnter2DEvent(collider);
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (this.OnTriggerExit2DEvent != null) {
			this.OnTriggerExit2DEvent(collider);
		}
	}
}
