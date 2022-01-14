using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnitBaseClass : MonoBehaviour
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

    public PathfindingTile next;

    public delegate void btnDelegate();
    protected btnDelegate btncall;

    public bool chosenSkillButton;

    public static EventSystem eventSystem;

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

    public virtual void Awake()
    {
        


        maxHealth = gameObject.GetComponent<TacticsCombat>().healthMax;
        attackStrength = gameObject.GetComponent<TacticsCombat>().attackStrength;
        attackRange = gameObject.GetComponent<TacticsCombat>().attackRange;
        defense = gameObject.GetComponent<TacticsCombat>().defense;
        agility = gameObject.GetComponent<TacticsCombat>().agility;
        maxSkillPoints = gameObject.GetComponent<TacticsCombat>().skillPointsMax;
        skillPointsCost = gameObject.GetComponent<TacticsCombat>().skillPointsCost;
        skillStrength = gameObject.GetComponent<TacticsCombat>().skillStrength;
        skillRange = gameObject.GetComponent<TacticsCombat>().skillRange;
        movement = gameObject.GetComponent<TacticsMovement>().move;

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        
    }

    protected void PreSkillTarget(int skillRange)
    {
        //Calls in correct order
        //Add check when trying to call skill to see if unit has sufficient skillpoints.
        TacticsCombat.activeUnit.GetComponent<TacticsCombat>().skillRange = skillRange;
        combatScript = TacticsCombat.activeUnit.GetComponent<TacticsCombat>();
        unitScript = TacticsCombat.activeUnit.GetComponent<TacticsMovement>();
        combatScript.FindSkillTiles();

        //find the order in which functions need to be called - use the pattern for Attack as a reference
        //only call the coming code after pressing [accept] (method needs to be repeated somehow)
        //if (counter == 4) CALL THIS METHOD AGAIN
        //if (counter == 5) EXECUTE THE SKILL AND END THE TURN
        if (combatScript.turnStateCounter == 4)
        {
            Debug.Log("Skillcall : Reverting to Action Buttons");
            //IMPORTANT. MAKE THIS WORK
            if(TacticsCombat.activeUnit.GetComponent<TacticsCombat>().chosenSkillBtn != null)
            {
                TacticsCombat.activeUnit.GetComponent<TacticsCombat>().turnStateCounter = 5;
            }
            // *IMPORTANT*  SHOW UI FOR GENERATED BUTTONS AGAIN
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

   
    protected void EndSkillTurn(bool tileSelecting = true)
    {
        combatScript.attacking = false;

        if (tileSelecting)
        {
            combatScript.RemoveSelectableTiles(combatScript.skillTiles);
        }
        //Debug.Log(eventSystem.name);
        eventSystem.SetSelectedGameObject(null);
        TurnManager.EndTurn();
    }


    protected void CallDelegateCounterMinus()
    {
        combatScript.turnStateCounter--;
    }

    protected void SpawnTileSkill()
    {
        Instantiate(tilePrefab, tilePos, Quaternion.identity);
        EndSkillTurn(false);
    }

    protected void SubtractSPC(int SPC)
    {
        TacticsCombat.activeUnit.GetComponent<TacticsCombat>().skillPoints -= SPC;
    }


    protected void WrongTarget(PathfindingTile t)
    {
        combatScript.attacking = false;
        //t.target = false;
        Debug.Log("Not a valid target!");
        combatScript.turnStateCounter--;
        Debug.Log(combatScript.turnStateCounter);
    }

    protected void EmptyTile(PathfindingTile t)
    {
        if (t.tileState == PathfindingTile.TileStates.SELECTABLE_SKILL)
        {
            //Empty tile
            combatScript.attacking = false;
            //t.target = false;
            Debug.Log("Not a valid target!");
            //Add bool to CheckSkill??
            combatScript.turnStateCounter--;
        }
    }
}
