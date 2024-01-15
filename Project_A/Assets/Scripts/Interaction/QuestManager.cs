using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int mainquestID; // ���� ����Ʈ ID
    public string mainquestName; // ���� ����Ʈ �̸�
    public string mainquestDescription; // ���� ����Ʈ ����
    public string mainquestGiver; // ����Ʈ ������
    // mainquestType�� ������� �����Ƿ� �����մϴ�.
    public Dictionary<string, int> questConditions = new Dictionary<string, int>(); // ����Ʈ ���ǵ�
    public string mainReward; // ���� ����
    public int rewardAmount; // ���� ����

    // ����Ʈ ������
    public Quest(int id, string name, string description, string giver, Dictionary<string, int> conditions, string reward, int amount)
    {
        mainquestID = id;
        mainquestName = name;
        mainquestDescription = description;
        mainquestGiver = giver;
        questConditions = conditions;
        mainReward = reward;
        rewardAmount = amount;
    }
}

public class QuestManager : MonoBehaviour
{
    // �������� ����Ʈ �ý����� ó���մϴ�.
    // ���� ��ü���� ���ӵǴ� ��ü�� �����ؾ� �մϴ�.

    List<Dictionary<string, object>> data_Mainquest; // CSV ���Ͽ��� �о�� ����Ʈ �����͸� ������ ����Ʈ

    // UI ��Ҹ� ������ TextMeshProUGUI ������
    public TextMeshProUGUI questname; // ����Ʈ �̸� UI
    public TextMeshProUGUI questcontent; // ����Ʈ ���� UI

    [SerializeField]
    int id; // ���� ����Ʈ�� ID

    private Dictionary<int, Quest> quests = new Dictionary<int, Quest>(); // Ȱ�� ����Ʈ��

    // ����Ʈ �����͸� �ε��մϴ�.
    public void LoadQuests(List<Dictionary<string, object>> questData)
    {
        quests.Clear(); // ���� ����Ʈ �����͸� Ŭ�����մϴ�.
        foreach (var data in questData)
        {
            // �����Ϳ��� �ʿ��� ������ �����մϴ�.
            int id = Convert.ToInt32(data["MainquestID"]);
            string name = data["MainquestName"].ToString();
            string description = data["MainquestDescription"].ToString();
            string giver = data["MainquestGiver"].ToString();
            string conditionName = data["MainquestConditionName"].ToString();
            int conditionAmount = Convert.ToInt32(data["MainquestConditionAmount"]);
            string reward = data["MainReward"].ToString();
            int rewardAmount = Convert.ToInt32(data["MainRewardAmount"]);

            // ����Ʈ ������ ������ �߰��մϴ�.
            Dictionary<string, int> conditions = new Dictionary<string, int>
        {
            { conditionName, conditionAmount }
        };

            // ���ο� ����Ʈ ��ü�� �����ϰ� ������ �߰��մϴ�.
            Quest newQuest = new Quest(id, name, description, giver, conditions, reward, rewardAmount);
            quests.Add(id, newQuest);
        }
    }

    // ����Ʈ�� Ȱ��ȭ�մϴ�.
    public void ActivateQuest(int questId)
    {
        if (quests.ContainsKey(questId))
        {
            Quest questToActivate = quests[questId];
            // ���⿡ ����Ʈ�� Ȱ��ȭ�ϴ� ������ �����մϴ�.
            // ���� ���, ����Ʈ ���¸� ������Ʈ�ϰų� UI�� ǥ���ϰ� ����Ʈ ���� ��Ȳ�� �����ϴ� ���� �۾��� �� �� �ֽ��ϴ�.
            Debug.Log($"����Ʈ Ȱ��ȭ: {questToActivate.mainquestName}");
        }
        else
        {
            Debug.LogError("����Ʈ ID�� ã�� �� �����ϴ�!");
        }
    }

    // ... ����Ʈ ������ ���õ� ��Ÿ �޼��� ...

    private void Awake() // ���� ���� �� ȣ��Ǵ� Awake �Լ�
    {
        // "mainquest" �κ��� Resources ������ �ִ� CSV ���� �̸��� ����մϴ�.
        data_Mainquest = CSVReader.Read("mainquest"); // CSVReader�� ����Ͽ� ����Ʈ �����͸� �о�� data_Mainquest�� �����մϴ�.
    }

    // ���� Ȱ��ȭ�� ����Ʈ�� �����ɴϴ�.
    public Quest GetCurrentQuest()
    {
        // �� ���� �ϳ��� ����Ʈ�� Ȱ��ȭ�ȴٰ� �����մϴ�.
        if (quests.Count > 0)
        {
            foreach (var quest in quests.Values)
            {
                // �߰ߵ� ù ��° ����Ʈ�� ��ȯ�մϴ�. ������ ������ �°� �����ϼ���.
                return quest;
            }
        }
        return null; // Ȱ��ȭ�� ����Ʈ�� �����ϴ�.
    }

    // =====================================================================================================

    // ����Ʈ�� ���� ���� ������ ������ �����ɴϴ�.
    public int GetCurrentItemAmount(int questId, string itemName)
    {
        // �÷��̾��� �κ��丮 ���� ����� �����ؾ� �մϴ�.
        // ���� ���, �÷��̾��� �κ��丮 �ý����� ��ȸ�Ͽ� Ư�� �������� ������ ������ �� �ֽ��ϴ�.
        int itemAmount = GetItemAmountFromPlayerInventory(itemName);
        return itemAmount;
    }

    // �÷��̾��� �κ��丮���� ������ ������ �������� ����� �����Դϴ�.
    private int GetItemAmountFromPlayerInventory(string itemName)
    {
        // �÷��̾� �κ��丮 �ý��ۿ� �����Ͽ� �־��� �������� ���� ������ ã���ϴ�.
        // �̴� ���� �ڸ� ǥ������ ��, ���� �κ��丮 �ý��� �ڵ�� ��ü�ؾ� �մϴ�.
        int itemAmount = 0; // �ڸ� ǥ���� ��

        // ... �κ��丮���� ������ ������ �������� �ڵ� ...

        return itemAmount;
    }

    // =====================================================================================================

    // CSV �����ͷκ��� quests ��ųʸ��� ����Ʈ�� �ε��մϴ�.

    public void LoadQuestsFromCSV(List<Dictionary<string, object>> questData)
    {
        // CSV �����ͷκ��� ����Ʈ�� �ε��ϴ� �ڵ带 �ۼ��ϼ���.
    }

    // ... ����Ʈ ������ ���õ� �ٸ� �޼���� ...

}