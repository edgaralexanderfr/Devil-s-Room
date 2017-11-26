using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawn : MonoBehaviour {
	[SerializeField]
	private GameObject adam            = null;
	[SerializeField]
	private GameObject characterShadow = null;

	void Awake () {
		var entries = GameObject.FindGameObjectsWithTag("Entry");

		foreach (GameObject entry in entries) {
			var entryScript = entry.GetComponent<Entry>();

			if (entryScript.EntryIndex != GS.Get<byte>("NextRoomEntryIndex")) {
				continue;
			}

			var character = (GameObject) Instantiate(this.adam, new Vector3(entryScript.X, entryScript.Y, this.adam.transform.position.z), this.adam.transform.rotation);
			var shadow    = (GameObject) Instantiate(this.characterShadow, this.characterShadow.transform.position, this.characterShadow.transform.rotation);
			Camera.main.GetComponent<ParentPosition>().Parent              = 
			GameObject.Find("Controls").GetComponent<Controls>().Character = 
			shadow.GetComponent<ParentPosition>().Parent                   = character.transform.GetChild(0).gameObject;
			var characterScript                                            = character.transform.GetChild(0).GetComponent<Character>();
			characterScript.InitialDirection                               = entryScript.Direction;

			break;
		}
	}

	void Start () {
		var colorFilter = GameObject.Find("Color filter");

		if (colorFilter == null) {
			return;
		}

		var colorFilterFadeScript = colorFilter.GetComponent<UIImageFade>();

		if (colorFilterFadeScript != null) {
			colorFilterFadeScript.FadeOut();
		}
	}
}
