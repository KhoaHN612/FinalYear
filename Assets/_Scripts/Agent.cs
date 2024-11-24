using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Agent : MonoBehaviour, ICanBeTimeAffect
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
    public bool isAffectByNegativeTimeManipulate = true;
    public float timeManipulateMutiplier = 1;
    private float defaultGravityScale;

    public State currentState = null, previousState = null;
    /*public State IdleState;*/

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

        defaultGravityScale = rb2d.gravityScale;

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

        agentInput.OnNextWeapon += NextWeapon;
        agentInput.OnPreviousWeapon += PreviousWeapon;

    }


    private void InitializeAgent()
    {
        TransitionToState(stateFactory.GetState(StateType.Idle));
        damagable.Initialize(agentData.maxHealth, agentData.maxMana, agentData.maxTime);
    }

    private void NextWeapon()
    {
        if (agentWeapon == null)
            return;
        agentWeapon.NextWeapon();
    }

    private void PreviousWeapon()
    {
        if (agentWeapon == null)
            return;
        agentWeapon.PreviousWeapon();
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

    public void StopTime()
    {
        if (!isAffectByNegativeTimeManipulate) return;
        if (currentState == stateFactory.GetState(StateType.Stop)) return; 
        agentRenderer.stopRotation = true;
        TransitionToState(stateFactory.GetState(StateType.Stop));
    }

    public void ResumeTime()
    {
        if (!isAffectByNegativeTimeManipulate) return;
        if (currentState == stateFactory.GetState(StateType.Stop))
        {
            agentRenderer.stopRotation = false;
            TransitionToState(previousState);
        }
    }

    public void AdjustSpeed(float speed)
    {
        if (!isAffectByNegativeTimeManipulate && speed<1) return;
        animationManager.AdjustAnimationSpeed(speed);
        rb2d.gravityScale = defaultGravityScale * speed;
        timeManipulateMutiplier = speed;
    }
}
