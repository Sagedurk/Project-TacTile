using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class UIEventSystem
{
    public EventSystem eventSystem;


    public void FindEventSystem()
    {
        if (eventSystem == null)
            eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    public void SetEventSystemSelection(GameObject objectToSelect)
    {
        eventSystem.SetSelectedGameObject(objectToSelect);
        //if(objectToSelect.TryGetComponent(out Selectable selectableComponent))
        //{
        //    selectableComponent.Select();
        //}

    }



}
