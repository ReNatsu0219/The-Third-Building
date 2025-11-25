using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInputManager InputManager { get; private set; }


    private void Awake()
    {
        animator = GetComponent<Animator>();

        StateMachine = new PlayerStateMachine();

        InputManager = PlayerInputManager.instance;

        StateMachine.Initialize(this);
    }
}
