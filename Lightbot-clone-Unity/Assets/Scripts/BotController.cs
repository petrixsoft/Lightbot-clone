using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

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

		/*availableOps.Add ("FWD", new ForwardOperation ());
		availableOps.Add ("TL", new TurnLeftOperation ());
		availableOps.Add ("TR", new TurnRightOperation ());
		availableOps.Add ("JMP", new JumpOperation ());
		availableOps.Add ("LGHT", new LightOperation ());*/
	}

	public void AddOperation(BotOperation operation, bool main, bool composite, string name)
	{
		if (main)
		{
			mainOp = (CompositeOperation)operation;
		} else if (!composite)
		{
			availableOps.Add (name, operation);
		} else
		{
			compositeOps.Add (name, operation);
		}
	}

	public void AddToMainOP(BotOperation operation, int index)
	{
		mainOp.AddOperation (operation);
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

		/*if (exists)
		{
			if (operation.ValidateOperation (gameObject, levelDef))
			{
				operation.RunOperation (gameObject, levelDef);
			} else
			{
				operation.FakeRunOperation (gameObject, levelDef);
			}
		}*/

	}

	public void RunMain()
	{
		mainOp.RunOperation (gameObject, levelDef);
	}

	IEnumerator CompositeRun(CompositeOperation comp)
	{
		comp.RunOperation (gameObject, levelDef);

		return null;
	}
}
