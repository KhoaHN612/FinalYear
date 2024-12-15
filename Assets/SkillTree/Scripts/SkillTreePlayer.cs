using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreePlayer : MonoBehaviour {

    [SerializeField] private ExperienceBar experienceBar;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;

    private SkillTreePlayerSword playerSword;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;
    private OriginalPlayerSkills playerSkills;

    private void Awake() {
        playerSword = GetComponent<SkillTreePlayerSword>();
        levelSystem = new LevelSystem();
        levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        playerSkills = new OriginalPlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, OriginalPlayerSkills.OnSkillUnlockedEventArgs e) {
        switch (e.skillType) {
        case OriginalPlayerSkills.OriginSkillType.MaxTime1:
            SetMovementSpeed(65f);
            break;
        case OriginalPlayerSkills.OriginSkillType.MaxTime2:
            SetMovementSpeed(80f);
            break;
        case OriginalPlayerSkills.OriginSkillType.MaxHealth1:
            SetHealthAmountMax(12);
            break;
        case OriginalPlayerSkills.OriginSkillType.MaxHealth2:
            SetHealthAmountMax(15);
            break;
        }
    }

    private void Start() {
        playerSword.OnEnemyKilled += PlayerSword_OnEnemyKilled;
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
        levelText.SetText((levelSystemAnimated.GetLevelNumber() + 1).ToString());
    }

    public OriginalPlayerSkills GetPlayerSkills() {
        return playerSkills;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e) {
        // Level Up
        levelText.SetText((levelSystemAnimated.GetLevelNumber() + 1).ToString());
        //SetHealthAmountMax(8 + levelSystemAnimated.GetLevelNumber());
        playerSkills.AddSkillPoint();
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e) {
        experienceBar.SetSize(levelSystemAnimated.GetExperienceNormalized());
    }

    private void PlayerSword_OnEnemyKilled(object sender, System.EventArgs e) {
        levelSystem.AddExperience(30);
    }

    public bool CanUseEarthshatter() {
        //return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Earthshatter);
        return true;
    }

    public bool CanUseWhirlwind() {
        //return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Whirlwind);
        return true;
    }

    private void SetMovementSpeed(float movementSpeed) {
        playerSword.SetMovementSpeed(movementSpeed);
    }

    private void SetHealthAmountMax(int healthAmountMax) {
        playerSword.SetHealthAmountMax(healthAmountMax);
    }

}
