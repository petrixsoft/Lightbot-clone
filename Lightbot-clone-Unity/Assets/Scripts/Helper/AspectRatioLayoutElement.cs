using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioLayoutElement :LayoutElement {

	public void CalculateLayoutInputVertical ()
	{
		RectTransform tr = transform as RectTransform;
		tr.sizeDelta = new Vector2 (tr.sizeDelta.x, tr.sizeDelta.x);

		Debug.Log (tr.sizeDelta);
		this.flexibleHeight = tr.sizeDelta.y;
	}

	public override float preferredHeight
	{ 
		get{
			RectTransform tr = transform as RectTransform;
			Debug.Log (tr.sizeDelta);
			return tr.sizeDelta.x;
		}
	}
}
