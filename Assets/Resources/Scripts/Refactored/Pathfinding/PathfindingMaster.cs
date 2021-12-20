using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMaster : Singleton<PathfindingMaster>
{
    [SerializeField] TileScript tempTile;
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
    List<TileScript.TileData> listOfTilesToCheck = new List<TileScript.TileData>();
    //List<Vector3> listOfNullTilesPosition = new List<Vector3>();
    //List<Vector3> listOfNullNeighboursPosition = new List<Vector3>();
    TileScript.TileData currentlyCheckedTile;
    //Vector3 positionOfRaycast;
    TileScript.TileData tileData = new TileScript.TileData();

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
            Pathfinding_BFS(!showOutermostTiles);

            if (showOutermostTiles)
            {
                //Pathfinding_BFS_Remove_Frontier();
                Pathfinding_BFS_Outermost();
            }
        }

        //if(isCoroutineDone)
        //    StartCoroutine(Pathfinding_BFS_IEnum());
    }


    void Pathfinding_BFS(bool isCheckingForBlockedTiles = false)
    {
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;

        TileScript.TileData data = new TileScript.TileData();
        data.tile = tempTile;
        data.position = tempTile.transform.position;

        listOfTilesToCheck.Add(data);

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[i];

            if (listOfTilesToCheck[i].tile != null)
            {
                //Update currentlyCheckedTile
                if (CheckIfTileIsObstructed(currentlyCheckedTile))
                    UpdateTileData(true, TileScript.TileStates.DEFAULT);    //If tile is obstructed
                else
                    UpdateTileData(false, TileScript.TileStates.SELECTABLE_SKILL);  //If tile is not obstructed

            }

            FindNeighbouringTiles(currentlyCheckedTile);
        }

        Pathfinding_BFS_Remove_Frontier();

       if (isCheckingForBlockedTiles)
            RemoveTilesFarAway();
    }
    
    void Pathfinding_BFS_Outermost()
    {
        int lastIndexOfList = listOfTilesToCheck.Count - 1;

        for (int i = 0; i < amountOfSteps * 4; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[lastIndexOfList - i];
            
            if (currentlyCheckedTile.tile == null)
                continue;

            currentlyCheckedTile.tile.ChangeTileState(TileScript.TileStates.TARGET);
        }

        if (showOnlyOutermostTiles)
        {
            for (int i = 1; i < listOfTilesToCheck.Count - amountOfSteps * 4; i++)
            {
                if(listOfTilesToCheck[i].tile != null)
                    listOfTilesToCheck[i].tile.Reset();
            }
        }
    }

    void Pathfinding_BFS_Remove_Frontier()
    {
        List<TileScript.TileData> listOfIndicies = new List<TileScript.TileData>();
        for (int i = 0; i < listOfTilesToCheck.Count; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[i];

            if (currentlyCheckedTile.tile == null)
                continue;

            if (ReturnsToOriginInAmountOfSteps(currentlyCheckedTile.tile))
                continue;


            currentlyCheckedTile.tile.Reset();
            listOfIndicies.Add(currentlyCheckedTile);

        }

        foreach (TileScript.TileData tile in listOfIndicies)
        {
            RemoveGizmo(tile.position);
            listOfTilesToCheck.Remove(tile);
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
        for (int i = 0; i < listOfTilesToCheck.Count; i++)
        {
            if(listOfTilesToCheck[i].tile != null)
                listOfTilesToCheck[i].tile.Reset();
        }
        listOfTilesToCheck.Clear();
        gizmoInformationList.Clear();
    }
    
    void UpdateTileData(bool blockedStatus, TileScript.TileStates tileState)
    {
        currentlyCheckedTile.tile.isBlocked = blockedStatus;
        currentlyCheckedTile.tile.ChangeTileState(tileState);
        currentlyCheckedTile.position = currentlyCheckedTile.tile.transform.position;
    }


    bool CheckIfTileIsObstructed(TileScript.TileData tile)
    {
        if (Physics.Raycast(tile.position, Vector3.up, out RaycastHit hit, 1))
        { 
            //Debug.Log(hit.collider.gameObject);
            return true;
        }
        else
            return false;
    }


    bool ReturnsToOriginInAmountOfSteps(TileScript tile)
    {

        TileScript tempTile = tile;
        for (int j = 0; j < amountOfSteps + 1; j++)
        {
            if (tempTile.previousTile == null)
            {
                return true;
            }


            tempTile = tempTile.previousTile;

        }

        return false;

    }





    void FindNeighbouringTiles(TileScript.TileData tileData)
    {
        //If list is populated 
        if (CheckNeighbourList(tileData))
        {
            return;
        }

        Vector3 boxOverlapScale;

        if(tileData.tile != null)
        {
            boxOverlapScale = tileData.tile.transform.lossyScale / 4;
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
                    FindTile(tileData.position, Vector3.forward, boxOverlapScale);
                    break;
                case 1:
                    FindTile(tileData.position, Vector3.right, boxOverlapScale);
                    break;
                case 2:
                    FindTile(tileData.position, Vector3.back, boxOverlapScale);
                    break;
                case 3:
                    FindTile(tileData.position, Vector3.left, boxOverlapScale);
                    break;
            }
        }
    }

    void FindTile(Vector3 overlapBoxPosition, Vector3 direction, Vector3 overlapBoxScale)
    {
        Vector3 nextTilePosition = overlapBoxPosition + direction;
        Collider[] tileColliders = Physics.OverlapBox(nextTilePosition, overlapBoxScale, Quaternion.identity);
        
        AddTileGizmo(nextTilePosition, overlapBoxScale);

        tileData.position = nextTilePosition;
        tileData.tile = null;


        //If no tile is found
        if (tileColliders.Length == 0)
        {
            for (int i = 0; i < listOfTilesToCheck.Count; i++)
            {
                if (listOfTilesToCheck[i].position == tileData.position)
                    return;
            }

            listOfTilesToCheck.Add(tileData);

            //Add neighbour if the tile checked from 
            if(currentlyCheckedTile.tile != null)
            {
                currentlyCheckedTile.tile.neighbourList.Add(tileData);
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
            tile.previousTile = currentlyCheckedTile.tile;
            listOfTilesToCheck.Add(tileData);
            

            if(currentlyCheckedTile.tile != null)
                currentlyCheckedTile.tile.neighbourList.Add(tileData);

        }

    }

    bool CheckNeighbourList(TileScript.TileData tileData)
    {
        if (!isUsingNeighbourList)
            return false;

        if (tileData.tile == null) 
        {

            return false;
        }

        //If list of neighbours is populated
        if (tileData.tile.neighbourList.Count > 0)
        {
        
           
            for (int i = 0; i < tileData.tile.neighbourList.Count; i++)
            {
                if(tileData.tile.neighbourList[i].tile == null)
                {
                    AddNeighbourGizmos(tileData.position, i);
                }

                if (TileStatusBlocked(tileData))
                    continue;
                

                //If tile is not blocked
                if (tileData.tile.neighbourList[i].tile != null)
                {
                    if (tileData.tile.neighbourList[i].tile.visited)
                        continue;

                    tileData.tile.neighbourList[i].tile.visited = true;
                }
                
                listOfTilesToCheck.Add(tileData.tile.neighbourList[i]);
                

            }
            return true;
        }

        return false;
    }

    bool TileStatusBlocked(TileScript.TileData data)
    {
        if (data.tile.isBlocked)
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

        return data.tile.isBlocked;
    }

    private void CheckIfNeighbourHasBeenAdded(TileScript neighbourTile)
    {
        if (currentlyCheckedTile.tile == null)
            return;

        bool isAdded = false;
        for (int i = 0; i < currentlyCheckedTile.tile.neighbourList.Count; i++)
        {
            if (currentlyCheckedTile.tile.neighbourList[i].tile == neighbourTile)
            {
                isAdded = true;
                break;
            }

        }
        if (!isAdded)
        {
            tileData.position = neighbourTile.transform.position;
            tileData.tile = neighbourTile;
            currentlyCheckedTile.tile.neighbourList.Add(tileData);
        }
    }

    void RemoveTilesFarAway()
    {
        Vector3 startPosition = tempTile.transform.position;
        List<TileScript.TileData> listOfTilesToRemove = new List<TileScript.TileData>();
        foreach (TileScript.TileData data in listOfTilesToCheck)
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
        
        foreach (TileScript.TileData data in listOfTilesToRemove)
        { 
            listOfTilesToCheck.Remove(data);
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
