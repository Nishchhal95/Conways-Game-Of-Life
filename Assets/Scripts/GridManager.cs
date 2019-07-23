using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Cell[,] grid;

    [SerializeField] private int row = 10;
    [SerializeField] private int column = 10;

    [SerializeField] private GameObject cellPrefab;

	// Use this for initialization
	void Start ()
    {
        CreateGrid();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Creates the Grid of Cells based on Number of rows and columns
    private void CreateGrid()
    {
        //Init the grid Array
        grid = new Cell[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                CreateGridBlock(i,j);
            }
        }
    }

    private void CreateGridBlock(int x, int y)
    {
        //Setting some offsets
        float xOffset = (row/2) - (row % 2 == 0 ? .5f : 1f);
        float yOffset = (column / 2) - (column % 2 == 0 ? .5f : 1f);

        //Spawn a Cell at (x,y)
        GameObject cellGO = Instantiate(cellPrefab, new Vector2(x-xOffset, y-yOffset), Quaternion.identity);
        Cell cell = cellGO.GetComponent<Cell>();
        cell.pos = new Vector2(x, y);
        grid[x, y] = cell;
        cell.transform.name = x + "," + y;
    }

    public Vector2 GetIndexOfCell(Cell cell)
    {
        if(cell != null)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if(cell == grid[i,j])
                    {
                        return cell.pos;
                    }
                }
            }
        }

        return Vector2.negativeInfinity;
    }
}
