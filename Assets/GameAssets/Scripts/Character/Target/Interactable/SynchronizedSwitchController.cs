using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizedSwitchController : MonoBehaviour
{
    public List<Switch> switches = new List<Switch>();

    private Coroutine activationCheckCoroutine;
    private HashSet<Switch> activatedSwitches = new HashSet<Switch>();
    private bool isSuccessTriggered = false;

    void Start()
    {
        foreach (var switchObj in switches)
        {

        }
    }
}
