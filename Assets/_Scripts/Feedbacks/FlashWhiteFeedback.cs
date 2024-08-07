using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feedback
{
    public class FlashWhiteFeedback : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private float feebdackTime = 0.1f;

        public void PlayFeedback()
        {
            if (spriteRenderer == null || spriteRenderer.material.HasProperty("_ActiceSolidShader") == false)
            {
                return;
            }
            ToggleMaterial(1);
            StopAllCoroutines();
            StartCoroutine(ResetColor());
        }

        private void ToggleMaterial(int val)
        {
            val = Mathf.Clamp(val, 0, 1);
            spriteRenderer.material.SetInt("_ActiceSolidShader", val);
        }

        IEnumerator ResetColor()
        {
            yield return new WaitForSeconds(feebdackTime);
            ToggleMaterial(0);
        }
    }
}
