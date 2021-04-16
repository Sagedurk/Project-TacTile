using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    public Text healthText;
    public Text nameText;
    public GameObject healthOffset;
    public GameObject nameOffset;

    public string unitName;
    public string manipulatorElement;
    public int manipulatorLevel;
    public int unitHealth;
    public int unitMaxHealth;
    public int unitSP;
    public int unitMaxSP;

    private void Start()
    {
        nameText.text = unitName;
        unitMaxHealth = gameObject.GetComponent<TacticsCombat>().healthMax;
        unitMaxSP = gameObject.GetComponent<TacticsCombat>().skillPointsMax;
    }

    private void Update()
    {
        
        unitHealth = gameObject.GetComponent<TacticsCombat>().health;
        unitSP = gameObject.GetComponent<TacticsCombat>().skillPoints;


        Vector3 namePos = Camera.main.WorldToScreenPoint(nameOffset.transform.position);
        Vector3 healthPos = Camera.main.WorldToScreenPoint(healthOffset.transform.position);
        healthText.transform.position = healthPos;

        nameText.transform.position = namePos;

        healthText.text = unitHealth.ToString() + "/" + unitMaxHealth.ToString();


    }

}
