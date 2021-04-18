using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InComCursor : MonoBehaviour
{

    PlayerControls cursorControls;
    public int cursorUpdate;
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
        cursorControls.Controller.Enable();
    }

    //Disable Input
    private void OnDisable()
    {
        cursorControls.Controller.Disable();
    }



    public void moveCursor(float axis1, float axis2, Vector3 direction)
    {
        if (axis1 != 0)
        {
            if (axis2 == 0)
            {
                TacticsMovement.cursor.transform.Translate(direction);
                transform.position = TacticsMovement.cursor.transform.position;
                cursorUpdate++;
            }
        }
    }



}
