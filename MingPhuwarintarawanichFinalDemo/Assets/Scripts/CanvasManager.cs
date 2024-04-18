using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killedGhosts;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI level;

    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject OptionsPanel;

    [SerializeField] GameObject gameOverPanel;

    HealthScript playerHealth;

    void Start()
    {
        PausePanel.SetActive(false);
        OptionsPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }

        int levelGhosts = DataManager.currentLevel == 1 ? DataManager.LEVEL_1_GHOSTS : DataManager.LEVEL_2_GHOSTS;
        killedGhosts.text = $"{DataManager.numberOfKilledGhosts}/{levelGhosts}";
        if(playerHealth.currentHealth <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        if(DataManager.currentLevel == 1 && DataManager.numberOfKilledGhosts == DataManager.LEVEL_1_GHOSTS)
        {
            DataManager.isTimeRun = false;
        }
        else
        {
            if (DataManager.currentLevel == 1)
            {
                time.text = $"Time: {DataManager.time.ToString("N2")}";
                if (DataManager.time <= 0)
                {
                    time.text = $"Time: 0.00";
                    Time.timeScale = 0;
                    gameOverPanel.SetActive(true);
                }
            }
            else
            {
                DataManager.isTimeRun = false;
                time.text = string.Empty;
            }
        }
        level.text = $"Level {DataManager.currentLevel}";
    }

    public void OpenOptionsPanel()
    {
        OptionsPanel.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        OptionsPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        OptionsPanel.SetActive(false);
    }
}
