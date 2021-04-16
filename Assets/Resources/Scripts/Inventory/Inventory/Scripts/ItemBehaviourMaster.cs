using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviourMaster : MonoBehaviour
{
    protected TacticsCombat combatScript;
    protected TacticsMovement unitScript;
    protected Vector3 cursorPos;
    protected Vector3 tilePos;
    protected bool tileConnect;

    public int maxHealth;                //Maximum health
    public int attackStrength;           //How much damage the unit does when using base attack
    public int attackRange;              //how many tiles away from itself the unit can attack
    public int defense;                  //How much incoming damage the unit will block (Can't be >= attackStrength)
    public int agility;                  //Agility determines the turn order  ????
    public int maxSkillPoints;           //How much max SP (Skill Points) a character has
    public int skillPoints;              //Current Amount of SP
    public int skillPointsCost;
    public int movement;                 //How many tiles the unit can move per turn
    public int skillStrength;
    public int skillRange;

    public string enemyTag;

    public TileScript next;
    private ItemBehaviourMaster itemMaster;
    public delegate void btnDelegate();
    protected btnDelegate btncall;

    public bool chosenSkillButton;

    //public static EventSystem eventSystem;

    public GameObject tilePrefab;
    public Vector3 tileSpawnLocation;


    protected int skillDamage;
    protected int allyHealth;
    protected int enemyHealth;
    protected int allyRange;
    protected int enemyRange;
    protected int allyDefense;
    protected int enemyDefense;
    protected int allyMovement;
    protected int enemyMovement;

    private void Start()
    {
        itemMaster = this;


    }


    protected static void PotionGreaterSP()
    {
        Debug.Log("Greater SP");
        GameObject.FindObjectOfType<ItemBehaviourMaster>().SkillPotion(40, 2);
    }

    protected static void PotionGreaterHP()
    {
        Debug.Log("Greater HP");
        GameObject.FindObjectOfType<ItemBehaviourMaster>().HealPotion(20);
    }

    protected static void PotionSP()
    {
        Debug.Log("SP");
        GameObject.FindObjectOfType<ItemBehaviourMaster>().SkillPotion(20, 2);
    }

    protected static void PotionHP()
    {
        Debug.Log("HP");
        GameObject.FindObjectOfType<ItemBehaviourMaster>().HealPotion(10);
    }

    public void SkillPotion(int restoredAmount, int itemRange = 1)
    {
        PreSkillTarget(itemRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckItem();
            TileScript t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (TacticsCombat.activeUnit.CompareTag(hit.collider.tag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    allyHealth = hit.collider.GetComponent<TacticsCombat>().skillPoints;

                    //
                    allyHealth += restoredAmount;

                    //Prevent overhealing someone
                    if (allyHealth > hit.collider.GetComponent<TacticsCombat>().skillPointsMax)
                    {
                        allyHealth = hit.collider.GetComponent<TacticsCombat>().skillPointsMax;
                    }

                    //Apply the healed health to the ally's health.
                    hit.collider.GetComponent<TacticsCombat>().skillPoints = allyHealth;

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


    public void HealPotion(int restoredAmount, int itemRange = 1)
    {
        PreSkillTarget(itemRange);
        //Post target selection, try to execute skill 
        if (combatScript.turnStateCounter == 6)
        {
            //When accept has been pressed, execute the skill
            combatScript.CheckItem();
            TileScript t = combatScript.next;
            Vector3 target = t.transform.position;

            if (Physics.Raycast(target, Vector3.up, out RaycastHit hit, 1, 9))
            {
                if (TacticsCombat.activeUnit.CompareTag(hit.collider.tag))
                //hit.collider.tag == gameObject.tag for ally & self targeting
                {
                    allyHealth = hit.collider.GetComponent<TacticsCombat>().health;
                 
                    //
                    allyHealth += restoredAmount;

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


    protected void PreSkillTarget(int itemRange)
    {
        //Calls in correct order
        //Add check when trying to call skill to see if unit has sufficient skillpoints.
        combatScript = TacticsCombat.activeUnit.GetComponent<TacticsCombat>();
        unitScript   = TacticsCombat.activeUnit.GetComponent<TacticsMovement>();
        combatScript.currentItemRange = itemRange;
        combatScript.FindItemTiles();

        //find the order in which functions need to be called - use the pattern for Attack as a reference
        //only call the coming code after pressing [accept] (method needs to be repeated somehow)
        //if (counter == 4) CALL THIS METHOD AGAIN
        //if (counter == 5) EXECUTE THE SKILL AND END THE TURN
        if (combatScript.turnStateCounter == 4)
        {
            Debug.Log("Skillcall : Reverting to Action Buttons");
            //IMPORTANT. MAKE THIS WORK
            //if (TacticsCombat.activeUnit.GetComponent<TacticsCombat>().chosenSkillBtn != null)
            //{
                TacticsCombat.activeUnit.GetComponent<TacticsCombat>().turnStateCounter = 5;
            //}
            // *IMPORTANT*  SHOW UI FOR GENERATED BUTTONS AGAIN
            EUS.HideUI(TacticsCombat.activeUnit.GetComponentInChildren<UserInterface>().GetComponent<RectTransform>());
            TacticsCombat.activeUnit.GetComponentInChildren<UserInterface>().DebugButtons.SetActive(false);
            //TacticsCombat.activeUnit.GetComponent<TacticsCombat>().DeleteSkillUI();

            //What needs to happen if cancelling skill and go back to choosing skill
            //removetiles
            //generate skill list
        }
        if (combatScript.turnStateCounter == 5)
        {
            Debug.Log(TacticsCombat.activeUnit.name + " " + TacticsCombat.activeUnit.tag);
            Debug.Log(TacticsCombat.activeUnit.GetComponent<PlayerMovement>().controls.Controller.enabled);

            enemyTag = TacticsCombat.activeUnit.GetComponent<TacticsCombat>().enemyTeam;
            if (!TacticsCombat.activeUnit.GetComponent<PlayerMovement>().controls.Controller.enabled)
            {
                PlayerMovement player = TacticsCombat.activeUnit.GetComponent<PlayerMovement>();
                player.controls.UI.Disable();
                player.controls.Controller.Enable();
            }

        }
    }

    protected void WrongTarget(TileScript t)
    {
        combatScript.attacking = false;
        t.target = false;
        Debug.Log("Not a valid target! Wrong Target");
        combatScript.turnStateCounter--;
        Debug.Log(combatScript.turnStateCounter);
    }

    protected void EmptyTile(TileScript t)
    {
        if (t.selectableItem)
        {
            //Empty tile
            combatScript.attacking = false;
            t.target = false;
            Debug.Log("Not a valid target! Empty");
            //Add bool to CheckSkill??
            combatScript.turnStateCounter--;
        }
    }

    protected void EndSkillTurn(bool tileSelecting = true)
    {
        combatScript.attacking = false;

        if (tileSelecting)
        {
            combatScript.RemoveSelectableTiles(combatScript.itemTiles);
        }
        //Debug.Log(eventSystem.name);
        //eventSystem.SetSelectedGameObject(null);
        TurnManager.EndTurn();
    }


}
