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
    public Node parentNode;
    
    
    public bool isBlocked = false;





    //Needs reworking
    public bool walkable = true;
    public bool attackable = false;

    public List<TileScript> adjacencyList = new List<TileScript>();
    public List<TileScript> attackList = new List<TileScript>();
    public List<TileScript> skillList = new List<TileScript>();
    public List<TileScript> itemList = new List<TileScript>();

    public List <Node> neighbourList = new List<Node>();
    public List <Node> ListOfNeighbourNodes = new List<Node>();

    [Space(10)]
    public Node previousNode;

    public bool visited = false;
    public TileScript parent = null;
    public int distance = 0;

    public float f = 0;
    public float g = 0;
    public float h = 0;

    [System.Serializable]
    public class Node
    {
        public Vector3 position = Vector3.zero;
        public TileScript tile = null;
        public Node previousNode = null;
        public bool visited = false;
        public bool isBlocked = false;

        public Node CreateNode(Vector3 position)
        {
            Node newNode = new Node();
            newNode.position = position;
            newNode.tile = null;
            newNode.previousNode = null;

            return newNode;
        }


        public void Reset()
        {
            visited = false;
            
            if(tile != null)
                tile.Reset();
        }
    }

    // ---------- End of variable declaration ---------- //

    private void Awake()
    {
        //Try to load saved data
        parentNode.position = transform.position;
        parentNode.tile = this;
    }

    private void Start()
    {
        CreateNeighbourReferences();
    }

    private void Update()
    {
        parentNode.position = transform.position;
        CheckNeighbours();
    }




    public void ChangeTileState(TileStates newStateOfTile)
    {
        //Debug.Log(newStateOfTile);
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

        if (!PathfindingMaster.Instance.isUsingNeighbourList)
            neighbourList.Clear();

        ChangeTileState(TileStates.DEFAULT);

        walkable = true;
        attackable = false;
        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void CreateNeighbourReferences()
    {
        Node node;
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    node = FindNeighbour(Vector3.forward);
                    ListOfNeighbourNodes.Add(node);
                    break;
                case 1:
                    node = FindNeighbour(Vector3.right);
                    ListOfNeighbourNodes.Add(node);
                    break;
                case 2:
                    node = FindNeighbour(Vector3.back);
                    ListOfNeighbourNodes.Add(node);
                    break;
                case 3:
                    node = FindNeighbour(Vector3.left);
                    ListOfNeighbourNodes.Add(node);
                    break;
                default:
                    break;
            }


        }



    }

    private Node FindNeighbour(Vector3 direction)
    {
        //if object is hit
        if(Physics.Raycast(parentNode.position, direction, out RaycastHit hit, 1))
        {
           if(hit.transform.TryGetComponent(out TileScript tile))
            {
                //If object is a tile
                return tile.parentNode;
            }

            //If object isn't a tile    (shouldn't happen, just for precaution)
            return parentNode.CreateNode(parentNode.position + direction);
        }

        //if no object is hit at all
        return parentNode.CreateNode(parentNode.position + direction);
    }

    private void UpdateNeighbour(Vector3 direction, Node nodeReference)
    {
        //if object is hit
        if (Physics.Raycast(parentNode.position, direction, out RaycastHit hit, 1))
        {
            if (hit.transform.TryGetComponent(out TileScript tile))
            {
                //If object is a tile
                nodeReference.tile = tile;
                nodeReference.position = tile.parentNode.position;
                return;
            }

            //If object isn't a tile    (shouldn't happen, just for precaution)
            if (nodeReference.tile != null)
            {
                //not working
                Node node = parentNode.CreateNode(parentNode.position + direction);
                int index = ListOfNeighbourNodes.IndexOf(nodeReference);
                ListOfNeighbourNodes.Remove(nodeReference);
                ListOfNeighbourNodes.Insert(index, node);
            }
            else
            {
                nodeReference.position = parentNode.position + direction;
            }

            return;
        }

        //if no object is hit at all
        if (nodeReference.tile != null)
        {
            //not working
            Node node = parentNode.CreateNode(parentNode.position + direction);
            int index = ListOfNeighbourNodes.IndexOf(nodeReference);
            ListOfNeighbourNodes.Remove(nodeReference);
            ListOfNeighbourNodes.Insert(index, node);
        }
        else
        {
            nodeReference.position = parentNode.position + direction;
        }

        return;
    }


    private void CheckNeighbours()
    {

        for (int i = 0; i < ListOfNeighbourNodes.Count; i++)
        {

            switch (i)
            {
                case 0:
                    UpdateNeighbour(Vector3.forward, ListOfNeighbourNodes[0]);
                    break;
                case 1:
                    UpdateNeighbour(Vector3.right, ListOfNeighbourNodes[1]);
                    break;
                case 2:
                    UpdateNeighbour(Vector3.back, ListOfNeighbourNodes[2]);
                    break;
                case 3:
                    UpdateNeighbour(Vector3.left, ListOfNeighbourNodes[3]);
                    break;
                default:
                    break;
            }
        }
    }


    // ---------- End of Updated Functions ---------- //

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
