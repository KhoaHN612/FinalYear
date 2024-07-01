using System.Collections;
using System.Collections.Generic;
using RespawnSystem;
using UnityEngine;

public class DestroyFallingObjects : MonoBehaviour
{
    public LayerMask objectsToDestoryLayerMask;
    public Vector2 size;

    [Header("Gizmo parameters")]
    public Color gizmoColor = Color.red;
    public bool showGizmo = true;

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, size, 0, objectsToDestoryLayerMask);
        if(collider != null)
        {
            Agent agent = collider.GetComponent<Agent>();
            if(agent == null)
            {
                Destroy(collider.gameObject);
                return;
            }
            var damagable = agent.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.GetHit(damagable.CurrentHealth);
                if (agent.CompareTag("Player"))
                {
                    agent.GetComponent<RespawnHelper>().RespawnPlayer();
                }
            }
                
            agent.AgentDied();
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, size);
        }
    }
}
