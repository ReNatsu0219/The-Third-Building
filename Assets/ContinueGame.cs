using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGame : MonoBehaviour, IEatable
{
    [SerializeField] private Sprite beforeSprite;
    [SerializeField] private Sprite afterSprite;
    [SerializeField] private SpriteRenderer r;
    [SerializeField] private VoidEventSO loadGameEvent;
    void Awake()
    {
        r.sprite = beforeSprite;
    }
    public bool BeEaten()
    {
        r.sprite = afterSprite;
        loadGameEvent.RaiseEvent();
        return false;
    }
}
