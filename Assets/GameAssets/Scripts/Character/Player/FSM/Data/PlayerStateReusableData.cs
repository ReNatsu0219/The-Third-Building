using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusableData
{
    private Player player;

    public float targetHorizontalSpeed;
    public float aclTime;

    public PlayerStateReusableData(Player player)
    {
        this.player = player;
    }
}
