using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaster : Singleton<UIMaster>
{
    public RectTransform actionButtons;
    public RectTransform skillButtons;
    public RectTransform inventory;
    public UIEventSystem eventSystemMaster = new UIEventSystem();

    private void Awake()
    {
        CheckInstance(this, true);
        eventSystemMaster.FindEventSystem();
    }

    public void SetUnitActionState(int actionStateIndex)
    {
        TurnOrder.Instance.activeUnit.turnStateOrder.actionState = (UnitTurnStateOrder.ActionStates)actionStateIndex;
        TurnOrder.Instance.activeUnit.turnStateOrder.SetTurnState(UnitTurnStateOrder.TurnStateDirections.ADVANCE);


        actionButtons.gameObject.SetActive(false);
        InputMaster.Instance.SetActionMap("Combat");
    }

    


    public void ShowActionButtons()
    {
        StartCoroutine(ShowActionButtonsNextFrame());
    }
    public void ShowSkillButtons()
    {
        StartCoroutine(ShowSkillButtonsNextFrame());
    }
    public void ShowUnitInventory()
    {
        StartCoroutine(ShowUnitInventoryNextFrame());
    }

    //---------- Show UI ----------//

    IEnumerator ShowActionButtonsNextFrame()
    {
        yield return null;
        InputMaster.Instance.SetActionMap("Empty Map");
        actionButtons.gameObject.SetActive(true);
        eventSystemMaster.SetEventSystemSelection(actionButtons.GetChild((int)TurnOrder.Instance.activeUnit.turnStateOrder.actionState).gameObject);
    }

    IEnumerator ShowSkillButtonsNextFrame()
    {
        yield return null;
        InputMaster.Instance.SetActionMap("Empty Map");
        skillButtons.gameObject.SetActive(true);

    }
    IEnumerator ShowUnitInventoryNextFrame()
    {
        yield return null;
        InputMaster.Instance.SetActionMap("Empty Map");
        inventory.gameObject.SetActive(true);

    }

}
