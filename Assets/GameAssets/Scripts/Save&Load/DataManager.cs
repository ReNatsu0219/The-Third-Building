using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[DefaultExecutionOrder(-10)]
public class DataManager : MonoSingleton<DataManager>
{
    private List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;
    private string jsonFolder;
    [field: Header("Event System")]
    [SerializeField] private VoidEventSO loadGameEvent;
    [SerializeField] private VoidEventSO saveGameEvent;

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

        InputManager.Instance.inputActions.Gameplay.Save.started += ctx => saveGameEvent.RaiseEvent();
        InputManager.Instance.inputActions.Gameplay.Load.started += ctx => loadGameEvent.RaiseEvent();
    }
    void OnDisable()
    {
        saveGameEvent.OnEventRaised -= Save;
        loadGameEvent.OnEventRaised -= Load;

        InputManager.Instance.inputActions.Gameplay.Save.started -= ctx => saveGameEvent.RaiseEvent();
        InputManager.Instance.inputActions.Gameplay.Load.started -= ctx => loadGameEvent.RaiseEvent();
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
            Directory.CreateDirectory(jsonFolder);
        }
        File.WriteAllText(resultPath, jsonData);
        //Debug.Log(jsonData);

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
