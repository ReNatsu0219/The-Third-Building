using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-11)]
public class InputManager : MonoSingleton<InputManager>
{
    public PlayerInputAction inputActions;
    public Vector2 MovementInput => inputActions.Gameplay.Move.ReadValue<Vector2>();
    [field: Header("Event Listener")]
    [SerializeField] private VoidEventSO playerDeathEvent;
    [SerializeField] private VoidEventSO loadGameEvent;
    [SerializeField] private VoidEventSO pauseEvent;
    protected override void Awake()
    {
        base.Awake();

        inputActions = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputActions?.Enable();
        //Debug.Log("inputActions Enabled");

        inputActions.Gameplay.Pause.started += ctx => pauseEvent.RaiseEvent();
        playerDeathEvent.OnEventRaised += OnPlayerDeathEvent;
        loadGameEvent.OnEventRaised += PlayerOnLoadGameEvent;
    }

    private void OnDisable()
    {
        inputActions?.Disable();

        inputActions.Gameplay.Pause.started -= ctx => pauseEvent.RaiseEvent();
        playerDeathEvent.OnEventRaised -= OnPlayerDeathEvent;
        loadGameEvent.OnEventRaised -= PlayerOnLoadGameEvent;
    }

    public Vector2 Movement
    {
        get
        {
            Vector2 movement = inputActions.Gameplay.Move.ReadValue<Vector2>();

            if (movement.x > 0) movement.x = 1f;
            else if (movement.x < 0) movement.x = -1f;
            else movement.x = 0f;

            if (movement.y > 0) movement.y = 1f;
            else if (movement.y < 0) movement.y = -1f;
            else movement.y = 0f;

            return movement;
        }
    }

    #region Event Methods
    private void OnPlayerDeathEvent()
    {
        inputActions.Gameplay.Disable();
    }
    private void PlayerOnLoadGameEvent()
    {
        inputActions.Gameplay.Enable();
    }
    #endregion
}
