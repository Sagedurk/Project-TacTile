using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingDebug : Singleton<PathfindingDebug>
{
    [SerializeField] Patterns pattern;
    [SerializeField] PathfindingTile startingTile;
    [SerializeField] int amountOfSteps;
    [Space(10)]
    [SerializeField] bool enable;


    PathfindingMaster.PatternArguments patternArguments = new PathfindingMaster.PatternArguments();
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
                SetArguments(amountOfSteps, startingTile, PathfindingTile.TileStates.CURRENT, PathfindingTile.TileStates.SELECTABLE_SKILL);
                PathfindingMaster.Instance.patterns.Radial(patternArguments);
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


    void SetArguments(int amountOfSteps, PathfindingTile startTile, PathfindingTile.TileStates stateOfStartTile, PathfindingTile.TileStates stateOfOtherTiles)
    {
        patternArguments.stepAmount = amountOfSteps;
        patternArguments.startingTile = startTile;
        patternArguments.startingTileState = stateOfStartTile;
        patternArguments.otherTileState = stateOfOtherTiles;

    }

}
