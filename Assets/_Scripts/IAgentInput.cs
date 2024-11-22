using System;
using UnityEngine;

public interface IAgentInput
{
    Vector2 MovementVector { get; }

    event Action OnAttack;
    event Action OnDash;
    event Action OnJumpPressed;
    event Action OnJumpReleased;
    event Action<Vector2> OnMovement;
    event Action OnWeaponChange;
    event Action OnRewindPressed;
    event Action OnRewindReleased;
    event Action OnNextWeapon;
    event Action OnPreviousWeapon;
    event Action<string> OnPlayAnimation;

}