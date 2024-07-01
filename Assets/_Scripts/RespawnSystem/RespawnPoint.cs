using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RespawnSystem
{
    public class RespawnPoint : MonoBehaviour
    {
        [SerializeField]
        private GameObject respawnTarget;

        [field: SerializeField]
        private UnityEvent OnSpawnPointActivated { get; set; }

        [field: SerializeField]
        private UnityEvent OnSpawnPointRespawn { get; set; }

        private void Start()
        {
            OnSpawnPointActivated.AddListener(() =>
                GetComponentInParent<RespawnPointManager>().UpdateRespawnPoint(this)
            );
            OnSpawnPointActivated.AddListener(() =>
                GetComponentInParent<RespawnPointManager>().ActiveRespawnPoint()
            );
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                this.respawnTarget = collision.gameObject;
                OnSpawnPointActivated?.Invoke();                
            }
        }

        public void RespawnPlayer()
        {
            OnSpawnPointRespawn?.Invoke();
            Vector2 spawnLocation = transform.position;
            spawnLocation.y += 5;
            respawnTarget.transform.position = spawnLocation;
        }

        public void SetPlayerGO(GameObject player)
        {
            respawnTarget = player;
            GetComponent<Collider2D>().enabled = false;
        }

        public void DisableRespawnPoint()
        {
            GetComponent<Collider2D>().enabled = false;
        }

        public void ResetRespawnPoint()
        {
            respawnTarget = null;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}


