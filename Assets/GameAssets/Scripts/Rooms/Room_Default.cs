using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Default : RoomBase
{


    public override void EnterRoom()
    {
        base.EnterRoom();

        Debug.Log($"Entered room {this.roomName}");
    }

    public override void ExitRoom()
    {
        base.ExitRoom();

        Debug.Log($"Exited room {this.roomName}");
    }
}
