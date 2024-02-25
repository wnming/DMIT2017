using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;


public static class SaveInfo
{
    public static PlayerInfo playerInfo;
    public static string path = "SaveData";

    public static void SaveInfoToFile()
    {
        playerInfo.spawnLocation = playerInfo.currentLocation;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        Stream stream = File.Open($"{path}/PlayerInfo", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerInfo));
        serializer.Serialize(stream, playerInfo);
        stream.Close();
    }

    public static void DeleteFile()
    {
        if (File.Exists(path + "/PlayerInfo"))
        {
            File.Delete(path + "/PlayerInfo");
        }
    }

    public static void ClearInfo()
    {
        playerInfo = new PlayerInfo();
        playerInfo.spawnLocation = new Vector3(420.6f, 0, 242);
        playerInfo.treasure = 0;
        playerInfo.currentHealth = 50;
        playerInfo.currentScene = "Overworld";
        playerInfo.townInfoList = new List<TownInfo>();

        for(int index = 0; index < 3; index++)
        {
            TownInfo town = new TownInfo();
            town.isTreasureCollected = false;
            town.TownName = $"Town {index + 1}";
            town.enemyHealth = new List<int>();
            town.enemyHealth.Add(30);
            town.enemyHealth.Add(30);
            town.enemyHealth.Add(30);
            playerInfo.townInfoList.Add(town);
        }

    }
}
