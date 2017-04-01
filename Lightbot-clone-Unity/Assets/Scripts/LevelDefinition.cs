using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDefinition
{
	public int numRows;
	public int numColumns;
	public int maxScore;
	public string name;
	public Vector2 botPos;

	public Tile[,] board;

	public override string ToString ()
	{
		string toString = "";

		toString += "NumRows: " + numRows + "\n";
		toString += "NumColumns: " + numColumns + "\n";
		toString += "MaxScore: " + maxScore + "\n";
		toString += "Name: " + name + "\n";
		toString += "BotPos: " + botPos + "\n";

		toString += "Board: \n";

		for (int i = 0; i < numRows; i++)
		{
			for (int j = 0; j < numColumns; j++)
			{
				toString += board [i, j].ToString ();
			}

			toString += "\n";
		}

		return toString;
	}
}
