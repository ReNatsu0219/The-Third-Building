using UnityEngine;

public class Swallowing : MonoBehaviour
{
    public Player player;
    public bool isMasticable;
    public bool Detected=false;

    private Animator biteAnimator;
    private Collider2D trigger;

    void Awake()
    {
        biteAnimator = GetComponent<Animator>();
        trigger = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        isMasticable = false;
        if (trigger != null)
            trigger.enabled = true;
    }

    void OnDisable()
    {
        isMasticable = false;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        Swallow(coll);
    }

    private void Swallow(Collider2D coll)
    {
        Detected = true;
        var masticable = coll.GetComponent<IEatable>()?.BeEaten();
        bool canEat = masticable != null && (bool)masticable;

        if (canEat)
        {
            isMasticable = true;

            AnimationManager.Instance.GetPlayerAnimator().SetBool("Eat", true);
        }
        else
        {
            isMasticable = false;

            biteAnimator.SetTrigger("Bite");
        }

        if (trigger != null)
            trigger.enabled = false;

    }

    public void OnBiteAnimationEnd()
    {
        if (player.StateMachine.currentState == player.StateMachine.SwallowingState)
        {
            player.StateMachine.ChangeState(player.StateMachine.IdlingState);
        }
        Detected = false;
        gameObject.SetActive(false);
    }
}