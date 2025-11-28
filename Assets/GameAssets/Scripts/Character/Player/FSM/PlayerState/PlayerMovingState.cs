using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovingState : StateBase
{
    public PlayerMovingState(Player player) : base(player)
    {

    }
    #region IState Methods
    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log(player.InputManager.Movement);
        AnimationManager.Instance.GetPlayerAnimator().SetBool("isMoving", true);
    }

    public override void OnUpdate()
    {
        UpdateTargetHorizontalSpeed();
    }

    public override void OnPhysicsUpdate()
    {
        ChangeVelocity();
    }

    public override void OnExit()
    {
        base.OnExit();
        AnimationManager.Instance.GetPlayerAnimator().SetBool("isMoving", false);
    }
    #endregion

    #region Reusable Methods
    protected override void AddEventListener()
    {
        base.AddEventListener();

        inputManager.inputActions.Gameplay.Move.canceled += OnMoveCanceled;
        inputManager.inputActions.Gameplay.Jump.started += OnJumpStrted;
        player.isGrounded.OnValueChanged += OnFalling;
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        inputManager.inputActions.Gameplay.Move.canceled -= OnMoveCanceled;
        inputManager.inputActions.Gameplay.Jump.started -= OnJumpStrted;
        player.isGrounded.OnValueChanged -= OnFalling;
    }
    #endregion
    #region State Methods
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    #endregion
}
