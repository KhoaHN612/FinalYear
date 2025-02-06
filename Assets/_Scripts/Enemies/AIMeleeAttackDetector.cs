using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AIEnemy
{
    public class AIMeleeAttackDetector : MonoBehaviour
    {
        public LayerMask targetLayer;

        public UnityEvent<GameObject> OnPlayerDetected;

        public bool isCircle = true;

        [Range(.1f, 1)]
        public float radius;

        public float width, height = 1;

        [Header("Gizmo parameters")]
        public Color gizmoColor = Color.green;
        public bool showGizmos = true;

        public bool PlayerDetected { get; internal set; }

        private void Update()
        {
            Collider2D collider;
            if (isCircle)
            {
                collider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
            }
            else
            {
                collider = Physics2D.OverlapBox(transform.position, new Vector2(width, height) , 0f, targetLayer);
            }

            PlayerDetected = collider != null;
            if (PlayerDetected)
                OnPlayerDetected?.Invoke(collider.gameObject);
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = gizmoColor;
                if (isCircle)
                {
                    Gizmos.DrawWireSphere(transform.position, radius);
                }
                else
                {
                    Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1f));
                }
            }
        }
    }
}