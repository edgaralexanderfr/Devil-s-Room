using UnityEngine;
using System.Collections;

public class YDepth : MonoBehaviour {
	private Vector3 extents = Vector3.zero;

	void Start () {
		this.extents = Utils.GetMaxBounds(this.gameObject).extents;
	}
	
	void Update () {
		var position            = this.transform.position;
		position.z              = position.y - extents.y;
		this.transform.position = position;
	}
}
