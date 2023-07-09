using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public IState CurrentState => currentState;

    public event EventHandler OnStateChanged;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();

        OnStateChanged?.Invoke(this, new EventArgs());
    }

    public virtual void UpdateStateMachine()
    {
        currentState?.Update();
    }
}
