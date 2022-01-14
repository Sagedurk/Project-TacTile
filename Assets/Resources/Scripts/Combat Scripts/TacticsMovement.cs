using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMovement : MonoBehaviour
{

    
    public bool turn = false;

    public List<PathfindingTile> selectableTiles = new List<PathfindingTile>();
    protected GameObject[] tiles;

    protected Stack<PathfindingTile> path = new Stack<PathfindingTile>();
    protected PathfindingTile currentTile;

    static public GameObject cursor;

    public bool moving = false;             //If the unit is moving or not
    public int move = 5;                    //How many tiles the unit can move
    public float jumpHeight = 1f;           //Height difference between 2 walkable tiles
    public float moveSpeed = 2;             //How fast the unit moves to other tiles
    public float jumpVelocity = 4.5f;       //How fast the jump is performed
    protected bool dead = false;
    public bool IsDead() { return dead; }
    public string enemyTeam;                

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    //Jump flags
    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;


    //Input flags
    static public bool allowFreeCam = true;


    //Turn State Flags
    public bool turnAcceptFlag;
    public bool turnCancelFlag;
    public int turnStateCounter = 0;
    public bool cancel;

    public PathfindingTile actualTargetTile;

    protected void Init()
    {

        
            tiles = GameObject.FindGameObjectsWithTag("Tile");

            halfHeight = GetComponent<Collider>().bounds.extents.y;

            TurnManager.AddUnit(this);
        
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.ChangeTileState(PathfindingTile.TileStates.CURRENT);
    }

    public void GetDefenseTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.ChangeTileState(PathfindingTile.TileStates.DEFEND);
    }

    public PathfindingTile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        PathfindingTile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<PathfindingTile>();
        }

        return tile;
    }


    public void ComputeAdjacencyList(float jumpHeight, PathfindingTile target)
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");        //if map is going to be changing

        foreach (GameObject tile in tiles)
        {
            PathfindingTile t = tile.GetComponent<PathfindingTile>();
            t.FindNeighbors(jumpHeight,target);

        }
    }


    public void FindSelectableTiles()
    {
        ComputeAdjacencyList(jumpHeight, null);
        GetCurrentTile();

        Queue<PathfindingTile> process = new Queue<PathfindingTile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave as null

        while (process.Count > 0)
        {
            PathfindingTile t = process.Dequeue();

            selectableTiles.Add(t);
            t.ChangeTileState(PathfindingTile.TileStates.SELECTABLE_WALK);

            if (t.distance < move)
            {
                foreach (PathfindingTile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }

        }
    }

    public void MoveToTile(PathfindingTile tile)
    {
        path.Clear();
        tile.ChangeTileState(PathfindingTile.TileStates.TARGET);
        moving = true;

        PathfindingTile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }

    }


    public void Move()
    {
        
        if(path.Count > 0)
        {
            PathfindingTile t = path.Peek();
            Vector3 target = t.transform.position;

            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;
                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                    
                }

                

                //Locomotion - act of moving - movement animation goes here
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();

            }

        }
        else
        {
            RemoveSelectableTiles(selectableTiles);
            turnStateCounter++;
            moving = false;


            // Debug NPC behaviour, delete this later
            if (tag == "NPC") {
                TurnManager.EndTurn();
            }
        }
    }

    public void RemoveSelectableTiles(List<PathfindingTile> listToClear)
    {
        if(currentTile != null)
        {
            currentTile.ChangeTileState(PathfindingTile.TileStates.DEFAULT);
            currentTile = null;
        }
        foreach (PathfindingTile tile in listToClear)
        {
            tile.Reset();
        }

        listToClear.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }
    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if(jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;

        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;
     
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }


    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }

    }
    
    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if(transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    protected PathfindingTile FindLowestF(List<PathfindingTile> list)
    {
        PathfindingTile lowest = list[0];

        foreach (PathfindingTile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);
        return lowest;
    }

    protected PathfindingTile FindEndTile(PathfindingTile t)
    {
        Stack<PathfindingTile> tempPath = new Stack<PathfindingTile>();

        PathfindingTile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            return t.parent;
        }

        PathfindingTile endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(PathfindingTile target)
    {
        ComputeAdjacencyList(jumpHeight, target);
        GetCurrentTile();

        List<PathfindingTile> openList = new List<PathfindingTile>();
        List<PathfindingTile> closedList = new List<PathfindingTile>();

        openList.Add(currentTile);

        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            PathfindingTile t = FindLowestF(openList);
            closedList.Add(t);

            if (t == target && t.walkable)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (PathfindingTile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                } 
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
                
            }
        }
        // What happens if AI can't find a path?
        TurnManager.EndTurn();

    }

    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }
}