using UnityEngine;
using System;
using System.Collections;

public struct NameValueAction<TOne, TTwo> {
	public TOne Name;
	public TTwo Value;
	public Action Action;

	public NameValueAction (TOne name, TTwo value, Action action = null) {
		this.Name   = name;
		this.Value  = value;
		this.Action = action;
	}

	public void InvokeAction () {
		if (this.Action != null) {
			this.Action();
		}
	}
}
