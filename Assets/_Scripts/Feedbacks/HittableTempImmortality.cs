using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using WeaponSystem;

namespace SVS.Feedback
{
    public class HittableTempImmortality : MonoBehaviour, IHittable
    {
        public Collider2D[] colliders = new Collider2D[0];
        public IHittable[] hitabilities = new IHittable[0];
        public float immortalityTime = 1;

        public SpriteRenderer spriteRenderer;
        public float flashDelay = 0.1f;
        [Range(0, 1)]
        public float flashAlpha = 0.5f;


        [Header("For debug purposes")]
        public bool isImmortal = false;

        public bool CanHit { get; set; } = true;

        private void Awake()
        {
            if (colliders.Length == 0)
                colliders = GetComponents<Collider2D>();

            if (hitabilities.Length == 0)
                hitabilities = GetComponents<IHittable>();
        }

        public void GetHit(GameObject gameObject, int weaponDamage)
        {
            if (!CanHit) return;

            if (this.enabled == false)
                return;


            /*ToggleColliders(false);
            StartCoroutine(ResetColliders());*/

            ToggleGetDamage(false);
            StartCoroutine(ResetGetDamage());

            StartCoroutine(Flash(flashAlpha));
        }

        private void ToggleGetDamage(bool val)
        {
            isImmortal = !val;
            foreach (var hitable in hitabilities)
            {
                hitable.CanHit = val; 
            }
        }

        private void ToggleColliders(bool val)
        {
            isImmortal = !val;
            foreach (var collider in colliders)
            {
                collider.enabled = val;
            }
        }

        IEnumerator ResetColliders()
        {
            yield return new WaitForSeconds(immortalityTime);
            StopAllCoroutines();
            ToggleColliders(true);
            ChangeSpriteRendererColorAlpha(1);
        }

        IEnumerator ResetGetDamage()
        {
            yield return new WaitForSeconds(immortalityTime);
            StopAllCoroutines();
            ToggleGetDamage(true);
            ChangeSpriteRendererColorAlpha(1);
        }

        private void ChangeSpriteRendererColorAlpha(float alpha)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }

        IEnumerator Flash(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            ChangeSpriteRendererColorAlpha(alpha);
            yield return new WaitForSeconds(flashDelay);
            StartCoroutine(Flash(alpha < 1 ? 1 : flashAlpha));

        }
    }
}
