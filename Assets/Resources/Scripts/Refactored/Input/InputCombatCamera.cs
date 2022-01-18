using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputCombatCamera : MonoBehaviour
{
    public CameraState cameraState;
    public enum CameraState
    {
        CURSOR_CAMERA,
        FREE_CAMERA
    }

    int axisInverterY = 1;
    int axisInverterX = -1;

    public GameObject secondaryRotator;
    public Camera cameraMain;

    float axisRotationX;
    float axisRotationY;
    [SerializeField] float rotationLockOffset = 15.0f;

    public Vector3 defaultCameraRotation;

    [HideInInspector] public float zoomIn;
    [HideInInspector] public float zoomOut;
    float camZoom;


    //Refactored Members
    //public InputCombatCursor cursor;

    public float rotationSpeed = 100f;
    public float zoomSpeed = 5f;
    public float maxZoomAmount = 20.0f;
    public float minZoomAmount = 1.0f;
    float camCursorDistance;

    Vector3 interpolateStartPosition;
    Vector3 interpolateEndPosition;
    [SerializeField]float interpolateSpeed;
    float interpolateTimeElapsed = 0.0f;

    bool isInterpolating = false;
    Coroutine MovementInterpolation = null;

    //----- Not Reworked -----//


    public float moveSpeed = 5f;
    

    private void Awake()
    {
        defaultCameraRotation = cameraMain.transform.localEulerAngles;

        //TacticsMovement.cursor = GameObject.FindGameObjectWithTag("Cursor");

        //cameraControls = new PlayerControls();
        //cursorControls = new PlayerControls();

        ////Set Input
        //cameraControls.Controller.ToggleFreeCam.performed += ctx => ToggleFreeCamera();


        //cameraControls.Controller.ZoomIn.performed += ctx => zoomIn = ctx.ReadValue<float>();
        //cameraControls.Controller.ZoomIn.canceled += ctx => zoomIn = 0;

        //cameraControls.Controller.ZoomOut.performed += ctx => zoomOut = -ctx.ReadValue<float>();
        //cameraControls.Controller.ZoomOut.canceled += ctx => zoomOut = 0;

        //cameraControls.Controller.FreeCamMove.performed += ctx => freeCameraMove = ctx.ReadValue<Vector2>();
        //cameraControls.Controller.FreeCamMove.canceled += ctx => freeCameraMove = Vector2.zero;

        //cameraControls.Controller.CamRotate.performed += ctx => camRotate = ctx.ReadValue<Vector2>();
        //cameraControls.Controller.CamRotate.canceled += ctx => camRotate = Vector2.zero;

        //cursorControls.Controller.CursorMove.performed += ctx => cursorMove = ctx.ReadValue<Vector2>();
        //cursorControls.Controller.CursorMove.canceled += ctx => cursorMove = Vector2.zero;
        //Set Input End

    }

    private void Start()
    {
        LookAtCursor();
    }

    public void Zoom()
    {
        camZoom = (zoomIn - zoomOut) * zoomSpeed;
        camCursorDistance = (cameraMain.transform.position - transform.position).magnitude;

        if ((camCursorDistance < minZoomAmount && camZoom > 0) || (camCursorDistance > maxZoomAmount && camZoom < 0))
            return;

        cameraMain.transform.Translate(Vector3.forward * camZoom, Space.Self);


        //Debug.Log(cameraMain.transform.position - cursor.transform.position);
    }

    public void Rotate(Vector2 rotationVector)
    {
        axisRotationX += rotationVector.y * rotationSpeed * axisInverterX * Time.deltaTime;
        axisRotationY += rotationVector.x * rotationSpeed * axisInverterY * Time.deltaTime;

        if (axisRotationY < -cameraMain.transform.localEulerAngles.x + rotationLockOffset)
            axisRotationY = -cameraMain.transform.localEulerAngles.x + rotationLockOffset;
        
        else if (axisRotationY > cameraMain.transform.localEulerAngles.x * 0.5f - rotationLockOffset)
            axisRotationY = cameraMain.transform.localEulerAngles.x * 0.5f - rotationLockOffset;



        switch (cameraState)
        {
            case CameraState.CURSOR_CAMERA:
                InputCombat.Instance.combatCursor.RotateCursor();
                transform.localEulerAngles = Vector3.up * axisRotationX;
                secondaryRotator.transform.localEulerAngles = Vector3.right * axisRotationY;
                LookAtCursor();

                break;
            case CameraState.FREE_CAMERA:
                cameraMain.transform.localEulerAngles = Vector3.up * axisRotationX;
                cameraMain.transform.localEulerAngles = Vector3.right * axisRotationY;
                break;
            default:
                break;
        }
    }

    void LookAtCursor() 
    {
        cameraMain.transform.LookAt(transform, Vector3.up);
    }

    public void MoveCamera()
    {
        //if (isInterpolating)
        //return;
        //if(MovementInterpolation != null)
            //StopCoroutine(MovementInterpolation);

        //UpdateInterpolationData(endPos);
        if(MovementInterpolation == null)
            MovementInterpolation = StartCoroutine(InterpolateCamera());
    }



    IEnumerator InterpolateCamera()
    {

        while (Vector3.Distance(transform.position, InputCombat.Instance.combatCursor.transform.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, InputCombat.Instance.combatCursor.transform.position, interpolateSpeed * Time.deltaTime);       
            yield return null;
        }

        transform.position = InputCombat.Instance.combatCursor.transform.position;
        MovementInterpolation = null;

    }

    private void Update()
    {
       
    }

    void UpdateInterpolationData(Vector3 endPos)
    {
        //interpolateStartPosition = transform.position;
        interpolateEndPosition = endPos;
        //interpolateTimer = 0.0f;
    }



}
