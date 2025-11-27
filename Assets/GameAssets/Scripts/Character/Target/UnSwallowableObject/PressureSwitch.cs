using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Unswallowable
{
    public float activationTimeWindow = 3f;
    public bool isActivated = false;
    public override bool BeEaten()
    {

        return false;
    }

}
