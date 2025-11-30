using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizedSwitchController : MonoSingleton<SynchronizedSwitchController>
{
    public List<SwitchFruit> switches = new List<SwitchFruit>();
    public CollectionFruit TargetFruit;

    private bool isSuccessTriggered = false;

    void Start()
    {
        TargetFruit.gameObject.SetActive(false);
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
        foreach (SwitchFruit switchObj in switches)
        {
            if (!switchObj.isActivated)
            {
                return;
            }
        }
        Debug.Log("大果大果嚼嚼嚼");
        isSuccessTriggered = true;
        TargetFruit.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckisSuccessTriggered();
    }
}