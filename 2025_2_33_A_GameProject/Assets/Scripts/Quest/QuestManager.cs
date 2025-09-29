using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    [Header("UI 요소들")]
    public GameObject questUI;
    public Text questTileTxt;
    public Text questDescriptionTxt;
    public Text questProgressTxt;
    public Button completeButton;

    [Header("퀘스트 목록")]
    public QuestData[] availableQuests;

    private QuestData currentQuest;
    private int currentQuestIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (availableQuests.Length > 0)
        {
            StartQuest(availableQuests[0]);
        }
        if (completeButton != null)
        {
            completeButton.onClick.AddListener(CompleteCurrentQuest);

        }
    }
    private void Update()
    {
        if (currentQuest != null && currentQuest.isActive)
        {
            CheckDeliveryProgress();
            UpdateQuestUI();
        }
    }

    void UpdateQuestUI()
    {
        if (currentQuest == null) return;

        if (questTileTxt != null)
        {
            questTileTxt.text = currentQuest.questTitle;
        }

        if (questDescriptionTxt != null)
        {
            questDescriptionTxt.text = currentQuest.questDescription;
        }

        if (questProgressTxt != null)
        {
            questProgressTxt.text = currentQuest.GetProgressTxt();
        }
    }

    public void StartQuest(QuestData quest)
    {
        if (quest == null) return;

        currentQuest = quest;
        currentQuest.Initialize();
        currentQuest.isActive = true;

        Debug.Log("퀘스트 시작 :" + questTileTxt);
        UpdateQuestUI();
        if (quest != null)
        {
            questUI.SetActive(true);
        }
    }

    void CheckDeliveryProgress()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, currentQuest.deliveryPos);

        if (distance <= currentQuest.deliveryRedius)
        {
            if (currentQuest.currentProgress == 0)
            {
                currentQuest.currentProgress = 1;
            }
        }
        else
        {
            currentQuest.currentProgress = 0;
        }
    }

    public void AddCollectProgress(string itemTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Collect && currentQuest.targetTag == itemTag)
        {
            currentQuest.currentProgress++;
            Debug.Log("아이템 수집" + itemTag);
        }
    }

    public void AddInteractProgress(string objectTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Interact && currentQuest.targetTag == objectTag)
        {
            currentQuest.currentProgress++;
            Debug.Log("상호작용 완료" + objectTag);
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuest == null || !currentQuest.isCompleted) return;

        Debug.Log("퀘스트 완");

        if (completeButton != null)
        {
            completeButton.gameObject.SetActive(false);
        }

        currentQuestIndex++;
        if (currentQuestIndex < availableQuests.Length)
        {
            StartQuest(availableQuests[currentQuestIndex]);
        }
        else
        {
            currentQuest = null;
            if (questUI != null)
            {
                questUI.gameObject.SetActive(false);
            }
        }
    }
}
