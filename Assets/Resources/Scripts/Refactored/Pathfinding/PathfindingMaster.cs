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
    [SerializeField]int amountOfTilesToCheck;
    List<TileScript.TileData> listOfTilesToCheck = new List<TileScript.TileData>();
    //List<Vector3> listOfNullTilesPosition = new List<Vector3>();
    //List<Vector3> listOfNullNeighboursPosition = new List<Vector3>();
    int indexOfNullList = 0;
    TileScript.TileData currentlyCheckedTile;
    bool isCoroutineDone = true;
    bool isTileBlocked = false;
    //Vector3 positionOfRaycast;
    TileScript.TileData tileData;

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

    private void Awake()
    {
        CheckInstance(this, true);
    }


    private void Start()
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
            Pathfinding_BFS();

            if (showOutermostTiles)
            {
                Pathfinding_BFS_Remove_Frontier();
                Pathfinding_BFS_Outermost();
            }
            Debug.Log(amountOfTilesToCheck);
        }

        //if(isCoroutineDone)
        //    StartCoroutine(Pathfinding_BFS_IEnum());
    }


    void Pathfinding_BFS()
    {
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;

        TileScript.TileData data;
        data.tile = tempTile;
        data.position = tempTile.transform.position;

        listOfTilesToCheck.Add(data);

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[i];

            if (listOfTilesToCheck[i].tile != null)
            {
                //If tile is obstructed
                if (CheckIfTileIsObstructed(currentlyCheckedTile))
                {
                    isTileBlocked = true;
                    currentlyCheckedTile.tile.ChangeTileState(TileScript.TileStates.DEFAULT);
                    currentlyCheckedTile.position = currentlyCheckedTile.tile.transform.position;

                }
                else
                {
                    isTileBlocked = false;
                    currentlyCheckedTile.tile.ChangeTileState(TileScript.TileStates.SELECTABLE_SKILL);
                    currentlyCheckedTile.position = currentlyCheckedTile.tile.transform.position;
                }
            }
            else    //if null
            {
                //currentlyCheckedTile.tile = null;
                //if (listOfNullTilesPosition.Count == indexOfNullList)
                //{

                //}
                //else
                //{

                //}

            }

            FindNeighbouringTiles(currentlyCheckedTile);
        }
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
        int lastIndexOfList = listOfTilesToCheck.Count - 1;

        for (int i = 0; i < (amountOfSteps + 1) * 4; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[lastIndexOfList - i];

            if (currentlyCheckedTile.tile != null)
                currentlyCheckedTile.tile.Reset();

            listOfTilesToCheck.RemoveAt(lastIndexOfList - i);
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
        //tempTile.ChangeTileState(TileScript.TileStates.CURRENT);

        //CastRayAll(tempTile.transform.position, Vector3.forward, amountOfSteps);
        //CastRayAll(tempTile.transform.position, Vector3.right, amountOfSteps);
        //CastRayAll(tempTile.transform.position, Vector3.back, amountOfSteps);
        //CastRayAll(tempTile.transform.position, Vector3.left, amountOfSteps);

        //foreach (TileScript tile in listOfTilesToCheck)
        //{
        //    tile.ChangeTileState(TileScript.TileStates.TARGET);
        //}

    }
    
    void Pathfinding_8_Directions()
    {
        //tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        //tempTile.visited = true;

        //listOfTilesToCheck.Add(tempTile);

        //if (amountOfSteps == 0)
        //    return;

        //CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.right, Vector3.forward);
        //CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.back, Vector3.right);
        //CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.left, Vector3.back);
        //CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.forward, Vector3.left);

        //for (int i = 0; i < amountOfSteps - 1; i++)
        //{
        //    for (int j = 0; j < 4; j++)
        //    {
        //        switch (j)
        //        {
        //            case 0:
        //                CastRayTwice(listOfTilesToCheck[i * 4 + 1].transform.position, Vector3.right, Vector3.forward);
        //                break;
        //            case 1:
        //                CastRayTwice(listOfTilesToCheck[i * 4 + 2].transform.position, Vector3.back, Vector3.right);
        //                break;
        //            case 2:
        //                CastRayTwice(listOfTilesToCheck[i * 4 + 3].transform.position, Vector3.left, Vector3.back);
        //                break;
        //            case 3:
        //                CastRayTwice(listOfTilesToCheck[i * 4 + 4].transform.position, Vector3.forward, Vector3.left);
        //                break;
        //        }
        //    }
        //}

        //CastRayAll(tempTile.transform.position, Vector3.forward, amountOfSteps);
        //CastRayAll(tempTile.transform.position, Vector3.right, amountOfSteps);
        //CastRayAll(tempTile.transform.position, Vector3.back, amountOfSteps);
        //CastRayAll(tempTile.transform.position, Vector3.left, amountOfSteps);

        //for (int i = 0; i < listOfTilesToCheck.Count; i++)
        //{
        //    if(listOfTilesToCheck[i] != null)
        //        listOfTilesToCheck[i].ChangeTileState(TileScript.TileStates.TARGET);
        //}



        ////foreach (TileScript tile in listOfTilesToCheck)
        ////{
        ////    tile.ChangeTileState(TileScript.TileStates.TARGET);
        ////}

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
        //listOfNullTilesPosition.Clear();
        //listOfNullNeighboursPosition.Clear();
        gizmoInformationList.Clear();
        indexOfNullList = 0;
    }
    

    void CastRay(Vector3 rayPosition, Vector3 rayDirection, bool isBlockedCurrentTile = false)
    {
        if (Physics.Raycast(rayPosition, rayDirection, out RaycastHit hit, 1))
        {

            if (hit.collider.TryGetComponent(out TileScript tile))
            {
                if (isBlockedCurrentTile)
                {
                    if (tile.visited)
                        return;
                    //IMPORTANT! SOMETHING IS FUCKED
                    //Debug.Log("tileIsBlocked: " + tile.transform.position);
                    //listOfTilesToCheck.Add(null);
                    //amountOfTilesToCheck++;
                }
                else    //If tile is not obstructed
                {
                    if (tile.visited)
                        return;

                    tile.visited = true;
                    //listOfTilesToCheck.Add(tile);
                }

                
            }
        }
        else
        {
            //for (int i = 0; i < listOfNullTilesPosition.Count; i++)
            //{
            //    if (listOfNullTilesPosition[i] == rayPosition + rayDirection)
            //        return;
            //}

            //listOfTilesToCheck.Add(null);
            //listOfNullTilesPosition.Add(rayPosition + rayDirection);
        }
    }

    void CastRayTwice(Vector3 rayPosition, Vector3 rayDirection, Vector3 secondRayDirection)
    {
        if (Physics.Raycast(rayPosition, rayDirection, out RaycastHit hit, 1))
        {
            if (Physics.Raycast(hit.transform.position, secondRayDirection, out RaycastHit hit_second, 1))
            {
                if (hit_second.collider.TryGetComponent(out TileScript tile))
                {
                    if (tile.visited)
                        return;

                    tile.visited = true;
                    //listOfTilesToCheck.Add(tile);
                }
            }
        }
        else
        {
            //for (int i = 0; i < listOfNullTilesPosition.Count; i++)
            //{
            //    if (listOfNullTilesPosition[i] == rayPosition + rayDirection)
            //        return;
            //}

            //listOfTilesToCheck.Add(null);
            //listOfNullTilesPosition.Add(rayPosition + rayDirection);
        }
    }

    void CastRayGlobal(Vector3 rayPosition, Vector3 rayDirection)
    {
        if (Physics.Raycast(rayPosition, rayDirection, out RaycastHit hit, 1))
        {
            if (hit.collider.TryGetComponent(out TileScript tile))
            {
                if (tile.visited)
                    return;

                tile.visited = true;
                //listOfTilesToCheck.Add(tile);
            }
        }
    }

    void CastRayAll(Vector3 rayPosition, Vector3 rayDirection, float rayDistance)
    {
        RaycastHit[] hits = Physics.RaycastAll(rayPosition, rayDirection, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out TileScript tile))
            {
                //listOfTilesToCheck.Add(tile);
            }
        }

    }

    bool CheckIfTileIsObstructed(TileScript.TileData tile)
    {
        if (Physics.Raycast(tile.position, Vector3.up, out RaycastHit hit, 1))
        { 
            Debug.Log(hit.collider.gameObject);
            return true;
        }
        else
            return false;
    }










    void FindNeighbouringTiles(TileScript.TileData tileData)
    {
        //Check if Neighbours have been found previously or not. If they have, just use the list instead.

        //TODO: NEIGHBOUR LIST FUCKS UP IF TILE IS NULL

        //If list isn't empty
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

        //check all 4 directions
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
        GizmoInformation gizmo = new GizmoInformation();
        gizmo.position = nextTilePosition;
        gizmo.scale = overlapBoxScale * 2;

        tileData.position = nextTilePosition;
        tileData.tile = null;

        gizmoInformationList.Add(gizmo);


        //If no tile is found
        if (tileColliders.Length == 0)
        {
            for (int i = 0; i < listOfTilesToCheck.Count; i++)
            {
                if (listOfTilesToCheck[i].position == tileData.position)
                    return;
            }

            listOfTilesToCheck.Add(tileData);
            //gizmoInformationList.Add(gizmo);

            if(currentlyCheckedTile.tile != null)
            {
                currentlyCheckedTile.tile.neighbourList.Add(tileData);

            }

            //listOfNullTilesPosition.Add(tileData.position);
            return;
        }

        if (tileColliders[0].TryGetComponent(out TileScript tile))
        {
            tileData.tile = tile;

            if (tile.isBlocked)
            {
                if (tile.visited)
                {
                    //amountOfTilesToCheck++;
                    return;
                }
                amountOfTilesToCheck --;

            }
            else    //If tile is not obstructed
            {
                if (tile.visited)
                {
                    CheckIfNeighbourHasBeenAdded(tile);
                    return;
                }

                tile.visited = true;
                listOfTilesToCheck.Add(tileData);
                //gizmoInformationList.Add(gizmo);
            }

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
            //What happens if the originTile is null?
            //i.e. the current position is not a tile            

            return false;
        }

        if (tileData.tile.neighbourList.Count > 0)
        {
        
            for (int i = 0; i < tileData.tile.neighbourList.Count; i++)
            {

                if(tileData.tile.neighbourList[i].tile == null)
                {
                    GizmoInformation gizmo = new GizmoInformation();
                    gizmo.position = tileData.position;
                    gizmo.scale = Vector3.one * 0.5f;

                    switch (i)
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


                if (isTileBlocked)
                {
                    if (tileData.tile.neighbourList[i].tile.visited)
                        continue;
                    //IMPORTANT! SOMETHING IS FUCKED
                    TileScript.TileData data;
                    data.tile = null;
                    data.position = tileData.position - (Vector3.up * 2);

                    listOfTilesToCheck.Add(data);
                    //amountOfTilesToCheck++;
                }
                else    //If tile is not obstructed
                {
                    if (tileData.tile.neighbourList[i].tile != null)
                    {
                        if (tileData.tile.neighbourList[i].tile.visited)
                            continue;

                        tileData.tile.neighbourList[i].tile.visited = true;
                    }
                    listOfTilesToCheck.Add(tileData.tile.neighbourList[i]);
                }

            }
            return true;
        }
        //int a = 1;

        return false;
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

    IEnumerator DebugTileSequence(float timeToWaitBetweenIterations)
    {
        isCoroutineDone = false;

        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;

        TileScript.TileData data;
        data.tile = tempTile;
        data.position = tempTile.transform.position;

        listOfTilesToCheck.Add(data);

      

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {
            
            //yield return new WaitUntil(() => UnityEngine.InputSystem.Keyboard.current.spaceKey.wasReleasedThisFrame);
            yield return new WaitForSeconds(timeToWaitBetweenIterations);
            
            
            Debug.Log(amountOfTilesToCheck);

            currentlyCheckedTile = listOfTilesToCheck[i];

            if (listOfTilesToCheck[i].tile != null)
            {
                //If tile is obstructed
                if (CheckIfTileIsObstructed(currentlyCheckedTile))
                {
                    isTileBlocked = true;
                    currentlyCheckedTile.tile.ChangeTileState(TileScript.TileStates.DEFAULT);
                    currentlyCheckedTile.position = currentlyCheckedTile.tile.transform.position;

                }
                else
                {
                    isTileBlocked = false;
                    currentlyCheckedTile.tile.ChangeTileState(TileScript.TileStates.SELECTABLE_SKILL);
                    currentlyCheckedTile.position = currentlyCheckedTile.tile.transform.position;
                }
            }
            else    //if null
            {
                //currentlyCheckedTile.tile = null;
                //if (listOfNullTilesPosition.Count == indexOfNullList)
                //{

                //}
                //else
                //{
                    
                //}

            }

            FindNeighbouringTiles(currentlyCheckedTile);

            

        }
        Debug.Log(amountOfTilesToCheck);
        Debug.Log("FINISHED");

        yield return new WaitUntil(() => UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame);

        ResetTiles();
        isCoroutineDone = true;
    }


}
