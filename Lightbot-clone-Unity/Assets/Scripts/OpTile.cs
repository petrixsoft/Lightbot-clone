using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpTile : MonoBehaviour {

	private Transform cTransform;
	private GameObject lastActive;

	void Awake()
	{
		cTransform = transform;
		Transform tr = cTransform.FindChild ("EMPTY");
		if (tr != null)
		{
			lastActive = tr.gameObject;
			lastActive.SetActive (true);
		}

	}

	public void ShowOp(string name)
	{
		Transform tr = cTransform.FindChild (name);

		if (tr != null)
		{
			GameObject go = tr.gameObject;
			lastActive.SetActive (false);
			lastActive = go;
			lastActive.SetActive (true);
		}
	}
}
