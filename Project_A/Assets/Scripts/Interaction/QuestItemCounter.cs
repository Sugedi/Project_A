using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// =============================================================================================================
// 이 스크립트는 플레이어가 특정 오브젝트(퀘스트 아이템)와 충돌할 때(`OnTriggerEnter`)마다 작동합니다.
// 아이템을 수집할 때마다 `itemValue`를 증가시키고,
// `UIManager`의 `RefreshItemCounter` 메서드를 호출하여 퀘스트 진행 상태를 업데이트합니다.
// =============================================================================================================

public class QuestItemCounter : MonoBehaviour
{
    // 플레이어가 수집해야 하는 아이템의 현재 개수
    public int itemValue = 0;

    private bool alreadyCollected = false; // 아이템이 이미 수집되었는지 여부를 확인하는 플래그

    // 플레이어가 퀘스트 아이템과 충돌할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        // 만약 이미 아이템을 수집했다면, 더 이상 코드를 실행하지 않음
        if (alreadyCollected)
        {
            return;
        }

        // 아이템을 수집할 때마다 itemValue를 1 증가시킴
        itemValue++;

        // 만약 플레이어가 첫 번째 아이템을 수집했다면 퀘스트를 클리어했다고 로그를 출력
        // itemValue가 어느 시점에서 올라가냐를 생각해보면 됨(++가 조건 이후에 증가가 되고 있으니까!!)
        // 특정 조건에 따라 추가 작업을 수행
        if (itemValue == 1)
        {
            Debug.Log("퀘스트 클리어");
            alreadyCollected = true; // 플래그를 true로 설정하여 더 이상 OnTriggerEnter가 작동하지 않도록 함
        }
            // 현재 게임 오브젝트(퀘스트 아이템)를 비활성화하여 더 이상 충돌하지 않도록 함
            gameObject.SetActive(false);
            Debug.Log("먹었당~");
    }

}

