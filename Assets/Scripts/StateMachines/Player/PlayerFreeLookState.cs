using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed"); // Strings are slow, Animator has a method to change the string value to a hash
    // readonly : A const would fail here as assignment happens at runtime. ReadOnly means that that once assignment is made in cannot be changed again
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { } //? ... : base(stateMachine) Runs the base implementation of the constructor in PlayerBaseState - https://www.dotnetperls.com/base

    public override void Enter()
    {
        // Notice the current stateMachine comes from the base class 'PlayerBaseState'
    }
    public override void Tick(float deltaTime)
    {
        FaceMovementDirection(deltaTime);
    }

    public override void Exit()
    {

    }

    private Vector3 CalculateMovement()
    {
        // Get camera transforms forward and right
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        // multiply input by camera direction to set forward and right respective of the camera.
        return forward * stateMachine.InputReader.MovementValue.y +
        right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, 0.1f, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }
}