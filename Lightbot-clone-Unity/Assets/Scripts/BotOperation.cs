using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotOperation
{
	protected GameObject botObject;
	protected LevelDefinition levelDef;

	public BotOperation()
	{

	}

	public BotOperation(LevelDefinition levelDef, GameObject botObject)
	{
		this.botObject = botObject;
		this.levelDef = levelDef;
	}

	public virtual void RunOperation ()
	{

	}

	public virtual bool ValidateOperation()
	{
		return true;
	}

	/// <summary>
	/// When the operation can't be run, depending on the operation we should do something to do feedback.
	/// jump up in case of an invalid jump, etc.
	/// </summary>
	public virtual void FakeRunOperation()
	{

	}
}
