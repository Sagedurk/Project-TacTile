using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : Singleton<TurnOrder>
{
    public UnitMaster activeUnit;

    private void Awake()
    {
        CheckInstance(this, true);
    }


}
