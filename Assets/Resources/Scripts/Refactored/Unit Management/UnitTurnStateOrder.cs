using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitTurnStateOrder
{
    //MIGHT MOVE THIS INTO [TurnOrder]

    [HideInInspector]public UnitMaster master;
    public TurnStates turnState = TurnStates.START_TURN;
    public ActionStates actionState = ActionStates.ATTACK;
    

   public enum TurnStates
    {
        START_TURN,
        SHOW_WALKING_TILES,
        CHOOSE_WALKING_TARGET,
        WALKING,
        CHOOSE_ACTION,
        CHOOSE_SKILL,
        CHOOSE_ITEM,
        CHOOSE_TARGET,
        END_TURN
    }

    [System.Serializable]
    public enum ActionStates
    {
        ATTACK,
        SKILLS,
        ITEMS,
        DEFEND
    }

    public enum TurnStateDirections
    {
        ADVANCE,
        RECEDE,
        NULL
    }


    public void AdvanceTurnState()
    {
        switch (turnState)
        {
            case TurnStates.START_TURN:

                //Set cursor position to unit's
                InputCombat.Instance.combatCursor.transform.position = master.gameObject.transform.position;
                
                //set cursor Y position
                InputCombat.Instance.combatCursor.transform.position += Vector3.up * (InputCombat.Instance.combatCursor.yAxisPosition - master.gameObject.transform.position.y);
                
                //Set Camera Interpolation Speed
                InputCombat.Instance.combatCamera.interpolateSpeed = Vector3.Distance(InputCombat.Instance.combatCursor.transform.position, InputCombat.Instance.combatCamera.transform.position);
                
                if (InputCombat.Instance.combatCamera.interpolateSpeed < InputCombat.Instance.combatCamera.GetDefaultInterpolationSpeed())
                    InputCombat.Instance.combatCamera.interpolateSpeed = InputCombat.Instance.combatCamera.GetDefaultInterpolationSpeed();


                TurnOrder.Instance.isTurnStateLocked = false;
                InputCombat.Instance.combatCamera.MoveCamera(TurnStateDirections.ADVANCE);

                SetTurnState(TurnStateDirections.ADVANCE);
                ShowWalkingTiles();
                //Show tiles

                break;

            case TurnStates.SHOW_WALKING_TILES:

                break;

            case TurnStates.CHOOSE_WALKING_TARGET:
                if (PathfindingMaster.Instance.ChooseTarget(PathfindingMaster.TargetOutcomes.NO_OBJECT_ABOVE_TILE, PathfindingTile.TileStates.SELECTABLE_WALK, true))
                { 
                    SetTurnState(TurnStateDirections.ADVANCE);
                    Walking();
                }
                break;

            case TurnStates.WALKING:
                

                break;
            case TurnStates.CHOOSE_ACTION:
                
                break;
            case TurnStates.CHOOSE_SKILL:
                
                break;
            case TurnStates.CHOOSE_ITEM:
                
                break;
            case TurnStates.CHOOSE_TARGET:
                
                turnState = TurnStates.END_TURN;
                break;
            case TurnStates.END_TURN:
                //Ends Turn, turnState has to be set to START_TURN at some point
                break;


            default:
                break;
        }
    }
    
    public void RecedeTurnState()
    {
        

        switch (turnState)
        {
            case TurnStates.START_TURN:
                //Nothing Happens

                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.SHOW_WALKING_TILES:

                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.CHOOSE_WALKING_TARGET:

                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.WALKING:


                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.CHOOSE_ACTION:


                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.CHOOSE_SKILL:


                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.CHOOSE_ITEM:


                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.CHOOSE_TARGET:


                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.END_TURN:


                SetTurnState(TurnStateDirections.RECEDE);
                break;


            default:
                break;
        }
    }


    public bool SetTurnState(TurnStateDirections direction)
    {
        if (TurnOrder.Instance.isTurnStateLocked || direction == TurnStateDirections.NULL)
            return false;

        if(direction == TurnStateDirections.ADVANCE)
        {
            switch (turnState)
            {
                case TurnStates.START_TURN:

                    turnState = TurnStates.SHOW_WALKING_TILES;
                    break;
                case TurnStates.SHOW_WALKING_TILES:

                    turnState = TurnStates.CHOOSE_WALKING_TARGET;
                    break;
                case TurnStates.CHOOSE_WALKING_TARGET:

                    turnState = TurnStates.WALKING;
                    break;
                case TurnStates.WALKING:

                    turnState = TurnStates.CHOOSE_ACTION;
                    break;
                case TurnStates.CHOOSE_ACTION:

                    switch (actionState)
                    {
                        case ActionStates.ATTACK:
                            turnState = TurnStates.CHOOSE_TARGET;
                            break;
                        case ActionStates.SKILLS:
                            turnState = TurnStates.CHOOSE_SKILL;
                            break;
                        case ActionStates.ITEMS:
                            turnState = TurnStates.CHOOSE_ITEM;
                            break;
                        case ActionStates.DEFEND:
                            turnState = TurnStates.CHOOSE_TARGET;
                            break;
                        default:
                            break;
                    }

                    break;
                case TurnStates.CHOOSE_TARGET:

                    turnState = TurnStates.END_TURN;
                    break;
                case TurnStates.END_TURN:
                    //Ends Turn, turnState has to be set to START_TURN at some point
                    break;

                default:
                    break;
            }
        }

        else if (direction == TurnStateDirections.RECEDE)
        {
            switch (turnState)
            {
                case TurnStates.START_TURN:
                    //Nothing Happens
                    break;
                case TurnStates.SHOW_WALKING_TILES:

                    turnState = TurnStates.START_TURN;
                    break;
                case TurnStates.CHOOSE_WALKING_TARGET:

                    turnState = TurnStates.SHOW_WALKING_TILES;
                    break;
                case TurnStates.WALKING:

                    turnState = TurnStates.CHOOSE_WALKING_TARGET;
                    break;
                case TurnStates.CHOOSE_ACTION:

                    turnState = TurnStates.WALKING;
                    break;
                case TurnStates.CHOOSE_SKILL:

                    turnState = TurnStates.CHOOSE_ACTION;
                    break;
                case TurnStates.CHOOSE_ITEM:

                    turnState = TurnStates.CHOOSE_ACTION;
                    break;
                case TurnStates.CHOOSE_TARGET:
                    switch (actionState)
                    {
                        case ActionStates.ATTACK:
                            turnState = TurnStates.CHOOSE_ACTION;
                            break;
                        case ActionStates.SKILLS:
                            turnState = TurnStates.CHOOSE_SKILL;
                            break;
                        case ActionStates.ITEMS:
                            turnState = TurnStates.CHOOSE_ITEM;
                            break;
                        case ActionStates.DEFEND:
                            turnState = TurnStates.CHOOSE_ACTION;
                            break;
                        default:
                            break;
                    }

                    break;
                case TurnStates.END_TURN:

                    turnState = TurnStates.CHOOSE_TARGET;
                    break;

                default:
                    break;
            }
        }


        return true;

    }



    void Walking()
    {
        PathfindingMaster.Instance.MoveObjectToTargetTile(master.gameObject, Vector3.up * master.gameObject.transform.position.y);
        PathfindingMaster.Instance.ResetNodes();
        SetTurnState(TurnStateDirections.ADVANCE);
        UIMaster.Instance.ShowActionButtons();

    }

    public void ShowWalkingTiles()
    {

        master.unitPathfinding.FindTiles(master.unitStats.movementRange, UnitPathfinding.Patterns.RADIAL, PathfindingTile.TileStates.SELECTABLE_WALK);
        SetTurnState(TurnStateDirections.ADVANCE);
    }

}
