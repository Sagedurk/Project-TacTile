using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMaster : MonoBehaviour
{
    public UnitTurnStateOrder turnStateOrder = new UnitTurnStateOrder();

    // Start is called before the first frame update
    void Start()
    {
        turnStateOrder.unitObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
