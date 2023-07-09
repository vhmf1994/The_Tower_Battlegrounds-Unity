using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    private const float PlayingTime = 3f;

    private float playTime;

    public PlayingState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        playTime = PlayingTime;
    }

    public override void Update()
    {
        playTime -= Time.deltaTime;

        if (playTime < 0)
        {
            //TODO change to GameOverState
        }
    }

    public override void Exit()
    {
    }
}
