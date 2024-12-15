using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected Agent agent;

    public UnityEvent OnEnter, OnExit;

    public void InitializeState(Agent agent)
    {
        this.agent = agent;
    }

    public void Enter()
    {
        this.agent.agentInput.OnAttack += HandleAttack;
        this.agent.agentInput.OnDash += HandleDash;
        this.agent.agentInput.OnJumpPressed += HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased += HandleJumpReleased;
        this.agent.agentInput.OnPlayAnimation += HandlePlayAnimation;
        this.agent.agentInput.OnMovement += HandleMovement;
        OnEnter?.Invoke();
        EnterState();
    }

    protected virtual void EnterState()
    {

    }

    protected virtual void HandlePlayAnimation(string animationName)
    {
    }

    protected virtual void HandleMovement(Vector2 obj)
    {
    }

    protected virtual void HandleJumpReleased()
    {
    }

    protected virtual void HandleJumpPressed()
    {
        TestJumpTransition();
    }

    private void TestJumpTransition()
    {
        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
        }
    }

    public virtual void HandleRewindPressed(){
        if (agent.movementData.canRewind){
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Rewind));
        }
    }

    public virtual void HandleRewindReleased(){
        if (TestFallTransition())
            return;
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
    }

    protected virtual void HandleAttack()
    {
        TestAttackTransition();
    }

    protected virtual void HandleDash()
    {
        if (agent.movementData.canDash){
            TestDashTransition();
        }
    }

    private void TestDashTransition()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Dash));
    }

    public virtual void StateUpdate()
    {
        TestFallTransition();
    }

    protected bool TestFallTransition()
    {
        if(agent.groundDetector.isGrounded == false)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
            return true;
        }
        return false;
    }

    protected virtual void TestAttackTransition()
    {
        if (agent.agentWeapon.CanIUseWeapon(agent.groundDetector.isGrounded))
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));

        }
    }

    public virtual void StateFixedUpdate()
    {

    }

    public void Exit()
    {
        this.agent.agentInput.OnAttack -= HandleAttack;
        this.agent.agentInput.OnDash -= HandleDash;
        this.agent.agentInput.OnJumpPressed -= HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased -= HandleJumpReleased;
        this.agent.agentInput.OnPlayAnimation -= HandlePlayAnimation;
        this.agent.agentInput.OnMovement -= HandleMovement;
        OnExit?.Invoke();
        ExitState();
    }

    protected virtual void ExitState()
    {
    }

    public virtual void GetHit()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.GetHit));
    }
    public virtual void GetStop()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Stop));
    }
    public virtual void Die()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Die));
    }
}
