using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    }
    public override void OnUpdate()
    {
        UpdateTargetHorizontalSpeed();

        if (player.Rigidbody.velocity.y <= player.OnFallingSpeedY) ;
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
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        player.isGrounded.OnValueChanged -= OnFallToLand;
    }

    private void Jump()
    {
        player.Rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
}
