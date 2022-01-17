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
    //----- Not Reworked -----//



    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;
    public float zoomSpeed = 5f;
    public float maxZoomAmount = 20.0f;
    public float minZoomAmount = 1.0f;
    Vector3 cameraPosition;


    static public bool freeCamera = false;

    PlayerControls cameraControls;
    public static PlayerControls cursorControls;


    Vector2 camRotate;
    Vector2 freeCameraMove;
    Vector2 cursorMove;

    [HideInInspector] public float zoomIn;
    [HideInInspector] public float zoomOut;
    float camZoom;


    //Refactored Members
    public InputCombatCursor cursor;


    private void Awake()
    {
        defaultCameraRotation = cameraMain.transform.localEulerAngles;
        cameraPosition = cameraMain.transform.position;
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

    }

    public void Zoom()
    {
        camZoom = (zoomIn - zoomOut) * zoomSpeed;

        cameraMain.transform.Translate(Vector3.forward * camZoom, Space.Self);

        //if((cameraMain.transform.position - cursor.transform.position).magnitude < maxZoomAmount 
        //    && (cameraMain.transform.position - cursor.transform.position).magnitude > minZoomAmount)
        //    cameraMain.transform.position = cameraPosition;
            

        //cameraPosition = cameraMain.transform.position;

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
        cameraMain.transform.LookAt(cursor.transform, Vector3.up);
    }



    private void Update()
    {
        return;



        Vector3 VecMoveFreeCam = new Vector3(freeCameraMove.x, 0, freeCameraMove.y) * moveSpeed * Time.deltaTime;
        Vector3 vecZoomIn = new Vector3(0, 0, zoomIn) * zoomSpeed * Time.deltaTime;
        Vector3 vecZoomOut = new Vector3(0, 0, zoomOut) * zoomSpeed * Time.deltaTime;
        Vector3 vecRotate = new Vector3(0, -camRotate.x, 0) * rotationSpeed * Time.deltaTime;
        Vector3 vecRotateSecondary = new Vector3(camRotate.y / 2, 0, 0) * rotationSpeed * Time.deltaTime;

        transform.Rotate(vecRotate);


        //Cursor Movement 
        if (!freeCamera)
        {
            if (cursor.cursorUpdate == 0)
            {
                if (transform.position != TacticsMovement.cursor.transform.position)
                {
                    transform.position = TacticsMovement.cursor.transform.position;
                }
                Vector3 vecCursorMove = new Vector3(cursorMove.x, 0, cursorMove.y);
                // Move cursor
                //cursor.MoveCursor(cursorMove.x, cursorMove.y, vecCursorMove);
                //cursor.MoveCursor(cursorMove.y, cursorMove.x, vecCursorMove);
            }
            else if (cursor.cursorUpdate != 0)
            {
                TacticsMovement.cursor.transform.Translate(0, 0, 0);

            }
        }
        // Cursor Movement End

        //Stop Cursor from freezing
        if (cursorMove.x != 0 && cursorMove.y != 0)
        {
            cursorControls.Disable();
            cursorControls.Enable();
        }





        //Zoom
        //How far you can zoom in
        //Use localPosition to check the position of an object if said object is a child of another object
        if (cameraMain.transform.localPosition.y > 3)
        {
            cameraMain.transform.Translate(vecZoomIn, Space.Self);
        }
        //How far you can zoom out
        if (cameraMain.transform.localPosition.y < 12)
        {
            cameraMain.transform.Translate(vecZoomOut, Space.Self);
        }
        //Zoom End

        CamRotationX(0, 31, vecRotateSecondary);
        CamRotationX(339, 360, vecRotateSecondary);



        //Free Camera Movement
        if (freeCamera && TacticsMovement.allowFreeCam)
        {

            transform.Translate(VecMoveFreeCam);
        }

        //Rotate Cursor when Camera Y-rotation is between 2 specified values
        CursorCamRotation(0f, 40f, 0f);
        CursorCamRotation(50f, 130f, 90f);
        CursorCamRotation(140f, 220f, 180f);
        CursorCamRotation(230f, 310f, 270f);
        CursorCamRotation(320f, 360f, 0f);
        CursorCamRotation(-220f, -140f, 180f);
        CursorCamRotation(-310f, -230f, 270f);
        CursorCamRotation(-130f, -50f, 90f);
        CursorCamRotation(-320f, -360f, 0f);

        if (transform.rotation.y >= 360 || transform.rotation.y <= -360)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //Rotate Cursor End


        //Update End
    }

    //Method for rotating Cursor
    private void CursorCamRotation(float minCamRot, float maxCamRot, float cursorRotation)
    {
        if (transform.rotation.eulerAngles.y >= minCamRot)
        {
            if (transform.rotation.eulerAngles.y <= maxCamRot)
            {
                TacticsMovement.cursor.transform.eulerAngles = new Vector3(0, cursorRotation, 0);


            }
        }
    }

    //Method for rotating the Camera, with limitations set how far you can rotate it [botRotAngle] -> [topRotAngle]
    private void CamRotationX(float botRotationAngle, float topRotationAngle, Vector3 rotationVector)
    {
        //SHOULD WORK PROPERLY
        if (secondaryRotator.transform.rotation.eulerAngles.x >= botRotationAngle)
        {
            if (secondaryRotator.transform.rotation.eulerAngles.x <= topRotationAngle)
            {
                secondaryRotator.transform.Rotate(rotationVector);
            }
        }
        if (secondaryRotator.transform.rotation.eulerAngles.x < botRotationAngle && secondaryRotator.transform.rotation.eulerAngles.x > botRotationAngle - 5)
        {
            secondaryRotator.transform.eulerAngles = new Vector3(botRotationAngle, secondaryRotator.transform.eulerAngles.y, 0);

        }
        if (secondaryRotator.transform.rotation.eulerAngles.x > topRotationAngle && secondaryRotator.transform.rotation.eulerAngles.x < topRotationAngle + 5)
        {
            secondaryRotator.transform.eulerAngles = new Vector3(topRotationAngle, secondaryRotator.transform.eulerAngles.y, 0);
        }


    }





    //Method for Toggling Free Camera
    public void ToggleFreeCamera()
    {
        if (freeCamera && TacticsMovement.allowFreeCam)
        {
            transform.localPosition = Vector3.zero;
            freeCamera = false;
        }
        else if (!freeCamera && TacticsMovement.allowFreeCam)
        {
            freeCamera = true;
        }

    }


    //Enable Input
    private void OnEnable()
    {
        //cameraControls.Controller.Enable();
        //cursorControls.Controller.Enable();
    }


    //Disable Input
    private void OnDisable()
    {
        //cameraControls.Controller.Disable();
        //cursorControls.Controller.Disable();
    }

}
