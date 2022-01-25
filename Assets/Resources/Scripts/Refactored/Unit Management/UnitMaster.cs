using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMaster : MonoBehaviour
{
    public UnitTurnStateOrder turnStateOrder = new UnitTurnStateOrder();
    public UnitPathfinding unitPathfinding = new UnitPathfinding();
    [Space(10)]
    public UnitStats unitStats;
    public UnitTeam unitTeam;

    // Start is called before the first frame update
    void Start()
    {
        turnStateOrder.master = this;
        unitPathfinding.master = this;

        if (unitStats == null)
            unitStats = GetComponent<UnitStats>();
    }


    [System.Serializable]
    public class UnitTeam
    {
        public Teams team;

        public enum Teams
        {
            PLAYER_1,
            PLAYER_2,
            AI

        }

    }







}
