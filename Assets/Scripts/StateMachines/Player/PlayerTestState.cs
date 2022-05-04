using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { } //? ... : base(stateMachine) Runs the base implementation of the constructor in PlayerBaseState - https://www.dotnetperls.com/base

    public override void Enter()
    {
        // Notice the current stateMachine comes from the base class 'PlayerBaseState'
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0;
        movement.z = stateMachine.InputReader.MovementValue.y;
        stateMachine.transform.Translate(movement * deltaTime);
    }

    public override void Exit()
    {

    }


}