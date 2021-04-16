using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int attackRange;         //how many tiles away from itself the unit can attack
    public int healthMax;         //The max health of the unit
    public int health;                  //current amount of health
    public int attackStrength;     //How much damage the unit does when using base attack
    public int defense;            //How much incoming damage the unit will block (Can't be >= enemy.attackStrength)
    public int damage;                  //How much damage the unit will deal to the enemy
    public int agility;                 //Agility determines the turn order, and evasiveness  ????
    public int skillPointsMax;          //How much max SP (Skill Points) a character has
    public int skillPoints;             //Current amount of SP
    public int skillPointsCost;
    public int skillStrength;
    public int skillRange;

    public int movement;                    //How many tiles the unit can move
    public float moveSpeed = 2f;             //How fast the unit moves to other tiles
    

    [HideInInspector]
    public float jumpHeight = 0f;           //Height difference between 2 walkable tiles
    [HideInInspector]
    public float jumpVelocity = 0f; //4.5f;       //How fast the jump is performed
}
