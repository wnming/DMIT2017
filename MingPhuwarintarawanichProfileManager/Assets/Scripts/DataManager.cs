using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public static class DataManager
{
    public static int currentProfile;
    public static List<ProfileData> allProfiles;
    public static List<LeaderboardData> leaderboardList;

    public static void SaveData()
    {
        for (int index = 0; index < 3; index++)
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

    public static void SaveLeaderboard()
    {
        Stream stream = File.Open($"SaveData\\Leaderboard", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<LeaderboardData>));
        serializer.Serialize(stream, leaderboardList);
        stream.Close();
    }

    public static void LoadLeaderboard()
    {
        if (File.Exists("SaveData\\Leaderboard"))
        {
            Stream stream = File.Open("SaveData\\Leaderboard", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<LeaderboardData>));
            leaderboardList = (List<LeaderboardData>)serializer.Deserialize(stream);
            stream.Close();
        }
        else
        {
            leaderboardList = new List<LeaderboardData>();
        }
    }
}
