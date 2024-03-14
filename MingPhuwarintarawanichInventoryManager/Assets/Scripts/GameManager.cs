using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button loadButton;
    [SerializeField] Button startGameButton;
    [SerializeField] Button resetButton;

    void Start()
    {
        LoadSaveData();
    }

    public void LoadSaveData()
    {
        if (DataManager.LoadInventoryData())
        {
            //Exists
            loadButton.interactable = true;
            startGameButton.interactable = true;
            resetButton.interactable = true;
            DataManager.ResetSpecialContainer(false);
        }
        else
        {
            //New
            loadButton.interactable = false;
            resetButton.interactable = false;
            DataManager.Reset();
        }
    }

    public void LoadGameScene(bool isNewGame)
    {
        if (isNewGame)
        {
            DataManager.Reset();
        }
        SceneManager.LoadScene(1);
    }

    public void ResetGame()
    {
        DataManager.Reset();
        LoadSaveData();
    }
}
