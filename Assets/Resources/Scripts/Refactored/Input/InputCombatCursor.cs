using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCombatCursor : MonoBehaviour
{

    PlayerControls cursorControls;
    public int cursorUpdate;

    Vector3 movementDirection = Vector3.zero;
    float secondsToWait = 0.5f;

    public bool isReadyToMove = true;
    // Start is called before the first frame update
    void Start()
    {
        cursorControls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


  


    //Enable Input
    private void OnEnable()
    {
        if (cursorControls == null)
            cursorControls = new PlayerControls();
        cursorControls.Controller.Enable();
    }

    //Disable Input
    private void OnDisable()
    {
        cursorControls.Controller.Disable();
    }


    IEnumerator DelayMovement(Vector2 direction, float secondsDelayed)
    {
        isReadyToMove = false;
        //Debug.Log("MOVEMENT CALLED");
        MoveCursor(direction);

        yield return new WaitForSecondsRealtime(secondsDelayed);
        isReadyToMove = true;
    }

    public void Move(Vector2 direction)
    {
        if (isReadyToMove)
            StartCoroutine(DelayMovement(direction, secondsToWait));
    }


    public void MoveCursor(Vector2 direction)
    {
        movementDirection.x = InputMaster.Instance.DeadzoneCheck(direction.x);
        movementDirection.z = InputMaster.Instance.DeadzoneCheck(direction.y);

        //if(movementDirection == Vector3.zero)
        //{
        //    isReadyToMove = true;
        //    StopCoroutine(DelayMovement(direction, secondsToWait));
        //    return;
        //}

        transform.Translate(movementDirection);
        //transform.position = TacticsMovement.cursor.transform.position;
    }

    


}
