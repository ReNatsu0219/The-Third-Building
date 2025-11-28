using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpingState : StateBase
{
    public PlayerJumpingState(Player player) : base(player)
    {
    }

    #region IState Methods
    public override void OnEnter()
    {
        base.OnEnter();

        Jump();
        AnimationManager.Instance.GetPlayerAnimator().SetBool("isJumping", true);
    }

    public override void OnExit()
    {
        base.OnExit();
        AnimationManager.Instance.GetPlayerAnimator().SetBool("isJumping", false);
    }
    public override void OnUpdate()
    {
        UpdateTargetHorizontalSpeed();

        if (player.Rigidbody.velocity.y <= player.OnFallingSpeedY)
            stateMachine.ChangeState(stateMachine.FallingState);
    }

    public override void OnPhysicsUpdate()
    {
        ChangeVelocity();
    }
    #endregion
    protected override void AddEventListener()
    {
        base.AddEventListener();

        player.isGrounded.OnValueChanged += OnFallToLand;
        inputManager.inputActions.Gameplay.Jump.started += OnJumpStrted;
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        player.isGrounded.OnValueChanged -= OnFallToLand;
        inputManager.inputActions.Gameplay.Jump.started -= OnJumpStrted;
    }

    private void Jump()
    {
        player.Rigidbody.AddForce(Vector2.up * settingData.JumpForce, ForceMode2D.Impulse);
    }
    protected override void OnJumpStrted(InputAction.CallbackContext context)
    {
        if (BuffManager.Instance.HasBuff<DoubleJumpBuff>())
        {
            BuffManager.Instance.RemoveBuff(BuffManager.Instance.GetBuff<DoubleJumpBuff>());
            Jump();
            AnimationManager.Instance.GetPlayerAnimator().SetBool("isJumping", false);
        }
    }
}
