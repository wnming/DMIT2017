using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public SaveContainer myContainer;

    public Button[] profileButtons;
    public Button playButton;
    public Button deleteButton;

    public Text[] leaderText;

    public InputField nameField;
    public Dropdown colorDropdown;
    public Dropdown shapeDropdown;
    public Text scoreText;
    public int index;
    // Use this for initialization
    void Start()
    {
        myContainer = new SaveContainer();
        LoadData();
        //index out of range
        index = -1;
        playButton.interactable = false;
        deleteButton.interactable = false;
    }

    public void LoadData()
    {
        // If the XML file exists then load the data.
        if (File.Exists("SaveFiles/Profiles.xml"))
        {
            Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
            myContainer = serializer.Deserialize(stream) as SaveContainer;
            stream.Close();

            for (int i = 0; i < myContainer.leaders.Length; i++)
            {
                leaderText[i].text = (i + 1) + ": " + myContainer.leaders[i].GetName() + "  " + myContainer.leaders[i].GetScore();
            }
        }
        else
        {
            ClearLeaderBoard();
        }

        UpdateProfileButtons();
    }

    public void SaveData()
    {
        if (!Directory.Exists("SaveFiles"))
        {
            Directory.CreateDirectory("SaveFiles");
        }
        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, myContainer);
        stream.Close();
    }

    public void SelectProfile(int buttonIndex)
    {
        // If the profile button pressed does not yet have a profile associated with it then add a new profile.
        if (buttonIndex > myContainer.players.Count - 1)
        {
            // Add a profile and set the index to that profile.
            myContainer.AddProfile();
            index = myContainer.players.Count - 1;
            myContainer.currentIndex = index;

            UpdateProfileButtons();
        }
        // Otherwise just select the profile
        else
        {
            // Set the index to the profile selected and update the profile info.
            index = buttonIndex;
            myContainer.currentIndex = index;
        }
        nameField.text = myContainer.players[index].GetName();
        colorDropdown.value = myContainer.players[index].GetColor();
        shapeDropdown.value = myContainer.players[index].GetShape();
        scoreText.text = "Highscore: " + myContainer.players[index].GetScore();

        playButton.interactable = true;
        deleteButton.interactable = true;
    }

    public void DeleteProfile()
    {
        if (index < myContainer.players.Count)
        {
            // Remove the selected profile.
            myContainer.players.RemoveAt(index);

            UpdateProfileButtons();
        }
    }

    void UpdateProfileButtons()
    {
        // Set all of the profile buttons active state to false.
        for (int i = 0; i < profileButtons.Length; i++)
        {
            profileButtons[i].gameObject.SetActive(false);
        }

        // For each loaded profile activate the profile button and change the text to the name of the profile.
        for (int i = 0; i < myContainer.players.Count; i++)
        {
            profileButtons[i].gameObject.SetActive(true);
            profileButtons[i].GetComponentInChildren<Text>().text = myContainer.players[i].GetName();
        }

        // If the number of profiles loaded is less than the profile buttons available then activate the next
        // profile button and set the text to "Add Profile".
        if (myContainer.players.Count < 5)
        {
            profileButtons[myContainer.players.Count].gameObject.SetActive(true);
            profileButtons[myContainer.players.Count].GetComponentInChildren<Text>().text = "Add Profile";
        }

        if (myContainer.players.Count == 0)
        {
            nameField.interactable = false;
            colorDropdown.interactable = false;
            shapeDropdown.interactable = false;
        }
        else
        {
            nameField.interactable = true;
            colorDropdown.interactable = true;
            shapeDropdown.interactable = true;
        }
        SaveData();
    }

    public void ChangeName(string changeName)
    {
        myContainer.players[index].SetName(changeName);
        UpdateProfileButtons();
    }
    public void ChangeColor(int changeColor)
    {
        myContainer.players[index].SetColor(changeColor);
        UpdateProfileButtons();
    }
    public void ChangeShape(int changeShape)
    {
        myContainer.players[index].SetShape(changeShape);
        UpdateProfileButtons();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ClearLeaderBoard()
    {
        for (int i = 0; i < myContainer.leaders.Length; i++)
        {
            myContainer.leaders[i].SetName("Empty");
            myContainer.leaders[i].SetScore(0);
            leaderText[i].text = (i + 1) + ": " + myContainer.leaders[i].GetName() + "  " + myContainer.leaders[i].GetScore();

            SaveData();
        }
    }
}
