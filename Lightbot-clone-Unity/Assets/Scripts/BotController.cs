using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

	public float operationDelay = .5f;
	public UIManager uiManager;

	private LevelDefinition levelDef;

	private Dictionary<string, BotOperation> availableOps;
	private Dictionary<string, BotOperation> compositeOps;

	private CompositeOperation mainOp;

	// Wether it's the main or another function, the operations will be added to the active composite operation
	private CompositeOperation currentComposite;
	private int indexOpSelected = -1;

	public LevelDefinition LevelDef
	{
		get
		{
			return levelDef;
		}
		set
		{
			levelDef = value;
		}
	}

	void Awake()
	{
		load ();
	}

	private void load()
	{
		availableOps = new Dictionary<string, BotOperation> ();

		mainOp = new CompositeOperation ();
		compositeOps = new Dictionary<string, BotOperation> ();
	}

	/// <summary>
	/// Selects an operation inside a composite so we can eliminate it later if we want
	/// </summary>
	/// <param name="index">Index.</param>
	public void SelectOperation(int index)
	{
		if (index != -1)
		{
			if (index == indexOpSelected)
			{
				uiManager.DeselectOperation (currentComposite.name, indexOpSelected);
				indexOpSelected = -1;
			} else
			{
				if (indexOpSelected != -1)
				{
					uiManager.DeselectOperation (currentComposite.name, indexOpSelected);
				}

				uiManager.SelectOperation (currentComposite.name, index);
				indexOpSelected = index;
			}
		} else
		{
			if (indexOpSelected != -1)
			{
				uiManager.DeselectOperation (currentComposite.name, indexOpSelected);
				indexOpSelected = -1;
			}
		}

		/*
		if (indexOpSelected != index)
		{
			if (index < currentComposite.opList.Count)
			{
				if (indexOpSelected != -1)
				{
					uiManager.DeselectOperation (currentComposite.name, indexOpSelected);
					indexOpSelected = -1;
				}
				if (index != -1)
				{
					indexOpSelected = index;
					uiManager.SelectOperation (currentComposite.name, index);
				}
			} else
			{
				if (indexOpSelected != -1)
				{
					uiManager.DeselectOperation (currentComposite.name, indexOpSelected);
					indexOpSelected = -1;
				}
			}
		} else
		{
			if (index < currentComposite.opList.Count && index != -1)
			{
				indexOpSelected = -1;
				uiManager.DeselectOperation (currentComposite.name, index);
			}
		}*/
	}

	/// <summary>
	/// Adds the operation to the available operations list
	/// </summary>
	/// <param name="operation">Operation.</param>
	/// <param name="main">If set to <c>true</c> main.</param>
	/// <param name="composite">If set to <c>true</c> composite.</param>
	/// <param name="name">Name.</param>
	public void AddOperation(BotOperation operation, bool main, bool composite, string name)
	{
		if (main)
		{
			CompositeOperation compOp = operation as CompositeOperation;
			mainOp = compOp;
			currentComposite = compOp;
			compositeOps.Add (name, mainOp);

		} else if (!composite)
		{
			availableOps.Add (name, operation);
		} else
		{
			compositeOps.Add (name, operation);
		}
	}

	/// <summary>
	/// Adds the selected operation to the main function
	/// </summary>
	/// <param name="operation">Operation.</param>
	/// <param name="index">Index.</param>
	public bool AddToCurrentComp(BotOperation operation, int index)
	{
		return currentComposite.AddOperation (operation);
	}

	public void RemoveFromCurrentComp()
	{
		if (indexOpSelected != -1)
		{
			currentComposite.removeOperation (indexOpSelected);
			uiManager.RemoveOperationFromBlock (currentComposite.name, indexOpSelected);
			indexOpSelected = -1;
		}
	}

	/// <summary>
	/// Adds the operation to the active composite operation
	/// </summary>
	/// <param name="op">Op.</param>
	public void RunOperation(string op)
	{
		BotOperation operation = null;
		bool exists = availableOps.TryGetValue (op, out operation);

		if (exists)
		{
			if (AddToCurrentComp (operation, 0))
			{
				uiManager.AddOperationToBlock (currentComposite.name, op);
			}
		} else
		{
			// Maybe its a composite
			BotOperation compOp = null;
			bool existsComp = compositeOps.TryGetValue (op, out compOp);

			if (existsComp)
			{
				CompositeOperation comp = compOp as CompositeOperation;
				if (AddToCurrentComp (comp, 0))
				{
					uiManager.AddOperationToBlock (currentComposite.name, op);
				}
			}
		}
	}

	/// <summary>
	/// Change the composite operation to which the operations will be added
	/// </summary>
	/// <param name="name">Name.</param>
	public void ChangeCurrentComposite(string name)
	{
		BotOperation operation = null;
		bool exists = compositeOps.TryGetValue (name, out operation);
		CompositeOperation compOp = operation as CompositeOperation;

		if (compOp != null)
		{
			uiManager.DeselectBlock (currentComposite.name);
			if (indexOpSelected != -1)
			{
				uiManager.DeselectOperation (currentComposite.name, indexOpSelected);
			}

			currentComposite = compOp;
			indexOpSelected = -1;
			uiManager.SelectBlock (currentComposite.name);
		}
	}

	public void RunMain()
	{
		uiManager.ChangeUIMode ("Run");
		StartCoroutine (CompositeRun ("Main"));
	}

	public void ResetBot()
	{
		uiManager.ChangeUIMode ("Build");
		StopAllCoroutines ();
	}

	IEnumerator CompositeRun(string name)
	{
		BotOperation operation;
		compositeOps.TryGetValue (name, out operation);
		CompositeOperation compOp = operation as CompositeOperation;
		List<BotOperation> botOpList = compOp.opList;

		if (botOpList != null)
		{
			for (int i = 0; i < botOpList.Count; i++)
			{
				BotOperation op = botOpList [i];
				CompositeOperation comp = op as CompositeOperation;

				if (comp != null)
				{
					// If this operation is a composite one we need to execute it first accordingly
					yield return StartCoroutine (CompositeRun(comp.name));
				}

				if (op != null && op.ValidateOperation(gameObject, levelDef))
				{
					op.RunOperation (gameObject, levelDef);

					if (CheckGameOver ())
					{
						// If all the lights are on then there's no need to continue running the processes, it's game over
						StopAllCoroutines ();
					}
				}

				yield return new WaitForSeconds (operationDelay);
			}
		}

		yield return null;
	}

	private bool CheckGameOver()
	{
		for (int i = 0; i < levelDef.numRows; i++)
		{
			for (int j = 0; j < levelDef.numColumns; j++)
			{
				Tile t = levelDef.board [i, j];

				if (t.lightable && !t.lightOn)
				{
					Debug.Log ("Try Again");
					return false;
				}
			}
		}

		Debug.Log ("You Win");

		return true;
	}
}
