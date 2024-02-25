using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button resumeButton;

    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    public void LoadGame()
    {
        //Load game
        if (File.Exists(SaveInfo.path + "/PlayerInfo"))
        {
            Stream stream = File.Open(SaveInfo.path + "/PlayerInfo", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerInfo));
            SaveInfo.playerInfo = (PlayerInfo)serializer.Deserialize(stream);
            stream.Close();
            resumeButton.interactable = true;
        }
        else
        {
            resumeButton.interactable = false;
            SaveInfo.playerInfo = new PlayerInfo();
        }
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene(SaveInfo.playerInfo.currentScene);
    }

    public void NewGame()
    {
        SaveInfo.DeleteFile();
        SaveInfo.ClearInfo();
        SceneManager.LoadScene("Overworld");
    }
}
