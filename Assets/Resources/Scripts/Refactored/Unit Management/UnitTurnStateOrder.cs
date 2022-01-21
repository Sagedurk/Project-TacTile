using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurnStateOrder
{
    //MIGHT MOVE THIS INTO [TurnOrder]

    public UnitMaster master;
    public TurnStates turnState = TurnStates.START_TURN;
    public ActionStates actionState = ActionStates.ATTACK;
    

   public enum TurnStates
    {
        START_TURN,
        PRE_WALKING,
        WALKING,
        CHOOSE_ACTION,
        CHOOSE_TARGET,
        END_TURN
    }

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
                //Interpolate Camera
                InputCombat.Instance.combatCamera.MoveCamera(TurnStateDirections.ADVANCE);
                


                //turnState = TurnStates.PRE_WALKING;
                break;
            case TurnStates.PRE_WALKING:

                master.unitPathfinding.FindTiles(master.unitStats.movementRange, UnitPathfinding.Patterns.RADIAL, PathfindingTile.TileStates.SELECTABLE_WALK);//, false, PathfindingTile.TileStates.SELECTABLE_WALK);
                PathfindingMaster.Instance.ChooseTarget(PathfindingTile.TileStates.SELECTABLE_WALK, true);

                //turnState = TurnStates.WALKING;
                break;
            case TurnStates.WALKING:
                
                turnState = TurnStates.CHOOSE_ACTION;
                break;
            case TurnStates.CHOOSE_ACTION:
                
                turnState = TurnStates.CHOOSE_TARGET;
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
            case TurnStates.PRE_WALKING:

                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.WALKING:


                SetTurnState(TurnStateDirections.RECEDE);
                break;
            case TurnStates.CHOOSE_ACTION:


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


    public void SetTurnState(TurnStateDirections direction)
    {
        if (TurnOrder.Instance.isTurnStateLocked || direction == TurnStateDirections.NULL)
            return;

        if(direction == TurnStateDirections.ADVANCE)
        {
            switch (turnState)
            {
                case TurnStates.START_TURN:

                    turnState = TurnStates.PRE_WALKING;
                    break;
                case TurnStates.PRE_WALKING:

                    turnState = TurnStates.WALKING;
                    break;
                case TurnStates.WALKING:

                    turnState = TurnStates.CHOOSE_ACTION;
                    break;
                case TurnStates.CHOOSE_ACTION:

                    turnState = TurnStates.CHOOSE_TARGET;
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
                case TurnStates.PRE_WALKING:

                    turnState = TurnStates.START_TURN;
                    break;
                case TurnStates.WALKING:

                    turnState = TurnStates.PRE_WALKING;
                    break;
                case TurnStates.CHOOSE_ACTION:

                    turnState = TurnStates.WALKING;
                    break;
                case TurnStates.CHOOSE_TARGET:

                    turnState = TurnStates.CHOOSE_ACTION;
                    break;
                case TurnStates.END_TURN:

                    turnState = TurnStates.CHOOSE_TARGET;
                    break;

                default:
                    break;
            }
        }




    }







}
