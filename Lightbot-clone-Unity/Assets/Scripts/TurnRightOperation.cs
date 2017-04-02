using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRightOperation : BotOperation 
{
	public override bool ValidateOperation (GameObject botObject, LevelDefinition levelDef)
	{
		return true;
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		switch (levelDef.dir)
		{
			case Direction.X:
				levelDef.dir = Direction.NY;
				break;
			case Direction.NY:
				levelDef.dir = Direction.NX;
				break;
			case Direction.NX:
				levelDef.dir = Direction.Y;
				break;
			case Direction.Y:
				levelDef.dir = Direction.X;
				break;
		}

		botObject.transform.Rotate (Vector3.up * 90);
	}
}
