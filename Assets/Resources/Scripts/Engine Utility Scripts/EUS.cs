using Shapes2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EUS : MonoBehaviour
{

    public static string EventSysPointer;
    public static EventSystem eventSystem;

    public delegate void RayDelegate();
    public static RayDelegate rayFalse;
    public static RayDelegate rayTrue;

    public static void DestroyAll(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject target in targets)
        {
            Destroy(target);
        }
    }


    public static void HideUI(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            //target.SetActive(false);
            target.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }

    public static void HideUI(RectTransform rectTransform)
    {
        rectTransform.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    public static void ShowUI(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            //target.SetActive(false);
            target.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    public static void EnableUIComponent(string tag, string component, bool enabled)
    {
        
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            if (component == "Button")
            target.GetComponent<Button>().enabled = enabled;
            else if (component == "Canvas")
            target.GetComponent<Canvas>().enabled = enabled;
        }
    }

    //eventSystem start
    public static void SetEventSys()
    {
        if (eventSystem == null) 
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }
    }
    public static void SetEventSysSelectedNull()
    {
        SetEventSys();
        eventSystem.SetSelectedGameObject(null);
    }
    public static void SetEventSysSelectedObj(string selectedObj)
    {
        SetEventSys();
        eventSystem.SetSelectedGameObject(GameObject.Find(selectedObj));
    }

    public static void SetEventSysSelectedBtn(string selectedObj, bool checkIfNull)
    {
        SetEventSys();
        if (checkIfNull)
        {
            if (EUS.eventSystem.currentSelectedGameObject == null)
            {
                GameObject.Find(selectedObj).GetComponent<Button>().Select();
            }
        }
        else
        {
            GameObject.Find(selectedObj).GetComponent<Button>().Select();
        }
    }

    //eventSystem end

    public static void MultRaycast(RayDelegate hitFalse, RayDelegate hitTrue, Vector3 rayPos, Vector3 dir1, Vector3 dir2, Vector3 dir3, Vector3 dir4)
    {    //4 directions
    
        if (!Physics.Raycast(rayPos, dir1, out RaycastHit hit, 1, 9))
        {
            if (!Physics.Raycast(rayPos, dir2, out hit, 1, 9))
            {
                if (!Physics.Raycast(rayPos, dir3, out hit, 1, 9))
                {
                    if (!Physics.Raycast(rayPos, dir4, out hit, 1, 9))
                    {
                        hitFalse();
                        return;
                    }

                }
            }
        }
        hitTrue();
    }

}
