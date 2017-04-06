using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{

	public GameObject contentGO;
	public NetworkCapabilities netC;


	[Header ("Upload score UI")]
	public Text inputNameText;
	public GameObject sendScoreGO;
	public Text scoreText;
	public Text feedbackMessage;

	[Header ("High Score UI")]
	public GameObject highScoreGO;
	public List<Text> scoreItems;

	[Header ("Building OpButtons")]
	public GameObject FWDButton;
	public GameObject JMPButton;
	public GameObject TRButton;
	public GameObject TLButton;
	public GameObject LGHTButton;
	public GameObject P1Button;

	[Header ("Building Function blocks")]
	public GameObject MainBlock;
	public GameObject P1Block;

	[Header ("Run buttons")]
	public GameObject runButton;
	public GameObject stopButton;

	public void ChangeUIMode(string mode)
	{
		if (mode == "Run")
		{
			runButton.SetActive (false);
			stopButton.SetActive (true);
		} else if (mode == "Build")
		{
			stopButton.SetActive (false);
			runButton.SetActive (true);
		}
	}

	public void AddOperationToBlock(string blockName, string opName)
	{
		
		if (blockName == "Main")
		{
			CompositeUI cUI = MainBlock.GetComponent<CompositeUI> ();
			if (cUI != null)
			{
				cUI.AddOperation (opName);
			}
		} else
		{
			CompositeUI cUI = P1Block.GetComponent<CompositeUI> ();
			if (cUI != null)
			{
				cUI.AddOperation (opName);
			}
		}
	}

	public void RemoveOperationFromBlock(string blockName, int index)
	{
		if (blockName == "Main")
		{
			CompositeUI cUI = MainBlock.GetComponent<CompositeUI> ();
			if (cUI != null)
			{
				cUI.RemoveOperation (index);
			}
		} else
		{
			CompositeUI cUI = P1Block.GetComponent<CompositeUI> ();
			if (cUI != null)
			{
				cUI.RemoveOperation (index);
			}
		}
	}

	public void EnableOp(string name)
	{
		getOpButtonGO (name).SetActive (true);
	}

	public void EnableBlock(string name, int limit)
	{
		if (name == "Main")
		{
			MainBlock.SetActive (true);
			CompositeUI compUI = MainBlock.GetComponent<CompositeUI> ();
			compUI.Select ();
			if (compUI != null)
			{
				compUI.Init (name, limit);
			}
		} else
		{
			P1Block.SetActive (true);
			CompositeUI compUI = P1Block.GetComponent<CompositeUI> ();
			compUI.Deselect ();

			if (compUI != null)
			{
				compUI.Init (name, limit);
			}
		}
	}

	public void SelectBlock(string name)
	{
		GameObject block = getBlock (name);

		if (block != null)
		{
			CompositeUI blockUI = block.GetComponent<CompositeUI> ();
			blockUI.Select ();
		}
	}

	public void DeselectBlock(string name)
	{
		GameObject block = getBlock (name);

		if (block != null)
		{
			CompositeUI blockUI = block.GetComponent<CompositeUI> ();
			blockUI.Deselect ();
		}
	}

	public void SelectOperation(string compName, int index)
	{
		GameObject comp = getBlock (compName);
		if (comp != null)
		{
			CompositeUI compUI = comp.GetComponent<CompositeUI> ();

			compUI.SelectOperation (index);
		}
	}

	public void DeselectOperation(string compName, int index)
	{
		GameObject comp = getBlock (compName);
		if (comp != null)
		{
			CompositeUI compUI = comp.GetComponent<CompositeUI> ();

			compUI.DeSelectOperation (index);
		}
	}

	/// <summary>
	/// Shows the highscore panel and tries to retrieve the score list
	/// </summary>
	public void OpenHighScore()
	{
		string levelName = "";
		GameObject botControllerGO = GameObject.Find ("Bot");
		BotController bController = null;
		if (botControllerGO != null)
		{
			bController = botControllerGO.GetComponent<BotController> ();
			highScoreGO.SetActive (true);
			netC.getScoreList (bController.LevelDef.name);
		}
	}

	/// <summary>
	/// The net manager will call this method which populates the score list with the given information
	/// </summary>
	/// <param name="list">List.</param>
	public void SetScoreList(List<string> list)
	{
		for (int i = 0; i < scoreItems.Count; i++)
		{
			Text scoreText = scoreItems [i];
			if (i < list.Count)
			{
				scoreText.text = (i+1) + ". " + list[i];
			} else
			{
				scoreText.text = (i+1) + ". ";
			}
		}
	}

	public void CloseHighScore()
	{
		highScoreGO.SetActive (false);
	}

	/// <summary>
	/// Opens the send score panel.
	/// </summary>
	/// <param name="score">Score.</param>
	public void OpenSendScore(int score)
	{
		sendScoreGO.SetActive (true);
		scoreText.text = score.ToString ();
	}

	public void SendScore()
	{
		if (!string.IsNullOrEmpty(inputNameText.text))
		{
			netC.SendScore (inputNameText.text);
		} else
		{
			SendScoreMessage ("Name must not be empty");
		}
	}

	/// <summary>
	/// Shows a feedback message regarding the success of the net request
	/// </summary>
	/// <param name="message">Message.</param>
	public void SendScoreMessage(string message)
	{
		feedbackMessage.text = message;
	}

	private GameObject getOpButtonGO(string name)
	{
		switch (name)
		{
			case "FWD":
				return FWDButton;

			case "JMP":
				return JMPButton;

			case "TR":
				return TRButton;

			case "TL":
				return TLButton;

			case "LGHT":
				return LGHTButton;
			case "P1":
				return P1Button;
		}

		return null;
	}

	private GameObject getBlock(string name)
	{
		if (name == "Main")
		{
			return MainBlock;
		} else if (name == "P1")
		{
			return P1Block;
		}

		return null;
	}

}
