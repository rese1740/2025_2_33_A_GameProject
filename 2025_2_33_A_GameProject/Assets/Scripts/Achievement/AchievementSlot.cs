using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    [Header("UI 세팅")]
    public Image iconImg;
    public Text nameTxt;
    public Text descriptionTxt;
    public Text progressTxt;
    public Slider progressSlider;

    public void SetAchievement(AchievementData data, float progress)
    {
        if(nameTxt != null)
            nameTxt.text = data.name;

        if(descriptionTxt != null)
            descriptionTxt.text = data.achievementDescription;

        if(iconImg != null)
            iconImg.sprite = data.icon;

        if(progressSlider != null)
            progressSlider.value = data.isUnlocked ? 1f : progress;

        if (progressTxt != null)
        {
            if (data.isUnlocked)
            {
                progressTxt.text = "완료";
            }
            else
            {
                int current = Mathf.FloorToInt(progress * data.requiredAmount);
                progressTxt.text = current + "/" + data.requiredAmount;
            }
        }
    }
}
