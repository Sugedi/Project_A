using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// =================================================================================
// 개별 퀘스트를 나타내는 클래스
// Quest는 오브젝트에 넣는 게 아니라 코드에서 불러올 것이기 때문에 상속은 지우고, 
// =================================================================================

[System.Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    public bool isCompleted = false;
    // 퀘스트 목표, 보상 등 필요한 다른 필드들을 추가합니다.

    public Quest(string name, string description)
    {
        questName = name;
        questDescription = description;
    }
}