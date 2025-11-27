using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using System.IO;
using System;

public class DataManager : MonoSingleton<DataManager>
{
    [SerializeField] private VoidEventSO loadGameEvent;
    [SerializeField] private VoidEventSO saveGameEvent;
    private List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;
    private string jsonFolder;

    protected override void Awake()
    {
        base.Awake();

        saveData = new Data();
        jsonFolder = Application.persistentDataPath + "/Save Data/";
        ReadSavedData();
    }
    void OnEnable()
    {
        saveGameEvent.OnEventRaised += Save;
        loadGameEvent.OnEventRaised += Load;
    }
    void OnDisable()
    {
        saveGameEvent.OnEventRaised -= Save;
        loadGameEvent.OnEventRaised -= Load;
    }

    private void Save()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }
        var resultPath = jsonFolder + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveData);
        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(resultPath);
        }
        File.WriteAllText(resultPath, jsonData);
    }
    private void Load()
    {
        foreach (var saveable in saveableList)
        {
            saveable.LoadData(saveData);
        }
    }
    public void RegisterSaveData(ISaveable saveable)
    {
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }
    public void UnRegisterSaveData(ISaveable saveable)
    {
        if (saveableList.Contains(saveable))
        {
            saveableList.Remove(saveable);
        }
    }

    public void ReadSavedData()
    {
        var resultPath = jsonFolder + "data.sav";
        if (File.Exists(resultPath))
        {
            var stringData = File.ReadAllText(resultPath);
            var jsonData = JsonConvert.DeserializeObject<Data>(stringData);
            saveData = jsonData;
        }
    }
}
