using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Reflection;

public class TacticsCombat : TacticsMovement
{
    private UnitStats unitStats;
    public int attackRange;         //how many tiles away from itself the unit can attack
    public int healthMax;         //The max health of the unit
    public int health;                  //current amount of health
    public int attackStrength;     //How much damage the unit does when using base attack
    public int defense;            //How much incoming damage the unit will block (Can't be >= attackStrength)
    public int damage;                  //How much damage the unit will deal to the enemy
    public int agility;                 //Agility determines the turn order  ????
    public int skillPointsMax;          //How much max SP (Skill Points) a character has
    public int skillPoints;             //Current amount of skillPoints
    public int skillPointsCost;
    public int skillStrength;
    public int skillRange;
    public int currentItemRange;    //range of the item that is currently being used

    public bool attacking = false;          //If the unit is attacking or not
    public bool chooseSkill = false;
    public bool chooseItem = false;
    

    public RectTransform skillUI;
    public RectTransform inventoryUI;
    public OnClickAdvanced chosenSkillBtn;
    public string chosenItemName;

    public List<TileScript> attackableTiles = new List<TileScript>();
    public List<TileScript> skillTiles = new List<TileScript>();
    public List<TileScript> itemTiles = new List<TileScript>();

    public List<UnitBaseClass> unitClasses;
    protected UnitSkillManager skillManager;

    public static bool chooseState = false;

    public TileScript next;


    public string chosenAction = "";

    public readonly string canvasTag = "Canvas";
    public readonly string SPCtag = "SPC";
    public readonly string nameTag = "SkillName";
    public readonly string SRtag = "SkillRange";


    protected GameObject[] actionButtons;
    //PlayerMovement
    public static GameObject activeUnit;
    public TacticsCombat currentUnit;
    public PlayerControls controls;
    protected GameObject chooseActionUI;

    protected readonly string actionButtonGroupTag = "ActionButtons";

    public bool attack = false;
    public bool defend = false;
    public bool skills = false;
    public bool items = false;
    //--------------------END OF VARIABLE DECLARATION--------------------\\
    private void Start()
    {
        unitStats = gameObject.GetComponent<UnitStats>();

        if(unitStats != null)
        {
            attackRange = unitStats.attackRange;                //how many tiles away from itself the unit can attack
            healthMax = unitStats.healthMax;                    //The max health of the unit
            health = unitStats.health;                          //current amount of health
            attackStrength = unitStats.attackStrength;          //How much damage the unit does when using base attack
            defense = unitStats.defense;                        //How much incoming damage the unit will block (Can't be >= attackStrength)
            damage = unitStats.damage;                          //How much damage the unit will deal to the enemy
            agility = unitStats.agility;                        //Agility determines the turn order  ????
            skillPointsMax = unitStats.skillPointsMax;          //How much max SP (Skill Points) a character has
            skillPoints = unitStats.skillPoints;                //Current amount of skillPoints
            skillPointsCost = unitStats.skillPointsCost;
            skillStrength = unitStats.skillStrength;
            skillRange = unitStats.skillRange;

            move = unitStats.movement;
            moveSpeed = unitStats.moveSpeed;

            jumpHeight = unitStats.jumpHeight;
            jumpVelocity = unitStats.jumpVelocity;
        }
    }

public void FindAttackableTiles()
    {

        ComputeAdjacencyList(jumpHeight, null);
        GetCurrentTile();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave as null

        while (process.Count > 0)
        {
            TileScript t = process.Dequeue();

            attackableTiles.Add(t);
            t.selectableAttack = true;

            if (t.distance < attackRange)
            {
                foreach (TileScript tile in t.attackList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }

        }
    }

    public void FindItemTiles()
    {
        ComputeAdjacencyList(jumpHeight, null);
        GetCurrentTile();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave as null

        while (process.Count > 0)
        {
            TileScript t = process.Dequeue();

            itemTiles.Add(t);
            t.selectableItem = true;

            if (t.distance < currentItemRange)
            {
                foreach (TileScript tile in t.itemList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }

        }
    }

    public void FindSkillTiles()
    {
        ComputeAdjacencyList(jumpHeight, null);
        GetCurrentTile();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave as null

        while (process.Count > 0)
        {
            TileScript t = process.Dequeue();

            skillTiles.Add(t);
            t.selectableSkill = true;

            if (t.distance < skillRange)
            {
                foreach (TileScript tile in t.skillList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }

        }
    }

    public void AttackTile(TileScript tile)
    {
        path.Clear();
        tile.target = true;
        attacking = true;


        next = tile;

        Debug.Log("AttackTile Method Called");
    }

    public void Attack(string enemyTag)
    {
        Debug.Log("Attack Method Called");
        TileScript t = next;
        Vector3 target = t.transform.position;
        RaycastHit hit;
        int enemyHealth;
        int enemyDefense;


        if (Physics.Raycast(target, Vector3.up, out hit, 1))
        {
            if (hit.collider.tag == enemyTag)
            {
                if (enemyTag == "NPC")
                {
                    enemyHealth = hit.collider.GetComponent<AIMovement>().health;
                    enemyDefense = hit.collider.GetComponent<AIMovement>().defense;
                }
                else
                {
                    enemyHealth = hit.collider.GetComponent<TacticsCombat>().health;
                    enemyDefense = hit.collider.GetComponent<TacticsCombat>().defense;

                }

                damage = attackStrength - enemyDefense;
                if (damage <= 0)
                {
                    damage = 1;
                }
                enemyHealth -= damage;


                //Apply the damage to the enemy
                if (enemyTag == "NPC")
                {
                    hit.collider.GetComponent<AIMovement>().health = enemyHealth;
                }
                else
                {
                    hit.collider.GetComponent<TacticsCombat>().health = enemyHealth;
                }

                attacking = false;
                Debug.Log("Ending Turn");
                TurnManager.EndTurn();
                Debug.Log("Turn Ended");
            }
            else
            {
                //If trying to attack anything except an enemy
                Debug.Log("Not a valid target! That is not an enemy!"); //Make this into a UI popup window
                attacking = false;
                //Go back to enemy selection (FAT)
                turnStateCounter--;
            }
        }
        else
        {
            //If trying to attack nothing
            Debug.Log("Not a valid target!");
            attacking = false;
            //Go back to enemy selection (FAT)
            turnStateCounter--;
        }
        RemoveSelectableTiles(attackableTiles);

    }


    public void CheckAttack()
    {
        Debug.Log("CheckAttack called");

        RaycastHit hit;

        //If the ray hits an enemy, check if the unit can attack it; if it can, do it
        //If it can't, set state -> Post-Moving
        if (Physics.Raycast(cursor.transform.position, -Vector3.up, out hit, 1))
        {
            if (hit.collider.tag == "Tile")
            {
                TileScript t = hit.collider.GetComponent<TileScript>();

                if (t.selectableAttack)
                {
                    AttackTile(t);
                }
                else
                {
                    //If out of attack range;
                    turnStateCounter = 4;
                    Debug.Log("Out of range!");

                }
            }
        }
    }

    public void CheckSkill()
    {

        RaycastHit hit;

        //If the ray hits an enemy, check if the unit can attack it; if it can, do it
        //If it can't, set state -> Post-Moving
        if (Physics.Raycast(cursor.transform.position, Vector3.down, out hit, 1))
        {
            if (hit.collider.tag == "Tile")
            {
                TileScript t = hit.collider.GetComponent<TileScript>();

                if (t.selectableSkill)
                {
                    AttackTile(t);
                }
                else
                {
                    //Out of range
                    Debug.Log("Out of range!");
                }
            }
        }
    }


    public void ChooseSkill()
    {
        skillManager = activeUnit.gameObject.GetComponent<UnitSkillManager>();
        //CONTROL SWITCH
        //activeUnit.GetComponent<TacticsCombat>().controls.UI.Enable();
        //activeUnit.GetComponent<TacticsCombat>().controls.Controller.Disable();
        skillManager.SetActiveSkills();
        TacticsCamera.cursorControls.Controller.Disable();
        turnStateCounter = 4;
        chosenSkillBtn = null;
        chooseActionUI.SetActive(false);
        Debug.Log("ChooseSkill");
        EUS.SetEventSysSelectedNull();
        activeUnit.GetComponent<TacticsCombat>().skillUI.localScale = Vector3.one;
        //Sets the first button in the hierarchy of the active unit's skillbuttons to the selected object for the eventsystem
        //Replace GetChild(0) with an index variable depending on the child index being pressed
        EUS.SetEventSysSelectedBtn(activeUnit.GetComponent<TacticsCombat>().skillUI.transform.Find(skillManager.unitSkills[skillManager.listIndex]).name, true);
        
        activeUnit.GetComponent<TacticsCombat>().chooseSkill = true;
    }


    public void UseSkill()
    {
        Debug.Log("UseSkill");
        chosenSkillBtn.intEvent.Invoke(chosenSkillBtn.SPC, chosenSkillBtn.range);
        //invoke skill
        //invoke OnClickAdvanced.CallButton();
        //call with update
    } 


    public void DeleteSkillUI()
    {
        //EUS.DestroyAll("SkillButton");
        //EUS.DestroyAll("PanelUI");

        skillUI.localScale = Vector3.zero;
        //EUS.HideUI("SkillButton");
        //EUS.EnableUIComponent("SkillButton", "Button", false);
        //chooseSkill = false;
        //DESTROYING SKILLBUTTON DOESN'T WORK
        //HIDEUI WORKS

        //DISABLE UI CONTROLS


    }

    public void CheckItem()
    {

        RaycastHit hit;

        //If the ray hits an enemy, check if the unit can attack it; if it can, do it
        //If it can't, set state -> Post-Moving
        if (Physics.Raycast(cursor.transform.position, Vector3.down, out hit, 1))
        {
            if (hit.collider.tag == "Tile")
            {
                TileScript t = hit.collider.GetComponent<TileScript>();

                if (t.selectableItem)
                {
                    AttackTile(t);
                }
                else
                {
                    //Out of range
                    Debug.Log("Out of range!");
                }
            }
        }
    }

    public void ChooseItem()
    {
        //skillManager = activeUnit.gameObject.GetComponent<UnitSkillManager>();
        ////CONTROL SWITCH
        ////activeUnit.GetComponent<TacticsCombat>().controls.UI.Enable();
        ////activeUnit.GetComponent<TacticsCombat>().controls.Controller.Disable();
        //skillManager.SetActiveSkills();
        //TacticsCamera.cursorControls.Controller.Disable();
        turnStateCounter = 4;
        //chosenSkillBtn = null;
        chooseActionUI.SetActive(false);
        //Debug.Log("ChooseSkill");
        //EUS.SetEventSysSelectedNull();

        activeUnit.GetComponentInChildren<UserInterface>().GetComponent<RectTransform>().localScale = Vector3.one;
        activeUnit.GetComponentInChildren<UserInterface>().DebugButtons.SetActive(true);
        //activeUnit.GetComponentInChildren<UserInterface>(true).gameObject.SetActive(true);

        //activeUnit.GetComponent<TacticsCombat>().skillUI.localScale = Vector3.one;
        //Sets the first button in the hierarchy of the active unit's skillbuttons to the selected object for the eventsystem
        //Replace GetChild(0) with an index variable depending on the child index being pressed
        //EUS.SetEventSysSelectedBtn(activeUnit.GetComponent<TacticsCombat>().skillUI.transform.Find(skillManager.unitSkills[skillManager.listIndex]).name, true);

        activeUnit.GetComponent<TacticsCombat>().chooseItem = true;
    }
    public void UseItem()
    {
        Debug.Log("UseItem");
        //IMPORTANT!!!!!!! FIX THE BUTTON CALL!!!!
        ItemBehaviourCaller.CallItemBehaviour(chosenItemName);
        //chosenSkillBtn.intEvent.Invoke(chosenSkillBtn.SPC, chosenSkillBtn.range);
        //invoke skill
        //invoke OnClickAdvanced.CallButton();
        //call with update
    }


    public void ChooseState()   //Show action choice UI
    {
        //What action will you choose? Attack, Skills, Items, Defend

        TacticsCamera.cursorControls.Controller.Disable();
        activeUnit.GetComponent<TacticsCombat>().controls.UI.Enable();



        Debug.Log("ChooseState");
        chooseActionUI.SetActive(true);
        //controls.Controller.Disable();
        if (chosenAction == "")
        {
            EUS.SetEventSysSelectedBtn("defend", true);
        }
        else
        {
            EUS.SetEventSysSelectedBtn(chosenAction, true);
        }


        actionButtons = GameObject.FindGameObjectsWithTag("ActionButton");
        if (activeUnit != null)
        {
            currentUnit = activeUnit.GetComponent<TacticsCombat>();
            foreach (GameObject button in actionButtons)
            {
                TacticsCombat actionButton = button.GetComponent<TacticsCombat>();
                actionButton.currentUnit = currentUnit;
                actionButton.chooseActionUI = chooseActionUI;
            }

        }

    }


    public void SetState()  //Called when choosing what action to take
    {
        //chooseState = false;
        Debug.Log("SetState");
        chosenAction = gameObject.name;
        Debug.Log("Chosen Action: " + chosenAction);
        TacticsCombat.activeUnit.GetComponent<TacticsCombat>().chosenAction = gameObject.name;

        if (chosenAction == "attack")
        {
            SetActionBool(true, false, false, false);
            activeUnit.GetComponent<TacticsCombat>().turnStateCounter = 4;
        }
        else if (chosenAction == "skills")
        {
            SetActionBool(false, true, false, false);
        }
        else if (chosenAction == "items")
        {
            SetActionBool(false, false, true, false);
        }
        else if (chosenAction == "defend")
        {
            SetActionBool(false, false, false, true);

            activeUnit.GetComponent<TacticsCombat>().turnStateCounter = 4;
        }


        chooseActionUI.SetActive(false);

        //TODO: IMPORTANT: Eventsystem integration with skillbuttons
        //disable UI controls, enable controller controls, enable cursor controls
        if (chosenAction == "skills")
        {
            //activeUnit.GetComponent<TacticsCombat>().skillUI.localScale = Vector3.one;
            //EUS.ShowUI("SkillButton");



        }
        else if (chosenAction == "items")
        {

        }
        else
        {
            activeUnit.GetComponent<TacticsCombat>().controls.UI.Disable();
            activeUnit.GetComponent<TacticsCombat>().controls.Controller.Enable();
            TacticsCamera.cursorControls.Controller.Enable();
            //turnStateCounter = 4;
            EUS.SetEventSysSelectedNull();
        }

    }

    public void ClassList()
    {
        //List of all unitClasses in the game
        SupportClass support = GetComponent<SupportClass>();
        TankClass tank = GetComponent<TankClass>();
        AssassinClass assassin = GetComponent<AssassinClass>();
        ArcherClass archer = GetComponent<ArcherClass>();

        unitClasses.Add(support);
        unitClasses.Add(tank);
        unitClasses.Add(assassin);
        unitClasses.Add(archer);
    }

    protected void SetActionBool(bool atkState, bool skillState, bool itemState, bool defState, bool activeUnit = false)
    {
        if (activeUnit)
        {
            this.attack = atkState;
            this.skills = skillState;
            this.items = itemState;
            this.defend = defState;
        }
        else
        {
            currentUnit.attack = atkState;
            currentUnit.skills = skillState;
            currentUnit.items = itemState;
            currentUnit.defend = defState;
        }

    }

}

