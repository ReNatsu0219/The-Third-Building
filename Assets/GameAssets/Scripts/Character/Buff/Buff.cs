using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public String BuffName { get; private set; }
    public float Duration { get; private set; }

    public bool IsPermanent => Duration <= 0;

    public Buff(string name, float duration)
    {
        BuffName = name;
        Duration = duration;
    }
    public virtual void OnApply()
    {

    }
    public virtual void OnUpdate(float deltaTime)
    {
        if (!IsPermanent)
        {
            Duration -= deltaTime;
        }
    }
    public virtual void OnRemove()
    {

    }
    public virtual bool IsFinished() => !IsPermanent && Duration <= 0;
}
