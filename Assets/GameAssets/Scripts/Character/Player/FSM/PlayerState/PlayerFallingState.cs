using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : StateBase
{
    public PlayerFallingState(Player player) : base(player)
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

        player.isGrounded.OnValueChanged += OnFallToLand;
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        player.isGrounded.OnValueChanged -= OnFallToLand;
    }
}
