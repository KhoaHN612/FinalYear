using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalPlayerSkills {

    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs {
        public OriginSkillType skillType;
    }

    public enum OriginSkillType {
        None,
        StopTime1,
        StopTime2,
        StopTime3,
        RewindTime1,
        RewindTime2,
        RewindTime3,
        SlowTime1,
        SlowTime2,
        SlowTime3,
        SpeedTime1,
        SpeedTime2,
        SpeedTime3,
        MaxTime1,
        MaxTime2,
        MaxHealth1,
        MaxHealth2,
    }

    private List<OriginSkillType> unlockedSkillTypeList;
    private int skillPoints;

    public OriginalPlayerSkills() {
        unlockedSkillTypeList = new List<OriginSkillType>();
    }

    public void AddSkillPoint() {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSkillPoints() {
        return skillPoints;
    }

    private void UnlockSkill(OriginSkillType skillType) {
        if (!IsSkillUnlocked(skillType)) {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(OriginSkillType skillType) {
        return unlockedSkillTypeList.Contains(skillType);
    }

    public bool CanUnlock(OriginSkillType skillType) {
        OriginSkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != OriginSkillType.None) {
            if (IsSkillUnlocked(skillRequirement)) {
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    public OriginSkillType GetSkillRequirement(OriginSkillType skillType) {
        switch (skillType) {
            case OriginSkillType.MaxHealth2: return OriginSkillType.MaxHealth1;
            case OriginSkillType.MaxTime2: return OriginSkillType.MaxTime1;

            case OriginSkillType.StopTime1: return OriginSkillType.MaxTime2;
            case OriginSkillType.StopTime2: return OriginSkillType.StopTime1;
            case OriginSkillType.StopTime3: return OriginSkillType.StopTime2;

            case OriginSkillType.RewindTime1: return OriginSkillType.MaxTime2;
            case OriginSkillType.RewindTime2: return OriginSkillType.RewindTime1;
            case OriginSkillType.RewindTime3: return OriginSkillType.RewindTime2;

            case OriginSkillType.SlowTime1: return OriginSkillType.MaxHealth2;
            case OriginSkillType.SlowTime2: return OriginSkillType.SlowTime1;
            case OriginSkillType.SlowTime3: return OriginSkillType.SlowTime2;

            case OriginSkillType.SpeedTime1: return OriginSkillType.MaxHealth2;
            case OriginSkillType.SpeedTime2: return OriginSkillType.SpeedTime1;
            case OriginSkillType.SpeedTime3: return OriginSkillType.SpeedTime2;
        }
        return OriginSkillType.None;
    }

    public bool TryUnlockSkill(OriginSkillType skillType) {
        if (CanUnlock(skillType)) {
            if (skillPoints > 0) {
                skillPoints--;
                OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
                UnlockSkill(skillType);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

}
