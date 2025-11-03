using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [Header("诀利 技泼")]
    public List<AchievementData> achievements = new List<AchievementData>();

    [Header("UI 技泼")]
    public GameObject achievementPopupPrefab;
    public Transform popupParent;
    public GameObject achievementPanel;
    public Transform achievementListContent;
    public GameObject achievementSlotPrefab;

    private Dictionary<AchievementType, int> progressData = new Dictionary<AchievementType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetAllAchievements();
        foreach (AchievementType type in System.Enum.GetValues(typeof(AchievementType)))
        {
            progressData[type] = 0;
        }

        LoadAchievements();
        UpdateAchiebementUI();
    }

    public void UpdateAchiebementUI()
    {
        if (achievementListContent == null || achievementPopupPrefab == null)
            return;

        foreach (Transform child in achievementListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (AchievementData achievement in achievements)
        {
            GameObject slot = Instantiate(achievementSlotPrefab, achievementListContent);
            AchievementSlot slotScript = slot.GetComponent<AchievementSlot>();
            if (slotScript != null)
            {
                slotScript.SetAchievement(achievement, GetProgress(achievement));
            }
        }
    }

    void ShowAchievementPopup(AchievementData achievement)
    {
        if (achievementPopupPrefab != null && popupParent != null)
        {
            GameObject popup = Instantiate(achievementPopupPrefab, popupParent);

            Text titleTxt = popup.transform.Find("Title")?.GetComponent<Text>();
            Text descTxt = popup.transform.Find("Description")?.GetComponent<Text>();

            if (titleTxt != null) titleTxt.text = "诀利 崔己";
            if (descTxt != null) descTxt.text = achievement.achievementName;

            Destroy(popup, 3.0f);
        }
    }

    public float GetProgress(AchievementData achievement)
    {
        if (achievement.isUnlocked) return 1f;
        int current = progressData.ContainsKey(achievement.achievementType) ? progressData[achievement.achievementType] : 0;
        return Mathf.Min((float)current / achievement.requiredAmount, 1f);
    }

    public void UpdateProgress(AchievementType type, int amount = 1)
    {
        progressData[type] += amount;

        foreach (AchievementData achievement in achievements)
        {
            if (achievement.achievementType == type && !achievement.isUnlocked)
            {
                if (progressData[type] >= achievement.requiredAmount)
                {
                    UnlockAchievement(achievement);
                }
            }
        }
    }
    void UnlockAchievement(AchievementData achievement)
    {
        achievement.isUnlocked = true;
        ShowAchievementPopup(achievement);
        UpdateAchiebementUI();
    }

    void SaveAchievements()
    {
        foreach (var kvp in progressData)
        {
            PlayerPrefs.SetInt("Achievement_" + kvp.Key, kvp.Value);
        }

        foreach (AchievementData achievement in achievements)
        {
            PlayerPrefs.SetInt("Unlocked_" + achievement.name, achievement.isUnlocked ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    void LoadAchievements()
    {
        foreach (AchievementType type in System.Enum.GetValues(typeof(AchievementType)))
        {
            progressData[type] = PlayerPrefs.GetInt("Achievement_" + type, 0);
        }

        foreach (AchievementData achievement in achievements)
        {
            achievement.isUnlocked = PlayerPrefs.GetInt("Unlocked_" + achievement.name, 0) == 1;
        }
    }

    void ResetAllAchievements()
    {
        foreach (AchievementType type in System.Enum.GetValues(typeof(AchievementType)))
        {
            progressData[type] = 0;
            PlayerPrefs.DeleteKey("Achievement_" + type);
        }

        foreach (AchievementData achievement in achievements)
        {
            achievement.isUnlocked = false;
           PlayerPrefs.DeleteKey("Unlocked_" +  achievement.name);
        }

        PlayerPrefs.Save();
        UpdateAchiebementUI();
    }
}
