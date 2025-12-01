using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Unswallowable
{
    public string layerOrigin;
    public int originLayer;
    public string layerIgnored = "Ignore Raycast";
    private int ignoreLayer;

    protected override void Awake()
    {
        base.Awake();
        layerIgnored = "Ignore Raycast";
        layerOrigin = "Environment";
        if (layerIgnored != null)
            ignoreLayer = LayerMask.NameToLayer(layerIgnored);

        originLayer = LayerMask.NameToLayer(layerOrigin);
    }
    public override bool BeEaten()
    {
        if (ignoreLayer != 0f)
            this.gameObject.layer = ignoreLayer;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Break);
        return base.BeEaten();
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
            this.gameObject.layer = canBeEaten ? originLayer : ignoreLayer;
        }
    }

}
