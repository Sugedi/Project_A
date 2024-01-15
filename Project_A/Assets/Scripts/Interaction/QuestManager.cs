using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int mainquestID; // 메인 퀘스트 ID
    public string mainquestName; // 메인 퀘스트 이름
    public string mainquestDescription; // 메인 퀘스트 설명
    public string mainquestGiver; // 퀘스트 제공자
    // mainquestType은 사용하지 않으므로 제외합니다.
    public Dictionary<string, int> questConditions = new Dictionary<string, int>(); // 퀘스트 조건들
    public string mainReward; // 메인 보상
    public int rewardAmount; // 보상 수량

    // 퀘스트 생성자
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
    // 전반적인 퀘스트 시스템을 처리합니다.
    // 게임 전체에서 지속되는 객체에 부착해야 합니다.

    List<Dictionary<string, object>> data_Mainquest; // CSV 파일에서 읽어온 퀘스트 데이터를 저장할 리스트

    // UI 요소를 연결할 TextMeshProUGUI 변수들
    public TextMeshProUGUI questname; // 퀘스트 이름 UI
    public TextMeshProUGUI questcontent; // 퀘스트 내용 UI

    [SerializeField]
    int id; // 현재 퀘스트의 ID

    private Dictionary<int, Quest> quests = new Dictionary<int, Quest>(); // 활성 퀘스트들

    // 퀘스트 데이터를 로드합니다.
    public void LoadQuests(List<Dictionary<string, object>> questData)
    {
        quests.Clear(); // 기존 퀘스트 데이터를 클리어합니다.
        foreach (var data in questData)
        {
            // 데이터에서 필요한 정보를 추출합니다.
            int id = Convert.ToInt32(data["MainquestID"]);
            string name = data["MainquestName"].ToString();
            string description = data["MainquestDescription"].ToString();
            string giver = data["MainquestGiver"].ToString();
            string conditionName = data["MainquestConditionName"].ToString();
            int conditionAmount = Convert.ToInt32(data["MainquestConditionAmount"]);
            string reward = data["MainReward"].ToString();
            int rewardAmount = Convert.ToInt32(data["MainRewardAmount"]);

            // 퀘스트 조건을 사전에 추가합니다.
            Dictionary<string, int> conditions = new Dictionary<string, int>
        {
            { conditionName, conditionAmount }
        };

            // 새로운 퀘스트 객체를 생성하고 사전에 추가합니다.
            Quest newQuest = new Quest(id, name, description, giver, conditions, reward, rewardAmount);
            quests.Add(id, newQuest);
        }
    }

    // 퀘스트를 활성화합니다.
    public void ActivateQuest(int questId)
    {
        if (quests.ContainsKey(questId))
        {
            Quest questToActivate = quests[questId];
            // 여기에 퀘스트를 활성화하는 로직을 구현합니다.
            // 예를 들어, 퀘스트 상태를 업데이트하거나 UI에 표시하고 퀘스트 진행 상황을 추적하는 등의 작업을 할 수 있습니다.
            Debug.Log($"퀘스트 활성화: {questToActivate.mainquestName}");
        }
        else
        {
            Debug.LogError("퀘스트 ID를 찾을 수 없습니다!");
        }
    }

    // ... 퀘스트 관리와 관련된 기타 메서드 ...

    private void Awake() // 게임 시작 시 호출되는 Awake 함수
    {
        // "mainquest" 부분은 Resources 폴더에 있는 CSV 파일 이름을 사용합니다.
        data_Mainquest = CSVReader.Read("mainquest"); // CSVReader를 사용하여 퀘스트 데이터를 읽어와 data_Mainquest에 저장합니다.
    }

    // 현재 활성화된 퀘스트를 가져옵니다.
    public Quest GetCurrentQuest()
    {
        // 한 번에 하나의 퀘스트만 활성화된다고 가정합니다.
        if (quests.Count > 0)
        {
            foreach (var quest in quests.Values)
            {
                // 발견된 첫 번째 퀘스트를 반환합니다. 게임의 로직에 맞게 조정하세요.
                return quest;
            }
        }
        return null; // 활성화된 퀘스트가 없습니다.
    }

    // =====================================================================================================

    // 퀘스트에 대한 현재 아이템 수량을 가져옵니다.
    public int GetCurrentItemAmount(int questId, string itemName)
    {
        // 플레이어의 인벤토리 추적 방식을 구현해야 합니다.
        // 예를 들어, 플레이어의 인벤토리 시스템을 조회하여 특정 아이템의 수량을 가져올 수 있습니다.
        int itemAmount = GetItemAmountFromPlayerInventory(itemName);
        return itemAmount;
    }

    // 플레이어의 인벤토리에서 아이템 수량을 가져오는 방법의 예시입니다.
    private int GetItemAmountFromPlayerInventory(string itemName)
    {
        // 플레이어 인벤토리 시스템에 접근하여 주어진 아이템의 현재 수량을 찾습니다.
        // 이는 단지 자리 표시자일 뿐, 실제 인벤토리 시스템 코드로 교체해야 합니다.
        int itemAmount = 0; // 자리 표시자 값

        // ... 인벤토리에서 아이템 수량을 가져오는 코드 ...

        return itemAmount;
    }

    // =====================================================================================================

    // CSV 데이터로부터 quests 딕셔너리에 퀘스트를 로드합니다.

    public void LoadQuestsFromCSV(List<Dictionary<string, object>> questData)
    {
        // CSV 데이터로부터 퀘스트를 로드하는 코드를 작성하세요.
    }

    // ... 퀘스트 관리와 관련된 다른 메서드들 ...

}