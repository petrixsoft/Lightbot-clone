using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOperation : BotOperation 
{
	public override bool ValidateOperation (GameObject botObject, LevelDefinition levelDef)
	{
		Vector2 direction = levelDef.getDirectionFromEnum ();
		Vector2 nextPos = direction + levelDef.botPos;

		// If the next position is in any way outside the board then it's illegal
		if (nextPos.x < 0 || nextPos.x >= levelDef.numRows
			|| nextPos.y < 0 || nextPos.y >= levelDef.numColumns)
		{
			return false;
		}

		Tile currentTile = levelDef.board [(int)levelDef.botPos.x, (int)levelDef.botPos.y];
		Tile nextTile = levelDef.board [(int)nextPos.x, (int)nextPos.y];

		// We won't jump if the destination is at the same height level
		if (currentTile.height == nextTile.height || 
			(currentTile.height < nextTile.height && nextTile.height - currentTile.height > 1) ||
			(currentTile.height > nextTile.height && currentTile.height - nextTile.height > 1))
		{
			return false;
		}

		return true;
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		botObject.transform.position = getNextPosition (botObject, levelDef);

		levelDef.botPos += levelDef.getDirectionFromEnum ();
	}

	private Vector3 getNextPosition(GameObject botObject, LevelDefinition levelDef)
	{
		Vector2 nextPos = levelDef.getDirectionFromEnum () + levelDef.botPos;
		Tile currentTile = levelDef.board [(int)levelDef.botPos.x, (int)levelDef.botPos.y];
		Tile nextTile = levelDef.board [(int)nextPos.x, (int)nextPos.y];

		Vector3 currentPosition = botObject.transform.position;
		Vector3 nextPosition = Vector3.zero;

		if (currentTile.height < nextTile.height)
		{
			currentPosition += botObject.transform.forward + botObject.transform.up;
		} else if (currentTile.height > nextTile.height)
		{
			currentPosition += botObject.transform.forward - botObject.transform.up;
		}

		return currentPosition;
	}
}
