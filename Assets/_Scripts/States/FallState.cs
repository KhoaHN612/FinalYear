using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MovementState
{
    [SerializeField]
    protected State ClimbState;
    bool temp = false;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.fall);
        temp = false        ;
    }

    protected override void HandleJumpPressed()
    {
        //Dont allow jumping in fall state
    }
    
    public override void StateUpdate()
    {
        movementData.currentVelocity = agent.rb2d.velocity;
        if (agent.previousState == agent.stateFactory.GetState(StateType.Rewind) && temp == false)
        {
            temp = true;
            Debug.Log(agent.rb2d.velocity);
        }


        movementData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime * agent.timeManipulateMutiplier;
        agent.rb2d.velocity = movementData.currentVelocity;

        CalculateVelocity();
        SetPlayerVelocity();

        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
        else if(agent.climbingDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
        {
            agent.TransitionToState(ClimbState);
        }
    }

}
