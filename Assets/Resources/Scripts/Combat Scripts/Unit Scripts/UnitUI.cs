using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public Text nameTemp;
    public Text healthTemp;
    public Text manipulatorEleTemp;
    public Text manipulatorLvlTemp;
    public Text SkillPoints;

    public GameObject unitInfoUI;

    Vector3 raycastOffset;

    private void Start()
    {
        raycastOffset = new Vector3(0, TacticsMovement.cursor.transform.position.y, 0);
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(TacticsMovement.cursor.transform.position - raycastOffset, Vector3.up, out hit, 1))
        {
            UnitInfo unit = hit.collider.GetComponent<UnitInfo>();
            if (unit != null)
            {
                Debug.Log("unit");
                unitInfoUI.SetActive(true);

                nameTemp.text = "Name: " + unit.unitName;
                healthTemp.text = "Health: " + unit.unitHealth.ToString() + "/" + unit.unitMaxHealth.ToString();
                manipulatorEleTemp.text = "Element: " + unit.manipulatorElement;
                manipulatorLvlTemp.text = "Manipulator lvl: " + unit.manipulatorLevel;
                SkillPoints.text = "Skill Points: " + unit.unitSP + "/" + unit.unitMaxSP;
            }
            else
            {
                Debug.Log("object, but no unit");
            }
        }
        else
        {
            Debug.Log("no object");
            unitInfoUI.SetActive(false);
        }


    }
}
