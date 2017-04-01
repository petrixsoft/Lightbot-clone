using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOperation : BotOperation 
{
	public override bool ValidateOperation (GameObject botObject, LevelDefinition levelDef)
	{
		return true;
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		botObject.transform.localPosition += botObject.transform.forward + botObject.transform.up;
	}
}
