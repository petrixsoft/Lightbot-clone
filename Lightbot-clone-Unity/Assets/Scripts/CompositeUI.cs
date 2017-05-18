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
	public Sprite selected;
	public Sprite deSelected;

	private List<GameObject> opList;
	// The next operation will be added at that index
	private int nextOpIndex;
	private bool isSelected;

	public bool IsSelected
	{
		get
		{
			return isSelected;
		}
	}

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
			tile.transform.localScale = Vector3.one;

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
			t.index = nextOpIndex;

			if (t != null)
			{
				t.ShowOp (name);
				nextOpIndex++;
			}
		}
	}

	public void Select()
	{
		Image im = GetComponent<Image> ();
		im.sprite = selected;
		isSelected = true;
	}

	public void Deselect()
	{
		Image im = GetComponent<Image> ();
		im.sprite = deSelected;
		isSelected = false;
	}

	public void SelectOperation(int index)
	{
		GameObject go = opList [index];
		OpTile tile = go.GetComponent<OpTile> ();

		tile.Select ();
	}

	public void  DeSelectOperation(int index)
	{
		GameObject go = opList [index];
		OpTile tile = go.GetComponent<OpTile> ();

		tile.DeSelect ();
	}

	/// <summary>
	/// Removes the operation from the grid
	/// </summary>
	/// <param name="index">Index.</param>
	public void RemoveOperation(int index)
	{
		GameObject go = opList [index];
		OpTile tile = go.GetComponent<OpTile> ();
		tile.DeSelect ();
		tile.ShowOp ("EMPTY");
		tile.index = -1;

		ReSortTiles ();
		nextOpIndex--;
	}

	/// <summary>
	/// After deleting an operation we update the compositeUI
	/// </summary>
	private void ReSortTiles()
	{
		for (int i = 0; i < opList.Count; i++)
		{
			GameObject go = opList [i];
			OpTile tile = go.GetComponent<OpTile> ();

			if (tile != null)
			{
				if (tile.LastActive.name == "EMPTY" && i <= opList.Count - 2)
				{
					GameObject nextGo = opList [i + 1];
					OpTile nextTile = nextGo.GetComponent<OpTile> ();

					if (nextTile.LastActive.name != "EMPTY")
					{
						tile.ShowOp (nextTile.LastActive.name);
						nextTile.DeSelect ();
						nextTile.ShowOp ("EMPTY");
						nextTile.index = -1;
						tile.index = i;
					}
				}
			}
		}
	}
}
