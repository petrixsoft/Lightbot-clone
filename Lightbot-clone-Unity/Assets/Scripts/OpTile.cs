using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpTile : MonoBehaviour {

	public int index = -1;

	private Transform cTransform;
	private GameObject lastActive;
	private BotController bot;

	public GameObject LastActive
	{
		get
		{
			return lastActive;
		}
	}

	void Awake()
	{
		cTransform = transform;
		Transform tr = cTransform.FindChild ("EMPTY");
		if (tr != null)
		{
			lastActive = tr.gameObject;
			lastActive.SetActive (true);
		}
		bot = FindObjectOfType<BotController> ();
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

	public void Select()
	{
		Image tileIm = lastActive.GetComponent<Image> ();
		tileIm.color = Color.green;
	}

	public void DeSelect()
	{
		Image tileIm = lastActive.GetComponent<Image> ();
		tileIm.color = Color.white;
	}

	public void SelectOperation ()
	{
		CompositeUI cUI = GetComponentInParent<CompositeUI> ();
		if (cUI.IsSelected)
		{
			bot.SelectOperation (index);
		}
	}
}
