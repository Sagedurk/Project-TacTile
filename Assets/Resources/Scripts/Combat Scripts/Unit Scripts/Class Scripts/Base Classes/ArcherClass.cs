using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
public class ArcherClass : UnitBaseClass
{
    public delegate void Delegate();
    protected Delegate delCall;
    public override void Awake()
    {
        base.Awake();

        maxHealth = 100;
        attackStrength = 15;
        attackRange = 3;
        defense = 5;
        //agility = null;               IMPLEMENT AGILITY
        maxSkillPoints = 100;
        skillPoints = 10;
        movement = 4;

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

    

    public void Piercing_Arrow(int skillPointsCost = 50, int skillRange = 5)
    {
        PreSkillTarget(skillRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            Debug.Log("skill executed");
            //When accept has been pressed, execute the skill
            combatScript.CheckSkill();
            TileScript t = combatScript.next;
            Vector3 target = t.transform.position;
            RaycastHit hit;

            int skillDamage;
            int allyHealth;

            if (Physics.Raycast(target, Vector3.up, out hit, 1, 9))
            {
                Debug.Log(hit.collider);
                if (hit.collider.tag == gameObject.tag)
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
                    combatScript.attacking = false;
                    //t.target = false;
                    Debug.Log("Not a valid target!");
                    //Go back to enemy selection (FAT)
                    combatScript.turnStateCounter--;
                    Debug.Log(combatScript.turnStateCounter);
                }
            }
            else
            {
                // *IMPORTANT*  OUT OF RANGE CALLS THIS 
                if (t.tileState == TileScript.TileStates.SELECTABLE_SKILL) { 
                    //Empty tile
                    combatScript.attacking = false;
                    //t.target = false;
                    Debug.Log("Not a valid target!");
                    //Go back to enemy selection (FAT)
                    //Add bool to CheckSkill??
                    combatScript.turnStateCounter--;
                    Debug.Log(combatScript.turnStateCounter);
                }
                //attacking needs to not get activated
            }
        }
            
            Debug.Log(combatScript.turnStateCounter);
        
    }

    public void Sniper_Shot(int skillPointsCost = 75, int skillRange = 10)
    {
        PreSkillTarget(skillRange);

    }
   
    public void Weakspot(int skillPointsCost = 37, int skillRange = 10)
    {
        Debug.Log(gameObject);
        Debug.Log(name);
    }
    
    public void Bow_Whack(int skillPointsCost = 77, int skillRange = 5)
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
            TileScript t = combatScript.next;
            Vector3 target = t.transform.position;
            RaycastHit hit;

            

            cursorPos = TacticsMovement.cursor.transform.position;
            tilePos = new Vector3(cursorPos.x, 0, cursorPos.z);
            tileConnect = true;
            if (!Physics.Raycast(cursorPos, Vector3.down, out hit, 1, 9))
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
                if (t.tileState == TileScript.TileStates.SELECTABLE_SKILL)
                {
                    //Empty tile
                    combatScript.attacking = false;
                    //t.target = false;
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
    public void Double_Arrow(int skillPointsCost = 1682, int skillRange = 10)
    { }
    public void Bounce_Shot(int skillPointsCost = 907, int skillRange = 10)
    { }
    public void Swift_Shot(int skillPointsCost = 58, int skillRange = 10)
    { }
    public void Blunt_Arrow(int skillPointsCost = 84, int skillRange = 10)
    { }
    public void Eirn_Blast(int skillPointsCost = 44, int skillRange = 10)
    { }
    public void Vaduuhn_Bolt(int skillPointsCost = 67, int skillRange = 10)
    { }
    public void Fayyuh_Storm(int skillPointsCost = 6, int skillRange = 10)
    { }
    public void Arnn_Barrier(int skillPointsCost = 110, int skillRange = 10)
    { }

}
