using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindState : MovementState
{
    public Object RewindEffect;
    protected override void EnterState()
    {
        agent.animationManager.StopAnimation();
        agent.movementData.canRewind = false;
        agent.movementData.isRewind = true;
        if (agent.rewindAgent != null)
        {
            agent.rewindAgent.StartRewind();
        }
    }
    public override void StateUpdate()
    {
    }

    protected override void ExitState(){
        StartCoroutine(rewindCooldown());
        agent.movementData.isRewind = false;
        agent.animationManager.StartAnimation();
        movementData.currentVelocity = agent.rb2d.velocity;
        if (agent.rewindAgent != null)
        {
            agent.rewindAgent.StopRewind();
        }
    }

    private IEnumerator rewindCooldown(){
        yield return new WaitForSeconds(agent.agentData.rewindCooldown * (1/ agent.timeManipulateMutiplier));
        movementData.canRewind = true;
    }
}
