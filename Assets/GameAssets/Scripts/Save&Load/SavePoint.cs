using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IEatable
{
    public VoidEventSO SaveEventSO;
    public bool isDone;

    public bool BeEaten()
    {
        if (!isDone)
        {
            isDone = true;
            SaveEventSO.RaiseEvent();
        }
        return false;
    }
}
