using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Agent : MonoBehaviour
{
    public AgentDataSO agentData;
    public MovementData movementData;
    public Rigidbody2D rb2d;
    public IAgentInput agentInput;
    public AgentAnimation animationManager;
    public AgentRenderer agentRenderer;
    public GroundDetector groundDetector;
    public ClimbingDetector climbingDetector;
    public RewindAgent rewindAgent;
    public StateFactory stateFactory;
    public Damagable damagable;

    public State currentState = null, previousState = null;
    public State IdleState;

    [HideInInspector]
    public AgentWeaponManager agentWeapon;

    [Header("State debugging:")]
    public string stateName = "";

    [field: SerializeField]
    private UnityEvent OnRespawnRequired { get; set; }
    [field: SerializeField]
    public UnityEvent OnAgentDie  { get; set; }

    private void Awake()
    {
        agentInput = GetComponentInParent<IAgentInput>();
        rb2d = GetComponent<Rigidbody2D>();
        movementData = GetComponent<MovementData>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        groundDetector = GetComponentInChildren<GroundDetector>();
        climbingDetector = GetComponentInChildren<ClimbingDetector>();
        agentWeapon = GetComponentInChildren<AgentWeaponManager>();
        rewindAgent = GetComponent<RewindAgent>();
        stateFactory = GetComponentInChildren<StateFactory>();
        damagable = GetComponentInChildren<Damagable>();

        stateFactory.InitializeStates(this);
    }

    public float GetFaceDirection(){
        if (transform.localScale.x>0){
            return 1;
        } else if (transform.localScale.x<0){
            return -1;
        } else {
            return 0;
        }
    }
    public void GetHit(){
        currentState.GetHit();
    }
    public void AgentDied()
    {
        if(damagable.CurrentHealth > 0)
        {
            OnRespawnRequired?.Invoke();
        }
        else
        {
            currentState.Die();
        }
    }

    private void Start()
    {
        agentInput.OnMovement += agentRenderer.FaceDirection;
        InitializeAgent();
    }

    private void InitializeAgent()
    {
        TransitionToState(IdleState);
        damagable.Initialize(agentData.maxHealth, agentData.maxMana, agentData.maxTime);
    }

    internal void TransitionToState(State desiredState)
    {
        if (desiredState == null)
            return;
        if (currentState != null)
            currentState.Exit();

        previousState = currentState;
        currentState = desiredState;
        currentState.Enter();

        DisplayState();

    }

    private void DisplayState()
    {
        if(previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }

    private void Update()
    {

        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        groundDetector.CheckIsGrounded();
        currentState.StateFixedUpdate();
    }

    public void PickUp(WeaponData weaponData)
    {
        if (agentWeapon == null)
            return;
        agentWeapon.PickUpWeapon(weaponData);
    }
}
