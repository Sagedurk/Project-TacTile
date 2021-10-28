using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    public float startX;
    public float startY;
    public int SpaceBetweenItemX;
    public int columnNum;
    public int SpaceBetweenItemY;

    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            obj.name = inventory.GetSlots[i].item.Name;

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
        //StartCoroutine("CreateSlotsDelay");
    }
    private IEnumerator CreateSlotsDelay()
    {
        yield return new WaitForSeconds(0.05f);
        
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(startX + (SpaceBetweenItemX * (i % columnNum)), startY + (-SpaceBetweenItemY * (i / columnNum)), 0f);
    }






}
