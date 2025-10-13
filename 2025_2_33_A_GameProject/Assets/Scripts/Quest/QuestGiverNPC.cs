using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverNPC : InteractableObject
{
    [Header("NPC Quest Setting")]
    public QuestData quest;
    public string npcName = "NPC";
    public string questStartMessage = "새 퀘스트 있음";
    public string noQuestMessage = "퀘스트 없음";
    public string QuestAlreadyMessage = "이미 진행중임";

    

    private QuestManager questManager;

    protected override void Start()
    {
        base.Start();
        questManager = GetComponent<QuestManager>();
        if(questManager != null)
        {
            Debug.Log("QuestManager 가 없습니다");
        }
        interactionText = "[E]" + npcName + "와 대화하기";
    }

    public override void Interact()
    {
        base.Interact();

        questManager.StartQuest(quest);
    }

    private void Update()
    {
        if (quest != null && questManager != null && questManager.currentQuest == null)
        {
            interactionText = "[E]" + npcName + "와 대화하기";
        }
        else if (questManager != null && questManager.currentQuest != null)
        {
            interactionText = "[E]" + npcName;
        }
    }

}
