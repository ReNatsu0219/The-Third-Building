using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpFruit : Swallowable
{
    [SerializeField] private SpeedUpBuff speedUpBuff;
    protected override void OnEnable()
    {
        speedUpBuff = new SpeedUpBuff("SpeedUpBuff", -1f);
    }
    public override bool BeEaten()
    {
        if (canBeEaten)
            BuffManager.Instance.Addbuff(speedUpBuff);
        return base.BeEaten();
    }
}
