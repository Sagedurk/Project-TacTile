using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [Header("Pathfinding Range")]
    public int attackRange;                     //Amount of tiles unit can reach when attacking
    public int movementRange;                   //Amount of tiles unit can move
    [HideInInspector] public int skillRange;    //Amount of tiles unit can reach when using skills, skill-dependent

    [Header("HP & SP")]
    public int healthMax;                       //Max amount of Health
    [HideInInspector] public int health;        //Current amount of Health
    public int skillPointsMax;                  //Max amount of Skill Points
    [HideInInspector] public int skillPoints;   //Current amount of Skill Points
    
    [Header("Combat")]
    public int attack;      //Attack determines how much damage will be dealt to target unit, when using attack
    public int defense;     //Defense determines how much incoming damage will be blocked from target unit, incoming damage can't be lower than 1
    public int agility;     //Agility determines the evasiveness of unit? And in which order units take their turns
    
    [HideInInspector] public int damage;                //The amount of damage target unit will take, move this to a more suitable script
    [HideInInspector] public int skillPointsCost;   //Amount of SP lost 
    [HideInInspector] public int skillStrength;
    [Header("Misc")]
    public float moveSpeed = 2f;    //How fast the unit moves to other tiles
    

    [HideInInspector]
    public float jumpHeight = 0f;           //Height difference between 2 walkable tiles
    [HideInInspector]
    public float jumpVelocity = 0f; //4.5f;       //How fast the jump is performed
}
