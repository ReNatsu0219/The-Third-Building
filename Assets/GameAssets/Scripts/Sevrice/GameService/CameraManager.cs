using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    public void SetRoom(RoomBase room)
    {

        if (!room.is_active)
            return;

        cam.transform.position = new Vector3(
        room.transform.position.x,
        room.transform.position.y,
        cam.transform.position.z
    );
    }
}
