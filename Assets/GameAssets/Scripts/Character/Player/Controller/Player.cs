using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine StateMachine { get; private set; }
    public InputManager InputManager { get; private set; }
    public Rigidbody2D Regidbody { get; private set; }

    public float PlayerSpeed;

    public float velocitySmooth;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        Regidbody=GetComponent<Rigidbody2D>();

        StateMachine = new PlayerStateMachine();

        InputManager = InputManager.Instance;

        StateMachine.Initialize(this);
    }

    private void Update()
    {
        StateMachine.OnUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.OnPhysicsUpdate();
    }
}
