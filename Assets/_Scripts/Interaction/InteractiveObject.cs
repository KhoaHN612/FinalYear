using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject: MonoBehaviour
{
    [SerializeField]
    public UnityEvent onInteract, onStopInteract;

    public void Interact()
    {
        if (onInteract != null)
        {
            onInteract.Invoke();
        }
        else
        {
            Debug.LogWarning("No interaction event assigned to this object.");
        }
    }

    public void StopInteract()
    {
        if (onStopInteract != null)
        {
            onStopInteract.Invoke();
        }
        else
        {
            Debug.LogWarning("No stop interaction event assigned to this object.");
        }
    }
}
