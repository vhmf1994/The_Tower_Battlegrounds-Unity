using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownToStartState : State
{
    private const float CountdownToStartTime = 3f;

    private float countdownTime;

    public CountdownToStartState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        countdownTime = CountdownToStartTime;
    }

    public override void Update()
    {
        countdownTime -= Time.deltaTime;

        if (countdownTime < 0)
        {
            //TODO change to PlayingState
            stateMachine.ChangeState(new PlayingState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
