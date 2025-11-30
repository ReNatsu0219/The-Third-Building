using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>, ISaveable
{
    public Player player;
    public float collectionNum;
    [Header("Event System")]
    [SerializeField] private VoidEventSO playerDeathEvent;

    void OnEnable()
    {

    }
    void OnDisable()
    {

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
}
