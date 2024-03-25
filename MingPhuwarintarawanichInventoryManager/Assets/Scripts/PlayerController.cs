using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject specialContainer;
    [SerializeField] GameObject treasurePanel;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] InventoryUIManager inventoryUIManager;

    [SerializeField] GameObject inventoryControllerPanel;
    [SerializeField] TextMeshProUGUI container1Text;
    [SerializeField] TextMeshProUGUI container2Text;
    [SerializeField] TextMeshProUGUI container3Text;
    [SerializeField] TextMeshProUGUI weaponSlotText;
    [SerializeField] TextMeshProUGUI armorSlotText;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 35.0f;
        rotationSpeed = 70.0f;
        pausePanel.SetActive(false);
        specialContainer.SetActive(false);
        inventoryControllerPanel.SetActive(false);
    }

    void Update()
    {
        if (!inventoryControllerPanel.activeSelf)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);
            if (Input.GetKeyDown(KeyCode.I))
            {
                Time.timeScale = 0;
                container1Text.text = string.Empty;
                container2Text.text = string.Empty;
                container3Text.text = string.Empty;
                weaponSlotText.text = string.Empty;
                armorSlotText.text = string.Empty;
                if (DataManager.inventoryControl.treasureSlots[0].items.Count > 0)
                {
                    for (int index = 0; index < DataManager.inventoryControl.treasureSlots[0].items.Count; index++)
                    {
                        container1Text.text += DataManager.inventoryControl.treasureSlots[0].items[index].Name + "\n";
                    }
                }
                else
                {
                    container1Text.text = "None";
                }
                if (DataManager.inventoryControl.treasureSlots[1].items.Count > 0)
                {
                    for (int index = 0; index < DataManager.inventoryControl.treasureSlots[1].items.Count; index++)
                    {
                        container2Text.text += DataManager.inventoryControl.treasureSlots[1].items[index].Name + "\n";
                    }
                }
                else
                {
                    container2Text.text = "None";
                }
                if (DataManager.inventoryControl.treasureSlots[2].items.Count > 0)
                {
                    for (int index = 0; index < DataManager.inventoryControl.treasureSlots[2].items.Count; index++)
                    {
                        container3Text.text += DataManager.inventoryControl.treasureSlots[2].items[index].Name + "\n";
                    }
                }
                else
                {
                    container3Text.text = "None";
                }
                if (DataManager.inventoryControl.weaponInventory.items.Count > 0)
                {
                    for (int index = 0; index < DataManager.inventoryControl.weaponInventory.items.Count; index++)
                    {
                        weaponSlotText.text += DataManager.inventoryControl.weaponInventory.items[index].Name + "\n";
                    }
                }
                else
                {
                    weaponSlotText.text = "None";
                }
                if (DataManager.inventoryControl.armorInventory.items.Count > 0)
                {
                    for (int index = 0; index < DataManager.inventoryControl.armorInventory.items.Count; index++)
                    {
                        armorSlotText.text += DataManager.inventoryControl.armorInventory.items[index].Name + "\n";
                    }
                }
                else
                {
                    armorSlotText.text = "None";
                }
                inventoryControllerPanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && DataManager.currentSelectedTool != null && Vector3.Distance(transform.position, enemy.transform.position) < 4.0f)
        {
            //Debug.Log(DataManager.currentSelectedTool.inventory == Swap.InventoryType.Weapon ? DataManager.inventoryControl.weaponInventory.items[DataManager.currentSelectedTool.itemIndex].Name : DataManager.inventoryControl.armorInventory.items[DataManager.currentSelectedTool.itemIndex].Name);
            if (DataManager.currentSelectedTool.inventory == Swap.InventoryType.Weapon)
            {
                //Weapon
                DataManager.randomItems.Add(DataManager.inventoryControl.weaponInventory.items[DataManager.currentSelectedTool.itemIndex]);
                attackText.text = $"You attacked an enemy with a/an {DataManager.inventoryControl.weaponInventory.items[DataManager.currentSelectedTool.itemIndex].Name}!";
                DataManager.inventoryControl.weaponInventory.items.RemoveAt(DataManager.currentSelectedTool.itemIndex);
            }
            else
            {
                //Armor
                DataManager.randomItems.Add(DataManager.inventoryControl.armorInventory.items[DataManager.currentSelectedTool.itemIndex]);
                attackText.text = $"You attacked an enemy with a/an {DataManager.inventoryControl.armorInventory.items[DataManager.currentSelectedTool.itemIndex].Name}!";
                DataManager.inventoryControl.armorInventory.items.RemoveAt(DataManager.currentSelectedTool.itemIndex);
            }
            inventoryUIManager.UpdateButtons();
            DataManager.currentSelectedTool = null;
            StartCoroutine(ResetEnemy());
        }
    }

    public void CloseInventoryController()
    {
        Time.timeScale = 1;
        inventoryControllerPanel.SetActive(false);
    }

    IEnumerator ResetEnemy()
    {
        enemy.SetActive(false);
        specialContainer.SetActive(true);
        yield return new WaitForSeconds(15);
        enemy.SetActive(true);
        inventoryUIManager.EnableButtons();
        specialContainer.SetActive(false);
        treasurePanel.SetActive(false);
        DataManager.currentContainer = 0;
        DataManager.ResetSpecialContainer(false);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void SaveGame()
    {
        DataManager.SaveInventoryData();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
