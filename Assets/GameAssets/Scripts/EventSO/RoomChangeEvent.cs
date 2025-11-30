using System.Collections;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/RoomChangeEventSO")]
public class RoomChangeEventSO : ScriptableObject
{
    public UnityAction<RoomBase> OnEventRaised;

    public void RaiseEvent(RoomBase currentRoom)
    {
        OnEventRaised?.Invoke(currentRoom);
    }
}
