using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeftOperation : BotOperation
{
	public override bool ValidateOperation (GameObject botObject, LevelDefinition levelDef)
	{
		return true;
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		botObject.transform.Rotate (Vector3.up * -90);
	}
}
