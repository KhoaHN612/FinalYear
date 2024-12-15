using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour, IDataPersistence
{
    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }

    private List<SkillType> unlockedSkillTypeList;
    [SerializeField]
    private int skillPoints;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }

    public void AddSkillPoint()
    {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }

    private void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }

    public bool CanUnlock(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.MaxHealth2: return SkillType.MaxHealth1;
            case SkillType.MaxTime2: return SkillType.MaxTime1;

            case SkillType.StopTime1: return SkillType.MaxTime2;
            case SkillType.StopTime2: return SkillType.StopTime1;
            case SkillType.StopTime3: return SkillType.StopTime2;

            case SkillType.RewindTime1: return SkillType.MaxTime2;
            case SkillType.RewindTime2: return SkillType.RewindTime1;
            case SkillType.RewindTime3: return SkillType.RewindTime2;

            case SkillType.SlowTime1: return SkillType.MaxHealth2;
            case SkillType.SlowTime2: return SkillType.SlowTime1;
            case SkillType.SlowTime3: return SkillType.SlowTime2;

            case SkillType.SpeedTime1: return SkillType.MaxHealth2;
            case SkillType.SpeedTime2: return SkillType.SpeedTime1;
            case SkillType.SpeedTime3: return SkillType.SpeedTime2;
        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        if (CanUnlock(skillType))
        {
            if (skillPoints > 0)
            {
                skillPoints--;
                OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
                UnlockSkill(skillType);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void LoadData(GameData data)
    {
        skillPoints = data.skillPoints;
        unlockedSkillTypeList = data.unlockedSkillTypeList;
    }

    public void SaveData(GameData data)
    {
        data.skillPoints = skillPoints;
        data.unlockedSkillTypeList = unlockedSkillTypeList;
    }
}

public enum SkillType
{
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
