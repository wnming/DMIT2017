using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] List<Button> weaponButtons;
    [SerializeField] List<Button> armorButtons;
    [SerializeField] List<Button> treasureButtons;
    [SerializeField] Button swapButton;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject hideInventoryPanel;

    void Start()
    {
        UpdateButtons();
        inventoryPanel.SetActive(false);
        hideInventoryPanel.SetActive(true);
    }


    void Update()
    {
        ChangeSwapColour();
        if (!DataManager.isSwapClicked)
        {
            EnableButtons();
        }
    }

    public void SwapItems()
    {
        DataManager.isSwapClicked = !DataManager.isSwapClicked;
    }

    public void ChangeSwapColour()
    {
        if (DataManager.isSwapClicked)
        {
            swapButton.image.color = Color.red;
        }
        else
        {
            swapButton.image.color = Color.black;
        }
    }

    public void DisableButtons()
    {
        Item currentItem;
        //treasure
        for (int index = 0; index < treasureButtons.Count; index++)
        {
            if(DataManager.currentContainer != 0)
            {
                currentItem = DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == DataManager.currentContainer).items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == DataManager.currentContainer).items[index] : null;
                if (currentItem is not null)
                {
                    if (currentItem.Type != DataManager.swapType)
                    {
                        treasureButtons[index].interactable = false;
                    }
                    else
                    {
                        treasureButtons[index].interactable = true;
                    }
                }
                else
                {
                    treasureButtons[index].interactable = false;
                }
            }
        }

        //weapon
        for (int index = 0; index < weaponButtons.Count; index++)
        {
            currentItem = DataManager.inventoryControl.weaponInventory.items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.weaponInventory.items[index] : null;
            if (currentItem is not null)
            {
                if (currentItem.Type != DataManager.swapType)
                {
                    weaponButtons[index].interactable = false;
                }
                else
                {
                    weaponButtons[index].interactable = true;
                }
            }
            else
            {
                weaponButtons[index].interactable = false;
            }
        }

        //armor slot
        for (int index = 0; index < armorButtons.Count; index++)
        {
            currentItem = DataManager.inventoryControl.armorInventory.items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.armorInventory.items[index] : null;
            if (currentItem is not null)
            {
                if (currentItem.Type != DataManager.swapType)
                {
                    armorButtons[index].interactable = false;
                }
                else
                {
                    armorButtons[index].interactable = true;
                }
            }
            else
            {
                armorButtons[index].interactable = false;
            }
        }
    }

    public void EnableButtons()
    {
        //set swap
        DataManager.isSwapClicked = false;
        DataManager.swapItem1 = null;

        //treasure
        for (int index = 0; index < treasureButtons.Count; index++)
        {
            treasureButtons[index].interactable = true;
        }

        //weapon slot
        for (int index = 0; index < weaponButtons.Count; index++)
        {
            weaponButtons[index].interactable = true;
        }

        //armor slot
        for (int index = 0; index < armorButtons.Count; index++)
        {
            armorButtons[index].interactable = true;
        }

    }

    private void ManageSelectedTool(Item.ItemType type, int index)
    {
        Item selectedItem;
        if (type == Item.ItemType.Weapon)
        {
            selectedItem = DataManager.inventoryControl.weaponInventory.items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.weaponInventory.items[index] : null;
        }
        else
        {
            selectedItem = DataManager.inventoryControl.armorInventory.items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.armorInventory.items[index] : null;
        }
        if (selectedItem is not null)
        {
            if (!DataManager.isSwapClicked)
            {
                DataManager.currentSelectedTool = new Swap();
                DataManager.currentSelectedTool.inventory = selectedItem.Type == Item.ItemType.Weapon ? Swap.InventoryType.Weapon : Swap.InventoryType.Armor;
                DataManager.currentSelectedTool.itemIndex = index;
            }
            else
            {
                DataManager.swapType = selectedItem.Type;
                DisableButtons();
                DataManager.SetSwapItem(type == Item.ItemType.Weapon ? Swap.InventoryType.Weapon : Swap.InventoryType.Armor, index);
                UpdateButtons();
            }
        }
        else
        {
            DataManager.currentSelectedTool = null;
        }
    }

    public void SelectWeapon(int index)
    {
        ManageSelectedTool(Item.ItemType.Weapon, index);
    }


    public void SelectArmor(int index)
    {
        ManageSelectedTool(Item.ItemType.Armor, index);
    }

    public void UpdateButtons()
    {
        if (!inventoryPanel.activeSelf)
        {
            OpenInventory();
        }
        //if(type == Item.ItemType.Weapon)
        //{
            for (int index = 0; index < weaponButtons.Count; index++)
            {
                TextMeshProUGUI buttonText = weaponButtons[index].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = DataManager.inventoryControl.weaponInventory.items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.weaponInventory.items[index].Name : string.Empty;
            }
        //}
        //if (type == Item.ItemType.Armor)
        //{
            for (int index = 0; index < armorButtons.Count; index++)
            {
                TextMeshProUGUI buttonText = armorButtons[index].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = DataManager.inventoryControl.armorInventory.items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.armorInventory.items[index].Name : string.Empty;
            }
        //}
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        hideInventoryPanel.SetActive(true);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        hideInventoryPanel.SetActive(false);
    }
}
