using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour, IEatable
{
    [SerializeField] private Sprite beforeSprite;
    [SerializeField] private Sprite afterSprite;
    [SerializeField] private VoidEventSO newGameEvent;
    [SerializeField] private SpriteRenderer r;
    [SerializeField] private RoomExit startExit;
    void Awake()
    {
        r.sprite = beforeSprite;
    }
    public bool BeEaten()
    {
        r.sprite = afterSprite;
        newGameEvent.RaiseEvent();
        RoomManager.Instance.RequestTransition(startExit, PlayerManager.Instance.player);
        return false;
    }
}
