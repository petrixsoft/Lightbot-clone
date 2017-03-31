using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
	public Tile[][] board;
}

[System.Serializable]
public class LevelDefinition
{
	public int numRows;
	public int numColumns;
	public int maxScore;
	public string name;
	public Vector2 botPos;

	public Board board;
}
