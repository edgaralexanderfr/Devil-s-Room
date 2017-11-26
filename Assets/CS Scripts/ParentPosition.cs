using UnityEngine;
using System.Collections;

public class ParentPosition : MonoBehaviour {
	public GameObject Parent       = null;
	public bool X                  = true;
	public bool Y                  = true;
	public bool Z                  = true;
	public bool AlignWithOriginalX = true;
	public bool AlignWithOriginalY = true;
	public bool AlignWithOriginalZ = true;
	
	private Vector3 originalPosition = Vector3.zero;

	void Start () {
		this.originalPosition = this.transform.position;
	}

	void LateUpdate () {
		if (this.Parent == null) {
			return;
		}

		var position = this.transform.position;

		if (this.X) {
			position.x = this.Parent.transform.position.x;

			if (this.AlignWithOriginalX) {
				position.x += this.originalPosition.x;
			}
		}

		if (this.Y) {
			position.y = this.Parent.transform.position.y;

			if (this.AlignWithOriginalY) {
				position.y += this.originalPosition.y;
			}
		}

		if (this.Z) {
			position.z = this.Parent.transform.position.z;

			if (this.AlignWithOriginalZ) {
				position.z += this.originalPosition.z;
			}
		}

		this.transform.position = position;
	}
}
