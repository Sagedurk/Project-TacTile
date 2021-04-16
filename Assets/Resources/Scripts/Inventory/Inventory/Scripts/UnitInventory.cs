using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UnitInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    private DynamicInterface inventoryUi;
    private StaticInterface equipmentUi;

    void Start()
    {
        inventory.Load();
        
        //inventoryUi = GetComponentInChildren<Canvas>().transform.GetComponentInChildren<DisplayInventory>();
        inventoryUi = GetComponentInChildren<Canvas>().GetComponentInChildren<DynamicInterface>();
        equipmentUi = GetComponentInChildren<Canvas>().GetComponentInChildren<StaticInterface>();












        //When moving over to different script, change load order to run this script before UnitInventory
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

        }




        equipment.Load();
    }

    public void AddItemUnitInv(ItemObject item)
    {
        //if (inventory.container.items.Length < 4)
        //{
        //    inventory.AddItem(new Item(item), 1);
        //}
        //else
        //{
            inventory.AddItem(new Item(item), 1);
           // Debug.Log("NO EMPTY INVENTORY SLOTS");
        //}
    }
    
    public void AddEquipment(ItemObject item)       /*  DEBUG PURPOSES  */
    {
        /*  DEBUG PURPOSES  */
                //if (inventory.container.items.Length < 4)
                //{
                    //    inventory.AddItem(new Item(item), 1);
                //}
                //else
                //{
        equipment.AddItem(new Item(item), 1, false);
                    // Debug.Log("NO EMPTY INVENTORY SLOTS");
                //}
    }

    public void ClearEquipment()
    {
        equipmentUi.slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        equipment.Clear();      //clears the actual inventory

    }

        public void ClearInventory()
    {
        inventoryUi.slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        inventory.Clear();      //clears the actual inventory
        
        //-- Add To EUS --\\
        //for (int i = 0; i < inventoryUi.transform.childCount; i++)      // This sort of function could be good to have for the EUS.
        //{
        //    Destroy(inventoryUi.transform.GetChild(i).gameObject);      // [DEPRECATED] deletes the inventory slots UI
        //}
        //----------------\\
    }

    private void OnApplicationQuit()
    {
        inventory.Save();
        inventory.Clear();
        //inventory.container.Slots = new InventorySlot[4];

        equipment.Save();
        equipment.Clear();
    }






    public Attribute[] attributes;

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized]
        public UnitInventory parent;
        public Attributes type;
        public ModifiableInt value;

        public void SetParent(UnitInventory _parent)
        {
            parent = _parent;
            value = new ModifiableInt(AttributeModified);
        }

        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }

    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if(_slot != null)
            if (_slot.ItemObject == null)
                return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.MainInventory:
                break;
            case InterfaceType.UnitInventory:
                break;
            case InterfaceType.UnitEquipment:
                //print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items:", string.Join(", ", _slot.AllowedItems)));
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }

    }
    
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.MainInventory:
                break;
            case InterfaceType.UnitInventory:
                break;
            case InterfaceType.UnitEquipment:
                //print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items:", string.Join(", ", _slot.AllowedItems)));
                
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }


}
