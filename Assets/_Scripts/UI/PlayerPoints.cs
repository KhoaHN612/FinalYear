using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.PlayerAgent
{
    public class PlayerPoints : MonoBehaviour, IDataPersistence
    {
        public UnityEvent<int> OnPointsValueChange;
        public UnityEvent OnPickUpPoints;
        private int points = 0;

        public int Points { get => points; private set => points = value; }

        private void Start()
        {
            OnPointsValueChange?.Invoke(Points);
        }

        public void Add(int amount)
        {
            Points += amount;
            OnPickUpPoints?.Invoke();
            OnPointsValueChange?.Invoke(Points);
        }

        public void LoadData(GameData data)
        {
            this.points = data.coinCount;
        }

        public void SaveData(GameData data)
        {
            data.coinCount = Points;
        }
    }
}