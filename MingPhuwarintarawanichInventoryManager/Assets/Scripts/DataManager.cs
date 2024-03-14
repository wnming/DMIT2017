using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public static class DataManager
{
    public static int currentContainer;
    public static InventoryControl inventoryControl = new InventoryControl();
    public static int WEAPON_INVENTORY_MAX = 3;
    public static int ARMOR_INVENTORY_MAX = 3;
    public static int TREASURE_INVENTORY_MAX = 3;
    public static bool isSwapClicked = false;
    public static Item.ItemType swapType;
    public static Swap swapItem1 = null;
    public static Swap currentSelectedTool = null;
    public static List<Item> originalRandomItems = new List<Item> 
        {
            new Item("Helm", Item.ItemType.Armor, 5),
            new Item("Solaris Shield", Item.ItemType.Armor, 5),
            new Item("Leather armor", Item.ItemType.Armor, 5),
            new Item("Cuirass", Item.ItemType.Armor, 5),
            new Item("Aegis", Item.ItemType.Armor, 5),
            new Item("Warhammer", Item.ItemType.Weapon, 10),
            new Item("Halberd", Item.ItemType.Weapon, 10),
            new Item("Axe", Item.ItemType.Weapon, 10),
            new Item("Bow", Item.ItemType.Weapon, 10),
            new Item("Rifle", Item.ItemType.Weapon, 10),
            new Item("Shield", Item.ItemType.Armor, 5),
            new Item("Hammer", Item.ItemType.Weapon, 10),
            new Item("Crossbow", Item.ItemType.Weapon, 10),
            new Item("Sword", Item.ItemType.Weapon, 10),
            new Item("Brigandine", Item.ItemType.Armor, 5),
            new Item("Chainmail", Item.ItemType.Armor, 5),
            new Item("Plate Armor", Item.ItemType.Armor, 5),
            new Item("Shotgun", Item.ItemType.Weapon, 10)
        };
    public static List<Item> randomItems = new List<Item>();

    public static string path = $"Inventory";

    public static bool LoadInventoryData()
    {
        bool isDataExists = false;
        if (File.Exists(path + "/InventoryData"))
        {
            Stream stream = File.Open(path + "/InventoryData", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(InventoryControl));
            inventoryControl = (InventoryControl)serializer.Deserialize(stream);
            stream.Close();
            isDataExists = true;
        }
        return isDataExists;
    }

    public static void SaveInventoryData()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        Stream stream = File.Open($"{path}/InventoryData", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(InventoryControl));
        serializer.Serialize(stream, inventoryControl);
        stream.Close();
    }

    public static void DeleteFile()
    {
        if (File.Exists(path + "/InventoryData"))
        {
            File.Delete(path + "/InventoryData");
        }
    }

    public static void Reset()
    {
        DeleteFile();
        inventoryControl = new InventoryControl();
        InitialTrasureSlots();
    }

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

        ResetSpecialContainer(true);
    }

    public static void ResetSpecialContainer(bool isNewGame)
    {
        randomItems = new List<Item>();
        for (int index = 0; index < originalRandomItems.Count; index++)
        {
            if (inventoryControl.weaponInventory.items.Where(x => x.Name == originalRandomItems[index].Name).Count() == 0 &&    inventoryControl.armorInventory.items.Where(x => x.Name == originalRandomItems[index].Name).Count() == 0 &&
              inventoryControl.treasureSlots[1].items.Where(x => x.Name == originalRandomItems[index].Name).Count() == 0 &&
              inventoryControl.treasureSlots[0].items.Where(x => x.Name == originalRandomItems[index].Name).Count() == 0 &&
              inventoryControl.treasureSlots[2].items.Where(x => x.Name == originalRandomItems[index].Name).Count() == 0 )
            {
                randomItems.Add(originalRandomItems[index]);
            }
        }
        //4 special
        if (isNewGame)
        {
            Treasure treasure = new Treasure();
            treasure.containerSlot = 4;
            treasure.items = new List<Item>();
            for (int index = 0; index < TREASURE_INVENTORY_MAX; index++)
            {
                int randomNum = Random.Range(0, randomItems.Count - 1);
                treasure.items.Add(randomItems[randomNum]);
                randomItems.RemoveAt(randomNum);
            }
            inventoryControl.treasureSlots.Add(treasure);
        }
        else
        {
            inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == 4).items = new List<Item>();
            for (int index = 0; index < TREASURE_INVENTORY_MAX; index++)
            {
                int randomNum = Random.Range(0, randomItems.Count - 1);
                inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == 4).items.Add(randomItems[randomNum]);
                randomItems.RemoveAt(randomNum);
            }
        }
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
                tempSwapItem = inventoryControl.weaponInventory.items[swapItem1.itemIndex];
                inventoryControl.weaponInventory.items[swapItem1.itemIndex] = inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == currentContainer).items[itemIndex];
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
