using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : MovementState
{
    private float previousGravityScale = 0;
    MovementData oldMovementData;

    protected override void EnterState()
    {
        agent.animationManager.StopAnimation();

        previousGravityScale = agent.rb2d.gravityScale;
        agent.rb2d.gravityScale = 0;
        oldMovementData = agent.movementData;
        movementData.currentSpeed = 0;
        movementData.currentVelocity = Vector2.zero;
        SetPlayerVelocity();
    }

    protected override void HandleJumpPressed()
    {
    }

    protected override void HandleAttack()
    {
    }

    protected override void HandleDash()
    {
    }

    public override void StateUpdate()
    {
    }

    public override void StateFixedUpdate()
    {
    }

    protected override void ExitState()
    {
        agent.rb2d.gravityScale = previousGravityScale;
        agent.movementData = oldMovementData; 
        agent.animationManager.StartAnimation();
    }
}
