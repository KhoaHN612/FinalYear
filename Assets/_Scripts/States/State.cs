using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField]
    protected State JumpState, FallState, DashingState, RewindState;

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
        this.agent.agentInput.OnMovement += HandleMovement;
        this.agent.agentInput.OnRewindPressed += HandleRewindPressed;
        this.agent.agentInput.OnRewindReleased += HandleRewindReleased;
        OnEnter?.Invoke();
        EnterState();
    }

    protected virtual void EnterState()
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
            agent.TransitionToState(JumpState);
        }
    }

    protected virtual void HandleRewindPressed(){
        if (agent.movementData.canDash){
            agent.TransitionToState(RewindState);
        }
    }

    protected virtual void HandleRewindReleased(){
    }

    protected virtual void HandleAttack()
    {
    }

    protected virtual void HandleDash()
    {
        if (agent.movementData.canDash){
            TestDashTransition();
        }
    }

    private void TestDashTransition()
    {
        agent.TransitionToState(DashingState);
    }

    public virtual void StateUpdate()
    {
        TestFallTransition();
    }

    protected bool TestFallTransition()
    {
        if(agent.groundDetector.isGrounded == false)
        {
            agent.TransitionToState(FallState);
            return true;
        }
        return false;
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
        this.agent.agentInput.OnMovement -= HandleMovement;
        this.agent.agentInput.OnRewindPressed -= HandleRewindPressed;
        this.agent.agentInput.OnRewindReleased -= HandleRewindReleased;
        OnExit?.Invoke();
        ExitState();
    }

    protected virtual void ExitState()
    {
    }
}
