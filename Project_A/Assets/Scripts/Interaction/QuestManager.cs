using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // ================================================================
    // 전반적인 퀘스트 시스템을 처리하도록 함
    // 게임 전체에서 지속되는 개체에 부착되어야 함
    // ================================================================

    public static QuestManager instance;  // 싱글톤 패턴 

    public List<Quest> activeQuests = new List<Quest>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartQuest(Quest quest)
    {
        activeQuests.Add(quest);
        UpdateQuestUI();
    }

    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        activeQuests.Remove(quest);
        UpdateQuestUI();
        // 필요한 경우 보상 로직을 여기에 추가합니다.
    }

    void UpdateQuestUI()
    {
        // UI 업데이트 로직을 여기에 구현합니다.
    }
}