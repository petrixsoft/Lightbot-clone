using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpRotate : MonoBehaviour {

	public AnimationCurve curve;
	public float duration;
	public Transform target;

	public Button me;
	public Button twin;

	private float currTime;
	private bool playing;
	private float increment;
	private float initialValue;
	private float lastValue;

	private Transform cTransform;

	// Use this for initialization
	void Start () {
		cTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playing)
		{
			currTime += Time.deltaTime;

			float currentStep = curve.Evaluate (currTime) * increment;
			float delta = currentStep - lastValue;

			Quaternion newRotation = new Quaternion ();
			newRotation.eulerAngles = new Vector3 (0, delta, 0);
			//target.localRotation = newRotation;
			target.Rotate (new Vector3(0,initialValue + delta, 0), Space.World);
			lastValue = target.localRotation.eulerAngles.y;

			if (Mathf.Approximately(1f, curve.Evaluate (currTime)))
			{
				playing = false;
				twin.interactable = true;
				me.interactable = true;
			}

		}
	}

	public void Start(float increment)
	{
		if (!playing)
		{
			currTime = 0;
			this.increment = increment;
			initialValue = target.rotation.eulerAngles.y;
			playing = true;
			twin.interactable = false;
			me.interactable = false;
		}
	}
}
