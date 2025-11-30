using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PhysicsCheck, ISaveable
{
    public Animator animator;
    public PlayerStateMachine StateMachine { get; private set; }
    public InputManager InputManager { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Swallowing swallowFwd;
    public Swallowing swallowDwd;

    [field: SerializeField] public PlayerSO SettingData { get; private set; }
    public PlayerStateReusableData ReusableData { get; private set; }

    [Header("State")]
    public bool isDead = false;

    [field: Header("Event Listener")]
    [SerializeField] private VoidEventSO playerDeathEvent;
    [SerializeField] private VoidEventSO loadGameEvent;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        AnimationManager.Instance.SetPlayerAnimator(animator);

        StateMachine = new PlayerStateMachine();
        ReusableData = new PlayerStateReusableData(this);

        InputManager = InputManager.Instance;

        StateMachine.Initialize(this);
    }
    void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();

        //测试死亡 下同
        InputManager.Instance.inputActions.Gameplay.Die.started += TestDie;

        loadGameEvent.OnEventRaised += PlayerOnLoadGameEvent;
    }
    void OnDisable()
    {
        InputManager.Instance.inputActions.Gameplay.Die.started -= TestDie;
        loadGameEvent.OnEventRaised -= PlayerOnLoadGameEvent;
    }

    private void TestDie(InputAction.CallbackContext context)
    {
        PlayerDead();
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

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Water"))
        {
            PlayerDead();
        }
        //可以用来播放不同的动画
        else if (coll.CompareTag("Spike"))
        {
            PlayerDead();
        }
    }
    private void PlayerDead()
    {
        playerDeathEvent.RaiseEvent();
        isDead = true;
    }
    #region Event Methods
    private void PlayerOnLoadGameEvent()
    {
        isDead = false;
    }

    public void LandingDone()
    {
        StateMachine.ChangeState(StateMachine.IdlingState);
    }

    public void EatDone()
    {
        AnimationManager.Instance.GetPlayerAnimator().SetBool("Eat", false);
    }

    #endregion

    #region ISaveable Methods
    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = new SerializableVector3(transform.position);
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, new SerializableVector3(transform.position));
        }
    }

    public void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
        }
    }
    #endregion
}
