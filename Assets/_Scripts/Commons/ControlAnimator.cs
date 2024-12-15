using System;
using System.Collections;
using UnityEngine;

public class ControlAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public string defaultParameterName = "isOpen";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError($"Animator not found on {gameObject.name}. Ensure the Animator component is attached.");
        }
    }

    public void SetBool(string parameterName, bool value)
    {
        if (animator != null)
        {
            if (HasParameter(parameterName))
            {
                animator.SetBool(parameterName, value);
            }
            else
            {
                Debug.LogWarning($"Animator parameter '{parameterName}' does not exist on {gameObject.name}.");
            }
        }
    }
    public void SetBoolTrue(string parameterName)
    {
        if (parameterName == null || parameterName == "")
        {
            parameterName = defaultParameterName;
        }
        SetBool(parameterName, true);
    }

    public void SetBoolFalse(string parameterName)
    {
        if (parameterName == null || parameterName == "")
        {
            parameterName = defaultParameterName;
        }
        SetBool(parameterName, false);
    }

    private bool HasParameter(string parameterName)
    {
        foreach (var param in animator.parameters)
        {
            if (param.name == parameterName)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeParameterTrueFor(float time)
    {
        SetBool(defaultParameterName, true);

        StartCoroutine(ChangeParameterFalseAfter(time));
    }

    private IEnumerator ChangeParameterFalseAfter(float time)
    {
        yield return new WaitForSeconds(time);
        SetBool(defaultParameterName, false);
    }
}
