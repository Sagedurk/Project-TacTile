using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class SupportClass : UnitBaseClass
{
    
    void Awake()
    {
        maxHealth = 100;
        attackStrength = 5;
        attackRange = 4;
        defense = 10;
        //agility = null;               IMPLEMENT AGILITY
        //maxSkillPoints = null;        IMPLEMENT SKILL POINTS
        skillStrength = 10;
        movement = 4;

        gameObject.GetComponent<TacticsCombat>().healthMax = maxHealth;
        gameObject.GetComponent<TacticsCombat>().attackStrength = attackStrength;
        gameObject.GetComponent<TacticsCombat>().attackRange = attackRange;
        gameObject.GetComponent<TacticsCombat>().defense = defense;
        gameObject.GetComponent<TacticsCombat>().agility = agility;
        gameObject.GetComponent<TacticsCombat>().skillPointsMax = maxSkillPoints;

        gameObject.GetComponent<TacticsCombat>().skillPointsCost = skillPointsCost;
        gameObject.GetComponent<TacticsCombat>().skillStrength = skillStrength;
        gameObject.GetComponent<TacticsCombat>().skillRange = skillRange;
        gameObject.GetComponent<TacticsMovement>().move = movement;
    }

    public void BuffAttack(int skillPointsCost = 10, int skillRange = 1)
    {

    }

    public void BuffDefense(int skillPointsCost = 15, int skillRange = 2)
    {
       
    }

    public void Heal(int skillPointsCost = 30, int skillRange = 5)
    {
        
        
        TileScript t = next;
        Vector3 target = t.transform.position;
        RaycastHit hit;

        int skillDamage;
        int allyHealth;

        if (Physics.Raycast(target, Vector3.up, out hit, 1))
        {
            if (hit.collider.tag == gameObject.tag)
            {
                allyHealth = hit.collider.GetComponent<TacticsCombat>().health;

                //Base healing is 10
                skillDamage = skillStrength + 10;
                allyHealth += skillDamage;

                //Subtract the SP the 
                skillPoints -= skillPointsCost;
                gameObject.GetComponent<TacticsCombat>().skillPoints = skillPoints;

                //Apply the healed health to the ally's health.
                hit.collider.GetComponent<TacticsCombat>().health = allyHealth;


                //END TURN NOW!
                //TurnManager.EndTurn();
                combatScript.attacking = false;
                combatScript.turnStateCounter++;

            }

        }
        unitScript.RemoveSelectableTiles(combatScript.skillTiles);

    }

    public void DebuffAttack(int skillPointsCost = 20, int skillRange = 4)
    {

    }

    public void DebuffDefense(int skillPointsCost = 25, int skillRange = 3)
    {

    }

    public void BuffMovement(int skillPointsCost = 60, int skillRange = 6)
    {

    }
    public void DebuffMovement(int skillPointsCost = 70, int skillRange = 7)
    {

    }

}
