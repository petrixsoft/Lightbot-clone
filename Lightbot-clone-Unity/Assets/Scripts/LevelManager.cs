using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour 
{
	public GameObject contentGO;
	public GameObject botGO;
	public GameObject boardGO;

	private LevelDefinition levelDefinition;

	void Start()
	{
		TextAsset levelJson = Resources.Load<TextAsset> ("1-1");
		levelDefinition = JsonUtility.FromJson<LevelDefinition> (levelJson.text);

		Debug.Log (levelDefinition.board.board);
	}
}
