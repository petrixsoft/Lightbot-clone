using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

	private LevelDefinition levelDef;

	private Dictionary<string, BotOperation> availableOps;

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

		availableOps.Add ("FWD", new ForwardOperation ());
		availableOps.Add ("TL", new TurnLeftOperation ());
		availableOps.Add ("TR", new TurnRightOperation ());
		availableOps.Add ("JMP", new JumpOperation ());
	}

	public void RunOperation(string op)
	{
		BotOperation operation = null;
		bool exists = availableOps.TryGetValue (op, out operation);

		if (exists)
		{
			if (operation.ValidateOperation (gameObject, levelDef))
			{
				operation.RunOperation (gameObject, levelDef);
			} else
			{
				operation.FakeRunOperation (gameObject, levelDef);
			}
		}

	}
}
