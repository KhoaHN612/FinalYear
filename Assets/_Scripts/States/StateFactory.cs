using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField]
    private State Idle, Move, Dash, Fall, Climbing, Jump, Attack, GetHit, Die, Rewind, Stop;

    public State GetState(StateType stateType)
        => stateType switch
        {
            StateType.Idle => Idle,
            StateType.Move => Move,
            StateType.Dash => Dash,
            StateType.Fall => Fall,
            StateType.Climbing => Climbing,
            StateType.Jump => Jump,
            StateType.Attack => Attack,
            StateType.GetHit => GetHit,
            StateType.Die => Die,
            StateType.Rewind => Rewind,
            StateType.Stop => Stop,
            _ => throw new System.Exception("State not defined " + stateType.ToString())
        };


    public void InitializeStates(Agent agent)
    {
        State[] states = GetComponents<State>();
        foreach (var state in states)
        {
            state.InitializeState(agent);
        }
    }
}

public enum StateType
{
    Idle,
    Move,
    Dash,
    Fall,
    Climbing,
    Jump,
    Attack,
    GetHit,
    Die,
    Rewind,
    Stop,
}

