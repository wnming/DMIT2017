using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryControl
{
    public List<Treasure> treasureSlots = new List<Treasure>();
    public Inventory weaponInventory = new Inventory();
    public Inventory armorInventory = new Inventory();
}
