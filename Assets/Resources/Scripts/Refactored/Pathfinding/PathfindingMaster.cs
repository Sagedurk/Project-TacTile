using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathfindingMaster : Singleton<PathfindingMaster>
{
    TileScript startTile;
    int amountOfSteps;

    public Patterns patterns;
    List<TileScript.Node> listOfNodesToCheck = new List<TileScript.Node>();
    TileScript.Node currentNode = new TileScript.Node();
    int amountOfTilesToCheck;

    [System.Serializable]
    public class Patterns
    {
        public void Radial(TileScript startTile, TileScript.TileStates startingTileState, TileScript.TileStates otherTileState, int stepAmount)
        {
            Instance.PathfindingSetup(startTile, stepAmount);
            Instance.Pathfinding_BFS(startingTileState, otherTileState);
        }


    }


    // ---------- Functions ---------- //

    private void Awake()
    {
        CheckInstance(this, true);
    }

    private void Pathfinding_BFS(TileScript.TileStates startTileState, TileScript.TileStates otherTileState)
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

            FindNeighbouringTiles(currentNode);
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

        List<TileScript.Node> listOfNodesToRemove = new List<TileScript.Node>();
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
        foreach (TileScript.Node tile in listOfNodesToRemove)
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

    public void PathfindingSetup(TileScript startingTile, int stepAmount)
    {
        startTile = startingTile;
        amountOfSteps = stepAmount;
    }

    private void UpdateNodeData(bool blockedStatus, TileScript.TileStates tileState)
    {
        currentNode.isBlocked = blockedStatus;
        currentNode.tile.ChangeTileState(tileState);
        currentNode.position = currentNode.tile.transform.position;
    }


    private bool CheckIfTileIsObstructed(TileScript.Node data)
    {
        if (Physics.Raycast(data.position, Vector3.up, 1) || data.tile == null)
            data.isBlocked = true;
        else
            data.isBlocked = false;

        return data.isBlocked;
    }


    private bool ReturnsToOriginInAmountOfSteps(TileScript.Node node)
    {
        TileScript.Node tempNode = node;

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


    private void FindNeighbouringTiles(TileScript.Node node)
    {
        for (int i = 0; i < 4; i++)
        {
            if (node.tile.ListOfNeighbourNodes[i].visited)
                continue;

            if (CheckIfTileIsObstructed(node.tile.ListOfNeighbourNodes[i]))
                continue;

            listOfNodesToCheck.Add(node.tile.ListOfNeighbourNodes[i]);
            node.tile.ListOfNeighbourNodes[i].visited = true;
            node.tile.ListOfNeighbourNodes[i].previousNode = node;
        }
    }

}