using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Swallowable : MonoBehaviour, IEatable
{
    public virtual bool BeEaten()
    {
        Destroy(this.gameObject);
        return true;
    }
}
