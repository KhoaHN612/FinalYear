using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI totalPlaytimeText;
    [SerializeField] private TextMeshProUGUI lastPlayTimeText;

    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        // there's no data for this profileId
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        // there is data for this profileId
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            totalPlaytimeText.text = FormatPlaytime(data.totalPlaytime);
            lastPlayTimeText.text = "Last play: " + DateTime.FromBinary(data.lastUpdated);
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
    public string FormatPlaytime(float totalPlaytime)
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(totalPlaytime);

        return string.Format("{0:D2}:{1:D2}:{2:D2}",
                             time.Hours,
                             time.Minutes,
                             time.Seconds);
    }

}