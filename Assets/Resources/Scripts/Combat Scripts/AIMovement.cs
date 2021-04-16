using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : TacticsCombat
{
   GameObject target;

    private void Awake()
    {
        health = healthMax;


    }


    void Start()
    {
        Init();
    }


    void Update()
    {

        //Passive combat stuff

        if (health <= 0)
        {
            //Add death animation here
            TurnManager.Wait(1);
            Destroy(gameObject);
        }


        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Move();
        }
    }

    void CalculatePath()
    {
        TileScript targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        //AI Behaviour

        //Advanced AI Behaviour?


        //Find The Nearest Enemy
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            
            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }



}
