using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : Breakable
{
    public override bool BeEaten()
    {
        PlayerManager.Instance.ropeBreak = true;
        PlayerManager.Instance.woodDown.SetActive(true);
        PlayerManager.Instance.woodUp.SetActive(false);
        return base.BeEaten();
    }
    // public override void GetSaveData(Data data)
    // {
    //     if (data.characterPosDict.ContainsKey(GetDataID().ID))
    //     {
    //         data.characterPosDict[GetDataID().ID] = new SerializableVector3(transform.position);
    //         data.floatSavedData[GetDataID().ID + "state"] = canBeEaten ? 1 : 0;
    //     }
    //     else
    //     {
    //         data.characterPosDict.Add(GetDataID().ID, new SerializableVector3(transform.position));
    //         data.floatSavedData.Add(GetDataID().ID + "state", canBeEaten ? 1 : 0);
    //     }
    // }

    // public override void LoadData(Data data)
    // {
    //     if (data.characterPosDict.ContainsKey(GetDataID().ID))
    //     {
    //         transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
    //         canBeEaten = data.floatSavedData[GetDataID().ID + "state"] == 0 ? false : true;
    //         r.color = canBeEaten ? new Color(255f, 255f, 255f, 1f) : new Color(255f, 255f, 255f, 0f);
    //     }
    // }
}
