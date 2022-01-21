using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : Singleton<TurnOrder>
{
    public UnitMaster activeUnit;
    public bool isTurnStateLocked = true;
    private void Awake()
    {
        CheckInstance(this, true);
    }


}
