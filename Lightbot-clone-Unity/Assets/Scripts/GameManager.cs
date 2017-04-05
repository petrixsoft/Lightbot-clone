using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public enum Scenes
	{
		TITLE,
		LEVELLIST,
		LEVEL
	}

	public List<string> sceneList;
	public GameObject canvasFade;


	private int currentSceneIndex;
	private FadeInOut fade;
	public static bool loaded;

	// Use this for initialization
	void Start () {
		currentSceneIndex = 0;
		if (!GameManager.loaded)
		{
			DontDestroyOnLoad (gameObject);
			DontDestroyOnLoad (canvasFade);

			SceneManager.sceneLoaded += SceneLoaded;
			fade = canvasFade.GetComponentInChildren<FadeInOut>();
			//fade.OnFinishLerping.AddListener (ChangeToNextScene);
			GameManager.loaded = true;
		} else
		{
			DestroyImmediate (canvasFade);
			DestroyImmediate (gameObject);
		}
	}

	public void ChangeScene(string nextLevel)
	{
		currentSceneIndex = sceneList.IndexOf(nextLevel);
		fade.FadeToBlack ();
	}

	public void ReturnPreviousScene()
	{
		int newIndex = currentSceneIndex - 1;
		if (newIndex >= 0)
		{
			currentSceneIndex--;
			ChangeScene (sceneList [newIndex]);
		}
	}

	public void ChangeToNextScene()
	{
		SceneManager.LoadScene (sceneList[currentSceneIndex]);
	}

	private void SceneLoaded(Scene scene, LoadSceneMode mode)
	{
		fade.FadeToNone ();
	}
}
