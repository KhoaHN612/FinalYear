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
    public int skillPoints;
    public List<SkillType> unlockedSkillTypeList;
    public int playerBonusHealth;
    public int playerBonusMana;
    public int playerBonusTime;

    public GameData()
    {
        this.totalPlaytime = 0;
        this.lastUpdated = 0;
        this.coinCount = 0;
        this.currentSpawnPointId = null; 
        this.playerPosition = Vector3.zero;
        this.coinsCollected = new SerializableDictionary<string, bool>();
        skillPoints = 0;
        this.unlockedSkillTypeList = new List<SkillType>();
        this.playerBonusHealth = 0;
        this.playerBonusMana = 0;
        this.playerBonusTime = 0;
    }
}