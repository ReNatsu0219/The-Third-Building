using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationManager : MonoSingleton<AnimationManager>
{
    public Animator PlayerAnimator;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetPlayerAnimator(Animator anim)
    {
        PlayerAnimator = anim;
    }

    public Animator GetPlayerAnimator()
    {
        return PlayerAnimator;
    }
}
