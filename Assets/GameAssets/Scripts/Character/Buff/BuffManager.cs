using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoSingleton<BuffManager>
{
    private List<Buff> activeBuffs = new List<Buff>();
    public VoidEventSO loadDataEvent;

    void OnEnable()
    {
        loadDataEvent.OnEventRaised += RemoveAllBuff;
    }
    void OnDisable()
    {
        loadDataEvent.OnEventRaised -= RemoveAllBuff;
    }

    private void RemoveAllBuff()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            var buff = activeBuffs[i];

            buff.OnRemove();
        }
    }

    void Update()
    {
        UpdateBuffs(Time.deltaTime);
    }
    public void Addbuff(Buff newBuff)
    {
        var existingBuff = activeBuffs.Find(b => b.BuffName == newBuff.BuffName);
        if (existingBuff != null)
        {
            RemoveBuff(existingBuff);
        }
        newBuff.OnApply();
        activeBuffs.Add(newBuff);
    }
    private void UpdateBuffs(float deltaTime)
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            var buff = activeBuffs[i];

            buff.OnUpdate(deltaTime);
            if (buff.IsFinished())
            {
                RemoveBuff(buff);
            }
        }
    }

    public void RemoveBuff(Buff buff)
    {
        buff.OnRemove();
        activeBuffs.Remove(buff);
    }
    public bool HasBuff<T>() where T : Buff => activeBuffs.Exists(b => b is T);
    public T GetBuff<T>() where T : Buff
    {
        return activeBuffs.Find(b => b is T) as T;
    }
}
