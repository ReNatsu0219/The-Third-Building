using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private Player player;

    public PlayerIdlingState IdlingState { get; private set; }
    public PlayerMovingState MovingState { get; private set; }
    public PlayerJumpingState JumpingState { get; private set; }
    public PlayerLandingState LandingState { get; private set; }
    public PlayerFallingState FallingState { get; private set; }
    public PlayerSwallowingState SwallowingState { get; private set; }
    public PlayerChewingState ChewingState { get; private set; }

    public void Initialize(Player player)
    {
        this.player = player;

        IdlingState = new PlayerIdlingState(player);
        MovingState = new PlayerMovingState(player);
        JumpingState = new PlayerJumpingState(player);
        LandingState = new PlayerLandingState(player);
        FallingState = new PlayerFallingState(player);
        SwallowingState = new PlayerSwallowingState(player);
        ChewingState = new PlayerChewingState(player);

        ChangeState(IdlingState);
    }
}
