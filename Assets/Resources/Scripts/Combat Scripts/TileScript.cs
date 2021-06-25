using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public enum TileStates
    {
        CURRENT,
        TARGET,
        SELECTABLE_WALK,
        SELECTABLE_ATTACK,
        SELECTABLE_SKILL,
        SELECTABLE_ITEM,
        DEFEND,
        DEFAULT,
    }

    public TileStates tileState;

    public bool walkable = true;
    public bool attackable = false;

    public List<TileScript> adjacencyList = new List<TileScript>();
    public List<TileScript> attackList = new List<TileScript>();
    public List<TileScript> skillList = new List<TileScript>();
    public List<TileScript> itemList = new List<TileScript>();

    public bool visited = false;
    public TileScript parent = null;
    public int distance = 0;

    public float f = 0;
    public float g = 0;
    public float h = 0;

    // ---------- End of variable declaration ---------- //


    public void ChangeTileState(TileStates newStateOfTile)
    {
        if (tileState == TileStates.CURRENT)
            if (newStateOfTile != TileStates.DEFAULT)
                return;


        tileState = newStateOfTile;

        switch (tileState)
        {
            case TileStates.CURRENT:
                GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case TileStates.TARGET:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case TileStates.SELECTABLE_WALK:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case TileStates.SELECTABLE_ATTACK:
            GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case TileStates.SELECTABLE_SKILL:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case TileStates.SELECTABLE_ITEM:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            case TileStates.DEFEND:
                GetComponent<Renderer>().material.color = new Color(0.748f, 0.3916f, 0.3916f);
                break;
            case TileStates.DEFAULT:
                GetComponent<Renderer>().material.color = Color.gray;
                break;
        }
    }

    public void Reset()
    {
        adjacencyList.Clear();
        attackList.Clear();

        ChangeTileState(TileStates.DEFAULT);

        walkable = true;
        attackable = false;
        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }


    public void FindNeighbors(float jumpHeight, TileScript target)
    {
        Reset();

        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, TileScript target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1+jumpHeight)/2.0f, 0.25f);

        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider item in colliders)
        {
            TileScript tile = item.GetComponent<TileScript>();
            if(tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {

                    adjacencyList.Add(tile);
                }
            }

            if(tile != null && !tile.attackable)
            {
             //   RaycastHit hit;
             //   if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
             // {
                    attackList.Add(tile);
                    skillList.Add(tile);
                    itemList.Add(tile);
             //   }
            }
        }

    }


}
