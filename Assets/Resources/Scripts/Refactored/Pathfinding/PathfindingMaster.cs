using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMaster : MonoBehaviour
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

    [Space(30)]
    [SerializeField] bool drawGizmos = false;
    [SerializeField] int amountOfGizmos = 0;

    //private variables
    int amountOfTilesToCheck;
    List<TileScript> listOfTilesToCheck = new List<TileScript>();
    List<Vector3> listOfNullTilesPosition = new List<Vector3>();
    int indexOfNullList = 0;
    TileScript currentlyCheckedTile;
    bool isCoroutineDone = true;
    bool isTileBlocked = false;
    Vector3 positionOfRaycast;


    //Script Debugging
    List<GizmoInformation> gizmoInformationList = new List<GizmoInformation>();

    [System.Serializable]
    class GizmoInformation
    {
        public Vector3 position;
        public Vector3 scale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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




    private void Update()
    {
        ResetTiles();

        if (globalPathfinding)
            Pathfinding_BFS_Global();

        else if (showPattern4Dir)
            Pathfinding_4_Directions();

        else if (showPattern8Dir)
            Pathfinding_8_Directions();

        else
        {
            Pathfinding_BFS();
            if (showOutermostTiles)
            {
                Pathfinding_BFS_Remove_Frontier();
                Pathfinding_BFS_Outermost();
            }
        }

        //if(isCoroutineDone)
        //    StartCoroutine(Pathfinding_BFS_IEnum());
    }


    void Pathfinding_BFS()
    {
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;
        

        listOfTilesToCheck.Add(tempTile);

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {

            if (listOfTilesToCheck[i] != null)
            {
                currentlyCheckedTile = listOfTilesToCheck[i];

                //If tile is obstructed
                if (CheckIfTileIsObstructed(currentlyCheckedTile))
                {
                    isTileBlocked = true;
                    currentlyCheckedTile.ChangeTileState(TileScript.TileStates.DEFAULT);
                    positionOfRaycast = currentlyCheckedTile.transform.position;

                }
                else
                {
                    isTileBlocked = false;
                    currentlyCheckedTile.ChangeTileState(TileScript.TileStates.SELECTABLE_SKILL);
                    positionOfRaycast = currentlyCheckedTile.transform.position;
                }
            }
            else    //if null
            {
                if(listOfNullTilesPosition.Count == indexOfNullList)
                {

                }
                else
                {
                    positionOfRaycast = listOfNullTilesPosition[indexOfNullList];
                    indexOfNullList++;
                }

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
            
            if (currentlyCheckedTile == null)
                continue;

            currentlyCheckedTile.ChangeTileState(TileScript.TileStates.TARGET);
        }

        if (showOnlyOutermostTiles)
        {
            for (int i = 1; i < listOfTilesToCheck.Count - amountOfSteps * 4; i++)
            {
                if(listOfTilesToCheck[i] != null)
                    listOfTilesToCheck[i].Reset();
            }
        }
    }

    void Pathfinding_BFS_Remove_Frontier()
    {
        int lastIndexOfList = listOfTilesToCheck.Count - 1;

        for (int i = 0; i < (amountOfSteps + 1) * 4; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[lastIndexOfList - i];

            if (currentlyCheckedTile != null)
                currentlyCheckedTile.Reset();

            listOfTilesToCheck.RemoveAt(lastIndexOfList - i);
        }
    }


    void Pathfinding_BFS_Global()
    {
        ResetTiles();
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;

        listOfTilesToCheck.Add(tempTile);
        for (int i = 0; i < listOfTilesToCheck.Count; i++)
        {
            currentlyCheckedTile = listOfTilesToCheck[i];

            currentlyCheckedTile.ChangeTileState(TileScript.TileStates.TARGET);
            positionOfRaycast = currentlyCheckedTile.transform.position;
            
            //check each direction
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        CastRayGlobal(positionOfRaycast, Vector3.forward);
                        break;
                    case 1:
                        CastRayGlobal(positionOfRaycast, Vector3.right);
                        break;
                    case 2:
                        CastRayGlobal(positionOfRaycast, Vector3.back);
                        break;
                    case 3:
                        CastRayGlobal(positionOfRaycast, Vector3.left);
                        break;
                }
            }
        }
    }

    void Pathfinding_4_Directions()
    {
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);

        CastRayAll(tempTile.transform.position, Vector3.forward, amountOfSteps);
        CastRayAll(tempTile.transform.position, Vector3.right, amountOfSteps);
        CastRayAll(tempTile.transform.position, Vector3.back, amountOfSteps);
        CastRayAll(tempTile.transform.position, Vector3.left, amountOfSteps);

        foreach (TileScript tile in listOfTilesToCheck)
        {
            tile.ChangeTileState(TileScript.TileStates.TARGET);
        }

    }
    
    void Pathfinding_8_Directions()
    {
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;

        listOfTilesToCheck.Add(tempTile);

        if (amountOfSteps == 0)
            return;

        CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.right, Vector3.forward);
        CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.back, Vector3.right);
        CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.left, Vector3.back);
        CastRayTwice(listOfTilesToCheck[0].transform.position, Vector3.forward, Vector3.left);

        for (int i = 0; i < amountOfSteps - 1; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        CastRayTwice(listOfTilesToCheck[i * 4 + 1].transform.position, Vector3.right, Vector3.forward);
                        break;
                    case 1:
                        CastRayTwice(listOfTilesToCheck[i * 4 + 2].transform.position, Vector3.back, Vector3.right);
                        break;
                    case 2:
                        CastRayTwice(listOfTilesToCheck[i * 4 + 3].transform.position, Vector3.left, Vector3.back);
                        break;
                    case 3:
                        CastRayTwice(listOfTilesToCheck[i * 4 + 4].transform.position, Vector3.forward, Vector3.left);
                        break;
                }
            }
        }

        CastRayAll(tempTile.transform.position, Vector3.forward, amountOfSteps);
        CastRayAll(tempTile.transform.position, Vector3.right, amountOfSteps);
        CastRayAll(tempTile.transform.position, Vector3.back, amountOfSteps);
        CastRayAll(tempTile.transform.position, Vector3.left, amountOfSteps);

        for (int i = 0; i < listOfTilesToCheck.Count; i++)
        {
            if(listOfTilesToCheck[i] != null)
                listOfTilesToCheck[i].ChangeTileState(TileScript.TileStates.TARGET);
        }



        //foreach (TileScript tile in listOfTilesToCheck)
        //{
        //    tile.ChangeTileState(TileScript.TileStates.TARGET);
        //}

    }



    // ---------- Helper Functions ---------- //

    void ResetTiles()
    {
        for (int i = 0; i < listOfTilesToCheck.Count; i++)
        {
            if(listOfTilesToCheck[i] != null)
                listOfTilesToCheck[i].Reset();
        }
        listOfTilesToCheck.Clear();
        listOfNullTilesPosition.Clear();
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
                    listOfTilesToCheck.Add(null);
                    //amountOfTilesToCheck++;
                }
                else    //If tile is not obstructed
                {
                    if (tile.visited)
                        return;

                    tile.visited = true;
                    listOfTilesToCheck.Add(tile);
                }

                
            }
        }
        else
        {
            for (int i = 0; i < listOfNullTilesPosition.Count; i++)
            {
                if (listOfNullTilesPosition[i] == rayPosition + rayDirection)
                    return;
            }

            listOfTilesToCheck.Add(null);
            listOfNullTilesPosition.Add(rayPosition + rayDirection);
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
                    listOfTilesToCheck.Add(tile);
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
                listOfTilesToCheck.Add(tile);
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
                listOfTilesToCheck.Add(tile);
            }
        }

    }

    bool CheckIfTileIsObstructed(TileScript tile)
    {
        if(Physics.Raycast(tile.transform.position, Vector3.up, 1))
            return true;
        else
            return false;
    }










    void FindNeighbouringTiles(TileScript originTile)
    {
        Vector3 boxOverlapScale = originTile.transform.lossyScale / 4;
        Vector3 boxOverlapPosition = originTile.transform.position;


        //check all 4 directions
        for (int j = 0; j < 4; j++)
        {
            switch (j)
            {
                case 0:
                    FindTile(boxOverlapPosition, Vector3.forward, boxOverlapScale);
                    break;
                case 1:
                    FindTile(boxOverlapPosition, Vector3.right, boxOverlapScale);
                    break;
                case 2:
                    FindTile(boxOverlapPosition, Vector3.back, boxOverlapScale);
                    break;
                case 3:
                    FindTile(boxOverlapPosition, Vector3.left, boxOverlapScale);
                    break;
            }
        }
    }

    void FindTile(Vector3 overlapBoxPosition, Vector3 direction, Vector3 overlapBoxScale)
    {
        Collider[] tileColliders = Physics.OverlapBox(overlapBoxPosition + direction, overlapBoxScale, Quaternion.identity);
        GizmoInformation gizmo = new GizmoInformation();
        gizmo.position = overlapBoxPosition + direction;
        gizmo.scale = overlapBoxScale * 2;

        gizmoInformationList.Add(gizmo);

        if (tileColliders[0].TryGetComponent(out TileScript tile))
        {
            if (isTileBlocked)
            {
                if (tile.visited)
                    return;
                //IMPORTANT! SOMETHING IS FUCKED
                listOfTilesToCheck.Add(null);
                //amountOfTilesToCheck++;
            }
            else    //If tile is not obstructed
            {
                if (tile.visited)
                    return;

                tile.visited = true;
                listOfTilesToCheck.Add(tile);
            }


        }
        else
        {
            for (int i = 0; i < listOfNullTilesPosition.Count; i++)
            {
                if (listOfNullTilesPosition[i] == overlapBoxPosition + direction)
                    return;
            }

            listOfTilesToCheck.Add(null);
            listOfNullTilesPosition.Add(overlapBoxPosition + direction);
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


}
