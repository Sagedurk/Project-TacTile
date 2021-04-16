using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankClass : UnitBaseClass
{
    void Awake()
    {
        maxHealth = 120;
        attackStrength = 10;
        attackRange = 1;
        defense = 10;
        //agility = null;               IMPLEMENT AGILITY
        //maxSkillPoints = null;        IMPLEMENT SKILL POINTS
        movement = 2;

        //Create a unitType (1,2 and 3) to represent the 3 base classes and increase attack/Multiply attack against the favorable type??

        gameObject.GetComponent<TacticsCombat>().healthMax = maxHealth;
        gameObject.GetComponent<TacticsCombat>().attackStrength = attackStrength;
        gameObject.GetComponent<TacticsCombat>().attackRange = attackRange;
        gameObject.GetComponent<TacticsCombat>().defense = defense;
        gameObject.GetComponent<TacticsCombat>().agility = agility;
        gameObject.GetComponent<TacticsCombat>().skillPointsMax = maxSkillPoints;
        gameObject.GetComponent<TacticsCombat>().skillPoints = skillPoints;
        gameObject.GetComponent<TacticsCombat>().skillPointsCost = skillPointsCost;
        gameObject.GetComponent<TacticsCombat>().skillStrength = skillStrength;
        gameObject.GetComponent<TacticsCombat>().skillRange = skillRange;
        gameObject.GetComponent<TacticsMovement>().move = movement;
    }

    
}
