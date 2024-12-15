using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Animations
{
    public class ShakeAnimation : MonoBehaviour
    {
        public float shakeStrength = 0.5f; 
        public float shakeDuration = 1f;   
        public float shakeVibrato = 10f;  
        public float shakeRandomness = 90f; 
        public Ease animationEase = Ease.Linear; 

        private Tween currentTween; 

        public void Shake()
        {
            if (currentTween != null)
            {
                currentTween.Kill();
            }

            // Perform the shake effect on the position
            currentTween = transform
                .DOShakePosition(shakeDuration, shakeStrength, (int)shakeVibrato, shakeRandomness, false, true)
                .SetEase(animationEase)
                .OnKill(() => Debug.Log("Shake Animation Complete")); 
        }
    }
}
