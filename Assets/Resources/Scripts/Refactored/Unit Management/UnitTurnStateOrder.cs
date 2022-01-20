using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurnStateOrder
{
    //MIGHT MOVE THIS INTO [TurnOrder]

    public GameObject unitObject;
    public TurnStates turnState = TurnStates.START_TURN;
    public ActionStates actionState = ActionStates.ATTACK;
    public bool isTurnStateLocked = false;

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


    public void AdvanceTurnState()
    {
        if (isTurnStateLocked)
            return;

        switch (turnState)
        {
            case TurnStates.START_TURN:
                //Set cursor position to unit's
                InputCombat.Instance.combatCursor.transform.position = unitObject.transform.position;
                
                //set cursor Y position
                InputCombat.Instance.combatCursor.transform.position += Vector3.up * (InputCombat.Instance.combatCursor.yAxisPosition - unitObject.transform.position.y);
                InputCombat.Instance.combatCamera.MoveCamera();
                



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
    
    public void RecedeTurnState()
    {
        if (isTurnStateLocked)
            return;

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
