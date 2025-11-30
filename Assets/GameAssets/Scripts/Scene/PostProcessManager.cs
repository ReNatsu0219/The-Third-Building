using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoSingleton<PostProcessManager>
{
    [field: Header("Event System")]
    [SerializeField] private FloatEventSO brightnessChangeEvent;
    [SerializeField] private RoomChangeEventSO roomChangeEvent;
    [field: Header("Post-Processing")]
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Volume darkRoom1Volume;
    private Volume currentVolume;
    private ColorAdjustments colorAdjustments;
    private string currentRoom;

    private float brightnessValue;
    protected override void Awake()
    {
        base.Awake();

        SwitchCurrentVolume(globalVolume);
    }
    void OnEnable()
    {
        brightnessChangeEvent.OnEventRaised += OnbrightnessChangeEvent;
        roomChangeEvent.OnEventRaised += OnRoomChangeEvent;
    }
    void OnDisable()
    {
        brightnessChangeEvent.OnEventRaised -= OnbrightnessChangeEvent;
        roomChangeEvent.OnEventRaised -= OnRoomChangeEvent;
    }

    private void SwitchCurrentVolume(Volume volume)
    {
        if (currentVolume != null)
            currentVolume.weight = 0f;
        currentVolume = volume;
        currentVolume.weight = 1f;

        if (currentVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            if (!colorAdjustments.postExposure.overrideState)
            {
                colorAdjustments.postExposure.overrideState = true;
            }
        }
    }

    #region Event Methods
    private void OnbrightnessChangeEvent(float value)
    {
        brightnessValue = value;

        if (currentRoom != "Room_04" && currentRoom != "Room_05")
            colorAdjustments.postExposure.value = value;
        else if (currentRoom == "Room_04" || currentRoom == "Room_05")
            colorAdjustments.postExposure.value = (10f * (brightnessValue - 1f));
    }
    private void OnRoomChangeEvent(RoomBase currentRoom)
    {
        this.currentRoom = currentRoom.roomName;
        if (currentRoom.roomName != "Room_04" && currentRoom.roomName != "Room_05")
        {
            SwitchCurrentVolume(globalVolume);
            OnbrightnessChangeEvent(brightnessValue);
        }
        else if (currentRoom.roomName == "Room_04" || currentRoom.roomName == "Room_05")
        {
            SwitchCurrentVolume(darkRoom1Volume);
            OnbrightnessChangeEvent(10f * (brightnessValue - 1f));
        }

    }
    #endregion
}
