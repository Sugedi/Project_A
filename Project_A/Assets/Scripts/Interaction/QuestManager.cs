using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // ================================================================
    // �������� ����Ʈ �ý����� ó���ϵ��� ��
    // ���� ��ü���� ���ӵǴ� ��ü�� �����Ǿ�� ��
    // ================================================================

    public static QuestManager instance;  // �̱��� ���� 

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
        // �ʿ��� ��� ���� ������ ���⿡ �߰��մϴ�.
    }

    void UpdateQuestUI()
    {
        // UI ������Ʈ ������ ���⿡ �����մϴ�.
    }
}