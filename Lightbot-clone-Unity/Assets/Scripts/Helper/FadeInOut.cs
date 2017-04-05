using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeInOut : MonoBehaviour {

	public UnityEvent OnFinishLerping;
	public CanvasGroup cGroup;
	private float target;
	private bool fading;
	private float time;

	// Use this for initialization
	void Start () {
		//OnFinishLerping = new UnityEvent ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fading)
		{
			time += Time.deltaTime;
			cGroup.alpha = Mathf.Lerp (cGroup.alpha, target, time);

			if(Mathf.Approximately(target, cGroup.alpha) && target != 0)
			{
				OnFinishLerping.Invoke ();
				FadeToNone ();
			}
		}
	}

	public void FadeToBlack()
	{
		target = 1f;
		fading = true;
		time = 0;
	}

	public void FadeToNone()
	{
		target = 0f;
		fading = true;
		time = 0;
	}
}
