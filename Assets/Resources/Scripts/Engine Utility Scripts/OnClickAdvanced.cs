using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class OnClickAdvanced : MonoBehaviour
{
    //Advanced Unity Button OnClick Manager
    public int SPC;
    public int range;
    public DuoIntEvent intEvent;
    GameObject activeUnit;
    UnitSkillManager activeSkillManager;

    Button selfBtn;

    private void Awake()
    {
        selfBtn = this.GetComponent<Button>();
        if (intEvent == null)
        {
            intEvent = new DuoIntEvent();
        }
    }

    private void OnEnable()
    {

        selfBtn.onClick.AddListener(CallButton);
    }
    
    void CallButton()
    {
    	//TacticsCombat.activeUnit.GetComponent<TacticsCombat>().turnStateCounter = 5;
    	Debug.Log(TacticsCombat.activeUnit.GetComponent<TacticsCombat>().turnStateCounter);
        if (SPC > TacticsCombat.activeUnit.GetComponent<TacticsCombat>().skillPoints)
        {
            Debug.Log("Not enough SP, can't use skill");
            TacticsCombat.activeUnit.GetComponent<TacticsCombat>().turnStateCounter = 4;
        }
        else
        {
            activeUnit = TacticsCombat.activeUnit;
            activeSkillManager = activeUnit.gameObject.GetComponent<UnitSkillManager>();
            Debug.Log("Button clicked : exe");
            Debug.Log(activeSkillManager.unitSkills.IndexOf(selfBtn.name));
            activeSkillManager.listIndex = activeSkillManager.unitSkills.IndexOf(selfBtn.name);
            Debug.Log(activeSkillManager.listIndex);
            activeUnit.GetComponent<TacticsCombat>().DeleteSkillUI();
            activeUnit.GetComponent<TacticsCombat>().chosenSkillBtn = this;
            activeUnit.GetComponent<TacticsCombat>().controls.UI.Disable();
            activeUnit.GetComponent<TacticsCombat>().controls.Controller.Enable();
            TacticsCamera.cursorControls.Controller.Enable();
            intEvent.Invoke(SPC, range);

        }
    }

    private void OnDisable()
    {
        selfBtn.onClick.RemoveAllListeners();
    }
}
