using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroupManager : MonoBehaviour
{
    public GameObject[] monsters; // 이 그룹에 속한 몬스터들
    public GameObject nextMonsterGroup; // 다음 그룹

    private int defeatedMonstersCount = 0;
    private void OnEnable()
    {
        // 이 그룹이 활성화될 때 그룹에 속한 모든 몬스터를 활성화
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
            // 모든 몬스터가 처치되면 다음 그룹을 활성화
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
