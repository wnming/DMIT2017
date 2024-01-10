using System;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using TMPro;

public class SaveLoadTest : MonoBehaviour
{
    [SerializeField] NameData myName;
    [SerializeField] TMP_InputField myInputField;

    [SerializeField] GameObject addProfilePanel;
    [SerializeField] GameObject mainProfileMenuPanel;

    //[SerializeField] TextMeshProUGUI error;

    [SerializeField] GameObject[] profileButtons;

    [SerializeField] int currentProfile;

    private string path = $"SaveData\\Profile1";

    void Start()
    {
        currentProfile = 0;
        myName = new NameData();

        //addProfilePanel.SetActive(false);
        //mainProfileMenuPanel.SetActive(true);
        //LoadData();
    }

    public void ChangeName(string newName)
    {
        myName.playerName = newName;
    }

    //private bool CheckDuplicateName(string name)
    //{
    //    return myNameList.Any(x => x.playerName == name);
    //}

    public void ChangeProfile(int profileNo)
    {
        path = $"SaveData\\Profile{currentProfile + 1}";
        currentProfile = profileNo;
    }

    public void SaveData()
    {
        //error.text = "";
        CreateFile();
        Stream stream = File.Open(path + "\\PlayerData", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(NameData));
        serializer.Serialize(stream, myName);
        stream.Close();
        //if (!CheckDuplicateName(myName.playerName))
        //{
        //    myNameList[myName.no] = myName;
        //    Stream stream = File.Open(path, FileMode.Create);
        //    XmlSerializer serializer = new XmlSerializer(typeof(NameData));
        //    serializer.Serialize(stream, myNameList);
        //    stream.Close();
        //}
        //else
        //{
        //    error.text = $"{myName.playerName} is already exists.";
        //}
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            Stream stream = File.Open(path + "\\PlayerData", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(NameData));
            myName = (NameData)serializer.Deserialize(stream);
            //string json = File.ReadAllText(path);
            //myNameList = JsonConvert.DeserializeObject<List<NameData>>(json);
            //(NameData)serializer.Deserialize<NameData>(stream);
            stream.Close();
            myInputField.text = myName.playerName;
        }
    }

    void CreateFile()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    //public void SaveData()
    //{
    //    Stream stream = File.Open("PlayerData", FileMode.Create);
    //    XmlSerializer serializer = new XmlSerializer(typeof(NameData));
    //    serializer.Serialize(stream, myName);
    //    stream.Close();
    //}

    //public void LoadData()
    //{
    //    Stream stream = File.Open("PlayerData", FileMode.Open);
    //    XmlSerializer serializer = new XmlSerializer(typeof(NameData));
    //    myName = (NameData)serializer.Deserialize(stream);
    //    stream.Close();

    //    myInputField.text = myName.playerName;
    //}

}

[Serializable]
public struct NameData
{
    public string playerName;
}