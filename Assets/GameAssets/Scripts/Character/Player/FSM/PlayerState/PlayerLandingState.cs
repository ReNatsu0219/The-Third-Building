using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : StateBase
{
    public PlayerLandingState(Player player) : base(player)
    {
    }

    public override void OnUpdate()
    {
        UpdateTargetHorizontalSpeed();
    }

    public override void OnPhysicsUpdate()
    {
        ChangeVelocity();
    }

    protected override void AddEventListener()
    {
        base.AddEventListener();

        inputManager.inputActions.Gameplay.Jump.started += OnJumpStrted;
        player.isGrounded.OnValueChanged += OnFalling;
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        inputManager.inputActions.Gameplay.Jump.started -= OnJumpStrted;
        player.isGrounded.OnValueChanged -= OnFalling;
    }
}
