using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections;

[DefaultExecutionOrder(-8)]
public class DataManager : MonoSingleton<DataManager>
{
    private List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;
    private string jsonFolder;
    [field: Header("Event System")]
    [SerializeField] private VoidEventSO newGameEvent;
    [SerializeField] private VoidEventSO loadGameEvent;
    [SerializeField] private VoidEventSO saveGameEvent;
    [SerializeField] private RoomChangeEventSO roomChangeEvent;

    protected override void Awake()
    {
        base.Awake();

        saveData = new Data();
        jsonFolder = Application.persistentDataPath + "/Save Data/";
        ReadSavedData();
    }
    void OnEnable()
    {
        //saveGameEvent.OnEventRaised += Save;
        loadGameEvent.OnEventRaised += Load;
        newGameEvent.OnEventRaised += OnNewGame;
        roomChangeEvent.OnEventRaised += SaveCurrentRoom;

        //InputManager.Instance.inputActions.Gameplay.Save.started += ctx => saveGameEvent.RaiseEvent();
        InputManager.Instance.inputActions.Gameplay.Load.started += ctx => loadGameEvent.RaiseEvent();
    }
    void OnDisable()
    {
        //saveGameEvent.OnEventRaised -= Save;
        loadGameEvent.OnEventRaised -= Load;
        newGameEvent.OnEventRaised -= OnNewGame;
        roomChangeEvent.OnEventRaised -= SaveCurrentRoom;

        //InputManager.Instance.inputActions.Gameplay.Save.started -= ctx => saveGameEvent.RaiseEvent();
        InputManager.Instance.inputActions.Gameplay.Load.started -= ctx => loadGameEvent.RaiseEvent();
    }

    private void OnNewGame()
    {
        StartCoroutine(NewGameRoutine());
    }
    #region New Game
    IEnumerator NewGameRoutine()
    {
        Debug.Log("开始新游戏流程...");

        // 步骤1: 删除存档文件
        DeleteSaveFile();

        // 步骤2: 清空内存中的存档数据
        ClearSaveData();

        // 步骤3: 通知所有系统重置状态
        yield return StartCoroutine(NotifyAllSystemsReset());

        // 步骤4: 重新加载空数据（可选）
        ReadSavedData(); // 这会加载一个空的saveData

        Debug.Log("新游戏准备完成");
    }
    private void DeleteSaveFile()
    {
        try
        {
            var saveFilePath = jsonFolder + "data.sav";
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
                Debug.Log("存档文件已删除: " + saveFilePath);
            }
            else
            {
                Debug.Log("没有找到存档文件，继续新游戏");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("删除存档文件失败: " + e.Message);
        }
    }

    private void ClearSaveData()
    {
        // 创建全新的空数据对象
        saveData = new Data();
        Debug.Log("内存存档数据已清空");
    }

    private IEnumerator NotifyAllSystemsReset()
    {
        // 通知所有已注册的Saveable对象进行重置
        foreach (var saveable in saveableList.ToArray()) // 使用ToArray避免集合修改异常
        {
            if (saveable is IResettable resettable)
            {
                resettable.OnNewGame();
            }
        }

        // 等待一帧确保所有系统完成重置
        yield return null;
    }
    #endregion

    private void SaveCurrentRoom(RoomBase currentRoom)
    {
        saveData.roomToSave = currentRoom.roomName;
        Save();
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
    }
    private void Load()
    {
        RoomManager.Instance.RequestTransition(saveData.roomToSave);
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
