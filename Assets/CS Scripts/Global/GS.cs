using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// GameState
public class GS {
	private static List<KeyValuePair<string, object>?> gs = new List<KeyValuePair<string, object>?>();

	public static KeyValuePair<string, object>? GetKeyValuePair (string name) {
		foreach (KeyValuePair<string, object> state in gs) {
			if (state.Key == name) {
				return state;
			}
		}

		return null;
	}

	public static T Get<T> (string name) {
		var state = GetKeyValuePair(name);

		if (state == null) {
			return default(T);
		}

		return (T) Convert.ChangeType(state.Value.Value, typeof(T));
	}

	public static void Set (string name, object value) {
		var state = GetKeyValuePair(name);

		if (state != null) {
			gs.Remove(state);
		}

		gs.Add(new KeyValuePair<string, object>(name, value));
	}
}
