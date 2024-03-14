using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class TreasureSlot : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> itemNames;
    [SerializeField] GameObject treasurePanel;
    [SerializeField] int containerSlot;

    [SerializeField] InventoryUIManager inventoryUIManager;

    void Start()
    {
        treasurePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inventoryUIManager.EnableButtons();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            DataManager.currentContainer = containerSlot;
            for (int index = 0; index < itemNames.Count; index++)
            {
                itemNames[index].text = DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == containerSlot).items.ElementAtOrDefault(index) is not null ? DataManager.inventoryControl.treasureSlots.FirstOrDefault(x => x.containerSlot == containerSlot).items[index].Name : string.Empty;
            }
            treasurePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DataManager.currentContainer = 0;
            inventoryUIManager.EnableButtons();
            treasurePanel.SetActive(false);
        }
    }
}
