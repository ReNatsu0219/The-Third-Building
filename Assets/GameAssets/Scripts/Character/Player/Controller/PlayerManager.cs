using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>, ISaveable
{
    public Player player;
    public float collectionNum;
    public bool meetBoss = false;
    public bool beStrong = false;
    [Header("Event System")]
    [SerializeField] private VoidEventSO playerDeathEvent;
    [SerializeField] private RoomChangeEventSO roomChangeEvent;

    void OnEnable()
    {
        roomChangeEvent.OnEventRaised += OnRoomChangeEvent;
    }
    void OnDisable()
    {
        roomChangeEvent.OnEventRaised -= OnRoomChangeEvent;
    }



    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.floatSavedData[GetDataID().ID] = collectionNum;
        }
        else
        {
            data.floatSavedData.Add(GetDataID().ID, collectionNum);
        }
    }

    public void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            collectionNum = data.floatSavedData[GetDataID().ID];
        }
    }

    #region Event Methods
    private void OnRoomChangeEvent(RoomBase currentRoom)
    {
        if (currentRoom.roomName == "Room_15")
        {
            meetBoss = true;
        }
    }
    #endregion
}
