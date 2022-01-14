using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputLinking : Singleton<InputLinking>
{
    /*
        Naming Scheme
            Type of input - i.e. Combat, or UI
            Linking
            Type of action - i.e. Move, Accept, or Pause

        Function Order  (Default)
            1. Joystick inputs
            2. D-pad inputs (All D-pad inputs)
            3. Trigger inputs
            4. Button inputs (Shoulder Buttons -> Special Buttons (Start/Select, etc.) -> Other Buttons)
    */





    private void Awake()
    {
        CheckInstance(this);
    }

    //---------- Combat Linking ----------//
    public void CombatLinkingMoveCursor(InputAction.CallbackContext ctx)
    {
        if (InputCombat.Instance.isToggleFreeCam)
            return;



        if (ctx.started)
        {
            InputMaster.Instance.movementInputVector = ctx.ReadValue<Vector2>();
        }
        else if (ctx.performed)
        {
            InputMaster.Instance.movementInputVector = ctx.ReadValue<Vector2>();
        }
        else if (ctx.canceled)
        {
            InputMaster.Instance.movementInputVector = Vector2.zero;
        }
    }

    public void CombatLinkingMoveFreeCam(InputAction.CallbackContext ctx)
    {
        if (!InputCombat.Instance.isToggleFreeCam)
            return;

        if (ctx.started)
        {
            InputMaster.Instance.movementInputVector = ctx.ReadValue<Vector2>();
        }
        else if (ctx.performed)
        {
            InputMaster.Instance.movementInputVector = ctx.ReadValue<Vector2>();
        }
        else if (ctx.canceled)
        {
            InputMaster.Instance.movementInputVector = Vector2.zero;
        }
    }

    public void CombatLinkingRotateCamera(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            InputMaster.Instance.rotationInputVector = ctx.ReadValue<Vector2>();
        }
        else if (ctx.performed)
        {
            InputMaster.Instance.rotationInputVector = ctx.ReadValue<Vector2>();
        }
        else if (ctx.canceled)
        {
            InputMaster.Instance.rotationInputVector = Vector2.zero;
        }
    }

    public void CombatLinkingToggleFreeCamera(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            InputCombat.Instance.isToggleFreeCam = !InputCombat.Instance.isToggleFreeCam;

            if(InputCombat.Instance.isToggleFreeCam)
                InputCombat.Instance.combatCamera.cameraState = InputCombatCamera.CameraState.FREE_CAMERA;
            else
            {
                InputCombat.Instance.combatCamera.cameraState = InputCombatCamera.CameraState.CURSOR_CAMERA;
                InputCombat.Instance.combatCamera.cameraMain.transform.localEulerAngles = InputCombat.Instance.combatCamera.defaultCameraRotation;
            }

        }
        else if (ctx.canceled)
        {

        }
    }

    public void CombatLinkingZoomIn(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Zoom In Started");

        }
        else if (ctx.performed)
        {
            Debug.Log("Zoom In Performed");

        }
        else if (ctx.canceled)
        {
            Debug.Log("Zoom In Canceled");

        }
    }
    
    public void CombatLinkingZoomOut(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Zoom Out Started");

        }
        else if (ctx.performed)
        {
            Debug.Log("Zoom Out Performed");

        }
        else if (ctx.canceled)
        {
            Debug.Log("Zoom Out Canceled");

        }
    }

    public void CombatLinkingCheckUnitInfo(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Check Unit Info Started");

        }
        else if (ctx.canceled)
        {
            Debug.Log("Check Unit Info Canceled");

        }
    }

    public void CombatLinkingPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Pause Started");

        }
        else if (ctx.canceled)
        {
            Debug.Log("Pause Canceled");

        }
    }
    
    public void CombatLinkingAccept(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            TurnOrder.Instance.activeUnit.turnStateOrder.AdvanceTurnState();
            Debug.Log(TurnOrder.Instance.activeUnit.turnStateOrder.turnState);
        }
        else if (ctx.canceled)
        {

        }
    }
    
    public void CombatLinkingCancel(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            TurnOrder.Instance.activeUnit.turnStateOrder.RecedeTurnState();
            Debug.Log(TurnOrder.Instance.activeUnit.turnStateOrder.turnState);
        }
        else if (ctx.canceled)
        {

        }
    }


    //---------- UI Linking ----------//


}
