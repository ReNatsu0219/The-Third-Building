using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProofOfStrength : Swallowable
{
    public override bool BeEaten()
    {
        //物体不会设置为false，请通过动画/图案表示状态
        if (canBeEaten && PlayerManager.Instance.meetBoss)
        {
            PlayerManager.Instance.beStrong = true;
            PlayerManager.Instance.strength.gameObject.SetActive(true);
            r.color = new Color(255f, 255f, 255f, 0f);
            canBeEaten = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = new SerializableVector3(transform.position);
            data.floatSavedData[GetDataID().ID + "state"] = canBeEaten ? 1 : 0;
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, new SerializableVector3(transform.position));
            data.floatSavedData.Add(GetDataID().ID + "state", canBeEaten ? 1 : 0);
        }
    }

    public override void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
            canBeEaten = data.floatSavedData[GetDataID().ID + "state"] == 0 ? false : true;
            r.color = canBeEaten ? new Color(255f, 255f, 255f, 1f) : new Color(255f, 255f, 255f, 0f);
        }
    }
}
