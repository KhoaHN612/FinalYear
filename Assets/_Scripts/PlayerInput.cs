using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [field: SerializeField]
    public Vector2 MovementVector { get; private set; }

    public event Action OnAttack, OnJumpPressed, OnJumpReleased, OnWeaponChange, OnDash, OnRewindPressed, OnRewindReleased;

    public event Action<Vector2> OnMovement;

    public KeyCode jumpKey, attackKey, dashKey, menuKey, rewindKey;

    public UnityEvent OnMenuKeyPressed;

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            GetMovementInput();
            GetJumpInput();
            GetAttackInput();
            GetDashInput();
            GetWeaponSwapInput();
            GetRewindInput();
        }

        GetMenuInput();
    }

    private void GetWeaponSwapInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnWeaponChange?.Invoke();
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
