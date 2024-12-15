using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : MonoBehaviour
{
    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private Sprite lineGlowSprite;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;
    [SerializeField]
    private TMPro.TextMeshProUGUI skillPointsText;

    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();

        skillButtonList.Add(new SkillButton(transform.Find("StopTime1Btn"), playerSkills, SkillType.StopTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("StopTime2Btn"), playerSkills, SkillType.StopTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("StopTime3Btn"), playerSkills, SkillType.StopTime3, skillLockedMaterial, skillUnlockableMaterial));
 
        skillButtonList.Add(new SkillButton(transform.Find("RewindTime1Btn"), playerSkills, SkillType.RewindTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("RewindTime2Btn"), playerSkills, SkillType.RewindTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("RewindTime3Btn"), playerSkills, SkillType.RewindTime3, skillLockedMaterial, skillUnlockableMaterial));
 
        skillButtonList.Add(new SkillButton(transform.Find("SlowTime1Btn"), playerSkills, SkillType.SlowTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("SlowTime2Btn"), playerSkills, SkillType.SlowTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("SlowTime3Btn"), playerSkills, SkillType.SlowTime3, skillLockedMaterial, skillUnlockableMaterial));
 
        skillButtonList.Add(new SkillButton(transform.Find("SpeedTime1Btn"), playerSkills, SkillType.SpeedTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("SpeedTime2Btn"), playerSkills, SkillType.SpeedTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("SpeedTime3Btn"), playerSkills, SkillType.SpeedTime3, skillLockedMaterial, skillUnlockableMaterial));
 
        skillButtonList.Add(new SkillButton(transform.Find("MaxTime1Btn"), playerSkills, SkillType.MaxTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("MaxTime2Btn"), playerSkills, SkillType.MaxTime2, skillLockedMaterial, skillUnlockableMaterial));
 
        skillButtonList.Add(new SkillButton(transform.Find("MaxHealth1Btn"), playerSkills, SkillType.MaxHealth1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("MaxHealth2Btn"), playerSkills, SkillType.MaxHealth2, skillLockedMaterial, skillUnlockableMaterial));


        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        playerSkills.OnSkillPointsChanged += PlayerSkills_OnSkillPointsChanged;

        UpdateVisuals();
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e)
    {
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateSkillPoints()
    {
        skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    }

    private void UpdateVisuals()
    {
        foreach (SkillButton skillButton in skillButtonList)
        {
            skillButton.UpdateVisual();
        }

        // Darken all links
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
        {
            foreach (Image linkImage in skillUnlockPath.linkImageArray)
            {
                linkImage.color = new Color(.8f, .8f, .8f);
                linkImage.sprite = lineSprite;
            }
        }

        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
        {
            if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType))
            {
                // Skill unlocked or can be unlocked
                foreach (Image linkImage in skillUnlockPath.linkImageArray)
                {
                    linkImage.color = Color.white;
                    linkImage.sprite = lineGlowSprite;
                }
            }
        }
    }

    /*
     * Represents a single Skill Button
     * */
    private class SkillButton
    {

        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private PlayerSkills playerSkills;
        private SkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockableMaterial;

        public SkillButton(Transform transform, PlayerSkills playerSkills, SkillType skillType, Material skillLockedMaterial, Material skillUnlockableMaterial)
        {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;

            image = transform.Find("Icon").GetComponent<Image>();

            backgroundImage = transform.GetComponent<Image>();

            transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (!playerSkills.IsSkillUnlocked(skillType))
                {
                    // Skill not yet unlocked
                    if (!playerSkills.TryUnlockSkill(skillType))
                    {
                        //OriginalTooltip_Warning.ShowTooltip_Static("Cannot unlock " + skillType + "!");
                    }
                }
            });
        }

        public void UpdateVisual()
        {
            if (playerSkills.IsSkillUnlocked(skillType))
            {
                image.material = null;
                backgroundImage.material = null;
            }
            else
            {
                if (playerSkills.CanUnlock(skillType))
                {
                    image.material = skillUnlockableMaterial;
                    backgroundImage.color = UtilsClass.GetColorFromString("4B677D");
                    transform.GetComponent<Button>().interactable = true;
                }
                else
                {
                    image.material = skillLockedMaterial;
                    backgroundImage.color = new Color(.3f, .3f, .3f);
                    transform.GetComponent<Button>().interactable = false;
                }
            }
        }

    }


    [System.Serializable]
    public class SkillUnlockPath
    {
        public SkillType skillType;
        public Image[] linkImageArray;
    }
}
