using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentRenderer : MonoBehaviour
{
    [SerializeField]
    private GroundDetector groundDetector;
    public UnityEvent onFlip;
    public bool stopRotation = false;

    public void FaceDirection(Vector2 input)
    {
        if (stopRotation)
            return;

        if (input.x < 0)
        {
            if (transform.parent.localScale.x > 0)
                if (groundDetector == null || groundDetector.isGrounded)
                    onFlip?.Invoke();
            transform.parent.localScale = new Vector3(-1 * Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, transform.parent.localScale.z);
        }
        else if (input.x > 0)
        {
            if (transform.parent.localScale.x < 0)
                if (groundDetector == null || groundDetector.isGrounded)
                    onFlip?.Invoke();
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, transform.parent.localScale.z);
        }
    }
}
