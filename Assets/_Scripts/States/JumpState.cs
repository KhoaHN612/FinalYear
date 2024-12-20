using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    private bool jumpPressed = false;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.jump);
        movementData.currentVelocity = agent.rb2d.velocity;
        movementData.currentVelocity.y = agent.agentData.jumpForce * agent.timeManipulateMutiplier;
        agent.rb2d.velocity = movementData.currentVelocity;
        jumpPressed = true;
    }

    protected override void HandleJumpPressed()
    {
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    public override void StateUpdate()
    {
        ControlJumpHeight();
        CalculateVelocity();
        SetPlayerVelocity();
        if (agent.rb2d.velocity.y <= 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
        }
        else if (agent.climbingDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climbing));
        }
    }

    private void ControlJumpHeight()
    {
        if (jumpPressed == false)
        {
            movementData.currentVelocity = agent.rb2d.velocity;
            movementData.currentVelocity.y += agent.agentData.lowJumpMultiplier * Physics2D.gravity.y * Time.deltaTime * agent.timeManipulateMutiplier;
            agent.rb2d.velocity = movementData.currentVelocity;
        }
    }
}
