using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StateBase : IState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected Animator animator;
    protected InputManager inputManager;
    public StateBase(Player player)
    {
        this.player = player;
        stateMachine = player.StateMachine;
        animator = player.animator;
        inputManager = player.InputManager;
    }
    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual void OnPhysicsUpdate()
    {

    }

    public virtual void OnUpdate()
    {

    }
}
