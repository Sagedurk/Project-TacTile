using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathfindingMaster : Singleton<PathfindingMaster>
{
    PathfindingTile startTile;
    int amountOfSteps;

    public Patterns patterns;
    

    List<PathfindingTile.Node> listOfNodesToCheck = new List<PathfindingTile.Node>();
    PathfindingTile.Node currentNode = new PathfindingTile.Node();
    int amountOfTilesToCheck;
    
    Coroutine chooseTarget = null;
    bool isValidTargetChosen = false;

    enum TargetOutcomes
    {
        NO_OBJECT,
        NO_TILE,
        TILE_OUT_OF_RANGE,
        TILE_RANGE_NO_OBJECT,
        TILE_RANGE_NO_UNIT,
        TILE_RANGE_UNIT_DIFFERENT_TEAM,
        TILE_RANGE_UNIT_SAME_TEAM_ALLY,
        TILE_RANGE_UNIT_SAME_TEAM_SELF

    }


    [System.Serializable]
    public class Patterns
    {
        public void Radial(int stepAmount, PathfindingTile startingTile, PathfindingTile.TileStates startingTileState, PathfindingTile.TileStates otherTileState, bool isBlocking)
        {
            Instance.PathfindingSetup(startingTile, stepAmount);
            Instance.Pathfinding_BFS(startingTileState, otherTileState, isBlocking);
        }


    }

    // ---------- Functions ---------- //

    private void Awake()
    {
        CheckInstance(this, true);
    }

    private void Pathfinding_BFS(PathfindingTile.TileStates startTileState, PathfindingTile.TileStates otherTileState, bool isBlocking)
    {
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;

        //Start node prepping
        startTile.ChangeTileState(startTileState);
        startTile.visited = true;
        startTile.parentNode.visited = true;
        listOfNodesToCheck.Add(startTile.parentNode);


        //Node looping
        for (int i = 0; i < amountOfTilesToCheck; i++)
        {
            if (i >= listOfNodesToCheck.Count)
                break;

            currentNode = listOfNodesToCheck[i];

            if (listOfNodesToCheck[i].tile != null)
            {
                UpdateNodeData(false, otherTileState);
            }

            FindNeighbouringTiles(currentNode, isBlocking);
        }

        Pathfinding_BFS_Remove_Frontier();
    }


    void Pathfinding_BFS_Outermost()
    {
        //REMAKE OUTERMOST 

        //int lastIndexOfList = listOfNodesToCheck.Count - 1;

        //for (int i = 0; i < amountOfSteps * 4; i++)
        //{
        //    currentNode = listOfNodesToCheck[lastIndexOfList - i];

        //    if (currentNode.tile == null)
        //        continue;

        //    currentNode.tile.ChangeTileState(TileScript.TileStates.TARGET);
        //}

        //if (showOnlyOutermostTiles)
        //{
        //    for (int i = 1; i < listOfNodesToCheck.Count - amountOfSteps * 4; i++)
        //    {
        //        if(listOfNodesToCheck[i].tile != null)
        //            listOfNodesToCheck[i].tile.Reset();
        //    }
        //}
    }

    void Pathfinding_BFS_Remove_Frontier()
    {
        //Determine which nodes are invalid

        List<PathfindingTile.Node> listOfNodesToRemove = new List<PathfindingTile.Node>();
        for (int i = 0; i < listOfNodesToCheck.Count; i++)
        {
            currentNode = listOfNodesToCheck[i];

            if (ReturnsToOriginInAmountOfSteps(currentNode))
                continue;

            //If tile doesn't return to start point
            currentNode.Reset();
            listOfNodesToRemove.Add(currentNode);
        }

        //remove invalid nodes from list
        foreach (PathfindingTile.Node tile in listOfNodesToRemove)
        {
            listOfNodesToCheck.Remove(tile);
        }
    }


    void Pathfinding_BFS_Global()
    {
        //Rework? Will it be different from BFS?
    }


    void Pathfinding_4_Directions()
    {

    }


    void Pathfinding_8_Directions()
    {

    }


    // ---------- Helper Functions ---------- //

    public void ResetTiles()
    {
        for (int i = 0; i < listOfNodesToCheck.Count; i++)
        {
            listOfNodesToCheck[i].Reset();
        }

        listOfNodesToCheck.Clear();
    }

    public void PathfindingSetup(PathfindingTile startingTile, int stepAmount)
    {
        startTile = startingTile;
        amountOfSteps = stepAmount;
    }

    private void UpdateNodeData(bool blockedStatus, PathfindingTile.TileStates tileState)
    {
        currentNode.isBlocked = blockedStatus;
        currentNode.tile.ChangeTileState(tileState);
        currentNode.position = currentNode.tile.transform.position;
    }


    private bool CheckIfTileIsObstructed(PathfindingTile.Node data, bool isBlocking)
    {
        if (data.tile == null)
            data.isBlocked = true;
        else if (Physics.Raycast(data.position, Vector3.up, 1) && isBlocking)
            data.isBlocked = true;
        else
            data.isBlocked = false;

        return data.isBlocked;
    }


    private bool ReturnsToOriginInAmountOfSteps(PathfindingTile.Node node)
    {
        PathfindingTile.Node tempNode = node;

        for (int j = 0; j < amountOfSteps + 1; j++)
        {
            if (tempNode.isBlocked)
                return false;

            if (tempNode.previousNode == null)
                return true;

            tempNode = tempNode.previousNode;
        }

        return false;
    }


    private void FindNeighbouringTiles(PathfindingTile.Node node, bool isBlocking)
    {
        for (int i = 0; i < 4; i++)
        {
            if (node.tile.ListOfNeighbourNodes[i].visited)
                continue;

            if (CheckIfTileIsObstructed(node.tile.ListOfNeighbourNodes[i], isBlocking))
                continue;

            listOfNodesToCheck.Add(node.tile.ListOfNeighbourNodes[i]);
            node.tile.ListOfNeighbourNodes[i].visited = true;
            node.tile.ListOfNeighbourNodes[i].previousNode = node;
        }
    }




    //Target Tile

    public void ChooseTarget(PathfindingTile.TileStates targetTileState, bool isCountingTileStateCurrentAsTargetTileState = true)
    {
        if(chooseTarget == null)
            chooseTarget = StartCoroutine(FindValidTarget(targetTileState, isCountingTileStateCurrentAsTargetTileState));

    }


    IEnumerator FindValidTarget(PathfindingTile.TileStates targetTileState, bool isCountingTileStateCurrentAsTargetTileState)
    {
        isValidTargetChosen = false;

        while (!isValidTargetChosen)
        {
            if (Physics.Raycast(InputCombat.Instance.combatCursor.transform.position, Vector3.down, out RaycastHit tileHit, 1.0f))
            {
                if (tileHit.transform.TryGetComponent(out PathfindingTile tile))    //If tile is found
                {
                    if(tile.tileState == targetTileState || (tile.tileState == PathfindingTile.TileStates.CURRENT && isCountingTileStateCurrentAsTargetTileState))   //If desired tile state
                    {
                        if (Physics.Raycast(tile.transform.position, Vector3.up, out RaycastHit unitHit, 1.0f)) //Object is above tile
                        {
                            if(unitHit.transform.TryGetComponent(out UnitMaster unit))  //Unit above tile
                            {
                                if(unit.unitTeam.team == TurnOrder.Instance.activeUnit.unitTeam.team)   //Ally team
                                {
                                    if(unit == TurnOrder.Instance.activeUnit)   //Unit is self
                                    {
                                        Debug.Log("Self found");
                                    }
                                    else    //Unit is ally
                                    {
                                        Debug.Log("Ally unit found");
                                    }
                                }
                                else    //Enemy unit
                                {
                                    Debug.Log("Enemy unit found");
                                }
                            }
                            else    //Object found, but not a unit
                            {
                                Debug.Log("Object found above, no unit");
                            }
                        }
                        else    //Object not found
                        {
                            Debug.Log("No Object Above Tile");
                        }

                    }
                    else    //Tile state is not desired one
                    {
                        Debug.Log("Tile is out of range");
                    }

                }
                else    //Tile not found, but object is   -   Shouldn't happen, but just in case
                {
                    Debug.Log("Cursor isn't on a tile...");
                }
            }
            else    //Tile not found, nor object
            {
                Debug.Log("Cursor isn't on anything");
            }


            yield return null;
        }


        chooseTarget = null;


    }

}