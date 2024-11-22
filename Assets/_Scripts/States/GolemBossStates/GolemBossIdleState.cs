using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossIdleState : State
{
    public State MoveState, ClimbState;
    [SerializeField]
    private bool canTransition = true;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.idle);
        if (agent.groundDetector.isGrounded)
            agent.rb2d.velocity = Vector2.zero;
    }
    public override void StateUpdate()
    {       
        if (canTransition)
        {
            if (TestFallTransition())
                return;

            if (agent.climbingDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
            {
                agent.TransitionToState(agent.stateFactory.GetState(StateType.Climbing));
            }
            else if (Mathf.Abs(agent.agentInput.MovementVector.x) > 0)
            {
                agent.TransitionToState(agent.stateFactory.GetState(StateType.Move));
            }
        }
    }

    protected override void HandlePlayAnimation(string animationName)
    {
        base.HandlePlayAnimation(animationName);
        canTransition = false;
        agent.animationManager.OnAnimationEnd.AddListener(ToggleTransition);
        agent.animationManager.PlayAnimationByName(animationName);
        if (agent.groundDetector.isGrounded)
            agent.rb2d.velocity = Vector2.zero;
    }

    private void ToggleTransition()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(ToggleTransition);
        canTransition = true;
    }

    /*    protected override void HandleMovement(Vector2 input)
        {
            if (agent.climbingDetector.CanClimb && Mathf.Abs(input.y) > 0)
            {
                agent.TransitionToState(ClimbState);
            }
            else if (Mathf.Abs(input.x) > 0)
            {
                agent.TransitionToState(MoveState);
            }
        }*/

}
