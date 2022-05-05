using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed"); // Strings are slow, Animator has a method to change the string value to a hash
    // readonly : A const would fail here as assignment happens at runtime. ReadOnly means that that once assignment is made in cannot be changed again

    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { } //? ... : base(stateMachine) Runs the base implementation of the constructor in PlayerBaseState - https://www.dotnetperls.com/base

    public override void Enter()
    {
        // Notice the current stateMachine comes from the base class 'PlayerBaseState'
        stateMachine.InputReader.TargetEvent += OnTarget;   // as soon as we load into PlayerFreeLookState subscribe to events
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
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

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}


//? NOTES
//  3)  The PlayerStateMachine class invoked me and passed me itself as the paramater for my constructor, which I received from my base parent class PlayerBaseState
//  4)  While the details of my constructor are hidden though abstraction I will assign the statemachine variable passed to me by my parent to PlayerStateMachine
//  5)  I will now b
