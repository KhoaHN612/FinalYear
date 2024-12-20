using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : MovementState
{
    private float previousGravityScale = 0;
    [SerializeField]
    private bool isDashing;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.dash);
        agent.animationManager.StopAnimation();

        movementData.isDashing = true;
        movementData.canDash = false;
        previousGravityScale = agent.rb2d.gravityScale;
        agent.rb2d.gravityScale = 0;
        movementData.currentVelocity.x = agent.GetFaceDirection() * agent.agentData.dashSpeed * agent.timeManipulateMutiplier;
        movementData.currentVelocity.y = 0;
        StartCoroutine(stopDashing());
        StartCoroutine(dashCooldown());
        SetPlayerVelocity();
    }

    private IEnumerator stopDashing(){
        yield return new WaitForSeconds(agent.agentData.dashTime * (1 / agent.timeManipulateMutiplier));
        movementData.isDashing = false;
    }

    private IEnumerator dashCooldown(){
        yield return new WaitForSeconds(agent.agentData.dashTime * (1/ agent.timeManipulateMutiplier));
        movementData.canDash = true;
    }

    protected override void HandleJumpPressed()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
    }

    public override void StateUpdate()
    {
        if (!movementData.isDashing){
            if (TestFallTransition())
                return;
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    protected override void ExitState()
    {
        agent.rb2d.gravityScale = previousGravityScale;
        agent.animationManager.StartAnimation();
    }
}
