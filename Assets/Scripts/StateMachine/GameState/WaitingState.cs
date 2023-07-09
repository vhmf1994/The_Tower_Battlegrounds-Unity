using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : State
{
    private const float startWaitingTime = 5f;

    private float waitingTime;

    public WaitingState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        waitingTime = startWaitingTime;
    }

    public override void Update()
    {
        waitingTime -= Time.deltaTime;

        if (waitingTime < 0)
        {
            //TODO change to CountdownToPlayState
            stateMachine.ChangeState(new CountdownToStartState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
