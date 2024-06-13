using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindState : MovementState
{
    protected override void EnterState()
    {
        agent.animationManager.StopAnimation();
        agent.movementData.canRewind = false;
        agent.movementData.isRewind = true;
        agent.rewindAgent.StartRewind();
    }
    public override void StateUpdate()
    {
    }

    protected override void HandleRewindReleased(){
        agent.rewindAgent.StopRewind();
        if (TestFallTransition())
            return;
        agent.TransitionToState(IdleState);
    }

    protected override void ExitState(){
        StartCoroutine(rewindCooldown());
        agent.movementData.isRewind = false;
        agent.animationManager.StartAnimation();
    }

    private IEnumerator rewindCooldown(){
        yield return new WaitForSeconds(agent.agentData.rewindCooldown);
        movementData.canRewind = true;
    }
}
