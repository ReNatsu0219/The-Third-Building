using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerEntryDirection
{
    Left,
    Right,
    Up,
    Down
}

public class RoomExit : MonoBehaviour
{
    public RoomBase FromRoom;
    public RoomBase ToRoom; 
    public RoomExit TargetExit;

    public PlayerEntryDirection EntryDirection;
    public PlayerEntryDirection ExitDirection;

    private void Awake()
    {
        if (FromRoom == null)
            FromRoom = GetComponentInParent<RoomBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null) return;

        RoomManager.Instance.RequestTransition(this, player);
    }
}
