using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.Common
{
    public class OnTriggerInteraction : MonoBehaviour
    {
        public LayerMask collisionMask;
        public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;
        public InteractiveObject interactiveObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & collisionMask) != 0)
            {
                var interactiveInterface = collision.GetComponent<IInteractiveInterface>();
                if (interactiveInterface != null)
                {
                    interactiveInterface.SetInteractiveObject(interactiveObject);
                }

                OnTriggerEnterEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & collisionMask) != 0)
            {
                var interactiveInterface = collision.GetComponent<IInteractiveInterface>();
                if (interactiveInterface != null)
                {
                    interactiveInterface.ClearInteractiveObject(interactiveObject);
                }

                OnTriggerExitEvent?.Invoke();
            }
        }

    }
}