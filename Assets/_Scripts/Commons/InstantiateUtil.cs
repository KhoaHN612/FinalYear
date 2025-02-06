using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Common
{
    public class InstantiateUtil : MonoBehaviour
    {
        public GameObject objectToInstantiate;
        public int number = 1;
        public Vector2 spawnArea = new Vector2(5f, 0.5f);

        public void CreateObject()
        {
            for (int i = 0; i < number; i++)
            {
                Vector3 randomPosition = GetRandomPosition();

                Instantiate(objectToInstantiate, randomPosition, Quaternion.identity);
            }
        }

        private Vector3 GetRandomPosition()
        {
            float randomX = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
            float randomY = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);

            return transform.position + new Vector3(randomX, randomY, 0f);
        }
    }
}