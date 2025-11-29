using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    public StateBase currentState;
    public void ChangeState(StateBase newState)
    {
        if (currentState != null) currentState?.OnExit();
        currentState = newState;
        Debug.Log(this.ToString()+"  Switching to state==>  "+newState.ToString());
        currentState?.OnEnter();
    }
    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }
    public void OnPhysicsUpdate()
    {
        currentState?.OnPhysicsUpdate();
    }
}
