using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompositeUI : MonoBehaviour {

	public Text title;
	public int opLimit;

	public GameObject grid;

	[Header ("Build")]
	public GameObject tileGO;

	private List<GameObject> opList;
	// The next operation will be added at that index
	private int nextOpIndex;

	void OnEnable()
	{
		opList = new List<GameObject> ();
	}

	/// <summary>
	/// Initialises the tile list and the name of the block
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="limit">Limit.</param>
	public void Init(string name, int limit)
	{
		title.text = name;
		opLimit = limit;

		for (int i = 0; i < opLimit; i++)
		{
			GameObject tile = Instantiate (tileGO);

			tile.transform.SetParent (grid.transform);

			opList.Add (tile);
		}
	}

	/// <summary>
	/// Adds the icon of the operation to the grid
	/// </summary>
	/// <param name="name">Name.</param>
	public void AddOperation(string name)
	{
		if (nextOpIndex < opLimit)
		{
			GameObject nextOperation = opList [nextOpIndex];
			OpTile t = nextOperation.GetComponent<OpTile> ();

			if (t != null)
			{
				t.ShowOp (name);
				nextOpIndex++;
			}
		}
	}

	/// <summary>
	/// Removes the operation from the grid
	/// </summary>
	/// <param name="index">Index.</param>
	public void RemoveOperation(int index)
	{
		GameObject go = opList [index];
		go.transform.SetParent (null);
		go.transform.SetParent (grid.transform);
		nextOpIndex--;
	}
}
