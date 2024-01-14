using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure to include this if you're using TextMeshPro

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questContentText;
    public TextMeshProUGUI valueText;

    private QuestManager questManager; // Your QuestManager script
    private Quest currentQuest;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();

        // QuestManager의 현재 퀘스트를 가져와서 할당
        currentQuest = FindObjectOfType<QuestManager>().GetCurrentQuest();
        UpdateQuestUI(); // UI를 업데이트하는 메서드 호출
    }

    public void UpdateQuestUI()
    {
        // Assume that the QuestManager has a method to get the current quest
        Quest currentQuest = questManager.GetCurrentQuest();

        if (currentQuest != null)
        {
            // Update the UI elements with the current quest data
            questNameText.text = currentQuest.mainquestName;
            questContentText.text = currentQuest.mainquestDescription;

            // Assuming you have a method to get the current amount of the quest item the player has
            int currentItemAmount = questManager.GetCurrentItemAmount(currentQuest.mainquestID, "mainquestCondition_name1");
            valueText.text = $"{currentItemAmount}/{currentQuest.questConditions["mainquestCondition_name1"]}";
        }
    }

    // Call this method when the player collects an item or when you need to refresh the UI
    public void RefreshItemCounter()
    {
        // You'd probably have to implement a method to get the current quest item count
        // and update the UI accordingly.
        int currentItemAmount = questManager.GetCurrentItemAmount(currentQuest.mainquestID, "mainquestCondition_name1");
        valueText.text = $"{currentItemAmount}/{currentQuest.questConditions["mainquestCondition_name1"]}";
    }

    // ... other methods related to UI ...
}
