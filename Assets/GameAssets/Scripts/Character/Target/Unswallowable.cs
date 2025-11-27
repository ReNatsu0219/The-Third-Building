using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unswallowable : MonoBehaviour, IEatable
{
    public virtual bool BeEaten()
    {
        Destroy(this.gameObject);
        return false;
    }
}
