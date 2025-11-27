using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unswallowable : MonoBehaviour, IEatable
{
    public bool BeEaten()
    {
        Destroy(this.gameObject);
        return false;
    }
}
