using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpBuff : Buff
{
    public DoubleJumpBuff(string name, float duration) : base(name, duration)
    {
    }

    public override void OnApply()
    {
        base.OnApply();
    }
}
[Serializable]
public class SpeedUpBuff : Buff
{
    public float speedMultiplier = 2f;
    public SpeedUpBuff(string name, float duration) : base(name, duration)
    {
    }

    public override void OnApply()
    {
        base.OnApply();
        PlayerManager.Instance.player.ReusableData.horizontalSpeedMultiplier *= speedMultiplier;
    }
    public override void OnRemove()
    {
        base.OnRemove();
        PlayerManager.Instance.player.ReusableData.horizontalSpeedMultiplier /= speedMultiplier;
    }
}
