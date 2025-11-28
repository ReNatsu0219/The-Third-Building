using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpFruit : Swallowable
{
    private DoubleJumpBuff doubleJumpBuff;
    void OnEnable()
    {
        doubleJumpBuff = new DoubleJumpBuff("DoubleJumpBuff", -1);
    }
    public override bool BeEaten()
    {
        if (canBeEaten)
            BuffManager.Instance.Addbuff(doubleJumpBuff);
        return base.BeEaten();
    }
}
