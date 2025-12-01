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
        inputManager.inputActions.Gameplay.Swallow.started += OnSwallowStarted;
    }
    protected virtual void RemoveEventListener()
    {
        inputManager.inputActions.Gameplay.Swallow.started -= OnSwallowStarted;
    }

    protected virtual void UpdateTargetHorizontalSpeed()
    {
        if (targetDir.x != inputManager.Movement.x)
            reusableData.aclTime = 0f;
        targetDir = new Vector2(inputManager.Movement.x, 0f);
        reusableData.targetHorizontalSpeed = targetDir.x * settingData.BaseSpeed * reusableData.horizontalSpeedMultiplier;

        if (targetDir.x != 0)
            player.transform.localScale = new Vector3((int)targetDir.x, 1f, 1f);
    }
    protected virtual void ChangeVelocity()
    {
        //Debug.Log($"VELCHECK: rb={player.Rigidbody}, reusable={reusableData}, setting={settingData}");
        float y = player.rb.velocity.y;
        player.rb.velocity =
               new Vector2(Mathf.Lerp(
                   player.rb.velocity.x,
                    reusableData.targetHorizontalSpeed,
                    reusableData.aclTime / settingData.AccelerateTime
               ), y);
        reusableData.aclTime += Time.fixedDeltaTime;

    }
    protected virtual void OnSwallowStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.SwallowingState);
    }
    protected virtual void OnJumpStrted(InputAction.CallbackContext context)
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
