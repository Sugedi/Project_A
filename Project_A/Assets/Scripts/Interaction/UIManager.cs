using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro�� ����ϴ� ��� �� ���ӽ����̽��� �ݵ�� �����ؾ� �մϴ�.
using System.Linq;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI questNameText; // ����Ʈ �̸��� ǥ���� UI �ؽ�Ʈ
    public TextMeshProUGUI questContentText; // ����Ʈ ������ ǥ���� UI �ؽ�Ʈ
    public TextMeshProUGUI valueText; // ����Ʈ�� ���� ��(��: ������ ������ ��)�� ǥ���� UI �ؽ�Ʈ

    private QuestManager questManager; // QuestManager ��ũ��Ʈ ����
    private Quest currentQuest; // ���� ���� ���� ����Ʈ ��ü

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>(); // �� �� QuestManager �ν��Ͻ� ã��

        // QuestManager���� ���� ���� ���� ����Ʈ�� �����ͼ� currentQuest ������ �Ҵ�
        currentQuest = FindObjectOfType<QuestManager>().GetCurrentQuest();
        UpdateQuestUI(); // UI�� ������Ʈ�ϴ� �޼��� ȣ�� // #################
    }

    public void UpdateQuestUI()
    {
        currentQuest = questManager.GetCurrentQuest(); // ���� Ȱ��ȭ�� ����Ʈ�� �����ɴϴ�.

        if (currentQuest != null)
        {
            questNameText.text = currentQuest.mainquestName; // ����Ʈ �̸��� UI�� �����մϴ�.
            questContentText.text = currentQuest.mainquestDescription; // ����Ʈ ������ UI�� �����մϴ�.

            string conditionKey = currentQuest.questConditions.Keys.FirstOrDefault(); // ����Ʈ ������ Ű�� �����ɴϴ�.
            if (conditionKey != null)
            {
                int currentItemAmount = questManager.GetCurrentItemAmount(currentQuest.mainquestID, conditionKey); // ���� ������ ������ �����ɴϴ�.
                valueText.text = $"{currentItemAmount}/{currentQuest.questConditions[conditionKey]}"; // UI�� ������ ������ ǥ���մϴ�.
            }
            else
            {
                Debug.LogError("����Ʈ ���� Ű�� questConditions ������ �����ϴ�.");
                valueText.text = "0/?"; // ������ ���� ����� �⺻�� ǥ��
            }
        }
    }




    // �÷��̾ �������� �������� �� �Ǵ� UI�� ���ΰ�ħ�� �ʿ䰡 ���� �� �� �޼��带 ȣ��
    public void RefreshItemCounter()
    {
        // ���� Ȱ��ȭ�� ����Ʈ�� �ٽ� �������� ���� �����ϴ�. ���� ����Ʈ�� �߰��� ����� �� �ֱ� �����Դϴ�.
        currentQuest = questManager.GetCurrentQuest();

        if (currentQuest != null)
        {
            string conditionKey = currentQuest.questConditions.Keys.FirstOrDefault(); // ù ��° Ű�� �����ɴϴ�.
            if (conditionKey != null)
            {
                int currentItemAmount = questManager.GetCurrentItemAmount(currentQuest.mainquestID, conditionKey); // ���� ������ ������ �����ɴϴ�.
                valueText.text = $"{currentItemAmount}/{currentQuest.questConditions[conditionKey]}"; // UI�� ������ ������ ǥ���մϴ�.
            }
            else
            {
                Debug.LogError("����Ʈ ���� Ű�� questConditions ������ �����ϴ�.");
                valueText.text = "0/?"; // ������ ���� ����� �⺻�� ǥ��
            }
        }
    }

    // ... UI ���� ��Ÿ �޼��� ...
}
