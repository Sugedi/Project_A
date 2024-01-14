using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    // 처음에는 공격 버튼과 상호작용 버튼을 따로 만들어서 사용하려고 했으나
    // 굳이 나눌 필요가 있나? + 백스테이지에서는 공격 버튼이 공격으로 쓰이지 않게 만들면 되잖아 + 전투 구역과 상호작용 가능한 오브젝트를 한 공간에 둘 건가?
    // ㄴ 그래서 그냥 interactionButton의 기능을 싹 빼고 공격 버튼에서만 해결가능하도록 만들어주심(by professor)
    // public GameObject attackButton; // (기본)공격 버튼 변수를 선언
    // public GameObject interactionButton; // (변경)상호작용 버튼 변수를 선언

    public int talkId; // 대화 ID 선언

    DialogueManager dialogueManager; // DialogueManager 클래스 참조 변수 선언

    // 아래 2줄은 GPT 대답에서는 없는 애들인데 일단 놔둬봄
    // 이유: Sphere Collider 영역(인지 범위) 내에 플레이어가 진입할 시 <- 가 조건이니까 조건 내용이 있어야하지 않을까?
    // public float interactionRange = 2f; // 상호작용 가능한 범위
    // ㄴ 주석처리 한 이유: 오브젝트에 콜라이더 설정했고, 아래에 OnTrigger 함수로 제어하고 있기 때문
    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부



    private void OnEnable() // 이 오브젝트가 활성화될 때 호출되는 함수
    {
        if (dialogueManager == null) // dialogueManager 변수가 null인 경우
        {
            dialogueManager = FindObjectOfType<DialogueManager>(); // Scene에서 DialogueManager 타입의 오브젝트를 찾아 대입
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
            //dialogueManager.ChangeTalkId(talkId); // 대화 ID 변경
            dialogueManager.SettingUI(true); // UI 설정

        }
    }

    void OnTriggerExit(Collider other) // 오브젝트가 트리거를 빠져나갈 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거를 빠져나간 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 없음을 표시

            //ChangeButtonsState(true, false); // 공격 버튼을 활성화하고, 상호작용 버튼을 비활성화 => 위와 같은 이유로 삭제

            dialogueManager.SettingUI(false); // UI 설정

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
