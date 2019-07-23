using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private GridManager gm;

    public float timeStep = 5f;

    private float currentTimeStep = 0;

    [SerializeField] private bool isTimeStepBased = false;

    private bool inUse = false;

    public Dictionary<Cell, bool> cellsToBeChanged = new Dictionary<Cell, bool>();

	// Use this for initialization
	void Start ()
    {
        currentTimeStep = timeStep;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTimeStep -= Time.deltaTime;

        if (currentTimeStep <= 0)
        {
            //This Calculate the neighbours and put the changes in the Dictionary
            CalculateNeighbours();
            //CheckGrid();

            //This iterates through the Dictionary and Change the required cells
            ChangeCellStates();

            //Setting the TimeStep again for next Iteration
            currentTimeStep = timeStep;
        }
	}

    //This iterates through the Dictionary and Change the required cells
    private void ChangeCellStates()
    {
        foreach (KeyValuePair<Cell, bool> cell in cellsToBeChanged)
        {
            if(cell.Value)
            {
                cell.Key.SetIsAlive(true);
            }

            else
            {
                cell.Key.SetIsAlive(false);
            }
        }

        cellsToBeChanged.Clear();
    }

    //This Calculate the neighbours and put the changes in the Dictionary
    private void CalculateNeighbours()
    {
        for (int i = 0; i < gm.grid.GetLength(0); i++)
        {
            for (int j = 0; j < gm.grid.GetLength(1); j++)
            {
                gm.grid[i, j].CheckNeighbours();
                CheckForNeighbours(i, j);
            }
        }
    }

    //The next moment in Time when we need to CHeck the grid for Alive or Dead Cells
    //private void CheckGrid()
    //{
    //    for (int i = 0; i < gm.grid.GetLength(0); i++)
    //    {
    //        for (int j = 0; j < gm.grid.GetLength(1); j++)
    //        {
    //            CheckForNeighbours(i,j);
    //        }
    //    }
    //}

    private void CheckForNeighbours(int x, int y) //This Sends the active neighbours to the Rule Function
    {
        int alive = gm.grid[x, y].activeNeighbourCount;

        RulesOfTheSimulation(gm.grid[x, y], alive);
    }

    //A Rule Holder----------------------------------------------
    void RulesOfTheSimulation(Cell currentCell, int aCC) //Rule holder for the Simulation
    {
        int aliveCellCount = aCC;

        Vector2 pos = gm.GetIndexOfCell(currentCell);

        //Debug.Log("Current Element " + pos.x + "," + pos.y + " active neihbours = " + aliveCellCount + " deadNeighbours = " + deadCellCount);

        RuleOne(currentCell, aliveCellCount);
        RuleTwo(currentCell, aliveCellCount);
        RuleThree(currentCell, aliveCellCount);
        RuleFour(currentCell, aliveCellCount);
    }

    //Rules----------------------------------------------------

    //If Live Cell has Greater than 2 Live Neighbours it has to Die
    private void RuleOne(Cell currentCell, int aliveCellCount)
    {
        if(currentCell.GetIsAlive() && aliveCellCount < 2)
        {
            //currentCell.SetIsAlive(false);
            cellsToBeChanged.Add(currentCell, false);
        }
    }

    //If Live Cell has 2 OR 3 Live Neighbours it Lives
    private void RuleTwo(Cell currentCell, int aliveCellCount)
    {
        if (currentCell.GetIsAlive() && (aliveCellCount == 2 || aliveCellCount == 3))
        {
            //currentCell.SetIsAlive(true);
        }
    }

    //If Live Cell has Greater Than 3 Live Neighbours it has to Die
    private void RuleThree(Cell currentCell, int aliveCellCount)
    {
        if (currentCell.GetIsAlive() && aliveCellCount > 3)
        {
            //currentCell.SetIsAlive(false);
            cellsToBeChanged.Add(currentCell, false);
        }
    }

    //If Dead Cell has exactly 3 Live Neighbours it Livess
    private void RuleFour(Cell currentCell, int aliveCellCount)
    {
        if (!currentCell.GetIsAlive() && aliveCellCount == 3)
        {
            //currentCell.SetIsAlive(true);
            cellsToBeChanged.Add(currentCell, true);
        }

        inUse = true;
    }
}
