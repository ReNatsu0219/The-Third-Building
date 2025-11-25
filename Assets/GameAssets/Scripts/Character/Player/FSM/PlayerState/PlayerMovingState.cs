using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovingState : StateBase
{
    private Vector2 InputMovement;
    private Vector2 TargetMovement;
    private Vector2 smoothVel;

    public PlayerMovingState(Player player) : base(player)
    {

    }

    public override void OnEnter()
    {
        Debug.Log(player.InputManager.Movement);
    }

    public override void OnUpdate()
    {
        InputMovement = player.InputManager.Movement;
        InputMovement.y = 0;
        TargetMovement = InputMovement*player.PlayerSpeed;
        if (TargetMovement == Vector2.zero)
            player.StateMachine.ChangeState(player.StateMachine.IdlingState);
    }

    public override void OnPhysicsUpdate()
    {
        player.Regidbody.velocity =
       Vector2.SmoothDamp(
           player.Regidbody.velocity,
           TargetMovement,
           ref smoothVel,
           player.velocitySmooth
       );
    }

    public override void OnExit()
    {

    }
}
