using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionFruit : Swallowable, ISaveable
{
    private DataDefination dataID;
    private bool isEaten = false;

    private void Awake()
    {
        dataID = GetComponent<DataDefination>();
    }

    public override bool BeEaten()
    {
        if (canBeEaten)
        {
            isEaten = true;
            canBeEaten = false;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.Bite);

            gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public override void GetSaveData(Data data)
    {
        string id = dataID.ID;

        data.floatSavedData[id] = isEaten ? 1f : 0f;

        base.GetSaveData(data);
    }

    public override void LoadData(Data data)
    {
        string id = dataID.ID;

        if (data.characterPosDict.TryGetValue(id, out var pos))
        {
            transform.position = pos.ToVector3();
        }

        if (data.floatSavedData.TryGetValue(id, out float eaten))
        {
            isEaten = eaten == 1f;

            if (isEaten)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                r.color = new Color(1, 1, 1, 1);
                canBeEaten = true;
            }
        }
    }
}
