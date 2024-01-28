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
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject addProfileDetail;
    [SerializeField] GameObject confirmDeleteProfile;
    [SerializeField] GameObject playButton;
    [SerializeField] TextMeshProUGUI addDeleteButton;
    [SerializeField] TextMeshProUGUI confirmDeleteText;
    [SerializeField] List<TextMeshProUGUI> profileButtons;
    public int currentProfile;
    public List<ProfileData> allProfiles;
    string path;

    public TMP_InputField nameField;
    public TMP_Dropdown typeDropdown;
    public TMP_Dropdown colorDropdown;
    public TMP_InputField highScoreField;

    // Start is called before the first frame update
    void Start()
    {
        playButton.SetActive(false);
        addProfileDetail.SetActive(false);
        confirmDeleteProfile.SetActive(false);
        currentProfile = 0;
        highScoreField.interactable = false;
        allProfiles = new List<ProfileData>();
        LoadData();
    }

    // Update is called once per frame
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
                allProfiles.Add((ProfileData)serializer.Deserialize(stream));
                stream.Close();
            }
            else
            {
                allProfiles.Add(new ProfileData());
            }
            profileButtons[index].text = allProfiles[index].name;
        }
    }

    public void SaveData()
    {
        for(int index = 0; index < 3; index++)
        {
            if (!Directory.Exists($"SaveData\\Profile{index}"))
            {
                Directory.CreateDirectory($"SaveData\\Profile{index}");
            }
            Stream stream = File.Open($"SaveData\\Profile{index}\\PlayerData", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(ProfileData));
            serializer.Serialize(stream, allProfiles[index]);
            stream.Close();
        }
    }

    public void DeleteProfile()
    {
        confirmDeleteText.text = $"Are you sure you want to delete '{allProfiles[currentProfile].name}'?";
        confirmDeleteProfile.SetActive(true);
    }

    public void Delete()
    {
        allProfiles[currentProfile] = new ProfileData();
        profileButtons[currentProfile].text = allProfiles[currentProfile].name;
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
        nameField.text = allProfiles[currentProfile].name.ToLower() == "empty" ? "" : allProfiles[currentProfile].name;
        typeDropdown.value = allProfiles[currentProfile].vehicleTypeIndex;
        colorDropdown.value = allProfiles[currentProfile].vehicleColorIndex;
        highScoreField.text = $"Best time: {allProfiles[currentProfile].highscore.ToString()}";
    }

    public void ChangeProfile(int profileNo)
    {
        currentProfile = profileNo;

        UpdateFields();

        addProfileDetail.SetActive(true);
        addDeleteButton.text = "Delete";
    }

    public void ChangeName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            allProfiles[currentProfile].name = name;
            profileButtons[currentProfile].text = name;
            SaveData();
        }
    }
    public void ChangeVehicleType(int vehicleTypeIndex)
    {
        allProfiles[currentProfile].vehicleTypeIndex = vehicleTypeIndex;
        SaveData();
    }
    public void ChangeColor(int vehicleColorIndex)
    {
        allProfiles[currentProfile].vehicleColorIndex = vehicleColorIndex;
        SaveData();
    }
}