using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeOperation : BotOperation 
{
	private List<BotOperation> opList;

	public CompositeOperation()
	{
		opList = new List<BotOperation> ();
	}

	public void AddOperation(BotOperation op)
	{
		opList.Add (op);
	}

	public void AddOperation(BotOperation op, int pos)
	{
		opList.Insert (pos, op);
	}

	public override bool ValidateOperation (GameObject botObject, LevelDefinition levelDef)
	{
		return true;
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		for (int i = 0; i < opList.Count; i++)
		{
			BotOperation botOp = opList [i];

			if (botOp != null && botOp.ValidateOperation(botObject, levelDef))
			{
				botOp.RunOperation (botObject, levelDef);
			}
		}
	}
}
