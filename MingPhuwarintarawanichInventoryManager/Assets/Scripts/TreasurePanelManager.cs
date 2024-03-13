using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreasurePanelManager : MonoBehaviour
{
    [SerializeField] InventoryUIManager inventoryUIManager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SelectTreasure(int index)
    {
        Item selectedItem = DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == DataManager.currentContainer).items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == DataManager.currentContainer).items[index] : null;
        if (selectedItem is not null)
        {
            if (!DataManager.isSwapClicked)
            {
                if (selectedItem.Type == Item.ItemType.Weapon)
                {
                    //check weapon
                    if (DataManager.inventoryControl.weaponInventory.items.Count < DataManager.WEAPON_INVENTORY_MAX)
                    {
                        DataManager.inventoryControl.weaponInventory.items.Add(selectedItem);
                        DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == DataManager.currentContainer).items.RemoveAt(index);
                        inventoryUIManager.UpdateButtons();
                    }
                }
                else
                {
                    //check armor
                    if (DataManager.inventoryControl.armorInventory.items.Count < DataManager.ARMOR_INVENTORY_MAX)
                    {
                        DataManager.inventoryControl.armorInventory.items.Add(selectedItem);
                        DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == DataManager.currentContainer).items.RemoveAt(index);
                        inventoryUIManager.UpdateButtons();
                    }
                }
            }
            else
            {
                DataManager.swapType = selectedItem.Type;
                inventoryUIManager.DisableButtons();
                DataManager.SetSwapItem(Swap.InventoryType.Treasure, index);
                inventoryUIManager.UpdateButtons();
            }
        }
    }
}
