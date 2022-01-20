using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputMaster : Singleton<InputMaster>
{
    //Input Behaviour Hub
    //Link Combat input with non-Combat input here, i.e.
    //Create helper functions for managing action maps, UI icons, etc?


    public Vector2 movementInputVector;
    public Vector2 rotationInputVector;
    
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


        public void LinkTesting(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Debug.Log("Link Testing Started");
            }
            else if (ctx.performed)
            {
                Debug.Log("Link Testing Performed");
            }
            
            else if (ctx.canceled)
            {
                Debug.Log("Link Testing Canceled");
            }
        }





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
        InputCombat.Instance.TryMoveCursor();
        InputCombat.Instance.TryMoveCombatFreeCam();
        InputCombat.Instance.TryRotateCamera();
        InputCombat.Instance.TryZoomCamera();
    }


    

    public Vector2 CheckStrongestAxisOnVector(Vector2 vector)
    {
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            return Vector2.right * vector.x;
        
        else if (Mathf.Abs(vector.y) > Mathf.Abs(vector.x))
            return Vector2.up * vector.y;

        else if (Mathf.Abs(vector.y) == Mathf.Abs(vector.x))
            return Vector2.right * vector.x + Vector2.up * vector.y;


        else
            return Vector2.zero;

    }

    public Vector2 CreateBinaryVector(Vector2 vectorToConvert)
    {
        Vector2 vectorToReturn = Vector2.zero;

        if (vectorToConvert.x < 0)
            vectorToReturn += Vector2.left;
        else if (vectorToConvert.x > 0)
            vectorToReturn += Vector2.right;

        if (vectorToConvert.y < 0)
            vectorToReturn += Vector2.down;
        else if (vectorToConvert.y > 0)
            vectorToReturn += Vector2.up;

        return vectorToReturn;

    }


}
