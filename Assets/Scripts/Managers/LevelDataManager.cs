using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class LevelDataManager : MonoBehaviour
{
    public string savedFile;
    public LevelData levelData = new LevelData();

    public static LevelDataManager Instance
    {
        get { return instance; }
    }
    private static LevelDataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        savedFile = Application.dataPath + "/levelData.json";
        LoadData();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void LoadData()
    {
        if (File.Exists(savedFile))
        {
            string data = File.ReadAllText(savedFile);
            levelData = JsonConvert.DeserializeObject<LevelData>(data);
        }
        else
        {
            levelData = new LevelData() 
            { 
                maxLevelReached = 1 
            };
            SaveData();
        }
    }

    public void SaveData()
    {
        LevelData newData = new LevelData()
        {
            maxLevelReached = levelData.maxLevelReached,
        };

        string jsonString = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(savedFile, jsonString);
    }

    public void NextLevel()
    {
        levelData.maxLevelReached++;
        SaveData();
    }
}
