using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RoomManager : MonoSingleton<RoomManager>
{
    public RoomBase[] Roomslist;

    public RoomBase CurrentRoom;

    public string StartroomName;

    public Dictionary<string, RoomBase> rooms = new Dictionary<string, RoomBase>();

    public bool IsTransitioning = false;

    protected override void Awake()
    {
        base.Awake();

        rooms.Clear();

        foreach (var room in Roomslist)
        {
            if (room != null && !rooms.ContainsKey(room.roomName))
            {
                rooms.Add(room.roomName, room);
            }
        }

        InitStartRoom();
    }

    private void InitStartRoom()
    {
        if (!rooms.ContainsKey(StartroomName))
        {
            Debug.LogError($"{name}: can't find startroom named {StartroomName}");
            return;
        }

        CurrentRoom = rooms[StartroomName];
        CurrentRoom.EnterRoom();
        CameraManager.Instance.SetRoom(CurrentRoom);
    }

    public void SwitchtoRoom(string roomname)
    {
        if (!rooms.ContainsKey(roomname))
        {
            Debug.LogError($"{name}: can't switch to room named {roomname}");
            return;
        }

        if (CurrentRoom != null)
        {
            CurrentRoom.ExitRoom();
        }

        CurrentRoom = rooms[roomname];
        CurrentRoom.EnterRoom();
    }

    public RoomBase GetRoom(string roomname)
    {
        if (rooms.TryGetValue(roomname, out var room))
            return room;

        return null;
    }

    public void RequestTransition(RoomExit exit, Player player)
    {
        if (IsTransitioning) return;
        if (exit == null || player == null) return;

        StartCoroutine(DoTransition(exit, player));
    }

    private IEnumerator DoTransition(RoomExit exit, Player player)
    {
        IsTransitioning = true;

        InputManager.Instance.inputActions.Disable();

        UIManager.Instance.GetBlackScreen().DOFade(1f, 0.4f);
        yield return new WaitForSeconds(0.45f);

        SwitchtoRoom(exit.ToRoom.roomName);
        yield return null;
            
        CameraManager.Instance.SetRoom(GetRoom(exit.ToRoom.roomName));

        player.transform.position = new Vector2(
            exit.TargetExit.SpawnPoint.position.x, 
            exit.TargetExit.SpawnPoint.position.y+(player.GetComponent<Collider2D>().bounds.size.y) / 2
            );

        UIManager.Instance.GetBlackScreen().DOFade(0f, 0.4f);
        yield return new WaitForSeconds(0.4f);

        InputManager.Instance.inputActions.Enable();

        IsTransitioning = false;
    }
}
