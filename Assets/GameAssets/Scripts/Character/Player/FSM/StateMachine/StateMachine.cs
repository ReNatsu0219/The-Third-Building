using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    private StateBase currentState;
    public void ChangeState(StateBase newState)
    {
        currentState?.OnExit();
        currentState = newState;
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
