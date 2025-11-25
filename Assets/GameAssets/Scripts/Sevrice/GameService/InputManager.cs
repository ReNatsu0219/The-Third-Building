using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public PlayerInputAction inputActions;
    public Vector2 MovementInput => inputActions.Gameplay.Move.ReadValue<Vector2>();
    protected override void Awake()
    {
        base.Awake();

        inputActions = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputActions?.Enable();
    }
    private void OnDisable()
    {
        inputActions?.Disable();
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
}
