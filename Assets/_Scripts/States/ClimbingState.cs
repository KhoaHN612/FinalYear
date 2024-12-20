using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : State
{
    private float previousGravityScale = 0;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.climb);
        agent.animationManager.StopAnimation();
        previousGravityScale = agent.rb2d.gravityScale;
        agent.rb2d.gravityScale = 0;
        agent.rb2d.velocity = Vector2.zero;
    }

    protected override void HandleJumpPressed()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
    }

    public override void StateUpdate()
    {
        if(agent.agentInput.MovementVector.magnitude > 0)
        {
            agent.animationManager.StartAnimation();
            agent.rb2d.velocity = new Vector2(agent.agentInput.MovementVector.x * agent.agentData.climbHorizontalSpeed, agent.agentInput.MovementVector.y * agent.agentData.climbVecticalSpeed);
        }
        else
        {
            agent.animationManager.StopAnimation();
            agent.rb2d.velocity = Vector2.zero;
        }

        if(agent.climbingDetector.CanClimb == false)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    protected override void ExitState()
    {
        agent.rb2d.gravityScale = previousGravityScale;
        agent.animationManager.StartAnimation();
    }
}
