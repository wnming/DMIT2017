using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static int currentContainer;
    public static List<Treasure> treasureSlots = new List<Treasure>();

    public static void InitialTrasureSlots()
    {
        //1
        Treasure treasure = new Treasure();
        treasure.containerSlot = 1;
        treasure.items = new List<Item> { 
            new Item("Rifle", Item.ItemType.Weapon, 10, true),
            new Item("Shield", Item.ItemType.Armor, 5, true),
            new Item("Hammer", Item.ItemType.Weapon, 10, true)
        };
        treasureSlots.Add(treasure);

        //2
        treasure = new Treasure();
        treasure.containerSlot = 2;
        treasure.items = new List<Item> {
            new Item("Crossbow", Item.ItemType.Weapon, 10, true),
            new Item("Sword", Item.ItemType.Weapon, 10, true),
            new Item("Brigandine", Item.ItemType.Armor, 5, true)
        };
        treasureSlots.Add(treasure);

        //3
        treasure = new Treasure();
        treasure.containerSlot = 3;
        treasure.items = new List<Item> {
            new Item("Chainmail", Item.ItemType.Armor, 5, true),
            new Item("Plate Armor", Item.ItemType.Armor, 5, true),
            new Item("Shotgun", Item.ItemType.Weapon, 10, true)
        };
        treasureSlots.Add(treasure);
    }
}
