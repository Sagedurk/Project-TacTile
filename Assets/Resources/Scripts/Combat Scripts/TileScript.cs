using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool attackable = false;
    public bool selectableAttack = false;
    public bool selectableSkill = false;
    public bool selectableItem = false;
    public bool defendTile = false;

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


    void Start()
    {
        
    }

    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }  
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (selectableAttack)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (selectableSkill)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (selectableItem)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (defendTile)
        {
            GetComponent<Renderer>().material.color = new Color(0.748f,0.3916f,0.3916f);
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }

    }

    public void Reset()
    {
        adjacencyList.Clear();
        attackList.Clear();

        walkable = true;
        current = false;
        target = false;
        selectable = false;
        selectableAttack = false;
        selectableSkill = false;
        selectableItem = false;
        attackable = false;
        defendTile = false;

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
