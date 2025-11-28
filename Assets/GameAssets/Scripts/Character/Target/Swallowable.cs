using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Swallowable : MonoBehaviour, IEatable, ISaveable
{
    [SerializeField] protected float recoverTime;
    protected float timer;
    protected bool canBeEaten = true;
    protected SpriteRenderer r;
    void Awake()
    {
        r = GetComponent<SpriteRenderer>();
    }
    public virtual bool BeEaten()
    {
        //物体不会设置为false，请通过动画/图案表示状态
        if (canBeEaten)
        {
            r.color = new Color(255f, 255f, 255f, 0f);
            canBeEaten = false;
            timer = recoverTime;
            return true;
        }
        else
        {
            return false;
        }
    }
    protected virtual void Update()
    {
        if (!canBeEaten && timer >= 0f)
        {
            timer -= Time.deltaTime;
        }
        //同理，请通过动画/图案表示状态
        else if (!canBeEaten)
        {
            r.color = new Color(255f, 255f, 255f, 1f);
            canBeEaten = true;
        }
    }
    void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }
    void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }
    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = new SerializableVector3(transform.position);
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, new SerializableVector3(transform.position));
        }
    }

    public void LoadData(Data data)
    {
        canBeEaten = true;
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
        }
    }
}
