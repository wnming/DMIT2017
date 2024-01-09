using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TMPro;

public class SaveLoadTest : MonoBehaviour
{
    [SerializeField] NameData myName;
    [SerializeField] TMP_InputField myInputField;
    // Start is called before the first frame update
    void Start()
    {
        myName = new NameData();

        LoadData();
    }

    public void ChangeName(string newName)
    {
        myName.playerName = newName;
    }

    public void SaveData()
    {
        Stream stream = File.Open("PlayerData", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(NameData));
        serializer.Serialize(stream, myName);
        stream.Close();
    }

    public void LoadData()
    {
        Stream stream = File.Open("PlayerData", FileMode.Open);
        XmlSerializer serializer = new XmlSerializer(typeof(NameData));
        myName = (NameData)serializer.Deserialize(stream);
        stream.Close();

        myInputField.text = myName.playerName;
    }

}

[Serializable]
public struct NameData
{
    public string playerName;
}