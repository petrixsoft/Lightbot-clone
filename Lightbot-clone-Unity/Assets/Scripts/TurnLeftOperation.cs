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
		switch (levelDef.dir)
		{
			case Direction.X:
				levelDef.dir = Direction.Y;
				break;
			case Direction.NY:
				levelDef.dir = Direction.X;
				break;
			case Direction.NX:
				levelDef.dir = Direction.NY;
				break;
			case Direction.Y:
				levelDef.dir = Direction.NX;
				break;
		}

		botObject.transform.Rotate (Vector3.up * -90);
	}
}
