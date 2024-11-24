using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Melee AoE weapon data", menuName = "Weapons/MeleeAoEWeaponData")]
    public class MeleeAoEWeaponData : WeaponData
    {
        public float aoeRadius = 1f; // Radius of the AoE (Area of Effect)

        public override bool CanBeUsed(bool isGrounded)
        {
            return isGrounded == true; // Only allow attacks when the character is grounded
        }

        public override void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction)
        {
            // Check all objects within the AoE radius
            Collider2D[] hits = Physics2D.OverlapCircleAll(agent.agentWeapon.transform.position, aoeRadius, hittableMask);

            foreach (var hit in hits)
            {
                // Find all IHittable components on each object hit
                foreach (var hittable in hit.GetComponents<IHittable>())
                {
                    hittable.GetHit(agent.gameObject, weaponDamage);
                }
            }
        }

        public override void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            // Draw the AoE (circle) area
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(origin, aoeRadius);
        }
    }
}
