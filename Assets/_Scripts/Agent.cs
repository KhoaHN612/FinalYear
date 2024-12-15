using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Agent : MonoBehaviour, ICanBeTimeAffect, IDataPersistence, IInteractiveInterface
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

    private int bonusHealth = 0;
    private int bonusMana = 0;
    private int bonusTime = 0;

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
    public InteractiveObject InteractiveObject { get ; set ; }

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

        if (agentInput is PlayerInput playerInput)
        {
            //Debug.Log(playerInput.playerSkills);
            playerInput.playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        }
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
        if (agentInput is PlayerInput playerInput)
        {
            playerInput.OnUsingTime += UsingTime;
            playerInput.OnInteract += CallInteractObject;
            playerInput.OnStopInteract += CallStopInteractObjetc;   
        }

    }

    private void CallInteractObject()
    {
        if (InteractiveObject != null)
        {
            //Debug.Log("Call Interact");
            InteractiveObject.Interact(this);
        }
    }

    private void CallStopInteractObjetc()
    {
        if (InteractiveObject != null)
        {
            InteractiveObject.StopInteract(this);
        }
    }

    private void UsingTime(int obj)
    {
        damagable.UseTime(obj);
    }

    private void InitializeAgent()
    {
        TransitionToState(stateFactory.GetState(StateType.Idle));
        damagable.Initialize(agentData.maxHealth + bonusHealth, agentData.maxMana + bonusMana, agentData.maxTime + bonusTime);
    }

    public void RespawnAgent()
    {
        damagable.Initialize(agentData.maxHealth + bonusHealth, agentData.maxMana + bonusMana, agentData.maxTime + bonusTime);
        TransitionToState(stateFactory.GetState(StateType.Idle));
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
        Debug.Log(transform.parent.name + " AdjustSpeed: " + speed);     
        animationManager.AdjustAnimationSpeed(speed);
        rb2d.gravityScale = defaultGravityScale * speed;
        timeManipulateMutiplier = speed;
    }

    public void RewindStart()
    {
        currentState.HandleRewindPressed();
    }

    public void RewindEnd()
    {
        currentState.HandleRewindReleased();
    }

    public void LoadData(GameData data)
    {
        if (agentInput is PlayerInput playerInput)
        {
            bonusHealth = data.playerBonusHealth;
            bonusMana = data.playerBonusMana;
            bonusTime = data.playerBonusTime;
        }
    }

    public void SaveData(GameData data)
    {
        if (agentInput is PlayerInput playerInput)
        {
            data.playerBonusHealth = bonusHealth;
            data.playerBonusMana = bonusMana;
            data.playerBonusTime = bonusTime;
        }
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case SkillType.MaxTime1:
                bonusTime = 25;
                break;
            case SkillType.MaxTime2:
                bonusTime = 50;
                break;
            case SkillType.MaxHealth1:
                bonusHealth = 25;
                break;
            case SkillType.MaxHealth2:
                bonusHealth = 50;
                break;
            default:
                return;
        }
        damagable.Initialize(agentData.maxHealth + bonusHealth, agentData.maxMana + bonusMana, agentData.maxTime + bonusTime);
    }

    public void SetInteractiveObject(InteractiveObject interactiveObject)
    {
        InteractiveObject = interactiveObject;
        Debug.Log("SetInteractiveObject: " + InteractiveObject);
    }

    public void ClearInteractiveObject(InteractiveObject interactiveObject)
    {
        if (InteractiveObject == interactiveObject)
        {
            InteractiveObject = null;
        }
    }
}
