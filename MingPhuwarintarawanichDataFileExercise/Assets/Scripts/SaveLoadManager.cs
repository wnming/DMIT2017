using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] HighScoreData nameScoreData;
    private HighScoreData currentHighScoreData;
    [SerializeField] TMP_InputField myInputField;

    [SerializeField] Button startButton;

    [SerializeField] TextMeshProUGUI highScoreData;
    [SerializeField] TextMeshProUGUI currentClick;
    [SerializeField] TextMeshProUGUI endGameText;

    private string path = $"SaveData\\HighScore";
    private string highScorePath = $"SaveData\\HighScore\\HighScoreData";

    private int currentCountDownValue;
    private bool isStartGame;

    private float timeLeft;

    void Start()
    {
        highScorePath = $"{path}\\HighScoreData";

        nameScoreData = new HighScoreData();
        currentHighScoreData = new HighScoreData();

        startButton.interactable = false;
        isStartGame = false;
        timeLeft = 10.0f;

        LoadHighScoreData();
    }

    public void Update()
    {
        currentClick.text = $"Current click: {nameScoreData.Score}";
        if (isStartGame)
        {
            startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Click!";
            timeLeft -= Time.deltaTime;
            if (timeLeft > 0)
            {
                startButton.GetComponent<Button>().onClick.RemoveAllListeners();
                startButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    nameScoreData.Score += 1;
                });
            }
            else
            {
                isStartGame = false;
                startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Saving...";
                startButton.interactable = false;
                SaveHighScoreData();
            }
        }
        if (Input.GetKey("escape"))
        {
            QuitApplication();
        }
    }

    public void ChangeName(string newName)
    {
        nameScoreData.PlayerName = newName;
        if (!string.IsNullOrEmpty(newName))
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    private void QuitApplication()
    {
        Application.Quit();
    }

    public void SaveHighScoreData()
    {
        if (nameScoreData.Score > currentHighScoreData.Score)
        {
            highScoreData.text = $"{nameScoreData.PlayerName}   {nameScoreData.Score}";
            CreateFile();
            Stream stream = File.Open(highScorePath, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
            serializer.Serialize(stream, nameScoreData);
            stream.Close();
        }
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Saved";
        endGameText.text = "Esc to close the game";
    }

    public void LoadHighScoreData()
    {
        if (File.Exists(highScorePath))
        {
            Stream stream = File.Open(highScorePath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
            currentHighScoreData = (HighScoreData)serializer.Deserialize(stream);
            stream.Close();
            highScoreData.text = $"{currentHighScoreData.PlayerName}   {currentHighScoreData.Score}";
        }
    }

    IEnumerator CountDownStartingGame(int countDownValue = 3)
    {
        currentCountDownValue = countDownValue;
        while (currentCountDownValue > 0)
        {
            startButton.GetComponentInChildren<TextMeshProUGUI>().text = currentCountDownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            currentCountDownValue--;
        }
        isStartGame = true;
        startButton.interactable = true;
    }
    public void StartGame()
    {
        if (!isStartGame) 
        {
            isStartGame = false;
            startButton.interactable = false;
            myInputField.interactable = false;
            StartCoroutine(CountDownStartingGame());
        }
    }

    void CreateFile()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}

[Serializable]
public struct HighScoreData
{
    public string PlayerName;
    public int Score;
}
