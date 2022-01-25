using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathfindingMaster : Singleton<PathfindingMaster>
{
    PathfindingTile startTile;
    int amountOfSteps;

    [HideInInspector] public Patterns patterns;

    List<PathfindingTile.Node> listOfNodesToCheck = new List<PathfindingTile.Node>();
    List<PathfindingTile.Node> listOfNodesToRemove = new List<PathfindingTile.Node>();
    PathfindingTile.Node currentNode = new PathfindingTile.Node();
    int amountOfTilesToCheck;
    
    public TargetOutcomes targetOutcome;
    PathfindingTile targetTile;

    public enum TargetOutcomes
    {
        NOTHING_BELOW,          //Needed for creation of tiles
        NO_TILE_BELOW,          //Always throw error?
        TILE_OUT_OF_RANGE,      //Always throw error
        NO_OBJECT_ABOVE_TILE,   //Needed for walk
        OBJECT_ABOVE_TILE,      //No use right now
        ENEMY_UNIT_ABOVE_TILE,  //Needed for attack & offensive skills/items
        ALLY_UNIT_ABOVE_TILE,   //Needed for defensive skills/items
        SELF_UNIT_ABOVE_TILE    //Needed for defense & defensive skills/items

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

    public void ResetNodes()
    {
        for (int i = 0; i < listOfNodesToCheck.Count; i++)
        {
            listOfNodesToCheck[i].Reset();
        }

        listOfNodesToCheck.Clear();
        listOfNodesToRemove.Clear();
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

    public bool ChooseTarget(TargetOutcomes desiredOutcome, PathfindingTile.TileStates targetTileState, bool isCountingTileStateCurrentAsTargetTileState = true)
    {
        targetTile = null;

        if (Physics.Raycast(InputCombat.Instance.combatCursor.transform.position, Vector3.down, out RaycastHit tileHit, 1.0f))
        {
            if (tileHit.transform.TryGetComponent(out PathfindingTile tile))    //If tile is found
            {
                if (tile.tileState == targetTileState || (tile.tileState == PathfindingTile.TileStates.CURRENT && isCountingTileStateCurrentAsTargetTileState))   //If desired tile state
                {
                    targetTile = tile;
                    if (Physics.Raycast(tile.transform.position, Vector3.up, out RaycastHit unitHit, 1.0f)) //Object is above tile
                    {
                        if (unitHit.transform.TryGetComponent(out UnitMaster unit))  //Unit above tile
                        {
                            if (unit.unitTeam.team == TurnOrder.Instance.activeUnit.unitTeam.team)   //Ally team
                            {
                                if (unit == TurnOrder.Instance.activeUnit)   //Unit is self
                                {
                                    targetOutcome = TargetOutcomes.SELF_UNIT_ABOVE_TILE;
                                }
                                else    //Unit is ally
                                {
                                    targetOutcome = TargetOutcomes.ALLY_UNIT_ABOVE_TILE;
                                }
                            }
                            else    //Enemy unit
                            {
                                targetOutcome = TargetOutcomes.ENEMY_UNIT_ABOVE_TILE;
                            }
                        }
                        else    //Object found, but not a unit
                        {
                            targetOutcome = TargetOutcomes.OBJECT_ABOVE_TILE;
                        }
                    }
                    else    //Object not found
                    {
                        targetOutcome = TargetOutcomes.NO_OBJECT_ABOVE_TILE;
                    }

                }
                else    //Tile state is not desired one
                {
                    targetOutcome = TargetOutcomes.TILE_OUT_OF_RANGE;
                }

            }
            else    //Tile not found, but object is   -   Shouldn't happen, but just in case
            {
                targetOutcome = TargetOutcomes.NO_TILE_BELOW;
            }
        }
        else    //Tile not found, nor object
        {
            targetOutcome = TargetOutcomes.NOTHING_BELOW;
        }


        if (targetOutcome != desiredOutcome)
            return false;


        return true;
    }


    public void MoveObjectToTargetTile(GameObject obj, Vector3 positionOffset)
    {
        obj.transform.position = targetTile.transform.position + positionOffset;
    }


}