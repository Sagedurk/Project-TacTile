using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCombat : MonoBehaviour
{
    //Handle Combat Input

    public Transform cursor;
    Vector2 camRotate;
    Vector2 freeCameraMove;
    int cursorUpdate = 0;

    protected void MoveCursor(Vector3 movementVector)
    {
        Debug.Log(movementVector);
        cursor.position += movementVector;
    }

    protected void rotateCursor(Vector2 cursorMove)
    {
        if (!TacticsCamera.freeCamera)
        {
            if (cursorUpdate == 0)
            {
                if (transform.position != TacticsMovement.cursor.transform.position)
                {
                    transform.position = TacticsMovement.cursor.transform.position;
                }
                Vector3 cm = new Vector3(cursorMove.x, 0, cursorMove.y);
                // Move cursor
                moveCursor(cursorMove.x, cursorMove.y, cm);
                moveCursor(cursorMove.y, cursorMove.x, cm);
            }
            else if (cursorUpdate != 0)
            {
                TacticsMovement.cursor.transform.Translate(0, 0, 0);

            }
        }
    }




    protected void moveCursor(float axis1, float axis2, Vector3 direction)
    {
        if (axis1 != 0)
        {
            if (axis2 == 0)
            {
                TacticsMovement.cursor.transform.Translate(direction);
                transform.position = TacticsMovement.cursor.transform.position;
                //cursorUpdate++;
            }
        }
    }

}
