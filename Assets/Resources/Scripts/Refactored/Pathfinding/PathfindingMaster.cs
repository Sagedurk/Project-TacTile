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


    //private variables
    int amountOfTilesToCheck;
    List<TileScript> listOfTilesToCheck = new List<TileScript>();
    List<Vector3> listOfNullTilesPosition = new List<Vector3>();
    int indexOfNullList = 0;
    TileScript currentlyCheckedTile;
    bool isCoroutineDone = true;
    Vector3 positionOfRaycast;

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
        ResetTiles();
        amountOfTilesToCheck = 2 * ((int)Mathf.Pow(amountOfSteps, 2) + amountOfSteps) + 1;
        tempTile.ChangeTileState(TileScript.TileStates.CURRENT);
        tempTile.visited = true;

        listOfTilesToCheck.Add(tempTile);

        for (int i = 0; i < amountOfTilesToCheck; i++)
        {

            if (listOfTilesToCheck[i] != null)
            {
                currentlyCheckedTile = listOfTilesToCheck[i];

                currentlyCheckedTile.ChangeTileState(TileScript.TileStates.SELECTABLE_SKILL);
                positionOfRaycast = currentlyCheckedTile.transform.position;
            }
            else    //if null
            {
                positionOfRaycast = listOfNullTilesPosition[indexOfNullList];
                indexOfNullList++;
            }


            //check each direction
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        CastRay(positionOfRaycast, Vector3.forward);
                        break;
                    case 1:
                        CastRay(positionOfRaycast, Vector3.right);
                        break;
                    case 2:
                        CastRay(positionOfRaycast, Vector3.back);
                        break;
                    case 3:
                        CastRay(positionOfRaycast, Vector3.left);
                        break;
                }
            }
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
        ResetTiles();
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
        ResetTiles();
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

        for (int i = 0; i < listOfTilesToCheck.Count; i++)
        {
            listOfTilesToCheck[i].ChangeTileState(TileScript.TileStates.TARGET);
            //listOfTilesToCheck[i].transform.position += Vector3.up * i;
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
    

    void CastRay(Vector3 rayPosition, Vector3 rayDirection)
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

}
