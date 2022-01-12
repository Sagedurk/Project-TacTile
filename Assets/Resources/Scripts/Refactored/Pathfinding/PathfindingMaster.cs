using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMaster : Singleton<PathfindingMaster>
{
    [SerializeField] TileScript startTile;
    [SerializeField] int amountOfSteps;
    //Debug bools
    [Space(10)] [SerializeField] bool globalPathfinding = false;
    [SerializeField] bool showPattern4Dir = false;
    [SerializeField] bool showPattern8Dir = false;
    
    [Header("Outermost")]
    [SerializeField] bool showOutermostTiles = false;
    [SerializeField] bool showOnlyOutermostTiles = false;

    [Header("Debug")]
    public bool isUsingNeighbourList = false;
    [Space(10)]
    [SerializeField] bool drawGizmos = false;
    [SerializeField] int amountOfGizmos = 0;
    

    //private variables
    int amountOfTilesToCheck;
    List<TileScript.Node> listOfNodesToCheck = new List<TileScript.Node>();
    //List<Vector3> listOfNullTilesPosition = new List<Vector3>();
    //List<Vector3> listOfNullNeighboursPosition = new List<Vector3>();
    TileScript.Node currentNode = new TileScript.Node();
    //Vector3 positionOfRaycast;
    TileScript.Node tileData = new TileScript.Node();

    //Script Debugging
    List<GizmoInformation> gizmoInformationList = new List<GizmoInformation>();


    [System.Serializable]
    class GizmoInformation
    {
        public Vector3 position;
        public Vector3 scale;
    }


    /*
     * 
     *          PATTERN RESEARCH
     *          
     *  *   *   *   OUTERMOST   *   *   *
     *          steps   -   frontier
     *              
     *              n   -   n * 4
     *              5   -   20  (5 * 4)
     *              4   -   16  (4 * 4)
     *              7   -   28  (7 * 4)
     */

    //Potential fix for blocked tiles:
    /*
     Reference to previous tile
     If it takes more than [amountOfSteps] steps to return to [tempTile], reset tile    (While loop)
     
     
     Set reference to previous tile
     
     */



    private void Awake()
    {
        CheckInstance(this, true);
    }


    private void Update()
    {
        
        


        //if(isCoroutineDone)
        ResetTiles();

        
        if (globalPathfinding)
            Pathfinding_BFS_Global();

        else if (showPattern4Dir)
            Pathfinding_4_Directions();

        else if (showPattern8Dir)
            Pathfinding_8_Directions();

        else
        {
            //if(isCoroutineDone)
            //StartCoroutine(DebugTileSequence(0.025f));
            Pathfinding_BFS_Updated();
            //Pathfinding_BFS(!showOutermostTiles);

            if (showOutermostTiles)
            {
                //Pathfinding_BFS_Remove_Frontier();
                Pathfinding_BFS_Outermost();
            }
        }

        //if(isCoroutineDone)
        //    StartCoroutine(Pathfinding_BFS_IEnum());



        
    }

    void Pathfinding_BFS_Updated()
    {
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        
        //Start node prepping
        startTile.ChangeTileState(TileScript.TileStates.CURRENT);
        startTile.visited = true;
        startTile.parentNode.visited = true;
        listOfNodesToCheck.Add(startTile.parentNode);

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {
            if (i >= listOfNodesToCheck.Count)
            {
                break;
            }

            currentNode = listOfNodesToCheck[i];

            if (listOfNodesToCheck[i].tile != null)
            {
                //Update currentlyCheckedTile
                    UpdateNodeData(false, TileScript.TileStates.SELECTABLE_SKILL);

            }

            FindNeighbouringTilesUpdated(currentNode);
        }

        Pathfinding_BFS_Remove_Frontier();

        Debug.Log(amountOfTilesToCheck);
        Debug.Log(listOfNodesToCheck.Count);
    }


    void Pathfinding_BFS(bool isCheckingForBlockedTiles = false)
    {
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        startTile.ChangeTileState(TileScript.TileStates.CURRENT);
        startTile.visited = true;

        TileScript.Node data = new TileScript.Node();
        data.tile = startTile;
        data.position = startTile.transform.position;

        listOfNodesToCheck.Add(data);

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {
            currentNode = listOfNodesToCheck[i];

            if (listOfNodesToCheck[i].tile != null)
            {
                //Update currentlyCheckedTile
                if (CheckIfTileIsObstructed(currentNode))
                    UpdateNodeData(true, TileScript.TileStates.DEFAULT);    //If tile is obstructed
                else
                    UpdateNodeData(false, TileScript.TileStates.SELECTABLE_SKILL);  //If tile is not obstructed

            }

            FindNeighbouringTiles(currentNode);
        }

        
        
        Pathfinding_BFS_Remove_Frontier();

    }
    
    void Pathfinding_BFS_Outermost()
    {
        int lastIndexOfList = listOfNodesToCheck.Count - 1;

        for (int i = 0; i < amountOfSteps * 4; i++)
        {
            currentNode = listOfNodesToCheck[lastIndexOfList - i];
            
            if (currentNode.tile == null)
                continue;

            currentNode.tile.ChangeTileState(TileScript.TileStates.TARGET);
        }

        if (showOnlyOutermostTiles)
        {
            for (int i = 1; i < listOfNodesToCheck.Count - amountOfSteps * 4; i++)
            {
                if(listOfNodesToCheck[i].tile != null)
                    listOfNodesToCheck[i].tile.Reset();
            }
        }
    }

    void Pathfinding_BFS_Remove_Frontier()
    {
        List<TileScript.Node> listOfNodesToRemove = new List<TileScript.Node>();
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
        foreach (TileScript.Node tile in listOfNodesToRemove)
        {
            RemoveGizmo(tile.position);
            listOfNodesToCheck.Remove(tile);
        }

    }


    void Pathfinding_BFS_Global()
    {
        //ResetTiles();
        //tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        //tempTile.visited = true;

        //listOfTilesToCheck.Add(tempTile);
        //for (int i = 0; i < listOfTilesToCheck.Count; i++)
        //{
        //    currentlyCheckedTile = listOfTilesToCheck[i];

        //    currentlyCheckedTile.ChangeTileState(TileScript.TileStates.TARGET);
        //    positionOfRaycast = currentlyCheckedTile.transform.position;
            
        //    //check each direction
        //    for (int j = 0; j < 4; j++)
        //    {
        //        switch (j)
        //        {
        //            case 0:
        //                CastRayGlobal(positionOfRaycast, Vector3.forward);
        //                break;
        //            case 1:
        //                CastRayGlobal(positionOfRaycast, Vector3.right);
        //                break;
        //            case 2:
        //                CastRayGlobal(positionOfRaycast, Vector3.back);
        //                break;
        //            case 3:
        //                CastRayGlobal(positionOfRaycast, Vector3.left);
        //                break;
        //        }
        //    }
        //}
    }

    void Pathfinding_4_Directions()
    {

    }
    
    void Pathfinding_8_Directions()
    {
        

    }



    // ---------- Helper Functions ---------- //

    void ResetTiles()
    {
        for (int i = 0; i < listOfNodesToCheck.Count; i++)
        {
            listOfNodesToCheck[i].Reset();
        }

        listOfNodesToCheck.Clear();
        gizmoInformationList.Clear();
    }
    
    void UpdateNodeData(bool blockedStatus, TileScript.TileStates tileState)
    {
        currentNode.isBlocked = blockedStatus;
        currentNode.tile.ChangeTileState(tileState);
        currentNode.position = currentNode.tile.transform.position;
    }


    bool CheckIfTileIsObstructed(TileScript.Node data)
    {
        if (Physics.Raycast(data.position, Vector3.up, 1) || data.tile == null)
        { 
            //Debug.Log(hit.collider.gameObject);
            data.isBlocked = true;
        }
        else
            data.isBlocked = false;

        return data.isBlocked;
    }


    bool ReturnsToOriginInAmountOfSteps(TileScript.Node tile)
    {
        TileScript.Node tempTile = tile;

        if (tempTile.tile == null)
            return false;

        for (int j = 0; j < amountOfSteps + 1; j++)
        {
            if (tempTile.isBlocked)
                return false;

            if (tempTile.previousNode == null)
            {
                    return true;
            }


            tempTile = tempTile.previousNode;

        }

        return false;

    }





    void FindNeighbouringTiles(TileScript.Node node)
    {
        //If list is populated 
        if (CheckNeighbourList(node))
        {
            return;
        }

        Vector3 boxOverlapScale;

        if(node.tile != null)
        {
            boxOverlapScale = node.tile.transform.lossyScale / 4;
        }
        else
        {
            boxOverlapScale = Vector3.one / 4;
        }


        //check all 4 directions for tiles
        for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        FindTile(node.position, Vector3.forward, boxOverlapScale);
                        break;
                    case 1:
                        FindTile(node.position, Vector3.right, boxOverlapScale);
                        break;
                    case 2:
                        FindTile(node.position, Vector3.back, boxOverlapScale);
                        break;
                    case 3:
                        FindTile(node.position, Vector3.left, boxOverlapScale);
                        break;
                }
            }
    }

    void FindNeighbouringTilesUpdated(TileScript.Node node)
    {
        for (int i = 0; i < 4; i++)
        {
            //if currentNode has no tile
            if (node.tile == null)
            {
                Vector3 direction = Vector3.zero;
                switch (i)
                {
                    case 0:
                        direction = Vector3.forward;
                        break;
                    case 1:
                        direction = Vector3.right;
                        break;
                    case 2:
                        direction = Vector3.back;
                        break;
                    case 3:
                        direction = Vector3.left;
                        break;
                    default:
                        break;
                }

                if ((node.position + direction) == node.previousNode.position)
                    continue;
                
                listOfNodesToCheck.Add(node);
                continue;
            }

            if (node.tile.ListOfNeighbourNodes[i].visited)
                continue;





            ////if neighbouring node has no tile
            //if (node.tile.ListOfNeighbourNodes[i].tile == null)
            //{
            //    listOfNodesToCheck.Add(node.tile.ListOfNeighbourNodes[i]);
            //    node.tile.ListOfNeighbourNodes[i].visited = true;
            //    node.tile.ListOfNeighbourNodes[i].previousNode = node;
            //    continue;
            //}

            //if neighbouring node has tile
            if (node.tile.ListOfNeighbourNodes[i].visited || CheckIfTileIsObstructed(node.tile.ListOfNeighbourNodes[i]))
                continue;

            listOfNodesToCheck.Add(node.tile.ListOfNeighbourNodes[i]);
            node.tile.ListOfNeighbourNodes[i].visited = true;
            node.tile.ListOfNeighbourNodes[i].previousNode = node;

        }
    }


    void FindTile(Vector3 overlapBoxPosition, Vector3 direction, Vector3 overlapBoxScale)
    {
        Vector3 nextTilePosition = overlapBoxPosition + direction;
        Collider[] tileColliders = Physics.OverlapBox(nextTilePosition, overlapBoxScale, Quaternion.identity);
        
        AddTileGizmo(nextTilePosition, overlapBoxScale);

        tileData = new TileScript.Node();
        tileData.previousNode = currentNode;
        tileData.position = nextTilePosition;
        tileData.tile = null;


        //If no tile is found
        if (tileColliders.Length == 0)
        {
            for (int i = 0; i < listOfNodesToCheck.Count; i++)
            {
                if (listOfNodesToCheck[i].position == tileData.position)
                    return;
            }

            listOfNodesToCheck.Add(tileData);

            //Add neighbour if the tile checked from 
            if(currentNode.tile != null)
            {
                currentNode.tile.neighbourList.Add(tileData);
            }

            return;
        }

        //If tile is found
        if (tileColliders[0].TryGetComponent(out TileScript tile))
        {
            tileData.tile = tile;

            if (TileStatusBlocked(tileData))
               return;
            
            
            //If tile isn't blocked
            if (tile.visited)
            {
                CheckIfNeighbourHasBeenAdded(tile);
                return;
            }

            tile.visited = true;
            listOfNodesToCheck.Add(tileData);
            

            if(currentNode.tile != null)
            {
                currentNode.tile.neighbourList.Add(tileData);
            }

        }

    }

    bool CheckNeighbourList(TileScript.Node data)
    {
        if (!isUsingNeighbourList)
            return false;

        if (data.tile == null) 
        {

            return false;
        }

        //If list of neighbours is populated
        if (data.tile.neighbourList.Count > 0)
        {
        
           
            for (int i = 0; i < data.tile.neighbourList.Count; i++)
            {
                if(data.tile.neighbourList[i].tile == null)
                {
                    AddNeighbourGizmos(data.position, i);
                }

                if (TileStatusBlocked(data))
                    continue;
                

                //If tile is not blocked
                if (data.tile.neighbourList[i].tile != null)
                {
                    if (data.tile.neighbourList[i].tile.visited)
                        continue;

                    data.tile.neighbourList[i].tile.visited = true;
                }
                
                listOfNodesToCheck.Add(data.tile.neighbourList[i]);
                

            }
            return true;
        }

        return false;
    }

    bool TileStatusBlocked(TileScript.Node data)
    {
        if (data.isBlocked)
        {



            data.tile.ChangeTileState(TileScript.TileStates.DEFAULT);
      
            //If only moving one direction
            //if (data.position.x - tempTile.transform.position.x == 0 ||
            //    data.position.z - tempTile.transform.position.z == 0)
            //{
            //    amountOfTilesToCheck -= 2;
            //}
            //else
            //    amountOfTilesToCheck--;

        }

        return data.isBlocked;
    }

    private void CheckIfNeighbourHasBeenAdded(TileScript neighbourTile)
    {
        if (currentNode.tile == null)
            return;

        bool isAdded = false;
        for (int i = 0; i < currentNode.tile.neighbourList.Count; i++)
        {
            if (currentNode.tile.neighbourList[i].tile == neighbourTile)
            {
                isAdded = true;
                break;
            }

        }
        if (!isAdded)
        {
            tileData.position = neighbourTile.transform.position;
            tileData.tile = neighbourTile;
            currentNode.tile.neighbourList.Add(tileData);
        }
    }

    void RemoveTilesFarAway()
    {
        Vector3 startPosition = startTile.transform.position;
        List<TileScript.Node> listOfTilesToRemove = new List<TileScript.Node>();
        foreach (TileScript.Node data in listOfNodesToCheck)
        {
            Vector3 distanceVector = startPosition - data.position;

            float xAbsolute = Mathf.Abs(distanceVector.x);
            float yAbsolute = Mathf.Abs(distanceVector.y);
            float zAbsolute = Mathf.Abs(distanceVector.z);

            float distanceUnits = xAbsolute + yAbsolute + zAbsolute;

            //if tile is too far, but still has been added to the list
            if (distanceUnits > amountOfSteps)
            {
                if(data.tile != null)
                    listOfTilesToRemove.Add(data);
            }

        }
        
        foreach (TileScript.Node data in listOfTilesToRemove)
        { 
            listOfNodesToCheck.Remove(data);
            data.tile.Reset();
            //data.tile.ChangeTileState(TileScript.TileStates.DEFAULT);
        }

        //listOfTilesToRemove.Clear();
    }




    /*----------- DEBUG -----------*/

    void AddNeighbourGizmos(Vector3 tilePosition, int switchIndex)
    {
        GizmoInformation gizmo = CreateGizmo(tilePosition, Vector3.one * 0.5f);

        switch (switchIndex)
        {
            case 0:
                gizmo.position += Vector3.forward;
                break;
            case 1:
                gizmo.position += Vector3.right;
                break;
            case 2:
                gizmo.position += Vector3.back;
                break;
            case 3:
                gizmo.position += Vector3.left;
                break;

            default:
                break;
        }

        gizmoInformationList.Add(gizmo);
    }

    void AddTileGizmo(Vector3 position, Vector3 scale)
    {
        GizmoInformation gizmo = CreateGizmo(position, scale * 2);
        gizmoInformationList.Add(gizmo);
    }

    void RemoveGizmo(Vector3 position)
    {
        List<GizmoInformation> gizmoRemovalList = new List<GizmoInformation>();
        foreach (GizmoInformation gizmo in gizmoInformationList)
        {
            if (gizmo.position != position)
                continue;

            gizmoRemovalList.Add(gizmo);
        }
        
        foreach (GizmoInformation gizmo in gizmoRemovalList)
        {
            gizmoInformationList.Remove(gizmo);
        }

    }

    GizmoInformation CreateGizmo(Vector3 position, Vector3 scale)
    {
        GizmoInformation gizmo = new GizmoInformation();
        gizmo.position = position;
        gizmo.scale = scale;

        return gizmo;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < gizmoInformationList.Count; i++)
        {
            if (i == amountOfGizmos)
                return;

            Gizmos.DrawWireCube(gizmoInformationList[i].position, gizmoInformationList[i].scale);
        }
    }

}
