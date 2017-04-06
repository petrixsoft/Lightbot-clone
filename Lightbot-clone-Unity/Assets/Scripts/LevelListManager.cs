using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class LevelListManager : MonoBehaviour {

	public static bool loaded;
	public string chosenLevelUrl;

	[Header ("For Building")]
	public GameObject listItem;

	private GameManager gManager;
	private List<LevelListItem> levelList;
	private GameObject grid;

	// Use this for initialization
	void Start () {
		if (!LevelListManager.loaded)
		{
			SceneManager.sceneLoaded += OnSceneLoad;
			DontDestroyOnLoad (this);
			LevelListManager.loaded = true;
			FirstInit ();
		} else
		{
			DestroyImmediate (gameObject);
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "LevelList")
		{
			FirstInit ();
		}
	}

	private void FirstInit()
	{
		grid = GameObject.Find ("LevelGrid");

		GameObject gManagerGO = GameObject.Find ("GameManager");
		if (gManagerGO != null)
		{
			gManager = gManagerGO.GetComponent<GameManager> ();
		}

		LoadLevelList ();
	}

	private void LoadLevelList()
	{
		TextAsset listAsset = Resources.Load<TextAsset> ("levelList");
		if (listAsset != null)
		{
			var content = JSON.Parse (listAsset.text);

			if (content != null)
			{
				JSONArray list = content ["levelList"].AsArray;

				for (int i = 0; i < list.Count; i++)
				{
					GameObject itemGO = GameObject.Instantiate (listItem);
					LevelListItem item = itemGO.GetComponent<LevelListItem> ();

					if (item != null)
					{
						item.SetData(list[i]["name"], list[i]["uri"]);
						item.gManager = gManager;
						item.lManager = this;

						itemGO.transform.SetParent (grid.transform);
					}
				}

			}
		}
	}
}
