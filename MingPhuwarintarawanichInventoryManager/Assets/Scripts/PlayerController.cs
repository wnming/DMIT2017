using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 35.0f;
        rotationSpeed = 70.0f;
        pausePanel.SetActive(false);
        specialContainer.SetActive(false);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);
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
