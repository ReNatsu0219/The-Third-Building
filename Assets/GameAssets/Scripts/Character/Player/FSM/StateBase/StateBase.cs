using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StateBase : IState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected Animator animator;
    protected InputManager inputManager;
    protected PlayerSO settingData;
    protected PlayerStateReusableData reusableData;

    protected Vector2 targetDir;
    public StateBase(Player player)
    {
        this.player = player;
        stateMachine = player.StateMachine;
        animator = player.animator;
        inputManager = player.InputManager;
        settingData = player.SettingData;
        reusableData = player.ReusableData;
    }
    public virtual void OnEnter()
    {
        AddEventListener();
    }

    public virtual void OnExit()
    {
        RemoveEventListener();
    }

    public virtual void OnPhysicsUpdate()
    {

    }

    public virtual void OnUpdate()
    {

    }

    #region Reusable Methods
    protected virtual void AddEventListener()
    {

    }
    protected virtual void RemoveEventListener()
    {

    }
    protected virtual void UpdateTargetHorizontalSpeed()
    {
        if (targetDir.x != inputManager.Movement.x)
            reusableData.aclTime = 0f;
        targetDir = new Vector2(inputManager.Movement.x, 0f);
        reusableData.targetHorizontalSpeed = targetDir.x * settingData.BaseSpeed;

        if (targetDir.x != 0)
            player.transform.localScale = new Vector3((int)targetDir.x, 1f, 1f);
    }
    protected virtual void ChangeVelocity()
    {
        float y = player.Rigidbody.velocity.y;
        player.Rigidbody.velocity =
               new Vector2(Mathf.Lerp(
                   player.Rigidbody.velocity.x,
                    reusableData.targetHorizontalSpeed,
                    reusableData.aclTime / settingData.AccelerateTime
               ), y);
        reusableData.aclTime += Time.fixedDeltaTime;

    }
    protected void OnJumpStrted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.JumpingState);
    }
    protected virtual void OnFallToLand(bool isGrounded)
    {
        if (isGrounded)
            stateMachine.ChangeState(stateMachine.LandingState);
    }
    protected virtual void OnFalling(bool isGrounded)
    {
        if (!isGrounded)
            stateMachine.ChangeState(stateMachine.FallingState);
    }
    #endregion
}
