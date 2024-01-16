using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemCounter : MonoBehaviour
{

    public TextMeshProUGUI questValue;
    public int itemValue;

    public TextMeshProUGUI systemMessageUI; // 시스템 메시지를 표시할 UI
    public float messageDisplayTime = 5f; // 메시지 표시 시간 (초)


    // public GameObject systemMessageUI; // Inspector에서 할당할 시스템 메시지 UI 오브젝트 // 메세지만 나오는거면 오브젝트는 괜찮을 것 같...?


    private void OnTriggerEnter(Collider other)
    {
        itemValue++;

        // 시스템 메시지 표시
        StartCoroutine(DisplaySystemMessage($"'{gameObject.name}'을(를) 획득하였습니다."));

        if (itemValue == 1) // itemValue가 어느 시점에서 올라가냐를 생각해보면 됨(++가 조건 이후에 증가가 되고 있으니까!!) // 특정 조건에 따라 추가 작업을 수행
        {
            Debug.Log("퀘스트 클리어");
        }
        else
        {
            questValue.text = itemValue.ToString(); // SetActive에서 false해서 밑에 update에 썼을 때는 카운팅만 되고 ui창에 표시X
            gameObject.SetActive(false);
            Debug.Log("먹었당~");

        }

        IEnumerator DisplaySystemMessage(string message)
        {
            systemMessageUI.text = message; // 메시지 텍스트 설정
            systemMessageUI.gameObject.SetActive(true); // 메시지 UI 활성화

            // 설정된 시간만큼 대기
            yield return new WaitForSeconds(messageDisplayTime);

            systemMessageUI.gameObject.SetActive(false); // 메시지 UI 비활성화
        }

        // 이 스크립트는 플레이어가 특정 오브젝트(퀘스트 아이템)와 충돌할 때(`OnTriggerEnter`)마다 작동합니다.
        // 아이템을 수집할 때마다 `itemValue`를 증가시키고, `UIManager`의 `RefreshItemCounter` 메서드를 호출하여 퀘스트 진행 상태를 업데이트합니다.
        // 또한, "특정 오브젝트의 이름을 획득하였습니다."라는 메시지를 시스템 메시지 UI에 5초간 표시합니다.
        //`DisplaySystemMessage` 코루틴은 설정된 시간(여기서는 5초) 동안 메시지를 표시한 후 자동으로 메시지를 숨깁니다.


    }

}

