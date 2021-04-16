﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    Potion,
    Helmet,
    Shoulderguard,
    Chestpiece,
    Gauntlets,
    Leggings,
    Boots,
    MainHand,
    EitherHand,
    OffHand,
    TwoHanded,
    Default
}

public enum Attributes
{
    Attack,   //Strength
    Defense,  //Toughness
    //Intellect,
    //Stamina, 
    Agility,
    Movement      //Speed??
}

public abstract class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    public UnityEvent uEvent;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
    public void UseItem()
    {

    }

}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public Item()   //empty item
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
            buffs[i].attribute = item.data.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifier
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

}