using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LightOperation : BotOperation {

	public Material lightOffMat;
	public Material lightOnMat;

	public LightOperation()
	{
		lightOffMat = Resources.Load<Material> ("Materials/LightOffMat");
		lightOnMat = Resources.Load<Material> ("Materials/LightOnMat");
	}

	public override bool ValidateOperation (GameObject botObject, LevelDefinition levelDef)
	{
		Vector2 currentPos = levelDef.botPos;
		Tile currentTile = levelDef.board [(int)currentPos.x, (int)currentPos.y];

		if (currentTile.lightable)
		{
			return true;
		} else
		{
			return false;
		}
	}

	public override void RunOperation (GameObject botObject, LevelDefinition levelDef)
	{
		Vector2 currentPos = levelDef.botPos;
		Tile currentTile = levelDef.board [(int)currentPos.x, (int)currentPos.y];
		currentTile.lightOn = true;
		Renderer r = currentTile.associatedGO.GetComponent<Renderer> ();
		r.sharedMaterial = lightOnMat;
	}
}
