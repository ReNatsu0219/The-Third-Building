using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Unswallowable
{
    public override bool BeEaten()
    {
        this.gameObject.SetActive(false);
        return false;
    }
    protected override void OnDisable()
    {
    }
    void OnDestroy()
    {
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }
    public override void LoadData(Data data)
    {
        base.LoadData(data);
        this.gameObject.SetActive(true);
    }
}
