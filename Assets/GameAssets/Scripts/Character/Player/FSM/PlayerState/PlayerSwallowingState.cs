using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwallowingState : StateBase
{
    private Swallowing swallowFwd;
    private Swallowing swallowDwd;

    public PlayerSwallowingState(Player player) : base(player)
    {
        swallowFwd = player.swallowFwd;
        swallowDwd = player.swallowDwd;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        reusableData.aclTime = 0f;

        TryToEat();

    }

    public override void OnExit()
    {
        base.OnExit();

        if (swallowFwd != null)
            swallowFwd.gameObject.SetActive(false);
        if (swallowDwd != null)
            swallowDwd.gameObject.SetActive(false);

        AnimationManager.Instance.GetPlayerAnimator().SetBool("Eat", false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!swallowFwd.Detected) AnimationManager.Instance.GetBiteAnimatorFWD().SetTrigger("Bite");
        if (!swallowDwd.Detected) AnimationManager.Instance.GetBiteAnimatorDWD().SetTrigger("Bite");
    }

    private void TryToEat()
    {
        // Press "Down" to eat downward.
        if (InputManager.Instance.inputActions.Gameplay.Move.ReadValue<Vector2>().y >= 0f)
            player.swallowFwd.gameObject.SetActive(true);
        else
            player.swallowDwd.gameObject.SetActive(true);
    }

    protected override void OnSwallowStarted(InputAction.CallbackContext context)
    {

    }
}
