using UnityEngine;
using System.Collections;

public class Entry : MonoBehaviour {
	[SerializeField]
	private bool enabled            = true;
	[SerializeField]
	private byte entryIndex         = 0;
	[SerializeField]
	private bool isTrigger          = false;
	[SerializeField]
	private string nextScene        = "";
	[SerializeField]
	private byte nextRoomEntryIndex = 0;
	[SerializeField]
	private float x                 = 0.0f;
	[SerializeField]
	private float y                 = 0.0f;
	[SerializeField]
	private float direction         = 0.0f;

	public bool Enabled {
		get {
			return this.enabled;
		}
	}

	public byte EntryIndex {
		get {
			return this.entryIndex;
		}
	}

	public bool IsTrigger {
		get {
			return this.isTrigger;
		}
	}

	public string NextScene {
		get {
			return this.nextScene;
		}
	}

	public byte NextRoomEntryIndex {
		get {
			return this.nextRoomEntryIndex;
		}
	}

	public float X {
		get {
			return this.x;
		}
	}

	public float Y {
		get {
			return this.y;
		}
	}

	public float Direction {
		get {
			return this.direction;
		}
	}

	void Start () {
		if (this.entryIndex == GS.Get<byte>("NextRoomEntryIndex")) {
			var audioSource = this.GetComponent<AudioSource>();

			if (audioSource != null) {
				audioSource.Play();
			}
		}
	}
}
