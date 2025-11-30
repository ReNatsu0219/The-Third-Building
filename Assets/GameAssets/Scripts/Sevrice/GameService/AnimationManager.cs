using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationManager : MonoSingleton<AnimationManager>
{
    public Animator PlayerAnimator;
    public Animator BiteAnimatorFWD;
    public Animator BiteAnimatorDWD;

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

    public Animator GetBiteAnimatorFWD()
    {
        return BiteAnimatorFWD;
    }

    public Animator GetBiteAnimatorDWD()
    {
        return BiteAnimatorDWD;
    }
}
