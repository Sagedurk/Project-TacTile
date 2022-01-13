using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingDebug : Singleton<PathfindingDebug>
{
    [SerializeField] Patterns pattern;
    [SerializeField] TileScript startingTile;
    [SerializeField] int amountOfSteps;
    [Space(10)]
    [SerializeField] bool enable;

    enum Patterns
    {
        RADIAL,
        RADIAL_OUTERMOST,
        RADIAL_OUTERMOST_DEBUG,
        GLOBAL,
        FOUR_DIRECTIONS,
        EIGHT_DIRECTIONS,

    }

    private void Awake()
    {
        CheckInstance(this);
    }

    private void Update()
    {
        if (!enable)
            return;

        PathfindingMaster.Instance.ResetTiles();

        switch (pattern)
        {
            case Patterns.RADIAL:
                PathfindingMaster.Instance.patterns.Radial(startingTile, TileScript.TileStates.CURRENT, TileScript.TileStates.SELECTABLE_SKILL, amountOfSteps);
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

}
