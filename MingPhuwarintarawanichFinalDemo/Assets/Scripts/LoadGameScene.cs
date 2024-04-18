using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    [SerializeField] GameObject playerSpwanLocation;
    GameObject player;
    [SerializeField] GameObject mainQuestPanel;

    void Awake()
    {
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpwanLocation.transform.position;
        player.transform.rotation = playerSpwanLocation.transform.rotation;
        mainQuestPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMainQuestPanel()
    {
        mainQuestPanel.SetActive(false);
        Time.timeScale = 1;
    }


    void Update()
    {
        
    }
}
