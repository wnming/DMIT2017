using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        Armor
    }
    public string Name;
    public ItemType Type;
    public int Damage;
    public bool IsActive;

    public Item(string name, ItemType type, int damage, bool isActive)
    {
        Name = name;
        Type = type;
        Damage = damage;
        IsActive = isActive;
    }
}
