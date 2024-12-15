using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour, IAgentInput
{
    [field: SerializeField]
    public Vector2 MovementVector { get; private set; }

    public event
        Action OnAttack, OnJumpPressed, OnJumpReleased,
        OnWeaponChange, OnNextWeapon, OnPreviousWeapon,
        On1Weapon, On2Weapon, On3Weapon, On4Weapon, On5Weapon, On6Weapon,
        OnDash, OnRewindPressed, OnRewindReleased, OnInteract, OnStopInteract;

    public UnityEvent 
        OnStopTimeActivate, OnStopTimeDeadActive, OnSlowTimeActivate, OnSlowTimeDeadActivate, 
        OnSpeedUpActivate, OnSpeedUpDeadActivate, OnRewindTimeActive, OnRewindTimeDeadActive;

    public event Action<Vector2> OnMovement;

    public event Action<string> OnPlayAnimation;

    public event Action<int> OnUsingTime;

    public KeyCode 
        jumpKey, attackKey, dashKey, menuKey, rewindKey, 
        nextWeaponKey = KeyCode.E, previousWeaponKey = KeyCode.Q, interactiveKey = KeyCode.R, skillTreeKey;

    public UnityEvent OnMenuKeyPressed;

    private bool canControl = true;

    public PlayerSkills playerSkills;
    public bool CanControl { get => canControl; set => canControl = value; }

    private void Awake()
    {
        if (playerSkills == null)
            playerSkills = GetComponent<PlayerSkills>();
    }
    private void Start()
    {
        SkillTreeMenu.Instance.uiSkillTree.SetPlayerSkills(playerSkills);
    }
    private void Update()
    {
        if (Time.timeScale > 0 && canControl)
        {
            GetMovementInput();
            GetJumpInput();
            GetAttackInput();
            GetDashInput();
            GetNextWeaponInput();
            GetPreviousWeaponInput();
            GetInteractiveInput();
            GetSkillTreeInput();
            GetSkillSpeedUpInput();
            GetSkillSlowTimeInput();
            GetSkillStopTimeInput();
            GetSkillRewindInput();
        }

        GetMenuInput();
    }

    private void GetSkillStopTimeInput()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.StopTime1))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (TimeManager.Instance.isStopTime == false)
            {
                OnUsingTime(50);
                TimeManager.Instance.StopTime();
                OnStopTimeActivate?.Invoke();
                StartCoroutine(ReleaseSkillStopTime());
            }
        }
    }

    private IEnumerator ReleaseSkillStopTime()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.StopTime3))
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (playerSkills.IsSkillUnlocked(SkillType.StopTime2))
        {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        TimeManager.Instance.ResumeTime();
        OnStopTimeDeadActive?.Invoke();
    }

    private void GetSkillSlowTimeInput()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.SlowTime1))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (TimeManager.Instance.isAdjustTime == false)
            {
                if (playerSkills.IsSkillUnlocked(SkillType.SlowTime3))
                {
                    TimeManager.Instance.AdjustTime(0.5f);
                }
                else if (playerSkills.IsSkillUnlocked(SkillType.SlowTime2))
                {
                    TimeManager.Instance.AdjustTime(0.35f);
                }
                else if (playerSkills.IsSkillUnlocked(SkillType.SlowTime1))
                {
                    TimeManager.Instance.AdjustTime(0.2f);
                }
                OnUsingTime(25);
                OnSlowTimeActivate?.Invoke();
                StartCoroutine(ReleaseSkillSlowTime());
            }
        }
    }

    private IEnumerator ReleaseSkillSlowTime()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.SlowTime3))
        {
            yield return new WaitForSeconds(1);
        }

        if (playerSkills.IsSkillUnlocked(SkillType.SlowTime2))
        {
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        TimeManager.Instance.AdjustTime(1);
        OnSlowTimeDeadActivate?.Invoke();
    }

    private void GetSkillSpeedUpInput()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.SpeedTime1))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (TimeManager.Instance.isSpeedUpPlayer == false)
            {
                if (playerSkills.IsSkillUnlocked(SkillType.SpeedTime3))
                {
                    TimeManager.Instance.SpeedUpPlayer(2f);
                }
                else if (playerSkills.IsSkillUnlocked(SkillType.SpeedTime2))
                {
                    TimeManager.Instance.SpeedUpPlayer(1.66f);
                }
                else if (playerSkills.IsSkillUnlocked(SkillType.SpeedTime1))
                {
                    TimeManager.Instance.SpeedUpPlayer(1.33f);
                }

                OnUsingTime(25);
                StartCoroutine(ReleaseSkillSpeedUp());
                OnSpeedUpActivate?.Invoke();
            }
        }
    }

    private IEnumerator ReleaseSkillSpeedUp()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.SpeedTime3))
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (playerSkills.IsSkillUnlocked(SkillType.SpeedTime2))
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        TimeManager.Instance.StopSpeedUpPlayer();
        OnSpeedUpDeadActivate?.Invoke();
    }

    private Coroutine executeRewind;
    private void GetSkillRewindInput()
    {
        if (!playerSkills.IsSkillUnlocked(SkillType.RewindTime1))
        {
            return;
        }

        if (Input.GetKeyDown(rewindKey))
        {
            TimeManager.Instance.StartRewind();
            executeRewind = StartCoroutine(UsingTimeOnTime());
            OnRewindTimeActive?.Invoke();
        }
        if (Input.GetKeyUp(rewindKey))
        {
            TimeManager.Instance.EndRewind();
            StopCoroutine(executeRewind);
            OnRewindTimeDeadActive?.Invoke();
        }
    }

    private IEnumerator UsingTimeOnTime()
    {
        while (true)
        {
            OnUsingTime(5);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void GetSkillTreeInput()
    {
        if (Input.GetKeyDown(skillTreeKey))
        {
            SkillTreeMenu.Instance.ToggleMenu();
        }
    }

    private void GetInteractiveInput()
    {
        if (Input.GetKeyDown(interactiveKey))
        {
            Debug.Log("Call Interact");
            OnInteract?.Invoke();
        }
        else if (Input.GetKeyDown(menuKey) || (MovementVector.magnitude > 0))
        {
            OnStopInteract?.Invoke();
        }
    }

    private void GetNextWeaponInput()
    {
        if (Input.GetKeyDown(nextWeaponKey))
        {
            OnNextWeapon?.Invoke();
        }
    }

    private void GetPreviousWeaponInput()
    {
        if (Input.GetKeyDown(previousWeaponKey))
        {
            OnPreviousWeapon?.Invoke();
        }
    }

    private void GetMenuInput()
    {
        if (Input.GetKeyDown(menuKey))
        {
            OnMenuKeyPressed?.Invoke();
        }
    }

    private void GetAttackInput()
    {
        if (Input.GetKeyDown(attackKey))
        {

            OnAttack?.Invoke();
        }
    }

    private void GetDashInput()
    {
        if (Input.GetKeyDown(dashKey))
        {

            OnDash?.Invoke();
        }
    }

    private void GetJumpInput()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpPressed?.Invoke();
        }
        if (Input.GetKeyUp(jumpKey))
        {
            OnJumpReleased?.Invoke();
        }
    }

    private void GetMovementInput()
    {
        MovementVector = GetMovementVector();
        OnMovement?.Invoke(MovementVector);
    }

    protected Vector2 GetMovementVector()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

}
