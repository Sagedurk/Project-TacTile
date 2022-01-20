using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCombatCursor : MonoBehaviour
{
    Vector3 newCursorPosition = Vector3.zero;
    public float yAxisPosition = 0.55f;

    [SerializeField]float secondsToWait = 0.1f;
    [SerializeField]float transitionSpeed = 35.0f;

    public bool isReadyToMove = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Enable Input
    private void OnEnable()
    {
        //if (cursorControls == null)
            //cursorControls = new PlayerControls();
        //cursorControls.Controller.Enable();
    }

    //Disable Input
    private void OnDisable()
    {
        //cursorControls.Controller.Disable();
    }


    IEnumerator DelayMovement(Vector2 direction, float secondsDelayed)
    {
        isReadyToMove = false;
        //Debug.Log("MOVEMENT CALLED");

        CalculateNewCursorPosition(direction);
        while (!MoveCursor())
        {
            yield return null;
        }

        yield return new WaitForSecondsRealtime(secondsDelayed);
        isReadyToMove = true;
    }

    public void Move(Vector2 direction)
    {
        if (isReadyToMove)
            StartCoroutine(DelayMovement(direction, secondsToWait));
    }


    void CalculateNewCursorPosition(Vector2 direction)
    {
        direction = InputMaster.Instance.CheckStrongestAxisOnVector(direction);
        direction = InputMaster.Instance.CreateBinaryVector(direction);

        newCursorPosition = transform.position;
        newCursorPosition += direction.y * transform.forward + direction.x * transform.right;
    }

    public bool MoveCursor()
    {
        transform.position = Vector3.MoveTowards(transform.position, newCursorPosition, transitionSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, newCursorPosition) < 0.001f)
            return true;

        return false;

            //if(movementDirection == Vector3.zero)
            //{
            //    isReadyToMove = true;
            //    StopCoroutine(DelayMovement(direction, secondsToWait));
            //    return;
            //}

            //transform.Translate(movementDirection);

            //transform.position = TacticsMovement.cursor.transform.position;
    }

    public void RotateCursor(float direction)
    {
        float angle = Quaternion.Angle(transform.rotation, InputCombat.Instance.combatCamera.transform.rotation);

        if (angle < 60)
            return;

        if (direction > 0)
            transform.Rotate(Vector3.up, -90);

        else if (direction < 0)
            transform.Rotate(Vector3.up, 90);


    }


}
