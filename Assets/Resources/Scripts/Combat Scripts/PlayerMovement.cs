using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : TacticsCombat
{
    Vector2 rotate;
    Vector2 playerRotate;
    Vector3 storedPosition;


    public bool turnStart = true;


    private void Awake()
    {
        skillManager = gameObject.GetComponent<UnitSkillManager>();
        controls = new PlayerControls();

        controls.Controller.AcceptTurn.performed += ctx => AcceptTurn();
        controls.Controller.CancelTurn.performed += ctx => CancelTurn();

        chooseActionUI = GameObject.FindGameObjectWithTag(actionButtonGroupTag);
        //THIS IS NOT WORKING FOR SOME REASON
        health = healthMax;

        /*
        controls.Controller.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Controller.Rotate.canceled += ctx => rotate = Vector2.zero;
        controls.Gameplay.Grow.started
        */
    }


    private void Start()
    {

        health = healthMax;
        skillPoints = skillPointsMax;
        if (chooseActionUI != null)
        {
            chooseActionUI.SetActive(false);
        }
        Init();

    }


    void Update()
    {
        //Passive combat stuff

        if (health <= 0)
        {
            //Add death animation here

            //Disable visual elements of dead unit
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
            //Make the death tile available
            gameObject.transform.position = new Vector3(0, -10, 0);

        }


        //What will happen if it's not this unit's turn
        if (!turn)
        {
            ResetFlags();
            return;
        }

        //Need to become Unit's turn before deleting 
        if (health <= 0)
        {
            dead = true;
            TurnManager.RemoveUnit(this);
            TurnManager.EndTurn();
            Destroy(gameObject);
        }
        //Turn start
        TacticsCombat.activeUnit = this.gameObject;
        Debug.Log("active unit: " + activeUnit.tag + " " + activeUnit.name);

        //Conditions for the turn order. i.e Select Tile -> Move -> Choose Action -> Perform Action -> End Turn

        //If you pressed [Accept] and started your unit's turn
        if (turnStateCounter >= 1)
        {

            if (moving)
            {
                InComCamera.cursorControls.Disable();
            }
            //As long as unit is not moving
            if (!moving)
            {
                if (turnStateCounter == 4 && skills)
                {

                }
                else
                {
                    InComCamera.cursorControls.Enable();
                }

                //ChooseSkill();
                //If Unit hasn't arrived at the post-Move state, show tiles the unit can walk to
                if (turnStateCounter < 3)
                {
                    FindSelectableTiles();
                    RemoveSelectableTiles(attackableTiles);
                    chooseActionUI.SetActive(false);
                    attack = false;
                    skills = false;
                    items = false;
                    defend = false;
                }

                //If tile has been selected, store the unit's position for backtracking
                //And check if the selected tile is a tile the unit can walk to
                if (turnStateCounter == 2)
                {
                    storedPosition = transform.position;
                    CheckCursor();

                }


                //TODO: Add an Action State & Check it here!
                //Start of Action Phase
                if (turnStateCounter == 3)
                {
                    //CheckAction / ChooseAction
                    ChooseState();

                }

            }

            //If Unit is moving
            else
            {
                Move();

            }
        }

        //If you haven't pressed [Accept] to start your unit's turn / Start of the unit's turn
        else
        {
            //Don't show the tiles the unit can walk to
            RemoveSelectableTiles(selectableTiles);
            //Don't show the tiles the unit can attack
            RemoveSelectableTiles(attackableTiles);
        }


        if (!attacking)
        {
            //Make 4 Buttons appear where you're about to choose which action to take
            //CHOOSE ACTION

            //If none has been selected, don't increase turnStateCounter
            if (turnStateCounter >= 3)
            {
                if (!attack && !skills && !items && !defend)
                {
                    turnStateCounter = 3;
                }
            }
            //skillRangeText.gameObject.SetActive(false);  
            //If you choose Attack
            if (attack)
            {
                //If Unit has arrived at post-Move state but not post-Attack state, show tiles the unit can attack
                if (turnStateCounter > 3 && turnStateCounter < 6)
                {
                    FindAttackableTiles();
                }

                //If tile has been selected, store the unit's position for backtracking
                //And check if the selected tile is a tile the unit can walk to


                if (turnStateCounter == 5)
                {
                    CheckAttack();
                }

            }

            //If you choose Skill
            else if (skills)
            {

                if (!chooseSkill)
                {
                    ChooseSkill();
                }
                //Generated button calls SetSkill();
                //CHANGE TO SHOW BUTTONS
                //LINK UNIT'S SKILL UI TO SCRIPT INSTANCE?

                //If Unit has arrived at post-Move state but not post-Attack state, show tiles the unit can attack
                if (turnStateCounter == 5)
                {
                    UseSkill();
                }
                if (turnStateCounter == 6)
                {
                    UseSkill();
                }
            }

            //If you choose Item
            else if (items)
            {
                if (!chooseItem)
                {
                Debug.Log("ITEM HAS BEEN CHOSEN");
                    ChooseItem();
                }
                //Generated button calls SetSkill();
                //CHANGE TO SHOW BUTTONS
                //LINK UNIT'S SKILL UI TO SCRIPT INSTANCE?

                //If Unit has arrived at post-Move state but not post-Attack state, show tiles the unit can attack
                if (turnStateCounter == 5)
                {
                    UseItem();
                }
                if (turnStateCounter == 6)
                {
                    UseItem();
                }
                //If Unit has arrived at post-Move state but not post-Attack state, show tiles the unit can attack
                if (turnStateCounter > 3 && turnStateCounter < 6)
                {
                    //FindSkillTiles();
                }

                if (turnStateCounter == 5)
                {
                    //CheckSkill();

                }
                if (turnStateCounter == 6)
                {
                    //TurnManager.EndTurn();
                }
            }

            //If you choose Defend
            else if (defend)
            {
                //If Unit has arrived at post-Move state but not post-Attack state, show tiles the unit can attack
                if (turnStateCounter == 4)
                {
                    //visualize the choice (Find starting tile)
                    GetDefenseTile();
                    InComCamera.cursorControls.Disable();
                }

                if (turnStateCounter == 5)
                {
                    //currentTile.defendTile = false;
                    currentTile.ChangeTileState(TileScript.TileStates.DEFAULT); // Is this correct?
                    InComCamera.cursorControls.Enable();
                    TurnManager.EndTurn();

                }
            }

        }
        else
        {
            if (attack)
            {
                Attack(enemyTeam);
            }
            else if (skills)
            {
                //UseSkill();
            }


        }


        //Placeholder end turn
        /*if (turnStateCounter == 6)
        {
            attack = false;
            items = false;
            defend = false;
            Debug.Log("[Accept] to End Turn");
        }*/

        //Turn Order Conditions End


        //Move cursor to the unit's position if it's the absolute beginning of the turn
        if (turnStateCounter == 0 && turnStart)
        {
            //Move cursor to unit's position & set flag to false so the cursor can be moved
            cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
            turnStart = false;
        }


        //REMINDER THAT UPDATE IS THIS LINE

        //End this unit's turn
        if (turnStateCounter >= 7)
        {



            TurnManager.EndTurn();
        }

    }

    void CheckCursor()
    {


        RaycastHit hit;

        //If the ray hits a tile, check if the unit can walk to it; if it can, do it
        //If it can't, set turn state -> Select Tile
        if (Physics.Raycast(cursor.transform.position, -Vector3.up, out hit, 1))
        {
            if (hit.collider.tag == "Tile")
            {
                TileScript t = hit.collider.GetComponent<TileScript>();

                if (t.tileState == TileScript.TileStates.SELECTABLE_WALK)
                {

                    allowFreeCam = false;
                    MoveToTile(t);

                }
                else
                {
                    turnStateCounter = 1;
                }
            }

        }
    }






    //Reset this unit's flags
    void ResetFlags()
    {
        attacking = false;
        turnStart = true;
        turnStateCounter = 0;
        skillManager.listIndex = 0;
        SetActionBool(false, false, false, false, true);

        if (chooseSkill)
            chooseSkill = false;
        if(chooseItem)
            chooseItem = false;
    }

    //What will happen if [Accept] is pressed
    void AcceptTurn()
    {
        //REMOVE TURNCANCELFLAG & TURNACCEPTFLAG

        //If beginning of turn and pressing [Accept], move cursor to unit's position
        //If it is this unit's turn
        if (turnStateCounter == 0 && turn)
        {
            cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
        }

        //Go to the next step of the turn (basically) if turn has begun & unit is not moving
        if (!turnStart && !moving)
            turnStateCounter++;
    }


    //What will happen if [Cancel] is pressed
    void CancelTurn()
    {
        //As long as it is not the start of the turn and the unit isn't moving
        if (turnStateCounter > 0 && !moving)
        {


            //If unit has moved and [Cancel] is pressed, go back to start of turn
            //And Reset Unit's Position
            if (turnStateCounter == 1)
            {
                cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
                turnStateCounter--;
            }
            else if (turnStateCounter == 3)
            {
                turnStateCounter -= 3;
                transform.position = storedPosition;
                cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
                chooseActionUI.SetActive(false);
                controls.UI.Disable();
                controls.Controller.Enable();
                InComCamera.cursorControls.Controller.Enable();
            }else if(turnStateCounter == 4)
            {
                //If unit has chosen to attack and [Cancel] is pressed, go back to the choice of action and remove the attackable tiles
                if (attack)
                {
                    turnStateCounter--;
                    RemoveSelectableTiles(attackableTiles);
                    cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
                }
                else if (defend)
                {
                    turnStateCounter--;
                    //currentTile.defendTile = false;
                    currentTile.ChangeTileState(TileScript.TileStates.DEFAULT); //Is this correct?
                    InComCamera.cursorControls.Enable();
                }
                else if (skills)
                {
                    turnStateCounter--;
                    this.DeleteSkillUI();
                    EUS.SetEventSysSelectedNull();
                    skillManager.listIndex = 0;
                    chooseSkill = false;
                    skills = false;

                }
                else if (items)
                {
                    turnStateCounter--;
                    GetComponentInChildren<UserInterface>().GetComponent<RectTransform>().localScale = Vector3.zero;
                    activeUnit.GetComponentInChildren<UserInterface>().DebugButtons.SetActive(false);
                    chooseItem = false;
                    items = false;
                }
            }
            else if (turnStateCounter == 5)
            {
                if(skills)
            {
                turnStateCounter--;
                RemoveSelectableTiles(skillTiles);
                cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
                chooseSkill = false;
            }
                if (items)
                {
                    turnStateCounter--;
                    RemoveSelectableTiles(itemTiles);
                    cursor.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
                    chooseItem = false;
                }
            }
            //In all other situation of [Cancel] being pressed, go back one step
            else
            {
                turnStateCounter--;
            }
        }
    }


    //Enable input
    private void OnEnable()
    {
        controls.Enable();
    }

    //Disable input
    private void OnDisable()
    {
        controls.Disable();
    }

}
