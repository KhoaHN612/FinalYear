using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour, IAgentInput, IInteractiveInterface
{
    [field: SerializeField]
    public Vector2 MovementVector { get; private set; }

    public event Action OnAttack, OnJumpPressed, OnJumpReleased, OnWeaponChange, OnNextWeapon, OnPreviousWeapon, On1Weapon, On2Weapon, On3Weapon, On4Weapon, On5Weapon, On6Weapon, OnDash, OnRewindPressed, OnRewindReleased;

    public event Action<Vector2> OnMovement;

    public event Action<string> OnPlayAnimation; 

    public KeyCode jumpKey, attackKey, dashKey, menuKey, rewindKey, nextWeaponKey = KeyCode.E, previousWeaponKey = KeyCode.Q, interactiveKey = KeyCode.R;

    public UnityEvent OnMenuKeyPressed;

    private bool canControl;

    public bool CanControl { get => canControl; set => canControl = value; }
    public InteractiveObject InteractiveObject { get; set; }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            GetMovementInput();
            GetJumpInput();
            GetAttackInput();
            GetDashInput();
            GetNextWeaponInput();
            GetPreviousWeaponInput();
            GetRewindInput();
            GetInteractiveInput();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Toggle Time Adjust");
            if (TimeManager.Instance.isAdjustTime == false)
            {
                TimeManager.Instance.AdjustTime(0.2f);
            }
            else
            {
                TimeManager.Instance.AdjustTime(1);
            }

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Toggle Time Stop");
            if (TimeManager.Instance.isStopTime == false)
            {
                TimeManager.Instance.StopTime();
            } else
            {
                TimeManager.Instance.ResumeTime();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Toggle Speed Up Player");
            if (TimeManager.Instance.isSpeedUpPlayer == false)
            {
                TimeManager.Instance.SpeedUpPlayer();
            }
            else
            {
                TimeManager.Instance.StopSpeedUpPlayer();
            }
        }


        GetMenuInput();
    }

    public void SetInteractiveObject(InteractiveObject interactiveObject)
    {
        InteractiveObject = interactiveObject;
    }

    public void ClearInteractiveObject(InteractiveObject interactiveObject)
    {
        if (InteractiveObject == interactiveObject)
        {
            InteractiveObject = null;
        }
    }

    private void GetInteractiveInput()
    {
        if (InteractiveObject == null) return;
        if (Input.GetKeyDown(interactiveKey))
        {
            InteractiveObject.Interact();
        }
        else if (Input.anyKeyDown) 
        {
            InteractiveObject.StopInteract();
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

    private void GetRewindInput()
    {
        if (Input.GetKeyDown(rewindKey))
        {
            OnRewindPressed?.Invoke();
        }
        if (Input.GetKeyUp(rewindKey))
        {
            OnRewindReleased?.Invoke();
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
