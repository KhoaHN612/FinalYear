using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State
{
    public bool isAttackCanBeInterrupted = true;

    public LayerMask hittableLayerMask;

    protected Vector2 direction;

    public int defaultWeaponIndex = 0;

    public UnityEvent<AudioClip> OnWeaponSound;
    [SerializeField]
    private bool showGizmos = false;

    protected override void EnterState()
    {
        if (agent.previousState != agent.stateFactory.GetState(StateType.Stop))
        {
            agent.animationManager.ResetEvents();
            if (agent.attackNum == 1)
            {
                agent.animationManager.PlayAnimation(AnimationType.attack1);
            }
            else if (agent.attackNum == 2)
            {
                agent.animationManager.PlayAnimation(AnimationType.attack2);

            }
            else if (agent.attackNum == 3)
            {
                agent.animationManager.PlayAnimation(AnimationType.attack3);
            }
        }


        if (agent.attackNum != 1)
        {
            agent.agentWeapon.SelectWeapon(agent.attackNum - 1);
        }

        agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdleState);
        agent.animationManager.OnAnimationAction.AddListener(PerformAttack);
        agent.agentRenderer.stopRotation = true;

        agent.agentWeapon.ToggleWeaponVisibility(true);
        direction = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);
        if (agent.groundDetector.isGrounded)
            agent.rb2d.velocity = Vector2.zero;
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(agent.agentWeapon.GetCurrentWeapon().weaponSwingSound);
        agent.agentWeapon.GetCurrentWeapon().PerformAttack(agent, hittableLayerMask, direction);
    }

    public override void GetHit()
    {
        if (isAttackCanBeInterrupted)
        {
            base.GetHit();
        }
    }

    private void TransitionToIdleState()
    {
        if (agent.attackNum != 1)
        {
            agent.agentWeapon.SelectWeapon(defaultWeaponIndex);
            agent.attackNum = 1;
        }
        agent.animationManager.OnAnimationAction.RemoveListener(PerformAttack);
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdleState);
        if (agent.groundDetector.isGrounded)
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        else
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
        agent.agentWeapon.ToggleWeaponVisibility(false);
        agent.agentRenderer.stopRotation = false;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        if (showGizmos == false)
            return;
        Gizmos.color = Color.red;
        if (agent.agentWeapon != null)
        {
            var pos = agent.agentWeapon.transform.position;
            agent.agentWeapon.GetCurrentWeapon()?.DrawWeaponGizmo(pos, direction);
        }

    }

    protected override void HandleAttack()
    {
        //prevent attacking
    }

    protected override void HandleJumpPressed()
    {
        //dont allow jumping
    }

    protected override void HandleJumpReleased()
    {
        
    }

    protected override void HandleMovement(Vector2 obj)
    {
        //stop flipping / rotation
    }

    public override void StateUpdate()
    {
        //prevent update
    }

    public override void StateFixedUpdate()
    {
        //prevent fixed update
    }
}
