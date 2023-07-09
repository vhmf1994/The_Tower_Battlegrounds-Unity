using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : IState
{
    protected StateMachine stateMachine;

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
