using UnityEngine;
using System.Collections;

public class ColliderGizmo : MonoBehaviour {
	[SerializeField]
	private Color color = Color.green;

	void OnDrawGizmos () {
		var boxColliders2D = this.GetComponents<BoxCollider2D>();

		if (boxColliders2D == null || boxColliders2D.Length < 1) {
			return;
		}

		Gizmos.color = this.color;
		int i;

		for (i = 0; i < boxColliders2D.Length; i++) {
			Gizmos.DrawCube(
				new Vector3(boxColliders2D[ i ].offset.x, boxColliders2D[ i ].offset.y, this.transform.position.z), 
				new Vector3(boxColliders2D[ i ].size.x, boxColliders2D[ i ].size.y, 0.0f)
			);
		}
	}
}
