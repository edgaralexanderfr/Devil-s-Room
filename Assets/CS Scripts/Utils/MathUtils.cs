using UnityEngine;
using System.Collections;

public class MathUtils {
	public static float ToRads (float degrees) {
		return (degrees * Mathf.PI) / 180;
	}

	public static float AngleBetween (Vector2 a, Vector2 b) {
		var vector    = b - a;
		float angle   = Mathf.Atan2(vector.y, vector.x);
		float degrees = angle * Mathf.Rad2Deg;

		return (degrees < 0) ? degrees + 360 : degrees ;
	}
}
