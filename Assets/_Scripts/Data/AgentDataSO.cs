using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentData", menuName = "Agent/Data")]
public class AgentDataSO : ScriptableObject
{
    [Header("Movement data")]
    [Space]
    public float maxSpeed = 6;
    public float acceleration = 50;
    public float deacceleration = 50;

    [Header("Dash data")]
    [Space]
    public float dashSpeed = 18f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    [Header("Jump data")]
    [Space]
    public float jumpForce = 12;
    public float lowJumpMultiplier = 2;
    public float gravityModifier = 0.5f;

    [Header("Climb data")]
    [Space]
    public float climbHorizontalSpeed = 2;
    public float climbVecticalSpeed = 5;
}
