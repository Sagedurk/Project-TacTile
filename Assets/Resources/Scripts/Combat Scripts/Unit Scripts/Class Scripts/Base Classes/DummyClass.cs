using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class DummyClass : UnitBaseClass
{
    
    public delegate void Delegate();
    protected Delegate delCall;
    public override void Awake()
    {
        base.Awake();

        /*maxHealth = 100;
        attackStrength = 15;
        attackRange = 3;
        defense = 5;
        //agility = null;               IMPLEMENT AGILITY
        maxSkillPoints = 100;
        skillPoints = 10;
        movement = 4;
        */
        skillStrength = 5;
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



    public void Heal(int skillPointsCost = 50, int skillRange = 5)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (TacticsCombat.activeUnit.CompareTag(hit.collider.tag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    allyHealth = hit.collider.GetComponent<TacticsCombat>().health;
                    //Base healing is 10
                    //skillDamage/skillHealing
                    skillDamage = skillStrength + 10;
                    allyHealth += skillDamage;
                    //Subtract SP amount and apply new amount
                    // *IMPORTANT* BEFORE CALLING SKILL, MAKE A SP CHECK
                    SubtractSPC(skillPointsCost);
                    //Prevent overhealing someone
                    if (allyHealth > hit.collider.GetComponent<TacticsCombat>().healthMax)
                    {
                        allyHealth = hit.collider.GetComponent<TacticsCombat>().healthMax;
                    }
                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().health = allyHealth;
                    //END TURN!
                    EndSkillTurn();
                }
                else
                {
                    //Wrong type of target
                    WrongTarget(t);
                }
            }
            else
            {
                // *IMPORTANT*  OUT OF RANGE CALLS THIS 
                EmptyTile(t);
            }
        }
    }

    public void Focused_Attack(int skillPointsCost = 75, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (hit.collider.CompareTag(enemyTag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    enemyHealth = hit.collider.GetComponent<TacticsCombat>().health;
                    //Base healing is 10
                    //skillDamage/skillHealing
                    skillDamage = attackStrength + skillStrength;
                    enemyHealth -= skillDamage;
                    //Subtract SP amount and apply new amount
                    // *IMPORTANT* BEFORE CALLING SKILL, MAKE A SP CHECK
                    SubtractSPC(skillPointsCost);
                    //Prevent overhealing someone
                    /*if (enemyHealth > hit.collider.GetComponent<TacticsCombat>().healthMax)
                    {
                        enemyHealth = hit.collider.GetComponent<TacticsCombat>().healthMax;
                    }*/
                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().health = enemyHealth;
                    //END TURN!
                    EndSkillTurn();
                }
                else
                {
                    WrongTarget(t);
                }
            }
            else
            {
                EmptyTile(t);
            }
        }

    }
    public void Reach(int skillPointsCost = 75, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (hit.collider.CompareTag(TacticsCombat.activeUnit.tag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    allyRange = hit.collider.GetComponent<TacticsCombat>().attackRange;
                    allyRange++;
                    SubtractSPC(skillPointsCost);
                    hit.collider.GetComponent<TacticsCombat>().attackRange = allyRange;
                    EndSkillTurn();
                }
                else
                {
                    //Wrong type of target
                    WrongTarget(t);
                }
            }
            else
            {
                // *IMPORTANT*  OUT OF RANGE CALLS THIS 
                EmptyTile(t);
            }
        }
    }
    public void Restrain(int skillPointsCost = 75, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (hit.collider.CompareTag(enemyTag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    //Make sure enemy doesn't have negative range
                    enemyRange = hit.collider.GetComponent<TacticsCombat>().attackRange;
                    enemyRange--;
                    SubtractSPC(skillPointsCost);
                    hit.collider.GetComponent<TacticsCombat>().attackRange = enemyRange;
                    EndSkillTurn();
                }
                else
                {
                    //Wrong type of target
                    WrongTarget(t);
                }
            }
            else
            {
                // *IMPORTANT*  OUT OF RANGE CALLS THIS 
                EmptyTile(t);
            }
        }
    }

    public void Bulk_Up(int skillPointsCost = 37, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (TacticsCombat.activeUnit.CompareTag(hit.collider.tag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    allyDefense = hit.collider.GetComponent<TacticsCombat>().defense;
                    //skillDamage/skillHealing      Same shit
                    skillDamage = skillStrength;
                    allyDefense += skillDamage;
                    //Subtract SP amount and apply new amount
                    // *IMPORTANT* BEFORE CALLING SKILL, MAKE A SP CHECK
                    SubtractSPC(skillPointsCost);
                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().defense = allyDefense;
                    //END TURN!
                    EndSkillTurn();
                }
                else
                {
                    WrongTarget(t);
                }
            }
            else
            {
                EmptyTile(t);
            }
        }
    }

    public void Sunder(int skillPointsCost = 37, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (hit.collider.CompareTag(enemyTag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    enemyDefense = hit.collider.GetComponent<TacticsCombat>().defense;
                    //skillDamage/skillHealing      Same shit
                    skillDamage = skillStrength;
                    enemyDefense -= skillDamage;
                    //Subtract SP amount and apply new amount
                    if (enemyDefense > hit.collider.GetComponent<TacticsCombat>().defense)
                    {
                        enemyDefense = hit.collider.GetComponent<TacticsCombat>().defense;
                    }
                    // *IMPORTANT* BEFORE CALLING SKILL, MAKE A SP CHECK
                    SubtractSPC(skillPointsCost);
                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().defense = enemyDefense;
                    //END TURN!
                    EndSkillTurn();
                }
                else
                {
                    WrongTarget(t);
                }
            }
            else
            {
                EmptyTile(t);
            }
        }
    }

    public void Slow_Down(int skillPointsCost = 37, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                //Add error if enemy already have the lowest amount of movement
                if (hit.collider.CompareTag(enemyTag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    enemyMovement = hit.collider.GetComponent<TacticsCombat>().move;
                    //skillDamage/skillHealing      Same shit
                    //skillDamage = skillStrength;
                    enemyMovement -= 1;
                    //Subtract SP amount and apply new amount
                    if (enemyMovement < 1)
                    {
                        enemyMovement = 1;
                    }
                    // *IMPORTANT* BEFORE CALLING SKILL, MAKE A SP CHECK
                    SubtractSPC(skillPointsCost);
                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().move = enemyMovement;
                    //END TURN!
                    EndSkillTurn();
                }
                else
                {
                    WrongTarget(t);
                }
            }
            else
            {
                EmptyTile(t);
            }
        }
    }

    public void Speed_Up(int skillPointsCost = 37, int skillRange = 10)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;
           
            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (hit.collider.CompareTag(TacticsCombat.activeUnit.tag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    allyMovement = hit.collider.GetComponent<TacticsCombat>().move;
                    //skillDamage/skillHealing      Same shit
                    //skillDamage = skillStrength;
                    allyMovement += 1;
                    //Subtract SP amount and apply new amount
                    // *IMPORTANT* BEFORE CALLING SKILL, MAKE A SP CHECK
                    SubtractSPC(skillPointsCost);
                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().move = allyMovement;
                    //END TURN!
                    EndSkillTurn();
                }
                else
                {
                    WrongTarget(t);
                }
            }
            else
            {
                EmptyTile(t);
            }
        }
    }

    public void Spawn_Tile(int skillPointsCost = 77, int skillRange = 5)
    {
        PreSkillTarget(skillRange);
        Debug.Log("Counter: " + combatScript.turnStateCounter);
        //Post target selection, try to execute skill 
        //combatScript.RemoveSelectableTiles(combatScript.skillTiles);
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            //combatScript.attacking = true;
            PathfindingTile t = combatScript.next;
            Vector3 target = t.transform.position;



            cursorPos = TacticsMovement.cursor.transform.position;
            tilePos = new Vector3(cursorPos.x, 0, cursorPos.z);
            tileConnect = true;
            if (!Physics.Raycast(cursorPos, Vector3.down, out RaycastHit hit, 1, 9))
            {
                Debug.Log("Nothing under cursor");
                //tilePrefab = Resources.Load<GameObject>("Prefabs/Map Components/Tile");

                //fill delegate
                EUS.rayFalse = CallDelegateCounterMinus;
                EUS.rayTrue = SpawnTileSkill;
                SubtractSPC(skillPointsCost);
                EUS.MultRaycast(EUS.rayFalse, EUS.rayTrue, tilePos, Vector3.forward, Vector3.right, Vector3.back, Vector3.left);



            }
            else
            {

                // *IMPORTANT*  OUT OF RANGE CALLS THIS 
                if (t.tileState == PathfindingTile.TileStates.SELECTABLE_SKILL)
                {
                    //Empty tile
                    combatScript.attacking = false;
                    //t.target = false;
                    t.ChangeTileState(PathfindingTile.TileStates.DEFAULT);   //Is this correct?
                    Debug.Log("Not a valid target!");
                    //Go back to enemy selection (FAT)
                    //Add bool to CheckSkill??
                }
                combatScript.turnStateCounter--;
                Debug.Log(combatScript.turnStateCounter);
                //attacking needs to not get activated
            }
        }
    }
}
