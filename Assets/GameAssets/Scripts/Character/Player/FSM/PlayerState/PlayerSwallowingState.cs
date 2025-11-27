using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwallowingState : StateBase
{
    private Swallowing swallow;
    public PlayerSwallowingState(Player player) : base(player)
    {
        swallow = player.swallow;
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
        player.swallow.gameObject.SetActive(true);
    }
    protected override void OnSwallowStarted(InputAction.CallbackContext context)
    {

    }
}
