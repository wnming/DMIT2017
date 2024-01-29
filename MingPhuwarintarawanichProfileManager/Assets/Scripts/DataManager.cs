using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public static class DataManager
{
    public static int currentProfile;
    public static List<ProfileData> allProfiles;

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
}
