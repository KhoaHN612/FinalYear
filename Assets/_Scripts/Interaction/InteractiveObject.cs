using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject: MonoBehaviour
{
    [SerializeField]
    public UnityEvent<Object> onInteract, onStopInteract;

    public void Interact(Object obj)
    {
        if (onInteract != null)
        {
            onInteract.Invoke(obj);
        }
        else
        {
            Debug.LogWarning("No interaction event assigned to this object.");
        }
    }

    public void StopInteract(Object obj)
    {
        if (onStopInteract != null)
        {
            onStopInteract.Invoke(obj);
        }
        else
        {
            Debug.LogWarning("No stop interaction event assigned to this object.");
        }
    }
}
