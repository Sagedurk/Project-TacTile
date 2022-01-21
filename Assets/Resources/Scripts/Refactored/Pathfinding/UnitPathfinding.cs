using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathfinding
{
    public UnitMaster master;
    PathfindingTile startingTile;

    public enum Patterns
    {
        RADIAL,
        RADIAL_OUTERMOST,
        RADIAL_OUTERMOST_DEBUG,
        GLOBAL,
        FOUR_DIRECTIONS,
        EIGHT_DIRECTIONS,

    }


    public void FindTiles(int stepAmount, Patterns pattern, PathfindingTile.TileStates tileState, bool isBlocking = false, PathfindingTile.TileStates startingTileState = PathfindingTile.TileStates.CURRENT)
    {
        PathfindingMaster.Instance.ResetTiles();
        FindStartTile();

        switch (pattern)
        {
            case Patterns.RADIAL:
                PathfindingMaster.Instance.patterns.Radial(stepAmount, startingTile, startingTileState, tileState, isBlocking);
                break;
            case Patterns.RADIAL_OUTERMOST:
                //Pathfinding_BFS_Outermost();
                break;
            case Patterns.RADIAL_OUTERMOST_DEBUG:
                //Pathfinding_BFS_Outermost();
                break;
            case Patterns.GLOBAL:
                //Pathfinding_BFS_Global();
                break;
            case Patterns.FOUR_DIRECTIONS:
                //Pathfinding_4_Directions();
                break;
            case Patterns.EIGHT_DIRECTIONS:
                //Pathfinding_8_Directions();
                break;
            default:
                break;
        }

    }


    void FindStartTile()
    {
        if (Physics.Raycast(master.gameObject.transform.position, Vector3.down,out RaycastHit hit, 1.0f))
        {
            if (hit.transform.TryGetComponent(out PathfindingTile tile))
            {
                startingTile = tile;
            }
        }
    }



}
