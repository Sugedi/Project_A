using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// =============================================================================================================================
// NPC 오브젝트에 붙이는 스크립트
// Main Quest 진행 과정이 저장되기 위해서 필요한 스크립트 (중요!)
// =============================================================================================================================


public class QuestManager : MonoBehaviour
{
    public static int mainQuest = 0;
    public static int subQuest = 0;


    // Start는 게임 오브젝트가 활성화될 때 한 번만 호출됩니다.
    // 따라서, Start에서 퀘스트 상태를 확인하는 로직은 적합하지 않을 수 있습니다.
    // 대신, 상태 변경이 필요할 때 호출할 수 있는 메서드를 정의합니다.
    public void UpdateMainQuest(int newStatus)
    {
        mainQuest = newStatus;
        ProcessMainQuest();
    }


    // 퀘스트 상태에 따라 처리하는 메서드
    private void ProcessMainQuest()
    {
        if (mainQuest == 0) // 상황: '진행하기' 버튼을 눌러 퀘스트를 받기 전
        {
            // 퀘스트 수락을 위한 로직
        }
        else if (mainQuest == 1) // 상황: 퀘스트를 받았으나 '퀘스트 아이템(완료 조건)'을 획득하지 못함
        {
            // 퀘스트 아이템을 찾는 로직
        }
        else if (mainQuest == 2) // 상황: '퀘스트 아이템(완료 조건)'을 획득하고, 거울에게 말 건 뒤에 보상 받기 전
        {
            // 보상을 받기 전의 상호작용 로직
        }
        else if (mainQuest == 3) // 상황: 보상 수령 후
        {
            // 퀘스트 완료 로직 및 보상 처리
        }
        // 상태가 3 이상인 경우 추가적인 상호작용은 발생하지 않습니다.
    }
}