using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviourCaller : ItemBehaviourMaster
{
    public static void CallItemBehaviour(string itemName)
    {
        Debug.Log("called: " + itemName);
        switch (itemName)
        {
            case "HP Potion":
                ItemBehaviourMaster.PotionHP();
                break;
            case "SP Potion":
                ItemBehaviourMaster.PotionSP();
                break;
            case "Greater HP Potion":
                ItemBehaviourMaster.PotionGreaterHP();
                break;
            case "Greater SP Potion":
                ItemBehaviourMaster.PotionGreaterSP();
                break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
            //case "":
            //    break;
        }

    }
}