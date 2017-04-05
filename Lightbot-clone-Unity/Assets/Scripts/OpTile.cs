using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpTile : MonoBehaviour, IPointerClickHandler {

	public int index;

	private Transform cTransform;
	private GameObject lastActive;
	private BotController bot;


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

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		bot.RemoveFromCurrentComp (index);
	}

	#endregion
}
