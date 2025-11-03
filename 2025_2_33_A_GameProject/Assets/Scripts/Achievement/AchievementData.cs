using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Achievement", menuName = "Achievement")]
public class AchievementData : ScriptableObject
{
    [Header("기본 세팅")]
    public string achievementName;
    public string achievementDescription;
    public AchievementType achievementType;
    public int requiredAmount;
    public int rewardCoins;
    public bool isUnlocked;
    public Sprite icon;
}
