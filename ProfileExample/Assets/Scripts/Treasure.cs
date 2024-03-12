using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    Button[] slots;

    [SerializeField]
    List<Item> storage;

    // Start is called before the first frame update
    void Start()
    {
        storage = new List<Item>();

        storage.Add(new Sword("Saber", 1, 5, 3));
        storage.Add(new Sword("Claymore", 2, 10, 1));
        storage.Add(new Helm("Iron Helm", 3, 1, 4));

        UpdateButtons();
    }

    void UpdateButtons()
    {
        int i;
        for (i = 0; i < storage.Count; i++)
        {
            slots[i].GetComponentInChildren<Text>().text = storage[i].Name;
        }
        for (; i < slots.Length; i++)
        {
            slots[i].GetComponentInChildren<Text>().text = "Empty";
        }
    }

    public Item TakeItem(int index) // Item is removed from the treasure inventory.
    {
        Item temp = storage[index];
        storage.RemoveAt(index);
        UpdateButtons();
        return temp;
    }

    public void LeaveItem(Item newItem) // Item is added to the treasure inventory.
    {
        storage.Add(newItem);
        UpdateButtons();
    }
}
