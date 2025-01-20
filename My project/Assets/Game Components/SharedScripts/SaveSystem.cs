using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    private static string saveFilePath  =  Application.persistentDataPath + "/savefile.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists((saveFilePath)))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        else
        {
            {
                Debug.Log("no faile found");
                return null;
            }
        }
    }

    public static bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}
