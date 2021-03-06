using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCombat : Singleton<InputCombat>
{
    public bool isToggleFreeCam;



    public InputCombatCursor combatCursor;
    public InputCombatCamera combatCamera;

    Vector2 invertedRotationVector = Vector2.zero;

    private void Awake()
    {
        CheckInstance(this, true);
    }

    public void TryMoveCursor()
    {
        if (InputMaster.Instance.movementInputVector == Vector2.zero || isToggleFreeCam)
            return;

        combatCursor.Move(InputMaster.Instance.movementInputVector);
        combatCamera.MoveCamera(UnitTurnStateOrder.TurnStateDirections.NULL);
    }
    
    public void TryMoveCombatFreeCam()
    {
        if ((InputMaster.Instance.movementInputVector == Vector2.zero && (combatCamera.zoomIn + combatCamera.zoomOut) == 0.0f) || !isToggleFreeCam)
            return;

        combatCamera.MoveFreeCamera(InputMaster.Instance.movementInputVector);
    }

    public void TryRotateCamera()
    {
        if (InputMaster.Instance.rotationInputVector == Vector2.zero)
            return;

        invertedRotationVector.x = InputMaster.Instance.rotationInputVector.y;
        invertedRotationVector.y = InputMaster.Instance.rotationInputVector.x;

        combatCamera.Rotate(InputMaster.Instance.rotationInputVector);


    }

    public void TryZoomCamera()
    {
        if (combatCamera.zoomIn + combatCamera.zoomOut == 0.0f || isToggleFreeCam)
            return;

        combatCamera.Zoom();
    }


}
