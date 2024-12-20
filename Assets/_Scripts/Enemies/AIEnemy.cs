using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIEnemy
{
    public class AIEnemy : MonoBehaviour, IAgentInput
    {
        public Vector2 MovementVector { get; set; }

        public event Action OnAttack;
        public event Action OnDash;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;
        public event Action<Vector2> OnMovement;
        public event Action OnRewindPressed;
        public event Action OnRewindReleased;
        public event Action OnWeaponChange;
        public event Action OnNextWeapon;
        public event Action OnPreviousWeapon;
        public event Action<string> OnPlayAnimation;

        public void CallOnAttack()
        {
            OnAttack?.Invoke();
        }

        public void CallOnDash()
        {
            OnDash?.Invoke();
        }

        public void CallOnJumpPressed()
        {
            OnJumpPressed?.Invoke();
        }

        public void CallOnJumpReleased()
        {
            OnJumpReleased?.Invoke();
        }

        public void CallOnPlayAnimation(string animationName)
        {
            OnPlayAnimation?.Invoke(animationName);
        }

        public void CallOnMovement(Vector2 input)
        {
            OnMovement?.Invoke(input);
        }

        public void CallOnRewindPressed()
        {
            OnRewindPressed?.Invoke();
        }

        public void CallOnRewindReleased()
        {
            OnRewindReleased?.Invoke();
        }

        public void CallAttack()
        {
            OnAttack?.Invoke();
        }

        public void CallOnWeaponChange()
        {
            OnWeaponChange?.Invoke();
        }
    }
}
