using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PhysicsCheck
{
    public Animator animator;
    public PlayerStateMachine StateMachine { get; private set; }
    public InputManager InputManager { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Swallowing swallow;

    [field: SerializeField] public PlayerSO SettingData { get; private set; }
    public PlayerStateReusableData ReusableData { get; private set; }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        StateMachine = new PlayerStateMachine();
        ReusableData = new PlayerStateReusableData(this);

        InputManager = InputManager.Instance;

        StateMachine.Initialize(this);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.OnUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        StateMachine.OnPhysicsUpdate();
    }
}
