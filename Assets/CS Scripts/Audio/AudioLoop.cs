using UnityEngine;
using System.Collections;

public class AudioLoop : MonoBehaviour {
	[SerializeField]
	private AudioClip audioClip = null;
	private float timeSoundEnd  = 0.0f;

	void Update () {
		if (this.audioClip == null) {
			return;
		}

		if (Time.time > timeSoundEnd) {
			AudioSource.PlayClipAtPoint(this.audioClip, new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z));
			timeSoundEnd = Time.time + this.audioClip.length;
		}
	}
}
