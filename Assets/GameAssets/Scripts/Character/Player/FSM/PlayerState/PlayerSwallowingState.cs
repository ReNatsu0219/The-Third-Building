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
        player.StartCoroutine(SwallowTimeout());

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

    private IEnumerator SwallowTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        if (player.StateMachine.currentState != player.StateMachine.SwallowingState)
            yield break;


        player.swallow.gameObject.SetActive(false);
        player.StateMachine.ChangeState(player.StateMachine.IdlingState);
    }
}