using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallowing : MonoBehaviour
{
    public Player player;
    public bool isMasticable;
    void OnEnable()
    {

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
        var masticable = coll.GetComponent<IEatable>()?.BeEaten();
        if (masticable == null)
        {
            isMasticable = false;
            AnimationManager.Instance.GetPlayerAnimator().SetBool("Bite", true);
            player.StateMachine.ChangeState(player.StateMachine.IdlingState);
            this.gameObject.SetActive(false);
        }
        else
        {
            isMasticable = (bool)masticable;
            AnimationManager.Instance.GetPlayerAnimator().SetBool("Eat", true);
            player.StateMachine.ChangeState(player.StateMachine.IdlingState);
            this.gameObject.SetActive(false);
        }
    }
}
