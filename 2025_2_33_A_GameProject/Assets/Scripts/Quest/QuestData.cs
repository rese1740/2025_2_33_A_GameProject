using UnityEngine;

public enum QuestType
{
    Delivery,
    Collect,
    Interact
}

[CreateAssetMenu(fileName = " New Quest", menuName = " Quest System")]
public class QuestData : ScriptableObject
{

    [Header("기본 정보")]
    public string questTitle;
    [TextArea(2, 4)]
    public string questDescription;
    public Sprite questIcon;

    [Header("퀘스트 설정")]
    public QuestType questType;
    public int Amount;

    [Header("배달")]
    public Vector3 deliveryPos;
    public float deliveryRedius = 3f;

    [Header("수집 / 상호작용")]
    public string targetTag = "";

    [Header("보상")]
    public int experienceReward = 100;
    public string rewardMessage = "퀘스트 완료";

    [Header("퀘스트 연결")]
    public QuestData nextQuest;

    [System.NonSerialized] public int currentProgress = 0;
    [System.NonSerialized] public bool isActive = false;
    [System.NonSerialized] public bool isCompleted = false;

    public void Initialize()
    {
        currentProgress = 0;
        isActive = false;
        isCompleted = false;
    }

    public bool IsCompleted()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return currentProgress >= 1;
            case QuestType.Collect:
            case QuestType.Interact:
                return currentProgress >= Amount;
            default:
                return false;
        }
    }

    public float GetProgress()
    {
        if (Amount <= 0) return 0f;
        return Mathf.Clamp01((float)currentProgress / Amount);
    }

    public string GetProgressTxt()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return isCompleted ? "배달 완료!" : "목적지로 이동하세요";
            case QuestType.Collect:
                return $"{currentProgress} / {Amount}";
            case QuestType.Interact:
                return $"{currentProgress} / {Amount}";
            default:
                return "";
        }
    }

}
