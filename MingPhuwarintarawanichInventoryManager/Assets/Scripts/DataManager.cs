using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataManager
{
    public static int currentContainer;
    public static InventoryControl inventoryControl = new InventoryControl();
    public static int WEAPON_INVENTORY_MAX = 3;
    public static int ARMOR_INVENTORY_MAX = 3;
    public static bool isSwapClicked = false;
    public static Item.ItemType swapType;
    public static Swap swapItem1 = null;
    public static Swap currentSelectedTool = null;

    public static void InitialTrasureSlots()
    {
        //1
        Treasure treasure = new Treasure();
        treasure.containerSlot = 1;
        treasure.items = new List<Item> { 
            new Item("Rifle", Item.ItemType.Weapon, 10),
            new Item("Shield", Item.ItemType.Armor, 5),
            new Item("Hammer", Item.ItemType.Weapon, 10)
        };
        inventoryControl.treasureSlots.Add(treasure);

        //2
        treasure = new Treasure();
        treasure.containerSlot = 2;
        treasure.items = new List<Item> {
            new Item("Crossbow", Item.ItemType.Weapon, 10),
            new Item("Sword", Item.ItemType.Weapon, 10),
            new Item("Brigandine", Item.ItemType.Armor, 5)
        };
        inventoryControl.treasureSlots.Add(treasure);

        //3
        treasure = new Treasure();
        treasure.containerSlot = 3;
        treasure.items = new List<Item> {
            new Item("Chainmail", Item.ItemType.Armor, 5),
            new Item("Plate Armor", Item.ItemType.Armor, 5),
            new Item("Shotgun", Item.ItemType.Weapon, 10)
        };
        inventoryControl.treasureSlots.Add(treasure);
    }

    public static void SetSwapItem(Swap.InventoryType inventory, int itemIndex)
    {
        if(swapItem1 is null)
        {
            swapItem1 = new Swap();
            swapItem1.inventory = inventory;
            swapItem1.itemIndex = itemIndex;
        }
        else
        {
            Item tempSwapItem;
            //trasure
            if (swapItem1.inventory == Swap.InventoryType.Treasure && inventory == Swap.InventoryType.Treasure)
            {
                tempSwapItem = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[swapItem1.itemIndex];
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[swapItem1.itemIndex] = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex];
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex] = tempSwapItem;
            }
            if (swapItem1.inventory == Swap.InventoryType.Treasure && inventory == Swap.InventoryType.Weapon)
            {
                tempSwapItem = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[swapItem1.itemIndex];
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[swapItem1.itemIndex] = inventoryControl.weaponInventory.items[itemIndex];
                inventoryControl.weaponInventory.items[itemIndex] = tempSwapItem;
            }
            if (swapItem1.inventory == Swap.InventoryType.Treasure && inventory == Swap.InventoryType.Armor)
            {
                tempSwapItem = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[swapItem1.itemIndex];
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[swapItem1.itemIndex] = inventoryControl.armorInventory.items[itemIndex];
                inventoryControl.armorInventory.items[itemIndex] = tempSwapItem;
            }

            //weapon
            if (swapItem1.inventory == Swap.InventoryType.Weapon && inventory == Swap.InventoryType.Treasure)
            {
                Debug.Log("yo");
                tempSwapItem = inventoryControl.weaponInventory.items[swapItem1.itemIndex];
                Debug.Log("count:" + inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items.Count + " name:" + inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex].Name);
                inventoryControl.weaponInventory.items[swapItem1.itemIndex] = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex];
                Debug.Log(inventoryControl.weaponInventory.items[swapItem1.itemIndex].Name);
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex] = tempSwapItem;
            }
            if (swapItem1.inventory == Swap.InventoryType.Weapon && inventory == Swap.InventoryType.Weapon)
            {
                tempSwapItem = inventoryControl.weaponInventory.items[swapItem1.itemIndex];
                inventoryControl.weaponInventory.items[swapItem1.itemIndex] = inventoryControl.weaponInventory.items[itemIndex];
                inventoryControl.weaponInventory.items[itemIndex] = tempSwapItem;
            }
            if (swapItem1.inventory == Swap.InventoryType.Weapon && inventory == Swap.InventoryType.Armor)
            {
                tempSwapItem = inventoryControl.weaponInventory.items[swapItem1.itemIndex];
                inventoryControl.weaponInventory.items[swapItem1.itemIndex] = inventoryControl.armorInventory.items[itemIndex];
                inventoryControl.armorInventory.items[itemIndex] = tempSwapItem;
            }

            //armor
            if (swapItem1.inventory == Swap.InventoryType.Armor && inventory == Swap.InventoryType.Treasure)
            {
                tempSwapItem = inventoryControl.armorInventory.items[swapItem1.itemIndex];
                inventoryControl.armorInventory.items[swapItem1.itemIndex] = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex];
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex] = tempSwapItem;
            }
            if (swapItem1.inventory == Swap.InventoryType.Armor && inventory == Swap.InventoryType.Weapon)
            {
                tempSwapItem = inventoryControl.armorInventory.items[swapItem1.itemIndex];
                inventoryControl.armorInventory.items[swapItem1.itemIndex] = inventoryControl.weaponInventory.items[itemIndex];
                inventoryControl.weaponInventory.items[itemIndex] = tempSwapItem;
            }
            if (swapItem1.inventory == Swap.InventoryType.Armor && inventory == Swap.InventoryType.Armor)
            {
                tempSwapItem = inventoryControl.armorInventory.items[swapItem1.itemIndex];
                inventoryControl.armorInventory.items[swapItem1.itemIndex] = inventoryControl.armorInventory.items[itemIndex];
                inventoryControl.armorInventory.items[itemIndex] = tempSwapItem;
            }
            isSwapClicked = false;
            swapItem1 = null;
        }
    }
}
