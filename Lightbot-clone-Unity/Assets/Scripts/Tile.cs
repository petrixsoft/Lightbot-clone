using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
	private int height;
	private bool lightable;
	private int[] position;

	// Use this for initialization
	public Tile()
	{
		position = new int[2];
	}

	public void setInfo(int height, bool lightable, int[] position)
	{
		this.height = height;
		this.lightable = lightable;
		this.position = position;
	}
}
