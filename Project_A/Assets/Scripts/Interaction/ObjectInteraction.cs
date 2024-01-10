using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    //public GameObject attackButton; // (기본)공격 버튼 변수를 선언
    //public GameObject interactionButton; // (변경)상호작용 버튼 변수를 선언

    public int talkId;

    DialogueManager dialogueManager;

    // 아래 2줄은 GPT 대답에서는 없는 애들인데 일단 놔둬봄
    // 이유: Sphere Collider 영역(인지 범위) 내에 플레이어가 진입할 시 <<가 조건이니까 조건 내용이 있어야하지 않을까?
    // public float interactionRange = 2f; // 상호작용 가능한 범위
    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부



    private void OnEnable()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }


    void OnTriggerEnter(Collider other) // 오브젝트가 트리거에 진입할 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 진입한 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = true; // 플레이어가 상호작용 범위 내에 있음을 표시

            // 위에 변수 부분에서 자료형을 Button에서 GameObject로 변경해주어야 SetActive를 쓸 수 있음
            // 아래 1줄은 GPT에서 알려준대로 자료형을 Button으로 했을 때의 함수였음
            // ChangeButtonsState(false, true); // 공격 버튼을 비활성화하고, 상호작용 버튼을 활성화
            // 아래 2줄은 준혁이가 알려준대로 자료형을 GameObject로 변경해주고, SetActive 기능 사용한 것
            // 기록해 놔야 내가 나중에 저 위의 방법이 불편하다는 것을 알고 다른 방법으로 바꿀 수 있어서 적어놓음
            dialogueManager.CangeTalkId(talkId);
            dialogueManager.SettingUI(true);
            
        }
    }

    void OnTriggerExit(Collider other) // 오브젝트가 트리거를 빠져나갈 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거를 빠져나간 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 없음을 표시

            //ChangeButtonsState(true, false); // 공격 버튼을 활성화하고, 상호작용 버튼을 비활성화 => 위와 같은 이유로 삭제

            dialogueManager.SettingUI(false);

        }
    }

    // 아래 주석처리한 내용(ChangeButtoneState)도 GPT의 도움을 받은 것이었으나 준혁이가 굳이 불필요하다고 알려주어 주석처리함
    // Sphere Collider 트리거에 진입할 때와 나갈 때 각각 'ChangeButtonsState' 함수를 호출하여 버튼의 활성화 여부를 변경하는 함수
    // attackButton은 공격 버튼을, interactionButton은 상호작용 버튼을 나타냄
    /* void ChangeButtonsState(bool attackButtonState, bool interactionButtonState)
    {
        if (attackButton != null) 
        {
            attackButton.interactable = attackButtonState;
        }

        if (interactionButton != null) 
        {
            interactionButton.interactable = interactionButtonState;
        }
    } */

}
