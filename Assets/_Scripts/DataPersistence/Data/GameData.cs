using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float totalPlaytime;
    public long lastUpdated;
    public int coinCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> coinsCollected;
    public string currentSpawnPointId;

    public GameData()
    {
        this.totalPlaytime = 0;
        this.lastUpdated = 0;
        this.coinCount = 0;
        this.currentSpawnPointId = null; 
        this.playerPosition = Vector3.zero;
        this.coinsCollected = new SerializableDictionary<string, bool>();
    }
}