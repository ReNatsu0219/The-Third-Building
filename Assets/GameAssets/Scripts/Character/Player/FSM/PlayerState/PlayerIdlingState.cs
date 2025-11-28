using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdlingState : StateBase
{
    public PlayerIdlingState(Player player) : base(player)
    {
    }

    #region IState Methods
    public override void OnEnter()
    {
        base.OnEnter();

        reusableData.aclTime = 0f;
        AnimationManager.Instance.GetPlayerAnimator().SetBool("isIDIE", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        reusableData.targetHorizontalSpeed = 0f;
    }

    public override void OnPhysicsUpdate()
    {
        if (!Mathf.Approximately(player.Rigidbody.velocity.sqrMagnitude, 0.1f))
            ChangeVelocity();
    }
    public override void OnExit()
    {
        base.OnExit();

        reusableData.aclTime = 0f;
        AnimationManager.Instance.GetPlayerAnimator().SetBool("isIDIE", false);
    }
    #endregion

    #region Reusable Methods
    protected override void AddEventListener()
    {
        base.AddEventListener();

        inputManager.inputActions.Gameplay.Move.started += OnMoveStarted;
        inputManager.inputActions.Gameplay.Jump.started += OnJumpStrted;
        player.isGrounded.OnValueChanged += OnFalling;
    }



    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        inputManager.inputActions.Gameplay.Move.started -= OnMoveStarted;
        inputManager.inputActions.Gameplay.Jump.started -= OnJumpStrted;
        player.isGrounded.OnValueChanged -= OnFalling;
    }
    #endregion

    #region State Mtthods
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MovingState);
    }
    #endregion
}
