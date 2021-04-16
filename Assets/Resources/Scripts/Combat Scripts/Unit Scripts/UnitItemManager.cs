using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitItemManager : MonoBehaviour
{
    //this will be on unit

    TacticsCombat unit;
    public List<string> unitSkills;
    public int listIndex;

    //Predicate<string> test;


    public void SetActiveSkills()
    {
        unit = gameObject.GetComponent<TacticsCombat>();

        foreach (Transform child in unit.skillUI)
        {
            child.gameObject.SetActive(false);
        }

        foreach (string name in unitSkills)
        {
            for (int i = 0; i < unit.skillUI.childCount; i++)
            {
                if (unit.skillUI.GetChild(i).name == name)
                {
                    unit.skillUI.GetChild(i).gameObject.SetActive(true);
                    Debug.Log(name + " : " + (40 * unitSkills.IndexOf(name)));
                    unit.skillUI.GetChild(i).transform.localPosition = new Vector3(0, 40 * -unitSkills.IndexOf(name), 0);
                }

            }
        }

    }


    //List the class skills this unit should have access to (could use for level up??)
    //Set skills it should have access to to active

}
