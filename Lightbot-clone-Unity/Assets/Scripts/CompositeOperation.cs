using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeOperation : BotOperation 
{
	public List<BotOperation> opList;
	public int limit;
	public string name;

	public CompositeOperation()
	{
		opList = new List<BotOperation> ();
	}

	public bool AddOperation(BotOperation op)
	{
		if (opList.Count < limit)
		{
			opList.Add (op);
			return true;
		}

		return false;
	}

	public void removeOperation(int index)
	{
		opList.RemoveAt (index);
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
	}
}
