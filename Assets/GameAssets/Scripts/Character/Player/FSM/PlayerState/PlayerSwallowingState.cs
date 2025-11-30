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

        //TestSetting
        TryToEat();

    }
    public override void OnUpdate()
    {
        base.OnUpdate();

    }
    private void TryToEat()
    {
        //Press "Down" to eat downward.
        if (InputManager.Instance.inputActions.Gameplay.Move.ReadValue<Vector2>().y >= 0f)
            player.swallowFwd.gameObject.SetActive(true);
        else
            player.swallowDwd.gameObject.SetActive(true);
    }
    protected override void OnSwallowStarted(InputAction.CallbackContext context)
    {

    }
}
