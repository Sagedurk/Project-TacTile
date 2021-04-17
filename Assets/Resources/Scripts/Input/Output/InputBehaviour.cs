using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBehaviour : InputCombat
{
    //Input Behaviour Hub
    //Link Combat input with non-Combat input here, i.e.
    //Create helper functions for managing action maps, UI icons, etc?

    Vector3 cursorDirection;

    public void CursorMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("started");
        }
        else if (ctx.performed)
        {
            Vector2 joystickVector = ctx.ReadValue<Vector2>();
            cursorDirection = new Vector3(joystickVector.x, 0, joystickVector.y);
            
        }
        else if (ctx.canceled)
        {
            cursorDirection = Vector3.zero;
        }
    }

    private void Update()
    {
        rotateCursor(cursorDirection);
    }
}
