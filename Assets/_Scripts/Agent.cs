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
    public PlayerInput agentInput;
    public AgentAnimation animationManager;
    public AgentRenderer agentRenderer;
    public GroundDetector groundDetector;
    public ClimbingDetector climbingDetector;
    public RewindAgent rewindAgent;
    public StateFactory stateFactory;
    public Damagable damagable;

    public State curretSate = null, previousState = null;
    public State IdleState;

    [HideInInspector]
    public AgentWeaponManager agentWeapon;

    [Header("State debugging:")]
    public string stateName = "";

    [field: SerializeField]
    private UnityEvent OnRespawnRequired { get; set; }

    private void Awake()
    {
        agentInput = GetComponentInParent<PlayerInput>();
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
        curretSate.GetHit();
    }
    public void AgentDied()
    {
        OnRespawnRequired?.Invoke();
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
        if (curretSate != null)
            curretSate.Exit();

        previousState = curretSate;
        curretSate = desiredState;
        curretSate.Enter();

        DisplayState();

    }

    private void DisplayState()
    {
        if(previousState == null || previousState.GetType() != curretSate.GetType())
        {
            stateName = curretSate.GetType().ToString();
        }
    }

    private void Update()
    {

        curretSate.StateUpdate();
    }

    private void FixedUpdate()
    {
        groundDetector.CheckIsGrounded();
        curretSate.StateFixedUpdate();
    }
}
