using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =============================================================================================================================
// NPC 오브젝트에 붙이는 스크립트
// Main Quest 진행 과정이 저장되기 위해서 필요한 스크립트 (중요!)
// =============================================================================================================================


public class QuestManager : MonoBehaviour
{
    int mainQuest = 0;
    int subQuest = 0;


    public void Start()
    {
        if (mainQuest == 0) // 상황: 퀘스트 받기 전
        {

        }

        if (mainQuest == 1) // 상황: 퀘스트는 받았으나 미완료
        {

        }

        if (mainQuest == 2) // 상황: 퀘스트는 완료했으나 보상 받기 전
        {

        }

        if (mainQuest == 3) // 상황: 보상 수령 후
        {

        }


        // 뤼튼에게 물어보니 switch문으로 정리했음
    //    public class QuestManager : MonoBehaviour
    //{
    //    public int mainQuest = 0;
    //    public int subQuest = 0;

    //    // 퀘스트 상태를 변경하는 메서드
    //    public void UpdateMainQuestStatus(int newStatus)
    //    {
    //        mainQuest = newStatus;
    //        ProcessQuestStatus();
    //    }

    //    // 퀘스트 상태에 따라 처리하는 메서드
    //    private void ProcessQuestStatus()
    //    {
    //        switch (mainQuest)
    //        {
    //            case 0: // 상황: 퀘스트 받기 전
    //                    // 퀘스트 받기 전에 해야 할 일 처리
    //                break;
    //            case 1: // 상황: 퀘스트는 받았으나 미완료
    //                    // 퀘스트 진행 중에 해야 할 일 처리
    //                break;
    //            case 2: // 상황: 퀘스트는 완료했으나 보상 받기 전
    //                    // 퀘스트 완료 후 보상을 받기 전에 해야 할 일 처리
    //                break;
    //            case 3: // 상황: 보상 수령 후
    //                    // 보상을 받은 후에 해야 할 일 처리
    //                break;
    //            default: // 상황: 4 이후
    //                     // 퀘스트가 완전히 끝난 후에 해야 할 일 처리
    //                break;
    //        }
    //    }
    }




}
