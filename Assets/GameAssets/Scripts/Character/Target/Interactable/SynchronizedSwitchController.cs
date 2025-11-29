using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizedSwitchController : MonoSingleton<SynchronizedSwitchController>
{
    public List<SwitchFruit> switches = new List<SwitchFruit>();

    private bool isSuccessTriggered = false;

    void Start()
    {
    }

    public void RegisterSwitch(SwitchFruit s)
    {
        if (!switches.Contains(s))
        {
            switches.Add(s);
        }
    }
    public void UnRegisterSwitch(SwitchFruit s)
    {
        if (switches.Contains(s))
        {
            switches.Remove(s);
        }
    }
    public void RemoveAllSwitchs()
    {
        foreach (var switchObj in switches)
        {
            switches.Remove(switchObj);
        }
    }
    public void CheckisSuccessTriggered()
    {
        foreach (var switchObj in switches)
        {
            if (!switchObj.isActivated)
            {
                isSuccessTriggered = true;
            }
        }
    }
}
