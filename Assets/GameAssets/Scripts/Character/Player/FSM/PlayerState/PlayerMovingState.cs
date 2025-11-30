using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovingState : StateBase
{
    private float stepTimer = 0f;
    private float stepInterval = 0.35f;
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
        PlayFootStep();
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
    private void PlayFootStep()
    {
        stepTimer += Time.deltaTime;

        if (stepTimer >= stepInterval)
        {
            AudioManager.Instance.PlayRandomSFX(AudioManager.Instance.Step);

            stepTimer = 0f;
        }
    }
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
