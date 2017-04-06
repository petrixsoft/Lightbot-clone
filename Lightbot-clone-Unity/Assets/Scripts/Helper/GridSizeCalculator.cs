using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSizeCalculator : MonoBehaviour {

	public GridLayoutGroup grid;
	private RectTransform cTransform;

	// Use this for initialization
	void Start () 
	{
		grid = GetComponent<GridLayoutGroup> ();
		cTransform = GetComponent<RectTransform> ();

		calculate ();
	}

	private void calculate()
	{
		int numCols = grid.constraintCount;
		Rect size = cTransform.rect;
		float padding = grid.padding.bottom + grid.padding.left + grid.padding.right + grid.padding.top;
		size.width -= padding / 2;
		size.width -= grid.spacing.x * (numCols -1);


		float width = size.width/ numCols;

		grid.cellSize = new Vector2 (width, width);
	}
}
