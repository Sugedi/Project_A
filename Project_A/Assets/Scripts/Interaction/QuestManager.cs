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
    }
}
