using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.Common
{
    public class OnTrigger2DUtil : MonoBehaviour
    {
        public LayerMask collisionMask;
        public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & collisionMask) != 0)
            {
                OnTriggerEnterEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & collisionMask) != 0)
            {
                OnTriggerExitEvent?.Invoke();
            }
        }

    }
}