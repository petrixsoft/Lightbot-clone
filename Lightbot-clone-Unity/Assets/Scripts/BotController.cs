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

	public void RemoveFromCurrentComp(int index)
	{
		currentComposite.removeOperation (index);
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
			currentComposite = compOp;
		}
	}

	// TODO Maybe deleting the composite operations and replace them by lists on this monobehavior would make things work (and we need that)
	public void RunMain()
	{
		StartCoroutine (CompositeRun ("Main"));
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
				}

				yield return new WaitForSeconds (operationDelay);
			}
		}

		yield return null;
	}
}
