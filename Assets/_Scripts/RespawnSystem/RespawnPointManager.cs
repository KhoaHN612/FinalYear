using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RespawnSystem
{
    public class RespawnPointManager : MonoBehaviour
    {
        List<RespawnPoint> respawnPoints = new List<RespawnPoint>();
        RespawnPoint currentRespawnPoint;
        [SerializeField]
        public UnityEvent respawnPointActive;

        private void Awake()
        {
            foreach (Transform item in transform)
            {
                respawnPoints.Add(item.GetComponent<RespawnPoint>());
            }
            currentRespawnPoint = respawnPoints[0];
        }

        public void UpdateRespawnPoint(RespawnPoint newRespawnPoint)
        {
            currentRespawnPoint.DisableRespawnPoint();
            currentRespawnPoint = newRespawnPoint;
        }

        public void ActiveRespawnPoint(){
            respawnPointActive?.Invoke();
        }

        public void Respawn(GameObject objectToRespawn)
        {
            currentRespawnPoint.RespawnPlayer();
            objectToRespawn.SetActive(true);
        }

        public void RespawnAt(RespawnPoint spawnPoint, GameObject playeGO)
        {

            spawnPoint.SetPlayerGO(playeGO);
            Respawn(playeGO);
        }

        public void ResetAllSpawnPoints()
        {
            foreach (var item in respawnPoints)
            {
                item.ResetRespawnPoint();

            }
            currentRespawnPoint = respawnPoints[0];
        }
    }
}

