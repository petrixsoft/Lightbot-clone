using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardOperation : BotOperation 
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

		// We can't reach there if we are in a different height level
		if (currentTile.height != nextTile.height)
		{
			return false;
		}

		return true;
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		levelDef.botPos += levelDef.getDirectionFromEnum ();
		botObject.transform.position += botObject.transform.forward;//botObject.transform.localToWorldMatrix.MultiplyVector(-botObject.transform.right);
	}
}
