using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

	public float operationDelay = .5f;

	private LevelDefinition levelDef;

	private Dictionary<string, BotOperation> availableOps;
	private Dictionary<string, List<BotOperation>> compositeOps;

	private List<BotOperation> mainOp;

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

		mainOp = new List<BotOperation> ();
		compositeOps = new Dictionary<string, List<BotOperation>> ();
	}

	public void AddOperation(BotOperation operation, bool main, bool composite, string name)
	{
		if (main)
		{
			mainOp = new List<BotOperation> ();
			compositeOps.Add (name, mainOp);

		} else if (!composite)
		{
			availableOps.Add (name, operation);
		} else
		{
			List<BotOperation> opList;
			compositeOps.TryGetValue (name, out opList);
			opList.Add (operation);
			//compositeOps.Add (name, operation);
		}
	}

	public void AddToMainOP(BotOperation operation, int index)
	{
		mainOp.Add (operation);
	}

	public void RemoveFromMainOp(int index)
	{

	}

	public void RunOperation(string op)
	{
		BotOperation operation = null;
		bool exists = availableOps.TryGetValue (op, out operation);

		if (exists)
		{
			AddToMainOP (operation, 0);
		}
	}

	// TODO Maybe deleting the composite operations and replace them by lists on this monobehavior would make things work (and we need that)
	public void RunMain()
	{
		StartCoroutine (CompositeRun ("Main"));
	}

	IEnumerator CompositeRun(string name)
	{
		List<BotOperation> botOpList;
		compositeOps.TryGetValue (name, out botOpList);

		if (botOpList != null)
		{
			for (int i = 0; i < botOpList.Count; i++)
			{
				BotOperation op = botOpList [i];

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
