using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject addProfileDetail;
    [SerializeField] GameObject confirmDeleteProfile;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject deleteButton;
    [SerializeField] TextMeshProUGUI confirmDeleteText;
    [SerializeField] List<TextMeshProUGUI> profileButtons;

    public TMP_InputField nameField;
    public TMP_Dropdown typeDropdown;
    public TMP_Dropdown colorDropdown;
    public TMP_InputField highScoreField;

    void Start()
    {
        playButton.SetActive(false);
        addProfileDetail.SetActive(false);
        confirmDeleteProfile.SetActive(false);
        DataManager.currentProfile = 0;
        highScoreField.interactable = false;
        DataManager.allProfiles = new List<ProfileData>();
        DataManager.LoadLeaderboard();
        LoadData();
    }

    void Update()
    {
        playButton.SetActive(!string.IsNullOrEmpty(nameField.text) ? true : false);
    }

    public void LoadData()
    {
        for(int index = 0; index < 3; index++)
        {
            string tempPath = $"SaveData\\Profile{index}\\PlayerData";
            if (File.Exists(tempPath))
            {
                Stream stream = File.Open(tempPath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(ProfileData));
                DataManager.allProfiles.Add((ProfileData)serializer.Deserialize(stream));
                stream.Close();
            }
            else
            {
                DataManager.allProfiles.Add(new ProfileData());
            }
            profileButtons[index].text = DataManager.allProfiles[index].name;
        }
    }

    public void SaveData()
    {
        DataManager.SaveData();
    }

    public void DeleteProfile()
    {
        confirmDeleteText.text = $"Are you sure you want to delete '{DataManager.allProfiles[DataManager.currentProfile].name}'?";
        confirmDeleteProfile.SetActive(true);
    }

    public void Delete()
    {
        DataManager.allProfiles[DataManager.currentProfile] = new ProfileData();
        profileButtons[DataManager.currentProfile].text = DataManager.allProfiles[DataManager.currentProfile].name;
        SaveData();
        UpdateFields();
        confirmDeleteProfile.SetActive(false);
    }

    public void RejectDelete()
    {
        confirmDeleteProfile.SetActive(false);
    }

    public void UpdateFields()
    {
        nameField.text = DataManager.allProfiles[DataManager.currentProfile].name.ToLower() == "empty" ? "" : DataManager.allProfiles[DataManager.currentProfile].name;
        typeDropdown.value = DataManager.allProfiles[DataManager.currentProfile].vehicleTypeIndex;
        colorDropdown.value = DataManager.allProfiles[DataManager.currentProfile].vehicleColorIndex;
        highScoreField.text = $"Best time: {DataManager.allProfiles[DataManager.currentProfile].highscore.ToString()}";
    }

    public void ChangeProfile(int profileNo)
    {
        DataManager.currentProfile = profileNo;

        UpdateFields();

        addProfileDetail.SetActive(true);
    }

    public void ChangeName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            deleteButton.SetActive(true);
            DataManager.allProfiles[DataManager.currentProfile].name = name;
            profileButtons[DataManager.currentProfile].text = name;
            SaveData();
        }
        else
        {
            deleteButton.SetActive(false);
        }
    }
    public void ChangeVehicleType(int vehicleTypeIndex)
    {
        DataManager.allProfiles[DataManager.currentProfile].vehicleTypeIndex = vehicleTypeIndex;
        SaveData();
    }
    public void ChangeColor(int vehicleColorIndex)
    {
        DataManager.allProfiles[DataManager.currentProfile].vehicleColorIndex = vehicleColorIndex;
        SaveData();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}