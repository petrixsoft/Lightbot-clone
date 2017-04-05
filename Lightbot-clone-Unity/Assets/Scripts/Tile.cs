using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
	public int height;
	public bool lightable;
	public bool lightOn;
	public Vector2 position;

	public GameObject associatedGO;

	// Use this for initialization
	public Tile()
	{
		position = Vector2.zero;
	}

	public void setInfo(int height, bool lightable, Vector2 position)
	{
		this.height = height;
		this.lightable = lightable;
		this.position = position;
	}
	public override string ToString ()
	{
		string toString = "";
		toString += "Height: " + height + ", lightable: " + lightable + ", Position: " + position + " | ";

		return toString;
	}
}
