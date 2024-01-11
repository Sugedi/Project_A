using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// =================================================================================
// ���� ����Ʈ�� ��Ÿ���� Ŭ����
// Quest�� ������Ʈ�� �ִ� �� �ƴ϶� �ڵ忡�� �ҷ��� ���̱� ������ ����� �����, 
// =================================================================================

[System.Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    public bool isCompleted = false;
    // ����Ʈ ��ǥ, ���� �� �ʿ��� �ٸ� �ʵ���� �߰��մϴ�.

    public Quest(string name, string description)
    {
        questName = name;
        questDescription = description;
    }
}