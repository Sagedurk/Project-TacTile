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

    int FreeCamAxisInverterY = -1;
    int FreeCamAxisInverterX = 1;

    public GameObject secondaryRotator;
    public Camera cameraMain;

    float axisRotationX;
    float axisRotationY;
    [SerializeField] float rotationLockOffset = 15.0f;

    public Vector3 savedCameraRotation;
    public Vector3 savedCameraPosition;
    Vector3 cameraLocalEulerAngles; 

    [HideInInspector] public float zoomIn;
    [HideInInspector] public float zoomOut;
    float camZoom;

    public float rotationSpeed = 100.0f;
    public float zoomSpeed = 5.0f;
    public float maxZoomAmount = 20.0f;
    public float minZoomAmount = 1.0f;
    float camCursorDistance;

    Vector3 freeCamMoveDirection;
    [SerializeField] float freeCameraMovementSpeed = 5.0f;

    [HideInInspector] public float interpolateSpeed = 4.0f;
    [SerializeField] float defaultInterpolateSpeed = 4.0f;
    Coroutine MovementInterpolation = null;

    private void Awake()
    {
        LookAtCursor();
        //savedCameraRotation = cameraMain.transform.eulerAngles;
        //SetCamera(CameraState.CURSOR_CAMERA);
        
    }

    private void Start()
    {
        
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
        
        
        switch (cameraState)
        {
            case CameraState.CURSOR_CAMERA:

                axisRotationX += rotationVector.x * rotationSpeed * axisInverterX * Time.deltaTime;
                axisRotationY += rotationVector.y * rotationSpeed * axisInverterY * Time.deltaTime;

                cameraLocalEulerAngles = cameraMain.transform.localEulerAngles;
                axisRotationY = RotationLock(axisRotationY, -cameraLocalEulerAngles.x, cameraLocalEulerAngles.x * 0.5f, rotationLockOffset);

                InputCombat.Instance.combatCursor.RotateCursor(rotationVector.x);
                transform.localEulerAngles = Vector3.up * axisRotationX;
                secondaryRotator.transform.localEulerAngles = Vector3.right * axisRotationY;
                LookAtCursor();

                break;
            case CameraState.FREE_CAMERA:

                axisRotationX += rotationVector.x * rotationSpeed * FreeCamAxisInverterX * Time.deltaTime;
                axisRotationY += rotationVector.y * rotationSpeed * FreeCamAxisInverterY * Time.deltaTime;

                axisRotationY = RotationLock(axisRotationY, -90.0f, 90.0f, rotationLockOffset);

                cameraMain.transform.eulerAngles = Vector3.up * axisRotationX + Vector3.right * axisRotationY;
                break;
            default:
                break;
        }
    }


    public void SetCamera(CameraState camState)
    {
        if (camState == CameraState.CURSOR_CAMERA)
        {
            axisRotationX = SetAxisRotation(transform.localEulerAngles, Vector3.up);
            axisRotationY = SetAxisRotation(secondaryRotator.transform.localEulerAngles, Vector3.right);

            cameraMain.transform.eulerAngles = savedCameraRotation;
            cameraMain.transform.position = savedCameraPosition;

        }
        else if (camState == CameraState.FREE_CAMERA)
        {
            axisRotationX = SetAxisRotation(cameraMain.transform.eulerAngles, Vector3.up);
            axisRotationY = SetAxisRotation(cameraMain.transform.eulerAngles, Vector3.right);

            savedCameraRotation = cameraMain.transform.eulerAngles;
            savedCameraPosition = cameraMain.transform.position;
        }

        cameraState = camState;
    }

    float SetAxisRotation(Vector3 eulerAngles, Vector3 axis)
    {
        if (axis == Vector3.right)
            return eulerAngles.x;
        
        else if (axis == Vector3.up)
            return eulerAngles.y;  

        else if (axis == Vector3.forward)
            return eulerAngles.z;

        return 0.0f;
    }

    float RotationLock(float axisRotation, float lessThan, float greaterThan, float lockOffset)
    {
        if (axisRotation < lessThan + lockOffset)
            axisRotation = lessThan + lockOffset;

        else if (axisRotation > greaterThan - lockOffset)
            axisRotation = greaterThan - lockOffset;

        return axisRotation;
    }

    void LookAtCursor() 
    {
        cameraMain.transform.LookAt(transform, Vector3.up);
    }

    public void MoveCamera(UnitTurnStateOrder.TurnStateDirections turnStateDirection)
    {
        if(MovementInterpolation == null)
            MovementInterpolation = StartCoroutine(InterpolateCamera(turnStateDirection));
    }

    public void MoveFreeCamera(Vector2 joystickDirection)
    {
        freeCamMoveDirection = Vector3.forward * joystickDirection.y + Vector3.right * joystickDirection.x;
        cameraMain.transform.Translate(freeCamMoveDirection * freeCameraMovementSpeed * Time.deltaTime, Space.Self);
    }

    IEnumerator InterpolateCamera(UnitTurnStateOrder.TurnStateDirections turnStateDirection)
    {

        while (Vector3.Distance(transform.position, InputCombat.Instance.combatCursor.transform.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, InputCombat.Instance.combatCursor.transform.position, interpolateSpeed * Time.deltaTime);       
            yield return null;
        }

        transform.position = InputCombat.Instance.combatCursor.transform.position;
        interpolateSpeed = defaultInterpolateSpeed;
        MovementInterpolation = null;

    }

   public float GetDefaultInterpolationSpeed()
    {
        return defaultInterpolateSpeed;
    }

}
