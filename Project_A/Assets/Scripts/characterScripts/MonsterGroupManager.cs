using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroupManager : MonoBehaviour
{
    public GameObject[] monsters; // �� �׷쿡 ���� ���͵�
    public GameObject nextMonsterGroup; // ���� �׷�

    private int defeatedMonstersCount = 0;
    private void OnEnable()
    {
        // �� �׷��� Ȱ��ȭ�� �� �׷쿡 ���� ��� ���͸� Ȱ��ȭ
        foreach (GameObject monster in monsters)
        {
            monster.SetActive(true);
        }
    }

    public void OnMonsterDefeated()
    {
        defeatedMonstersCount++;
        if (defeatedMonstersCount >= monsters.Length)
        {
            // ��� ���Ͱ� óġ�Ǹ� ���� �׷��� Ȱ��ȭ
            if (nextMonsterGroup != null)
            {
                Invoke("ActivateNextGroup", 1f);
            }
        }
    }
    private void ActivateNextGroup()
    {
        nextMonsterGroup.SetActive(true);
    }
}
