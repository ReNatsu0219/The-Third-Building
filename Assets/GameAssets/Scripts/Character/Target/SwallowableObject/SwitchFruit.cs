using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFruit : Swallowable
{
    public float activationTimeWindow = 3f;
    public bool isActivated;

    void OnEnable()
    {
        SwitchFruit s = this;
        SynchronizedSwitchController.Instance.RegisterSwitch(s);
    }
    void OnDisable()
    {

    }
    protected override void Update()
    {
        if (!canBeEaten && timer >= 0f)
        {
            timer -= Time.deltaTime;
        }
        //同理，请通过动画/图案表示状态
        else if (!canBeEaten)
        {
            r.color = new Color(255f, 255f, 255f, 1f);
            canBeEaten = true;
            isActivated = false;
        }
    }
    public override bool BeEaten()
    {
        isActivated = true;
        return base.BeEaten();
    }

}
