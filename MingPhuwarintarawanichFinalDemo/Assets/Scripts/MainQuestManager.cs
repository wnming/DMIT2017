using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestManager : MonoBehaviour
{
    [SerializeField] GameObject mainQuestPanel;

    void Start()
    {
        mainQuestPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !mainQuestPanel.activeSelf)
        {
            mainQuestPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CloseMainQuestPanel()
    {
        mainQuestPanel.SetActive(false);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
