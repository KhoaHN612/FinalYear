using SVS.PlayerAgent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.Pickable
{
    public class CoinPickable : Pickable, IDataPersistence
    {
        [SerializeField] private string id;

        [ContextMenu("Generate guid for id")]
        private void GenerateGuid()
        {
            id = System.Guid.NewGuid().ToString();
        }

        public UnityEvent OnPickUp;
        [SerializeField]
        private int pointsToAdd = 1;
        private bool collected = false;

        public override void PickUp(Agent agent)
        {
            PlayerPoints playerPoints = agent.GetComponent<PlayerPoints>();
            playerPoints.Add(pointsToAdd);
            collected = true;
        }

        public void LoadData(GameData data)
        {
            data.coinsCollected.TryGetValue(id, out collected);
            if (collected)
            {
                this.gameObject.SetActive(false);
            }
        }

        public void SaveData(GameData data)
        {
            if (data.coinsCollected.ContainsKey(id))
            {
                data.coinsCollected.Remove(id);
            }
            data.coinsCollected.Add(id, collected);
        }
    }
}