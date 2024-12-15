using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class OriginalUI_SkillTree : MonoBehaviour {

    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private OriginalSkillUnlockPath[] skillUnlockPathArray;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private Sprite lineGlowSprite;

    private OriginalPlayerSkills playerSkills;
    private List<OriginalSkillButton> skillButtonList;
    private TMPro.TextMeshProUGUI skillPointsText;

    private void Awake() {
        skillPointsText = transform.Find("skillPointsText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetPlayerSkills(OriginalPlayerSkills playerSkills) {
        this.playerSkills = playerSkills;

        skillButtonList = new List<OriginalSkillButton>();

        skillButtonList.Add(new OriginalSkillButton(transform.Find("stopTime1Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.StopTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("stopTime2Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.StopTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("stopTime3Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.StopTime3, skillLockedMaterial, skillUnlockableMaterial));

        skillButtonList.Add(new OriginalSkillButton(transform.Find("rewindTime1Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.RewindTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("rewindTime2Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.RewindTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("rewindTime3Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.RewindTime3, skillLockedMaterial, skillUnlockableMaterial));

        skillButtonList.Add(new OriginalSkillButton(transform.Find("slowTime1Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.SlowTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("slowTime2Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.SlowTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("slowTime3Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.SlowTime3, skillLockedMaterial, skillUnlockableMaterial));

        skillButtonList.Add(new OriginalSkillButton(transform.Find("speedTime1Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.SpeedTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("speedTime2Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.SpeedTime2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("speedTime3Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.SpeedTime3, skillLockedMaterial, skillUnlockableMaterial));

        skillButtonList.Add(new OriginalSkillButton(transform.Find("maxTime1Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.MaxTime1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("maxTime2Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.MaxTime2, skillLockedMaterial, skillUnlockableMaterial));

        skillButtonList.Add(new OriginalSkillButton(transform.Find("maxHealth1Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.MaxHealth1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new OriginalSkillButton(transform.Find("maxHealth2Btn"), playerSkills, OriginalPlayerSkills.OriginSkillType.MaxHealth2, skillLockedMaterial, skillUnlockableMaterial));


        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        playerSkills.OnSkillPointsChanged += PlayerSkills_OnSkillPointsChanged;

        UpdateVisuals();
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e) {
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, OriginalPlayerSkills.OnSkillUnlockedEventArgs e) {
        UpdateVisuals();
    }

    private void UpdateSkillPoints() {
        skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    }

    private void UpdateVisuals() {
        foreach (OriginalSkillButton skillButton in skillButtonList) {
            skillButton.UpdateVisual();
        }

        // Darken all links
        foreach (OriginalSkillUnlockPath skillUnlockPath in skillUnlockPathArray) {
            foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                linkImage.color = new Color(.5f, .5f, .5f);
                linkImage.sprite = lineSprite;
            }
        }
        
        foreach (OriginalSkillUnlockPath skillUnlockPath in skillUnlockPathArray) {
            if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType)) {
                // Skill unlocked or can be unlocked
                foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                    linkImage.color = Color.white;
                    linkImage.sprite = lineGlowSprite;
                }
            }
        }
    }

    /*
     * Represents a single Skill Button
     * */
    private class OriginalSkillButton {

        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private OriginalPlayerSkills playerSkills;
        private OriginalPlayerSkills.OriginSkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockableMaterial;

        public OriginalSkillButton(Transform transform, OriginalPlayerSkills playerSkills, OriginalPlayerSkills.OriginSkillType skillType, Material skillLockedMaterial, Material skillUnlockableMaterial) {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;

            image = transform.Find("image").GetComponent<Image>();
            backgroundImage = transform.Find("background").GetComponent<Image>();

            transform.GetComponent<Button_UI>().ClickFunc = () => {
                if (!playerSkills.IsSkillUnlocked(skillType)) {
                    // Skill not yet unlocked
                    if (!playerSkills.TryUnlockSkill(skillType)) {
                        OriginalTooltip_Warning.ShowTooltip_Static("Cannot unlock " + skillType + "!");
                    }
                }
            };
        }

        public void UpdateVisual() {
            if (playerSkills.IsSkillUnlocked(skillType)) {
                image.material = null;
                backgroundImage.material = null;
            } else {
                if (playerSkills.CanUnlock(skillType)) {
                    image.material = skillUnlockableMaterial;
                    backgroundImage.color = UtilsClass.GetColorFromString("4B677D");
                    transform.GetComponent<Button_UI>().enabled = true;
                } else {
                    image.material = skillLockedMaterial;
                    backgroundImage.color = new Color(.3f, .3f, .3f);
                    transform.GetComponent<Button_UI>().enabled = false;
                }
            }
        }

    }


    [System.Serializable]
    public class OriginalSkillUnlockPath {
        public OriginalPlayerSkills.OriginSkillType skillType;
        public Image[] linkImageArray;
    }

}
