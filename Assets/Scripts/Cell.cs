using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool isAlive = false;
    private SpriteRenderer sp;

    public Vector2 pos = new Vector2();

    public int activeNeighbourCount = 0;
    public int deadNeighbourCount = 0;

    public void SetIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

	// Use this for initialization
	void Start ()
    {
        sp = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Alive();
        }

        else
        {
            Dead();
        }
	}

    public void Alive()
    {
        sp.color = Color.red;
    }

    public void Dead()
    {
        sp.color = Color.white;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere(this.transform.position, Utils.CIRCLE_RADIUS);
    //}

    public void CheckNeighbours()
    {
        Collider2D[] neighbourColliders = Physics2D.OverlapCircleAll(transform.position, Utils.CIRCLE_RADIUS);

        List<Collider2D> neighbourCollidersList = new List<Collider2D>();

        foreach (Collider2D col in neighbourColliders)
        {
            if(col.GetComponent<Cell>() != GetComponent<Cell>())
            {
                neighbourCollidersList.Add(col);
            }
        }

        Cell[] neighbourCells = new Cell[neighbourCollidersList.Count];

        for (int i = 0; i < neighbourCollidersList.Count; i++)
        {
            Cell cell = neighbourCollidersList[i].gameObject.GetComponent<Cell>();
            if (cell != null)
            {
                neighbourCells[i] = neighbourCollidersList[i].gameObject.GetComponent<Cell>();
            }
        }

        activeNeighbourCount = 0;
        deadNeighbourCount = 0;
        if (neighbourCells != null && neighbourCells.Length > 0)
        {
            foreach (Cell cell in neighbourCells)
            {
                if (cell.GetIsAlive())
                {
                    activeNeighbourCount++;
                }

                else
                {
                    deadNeighbourCount++;
                }
            }
        }
    }

    //Click Events
    private void OnMouseDown()
    {
        isAlive = !isAlive;
    }
}
