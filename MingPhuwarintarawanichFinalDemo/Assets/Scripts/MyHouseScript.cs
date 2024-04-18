using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyHouseScript : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] MainMenuManager sceneManager;

    [SerializeField] GameObject houseIsLocked;

    [SerializeField] AudioSource winAudio;

    bool isTextShowing;

    void Start()
    {
        isTextShowing = false;
        houseIsLocked.SetActive(false);
        winPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !DataManager.isGameEnd)
        {
            if (DataManager.currentLevel == 2 && DataManager.isKeyCollected && DataManager.numberOfKilledGhosts == DataManager.LEVEL_2_GHOSTS)
            {
                DataManager.isGameEnd = true;
                StartCoroutine(DisplayWinPanel());
            }
            else
            {
                if (!isTextShowing)
                {
                    isTextShowing = true;
                    StartCoroutine(DisplayText());
                }
            }
        }
    }

    IEnumerator DisplayText()
    {
        houseIsLocked.SetActive(true);
        yield return new WaitForSeconds(3);
        houseIsLocked.SetActive(false);
        isTextShowing = false;
    }

    IEnumerator DisplayWinPanel()
    {
        winAudio.Play();
        winPanel.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        sceneManager.CloseGame();
    }
}
