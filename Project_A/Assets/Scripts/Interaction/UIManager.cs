using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하는 경우 이 네임스페이스를 반드시 포함해야 합니다.
using System.Linq;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI questNameText; // 퀘스트 이름을 표시할 UI 텍스트
    public TextMeshProUGUI questContentText; // 퀘스트 내용을 표시할 UI 텍스트
    public TextMeshProUGUI valueText; // 퀘스트의 진행 값(예: 수집한 아이템 수)을 표시할 UI 텍스트

    private QuestManager questManager; // QuestManager 스크립트 참조
    private Quest currentQuest; // 현재 진행 중인 퀘스트 객체

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>(); // 씬 내 QuestManager 인스턴스 찾기

        // QuestManager에서 현재 진행 중인 퀘스트를 가져와서 currentQuest 변수에 할당
        currentQuest = FindObjectOfType<QuestManager>().GetCurrentQuest();
        UpdateQuestUI(); // UI를 업데이트하는 메서드 호출 // #################
    }

    public void UpdateQuestUI()
    {
        currentQuest = questManager.GetCurrentQuest(); // 현재 활성화된 퀘스트를 가져옵니다.

        if (currentQuest != null)
        {
            questNameText.text = currentQuest.mainquestName; // 퀘스트 이름을 UI에 설정합니다.
            questContentText.text = currentQuest.mainquestDescription; // 퀘스트 설명을 UI에 설정합니다.

            string conditionKey = currentQuest.questConditions.Keys.FirstOrDefault(); // 퀘스트 조건의 키를 가져옵니다.
            if (conditionKey != null)
            {
                int currentItemAmount = questManager.GetCurrentItemAmount(currentQuest.mainquestID, conditionKey); // 현재 아이템 수량을 가져옵니다.
                valueText.text = $"{currentItemAmount}/{currentQuest.questConditions[conditionKey]}"; // UI에 아이템 수량을 표시합니다.
            }
            else
            {
                Debug.LogError("퀘스트 조건 키가 questConditions 사전에 없습니다.");
                valueText.text = "0/?"; // 오류가 있을 경우의 기본값 표시
            }
        }
    }




    // 플레이어가 아이템을 수집했을 때 또는 UI를 새로고침할 필요가 있을 때 이 메서드를 호출
    public void RefreshItemCounter()
    {
        // 현재 활성화된 퀘스트를 다시 가져오는 것이 좋습니다. 현재 퀘스트가 중간에 변경될 수 있기 때문입니다.
        currentQuest = questManager.GetCurrentQuest();

        if (currentQuest != null)
        {
            string conditionKey = currentQuest.questConditions.Keys.FirstOrDefault(); // 첫 번째 키를 가져옵니다.
            if (conditionKey != null)
            {
                int currentItemAmount = questManager.GetCurrentItemAmount(currentQuest.mainquestID, conditionKey); // 현재 아이템 수량을 가져옵니다.
                valueText.text = $"{currentItemAmount}/{currentQuest.questConditions[conditionKey]}"; // UI에 아이템 수량을 표시합니다.
            }
            else
            {
                Debug.LogError("퀘스트 조건 키가 questConditions 사전에 없습니다.");
                valueText.text = "0/?"; // 오류가 있을 경우의 기본값 표시
            }
        }
    }

    // ... UI 관련 기타 메서드 ...
}
