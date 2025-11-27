using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomBase : MonoBehaviour
{
    public string roomName;

    public bool is_active = false;
    public bool is_entered = false;

    protected virtual void Awake()
    {
        roomName = gameObject.name;
        gameObject.SetActive(is_active);
    }

    public virtual void ActiveRoom()
    {
        is_active = true;
        gameObject.SetActive(is_active);
    }

    public virtual void DeactiveRoom()
    {
        is_active = false;
        gameObject.SetActive(is_active);
    }

    public virtual void EnterRoom()
    {
        ActiveRoom();
        is_entered = true;
    }

    public virtual void ExitRoom()
    {
        DeactiveRoom();
    }

}
