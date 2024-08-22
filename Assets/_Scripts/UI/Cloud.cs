using System;
using UnityEngine;

namespace SVS.UI
{
    public class Cloud : MonoBehaviour
    {
        [SerializeField]
        private float minScale = 1f, maxScale = 1.5f;

        public float speed = 1;

        public event Action OnOutsideScreen;
        public float outsideScreenDistance;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main; 
        }

        private void Update()
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);
            if (screenPoint.x > Screen.width + outsideScreenDistance || screenPoint.y > Screen.height + outsideScreenDistance /*|| screenPoint.x < -outsideScreenDistance*/ || screenPoint.y < -outsideScreenDistance)
            {
                OnOutsideScreen?.Invoke();
                Destroy(gameObject);
            }
        }

        public float GetCloudScale()
        {
            return UnityEngine.Random.Range(minScale, maxScale);
        }

        public void Initialize(float distance, Action onOutsideScreenHandler)
        {
            outsideScreenDistance = distance;
            OnOutsideScreen += onOutsideScreenHandler;
        }
    }
}
