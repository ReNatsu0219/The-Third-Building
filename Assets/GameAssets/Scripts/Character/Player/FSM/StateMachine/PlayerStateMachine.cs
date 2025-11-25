using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private Player player;

    public PlayerIdlingState IdlingState { get; private set; }

    public void Initialize(Player player)
    {
        this.player = player;

        IdlingState = new PlayerIdlingState(player);
    }
}
