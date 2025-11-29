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
        player.StartCoroutine(SwallowTimeout());

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

    private IEnumerator SwallowTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        if (player.StateMachine.currentState != player.StateMachine.SwallowingState)
            yield break;


        player.swallow.gameObject.SetActive(false);
        player.StateMachine.ChangeState(player.StateMachine.IdlingState);
    }
}