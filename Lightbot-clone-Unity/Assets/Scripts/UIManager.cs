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
