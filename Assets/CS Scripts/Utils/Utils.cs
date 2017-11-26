using UnityEngine;
using System.Collections;

public class Utils {
	public static Bounds GetMaxBounds (GameObject gameObject) {
		var bounds = new Bounds(gameObject.transform.position, Vector3.zero);

		foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>()) {
			bounds.Encapsulate(renderer.bounds);
		}

		return bounds;
	}
}
