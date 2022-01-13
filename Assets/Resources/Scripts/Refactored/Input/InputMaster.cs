using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputMaster : Singleton<InputMaster>
{


    Vector2 movementVector;
    Vector2 rotationVector;

    float oneAxisMovementDeadzone = 0.71f;

    public InputCombat combatInput;
    // Start is called before the first frame update
    void Awake()
    {
        CheckInstance(this, true);
    }

    // Update is called once per frame
    void Update()
    {
        TryMoveCombatCursor();
        
    }

    public void ReadMovementInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
            movementVector = Vector2.zero;
        else 
            movementVector = context.ReadValue<Vector2>();
    }

    void TryMoveCombatCursor()
    {
        if (movementVector == Vector2.zero)
            return;

        Debug.Log(movementVector);
        combatInput.combatCursor.Move(movementVector);
    
    }

    public float DeadzoneCheck(float axis)
    {
        if (axis > oneAxisMovementDeadzone)
            return 1.0f;
        else if (axis < -oneAxisMovementDeadzone)
            return -1.0f;
        else 
            return 0.0f;
    }

}
