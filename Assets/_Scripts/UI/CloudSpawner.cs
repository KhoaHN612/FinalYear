using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SVS.UI
{
    public class CloudSpawner : MonoBehaviour
    {
        [SerializeField]
        private float width = 1920, length = 1080;
        [SerializeField]
        private Color gizmoColor = new Color(1, 0, 0, 0.2f);
        [SerializeField]
        private bool showGizmo = true;
        [SerializeField]
        private List<Cloud> cloudPrefabs = new List<Cloud>();

        [SerializeField]
        private float cloudSpeedMin = 1;
        [SerializeField]
        private float cloudSpeedMax = 10;
        [SerializeField]
        private float scaleModifier = 0;

        public Canvas canvas;


        private void Start()
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<Cloud>().Initialize(canvas.scaleFactor + 50, SpawnClouds);
            }
        }

        private void SpawnClouds()
        {
            Vector3 position = GetRandomPosition();

            int cloudIndex = Random.Range(0, cloudPrefabs.Count);
            Cloud cloud = cloudPrefabs[cloudIndex];

            float scale = cloud.GetCloudScale() + scaleModifier;

            GameObject cloudObject = Instantiate(cloud.gameObject);

            RectTransform rectTransform = cloudObject.GetComponent<RectTransform>();

            rectTransform.position = position;

            rectTransform.localScale = Vector3.one * scale * canvas.scaleFactor;

            //Debug.Log(rectTransform.localScale);

            Cloud newCloud = cloudObject.GetComponent<Cloud>();

            newCloud.speed = Random.Range(cloudSpeedMin,cloudSpeedMax);

            rectTransform.SetParent(transform,false);

            newCloud.Initialize(canvas.scaleFactor + 50, SpawnClouds);

        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                transform.position.x - (width / 2) * canvas.scaleFactor - 500 ,
                Random.Range(transform.position.y - length / 2 * canvas.scaleFactor, transform.position.y + length / 2 * canvas.scaleFactor),
                0);
        }

        /*        private void OnDrawGizmos()
                {
                    if (showGizmo && canvas != null)
                    {
                        Gizmos.color = gizmoColor;
                        Gizmos.DrawCube(
                            transform.position,
                            new Vector2(width, length) * canvas.scaleFactor);

                    }
                }*/

        private void OnDrawGizmos()
        {
            if (showGizmo && canvas != null)
            {
                Gizmos.color = gizmoColor;

                // Convert the center position from screen space to world space
                Vector3 uiWorldPosition = transform.position;

                // Calculate the size in world space, but scale it down appropriately
                Vector3 screenSize = new Vector3(width, length, 0);
                Vector3 sizeInWorldSpace = canvas.worldCamera.ScreenToWorldPoint(screenSize + new Vector3(Screen.width / 2, Screen.height / 2, canvas.planeDistance))
                                           - canvas.worldCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, canvas.planeDistance));

                // Ensure the Z component is set to 1 for 2D Gizmos
                sizeInWorldSpace.z = 1;

                // Draw the Gizmos cube in the correct position and size
                Gizmos.DrawCube(uiWorldPosition, sizeInWorldSpace);
            }
        }


    }
}