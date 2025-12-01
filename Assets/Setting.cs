using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour, IEatable
{
    [SerializeField] private Sprite beforeSprite;
    [SerializeField] private Sprite afterSprite;
    [SerializeField] private SpriteRenderer r;
    void Awake()
    {
        r.sprite = beforeSprite;
    }
    public bool BeEaten()
    {
        r.sprite = afterSprite;
        return false;
    }
}
