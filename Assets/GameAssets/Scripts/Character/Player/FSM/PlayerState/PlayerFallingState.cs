using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallingState : StateBase
{
    public PlayerFallingState(Player player) : base(player)
    {
    }

    public override void OnUpdate()
    {
        UpdateTargetHorizontalSpeed();
    }

    public override void OnPhysicsUpdate()
    {
        ChangeVelocity();
    }
    protected override void AddEventListener()
    {
        base.AddEventListener();

        player.isGrounded.OnValueChanged += OnFallToLand;
        inputManager.inputActions.Gameplay.Jump.started += OnJumpStrted;
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();

        player.isGrounded.OnValueChanged -= OnFallToLand;
        inputManager.inputActions.Gameplay.Jump.started -= OnJumpStrted;
    }
    private void Jump()
    {
        player.Rigidbody.AddForce(Vector2.up * settingData.JumpForce, ForceMode2D.Impulse);
    }
    protected override void OnJumpStrted(InputAction.CallbackContext context)
    {
        if (BuffManager.Instance.HasBuff<DoubleJumpBuff>())
        {
            BuffManager.Instance.RemoveBuff(BuffManager.Instance.GetBuff<DoubleJumpBuff>());
            Jump();
        }
    }
}
