using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RespawnSystem
{
    public class RespawnPointManager : MonoBehaviour, IDataPersistence
    {
        [SerializeField] Transform startingPoint;
        List<RespawnPoint> respawnPoints = new List<RespawnPoint>();
        [SerializeField] RespawnPoint currentRespawnPoint;
        [SerializeField]
        public UnityEvent respawnPointActive;

        private void Awake()
        {
            foreach (Transform item in transform)
            {
                if (item.GetComponent<RespawnPoint>() != null)
                {
                    respawnPoints.Add(item.GetComponent<RespawnPoint>());
                }
            }
            if (respawnPoints.Count > 0)
                currentRespawnPoint = respawnPoints[0];

            //Debug.Log(respawnPoints.Count);
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

        public void LoadData(GameData data)
        {
            GameObject player = GameObject.FindWithTag("Player");
            //Debug.Log(respawnPoints.Count);
            foreach (RespawnPoint respawnPoint in respawnPoints)
            {
                if (respawnPoint.id.Equals(data.currentSpawnPointId))
                {
                    UpdateRespawnPoint(respawnPoint);
                    respawnPoint.SetPlayerGO(player);
                    Respawn(player);
                    return;
                }
            }
            player.transform.position = startingPoint.position;
        }

        public void SaveData(GameData data)
        {
            //Debug.Log("Current spawnpoint" + currentRespawnPoint.id);
            if (currentRespawnPoint != null)
                data.currentSpawnPointId = currentRespawnPoint.id;
        }
    }
}

