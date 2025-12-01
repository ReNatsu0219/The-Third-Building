using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionFruit : Swallowable
{
    private bool isTimeStopped = false;
    [field: SerializeField] private VoidEventSO pauseEvent;
    [field: SerializeField] private RoomChangeEventSO roomChangeEvent;
    [field: SerializeField] private float fallingSpeed;
    public override bool BeEaten()
    {
        //物体不会设置为false，请通过动画/图案表示状态
        if (canBeEaten)
        {
            PlayerManager.Instance.collectionNum++;
            r.color = new Color(255f, 255f, 255f, 0f);
            canBeEaten = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Bite);
            return true;
        }
        else
        {
            return false;
        }
    }
    protected override void Update()
    {
        base.Update();

        if (isTimeStopped)
            return;
        else if (RoomManager.Instance.CurrentRoom.roomName == "Room_12")
        {
            transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        pauseEvent.OnEventRaised += OnPauseEventRaised;
        //roomChangeEvent.OnEventRaised += OnRoomChangeEventRaised;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        pauseEvent.OnEventRaised -= OnPauseEventRaised;
        //roomChangeEvent.OnEventRaised -= OnRoomChangeEventRaised;
    }

    // private void OnRoomChangeEventRaised(RoomBase room)
    // {
    //     if (room.roomName == "Room_12" && originPos != null && originPos != Vector3.zero)
    //     {
    //         transform.position = originPos;
    //     }
    // }

    public override void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = new SerializableVector3(transform.position);
            data.floatSavedData[GetDataID().ID + "state"] = canBeEaten ? 1 : 0;
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, new SerializableVector3(transform.position));
            data.floatSavedData.Add(GetDataID().ID + "state", canBeEaten ? 1 : 0);
        }
    }

    public override void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
            canBeEaten = data.floatSavedData[GetDataID().ID + "state"] == 0 ? false : true;
            r.color = canBeEaten ? new Color(255f, 255f, 255f, 1f) : new Color(255f, 255f, 255f, 0f);
        }
    }

    #region Event Methods
    private void OnPauseEventRaised()
    {
        isTimeStopped = !isTimeStopped;
    }
    #endregion
}
