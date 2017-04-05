using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelListItem : MonoBehaviour, IPointerClickHandler {

	public Text nameText;
	public LevelListManager lManager;
	public GameManager gManager;

	private string name;
	private string url;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetData(string name, string url)
	{
		this.name = name;
		this.url = url;
		nameText.text = name;
	}


	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		lManager.chosenLevelUrl = this.url;
		gManager.ChangeScene ("Level");
	}
	#endregion
}
