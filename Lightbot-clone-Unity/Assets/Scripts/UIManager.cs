using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{

	public GameObject contentGO;

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

			if (compUI != null)
			{
				compUI.Init (name, limit);
			}
		} else
		{
			P1Block.SetActive (true);
			CompositeUI compUI = P1Block.GetComponent<CompositeUI> ();

			if (compUI != null)
			{
				compUI.Init (name, limit);
			}
		}
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

}
