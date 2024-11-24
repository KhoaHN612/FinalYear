using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimation : MonoBehaviour
{
    private Animator animator;

    public UnityEvent OnAnimationAction;
    public UnityEvent OnAnimationEnd;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void PlayAnimation(AnimationType animationType)
    {
        if (animator.speed == 0)
            return;
        switch (animationType)
        {
            case AnimationType.die:
                Play("Die");
                break;
            case AnimationType.hit:
                Play("GetHit");
                break;
            case AnimationType.idle:
                Play("Idle");
                break;
            case AnimationType.attack:
                Play("Attack");
                break;
            case AnimationType.attack1:
                Play("Attack1");
                break;
            case AnimationType.attack2:
                Play("Attack2");
                break;
            case AnimationType.attack3:
                Play("Attack3");
                break;
            case AnimationType.run:
                Play("Run");
                break;
            case AnimationType.dash:
                Play("Dashing");
                break;
            case AnimationType.jump:
                Play("Jump");
                break;
            case AnimationType.fall:
                Play("Fall");
                break;
            case AnimationType.climb:
                Play("Climbing");
                break;
            case AnimationType.land:
                break;
            default:
                break;
        }
    }

    public void PlayAnimationByName(string animationName)
    {
        Play(animationName);
    }

    internal void StopAnimation()
    {
        animator.enabled = false;
    }

    internal void StartAnimation()
    {
        animator.enabled = true;
    }

    internal void AdjustAnimationSpeed(float f)
    {
        animator.speed = f;
    }

    public void Play(string name)
    {
        int stateHash = Animator.StringToHash(name);

        if (animator.HasState(0, stateHash))
        {
            animator.Play(stateHash, -1, 0f);
        }
        else
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            AnimationClip foundClip = clips.FirstOrDefault(clip => clip.name.Contains(name));

            if (foundClip != null)
            {
                animator.Play(foundClip.name, -1, 0f);
                Debug.Log($"Animation '{foundClip.name}' played instead of '{name}'");
            }
            else
            {
                Debug.LogWarning($"No animation found with name '{name}' or containing '{name}'");
            }
        }
    }

        public void ResetEvents()
    {
        OnAnimationAction.RemoveAllListeners();
        OnAnimationEnd.RemoveAllListeners();
    }

    public void InvokeAnimationAction()
    {
        OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}

public enum AnimationType
{
    die,
    hit,
    idle,
    attack,
    attack1,
    attack2,
    attack3,
    run,
    dash,
    jump,
    fall,
    climb,
    land,
}
