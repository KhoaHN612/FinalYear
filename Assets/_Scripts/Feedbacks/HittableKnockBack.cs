using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace SVS.Feedback
{
    public class HittableKnockBack : MonoBehaviour, IHittable
    {
        [SerializeField]
        private Rigidbody2D rb2d;
        [SerializeField]
        private float force = 10;

        public bool CanHit { get; set; } = true;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        public void GetHit(GameObject opponent, int weaponDamage)
        {
            if (!CanHit) return;

            Vector2 direction = transform.position - opponent.transform.position;
            rb2d.AddForce(new Vector2(direction.normalized.x, 0) * force, ForceMode2D.Impulse);
        }
    }
}
