using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneHelper : MonoBehaviour {

	private GameManager gm;

	void Start()
	{
		GameObject gmGO = GameObject.Find ("GameManager");

		if (gmGO != null)
		{
			gm = gmGO.GetComponent<GameManager> ();
		}
	}

	public void ChangeScene(string name)
	{
		gm.ChangeScene (name);
	}

	public void ReturnLastScene()
	{
		gm.ReturnPreviousScene ();
	}
}
