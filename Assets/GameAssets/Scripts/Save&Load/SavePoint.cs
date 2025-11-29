using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public VoidEventSO SaveEventSO;
    public bool isDone = false;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !isDone)
        {
            isDone = true;
            SaveEventSO.RaiseEvent();
        }
    }
}
