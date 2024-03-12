using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class TreasureSlot : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] List<TextMeshProUGUI> itemNames;
    [SerializeField] GameObject treasurePanel;
    [SerializeField] int containerSlot;

    void Start()
    {
        treasurePanel.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DataManager.currentContainer = containerSlot;
            for (int index = 0; index < itemNames.Count; index++)
            {
                if(DataManager.treasureSlots.FirstOrDefault(x => x.containerSlot == containerSlot).items[index].IsActive)
                {
                    itemNames[index].text = DataManager.treasureSlots.FirstOrDefault(x => x.containerSlot == containerSlot).items[index].Name;
                }
            }
            treasurePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DataManager.currentContainer = 0;
            treasurePanel.SetActive(false);
        }
    }
}
