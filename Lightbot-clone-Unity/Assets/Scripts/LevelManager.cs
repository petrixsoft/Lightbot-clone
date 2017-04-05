using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class LevelManager : MonoBehaviour 
{
	public GameObject contentGO;
	public GameObject actualBotGO;
	public GameObject boardGO;
	public UIManager uiManager;

	[Header ("To be used to build the world")]
	public GameObject tileGO;

	private LevelDefinition levelDefinition;
	private GameObject[,] levelBuild;

	void Start()
	{
		LevelLoad ();
		BuildLevel ();
	}

	/// <summary>
	/// Levels are defined by a JSON file, this method parses that file and loads the level information into memory
	/// </summary>
	private void LevelLoad()
	{
		BotController  bController = actualBotGO.GetComponent<BotController> ();
		TextAsset levelJson = Resources.Load<TextAsset> ("1-1");
		levelDefinition = new LevelDefinition ();

		var levelD = JSON.Parse (levelJson.text);
		if (levelD != null)
		{
			levelDefinition.name = levelD ["name"];
			levelDefinition.maxScore = levelD ["maxScore"];
			levelDefinition.numRows = levelD ["numRows"];
			levelDefinition.numColumns = levelD ["numColumns"];
			levelDefinition.botPos = new Vector2 (levelD ["botPos"] ["x"].AsInt, levelD ["botPos"] ["y"].AsInt);

			levelDefinition.board = new Tile[levelDefinition.numRows, levelDefinition.numColumns];

			for (int i = 0; i < levelDefinition.numRows ; i++)
			{
				for (int j = 0; j < levelDefinition.numColumns; j++)
				{
					Tile t = new Tile ();

					t.setInfo (levelD ["board"] [i] [j] ["height"].AsInt, 
						levelD ["board"] [i] [j] ["lightable"].AsBool, 
						new Vector2 (levelD ["board"] [i] [j] ["position"] ["x"].AsInt, levelD ["board"] [i] [j] ["position"] ["y"].AsInt));

					levelDefinition.board [i, j] = t;
				}
			}
		}

		int opAmount = levelD ["availableOps"].AsArray.Count;

		for (int i = 0; i < opAmount; i++)
		{
			var op = levelD ["availableOps"] [i];
			string name = op ["name"];

			switch (name)
			{
				case "FWD":
					bController.AddOperation (new ForwardOperation (), false, false, "FWD");
					uiManager.EnableOp (name);
					break;
				case "TL":
					bController.AddOperation (new TurnLeftOperation (), false, false, "TL");
					uiManager.EnableOp (name);
					break;
				case "TR":
					bController.AddOperation (new TurnRightOperation (), false, false, "TR");
					uiManager.EnableOp (name);
					break;
				case "JMP":
					bController.AddOperation (new JumpOperation (), false, false, "JMP");
					uiManager.EnableOp (name);
					break;
				case "LGHT":
					bController.AddOperation (new LightOperation (), false, false, "LGHT");
					uiManager.EnableOp (name);
					break;
				case "Main":
					CompositeOperation compOp = new CompositeOperation ();
					compOp.limit = op ["limit"].AsInt;
					compOp.name = "Main";
					bController.AddOperation (compOp, true, true, "Main");
					uiManager.EnableBlock (name, op["limit"].AsInt);
					break;
				default:
					//  If the operation is not one of the above it means that it's a function (or an unknown operation which isn't good)
					if (op ["type"].Value == "Composite")
					{
						CompositeOperation comp = new CompositeOperation ();
						comp.limit = op ["limit"].AsInt;
						comp.name = op ["name"];
						bController.AddOperation (comp, false, true, op ["name"]);
						uiManager.EnableBlock (name, op["limit"].AsInt);
						uiManager.EnableOp (name);
					}
					break;
			}
		}

		bController.LevelDef = levelDefinition.Clone();

		Resources.UnloadAsset (levelJson);

		//Debug.Log (levelDefinition.ToString());
	}

	/// <summary>
	/// Once the level information has been loaded from resources we build the world with that info
	/// </summary>
	private void BuildLevel()
	{
		levelBuild = new GameObject[levelDefinition.numRows, levelDefinition.numColumns];

		// Instancing all the gameObjects we are going to need
		actualBotGO.transform.localPosition = new Vector3(levelDefinition.board[(int)levelDefinition.botPos.x, (int)levelDefinition.botPos.y].position.x, 
			levelDefinition.board[(int)levelDefinition.botPos.x, (int)levelDefinition.botPos.y].height, 
			levelDefinition.board[(int)levelDefinition.botPos.x, (int)levelDefinition.botPos.y].position.y);

		for (int i = 0; i < levelDefinition.numRows; i++)
		{
			for (int j = 0; j < levelDefinition.numColumns; j++)
			{
				GameObject tile = Instantiate (tileGO, boardGO.transform);
				Tile t = levelDefinition.board [i, j];
				t.associatedGO = tile;

				tile.transform.position = new Vector3 (t.position.x, t.height, t.position.y);

				if (t.lightable)
				{
					Material light = Resources.Load<Material> ("Materials/LightOffMat");
					Renderer r = tile.GetComponent<Renderer> ();
					r.sharedMaterial = light;
				}

				levelBuild [i, j] = tile;
			}
		}
	}

	public void ResetLevel()
	{
		BotController  bController = actualBotGO.GetComponent<BotController> ();
		bController.LevelDef = levelDefinition.Clone ();

		actualBotGO.transform.localPosition = new Vector3(levelDefinition.board[(int)levelDefinition.botPos.x, (int)levelDefinition.botPos.y].position.x, 
			levelDefinition.board[(int)levelDefinition.botPos.x, (int)levelDefinition.botPos.y].height, 
			levelDefinition.board[(int)levelDefinition.botPos.x, (int)levelDefinition.botPos.y].position.y);

		Quaternion newRotation = new Quaternion ();
		newRotation.eulerAngles = new Vector3 (0, 90, 0);

		actualBotGO.transform.localRotation = newRotation;

		for (int i = 0; i < levelDefinition.numRows; i++)
		{
			for (int j = 0; j < levelDefinition.numColumns; j++)
			{
				GameObject tile = levelBuild [i, j];
				Tile t = levelDefinition.board [i, j];

				if (t.lightable)
				{
					Material light = Resources.Load<Material> ("Materials/LightOffMat");
					Renderer r = tile.GetComponent<Renderer> ();
					r.sharedMaterial = light;
					t.lightOn = false;
				}
			}
		}

		bController.ResetBot ();
	}
}
