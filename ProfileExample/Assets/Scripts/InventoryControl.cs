using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    [SerializeField] // Link to all the panels and the chest object
    GameObject treasurePanel, inventoryPanelOpen, inventoryPanelClosed, chest;

    [SerializeField] // Buttons on the inventory panel open version
    Button[] slots;

    List<Item> playerInventory; // The list of items in the player inventory

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Toggle the treasure inventory
        {
            ToggleTreasurePanel();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel(); // Toggle the player inventory
        }

        if (Input.GetKeyDown(KeyCode.A)) // Attack with the item in first slot if it is a weapon
        {
            if (playerInventory.Count > 0)
            {
                if (playerInventory[0] is Weapon)
                {
                    Weapon weapon = (Weapon)playerInventory[0];
                    weapon.Attack();
                }
            }            
        }
    }

    public void ToggleTreasurePanel()
    {
        treasurePanel.SetActive(!treasurePanel.activeSelf);
    }

    public void ToggleInventoryPanel()
    {
        inventoryPanelClosed.SetActive(!inventoryPanelClosed.activeSelf);
        inventoryPanelOpen.SetActive(!inventoryPanelOpen.activeSelf);
    }

    public void TakeItem(int index) // Take an item from the treasure inventory and add it to the player inventory.
    {
        playerInventory.Add(chest.GetComponent<Treasure>().TakeItem(index));
        UpdateButtons();
    }

    public void LeaveItem(int index) // Leave an item in the treasure inventory and remove it from the player inventory.
    {
        Item temp = playerInventory[index];
        playerInventory.RemoveAt(index);
        chest.GetComponent<Treasure>().LeaveItem(temp);
        UpdateButtons();
    }

    void UpdateButtons()
    {
        // Fill all the button names with the names of the items in the list. At the end of the list any extra buttons will get the name Empty.
        int i;
            for(i = 0; i < playerInventory.Count; i++)
        {
            slots[i].GetComponentInChildren<Text>().text = playerInventory[i].Name;
        }
        for (; i < slots.Length; i++)
        {
            slots[i].GetComponentInChildren<Text>().text = "Empty";
        }
    }
}
