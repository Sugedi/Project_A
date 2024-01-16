using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// =============================================================================================================================
// NPC 오브젝트에 붙이는 스크립트
// 상호작용 트리거 범위 내에 플레이어가 닿을 시, '상호작용 버튼'이 생성됨
// 
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    // 처음에는 공격 버튼과 상호작용 버튼을 따로 만들어서 사용하려고 했으나
    // 굳이 나눌 필요가 있나? 백스테이지에서는 공격 버튼이 공격으로 쓰이지 않게 만들면 되잖아.
    // ㄴ 그래서 그냥 interactionButton의 기능을 싹 빼고 공격 버튼에서만 해결 가능하도록 만들어주심 (by professor)
    // public GameObject attackButton; // (기본)공격 버튼 변수를 선언
    // public GameObject interactionButton; // (변경)상호작용 버튼 변수를 선언

    DialogueManager dialogueManager; // DialogueManager 클래스 참조 변수 선언

    public GameObject button; // UI 버튼을 할당할 변수

    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부

    public GameObject dialogue1; // 새로운 UI 패널 오브젝트
    public GameObject _button;   // 숨길 버튼 오브젝트 (위의 button 변수와 같은 변수이나, 다른 기능 구현에서 필요한 변수라서 _붙임)

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
            button.SetActive(true); // 버튼을 활성화
            // 위에 변수 부분에서 자료형을 Button에서 GameObject로 변경해주어야 SetActive를 쓸 수 있음
        }
    }


    void OnTriggerExit(Collider other) // 오브젝트가 트리거를 빠져나갈 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거를 빠져나간 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 없음을 표시
            button.SetActive(false); // 버튼을 비활성화

        }
    }

    // 버튼이 눌렸을 때 호출되는 메서드
    public void OnButtonPressed()
    {
        dialogue1.SetActive(true); // 새로운 패널을 활성화
        _button.SetActive(false);  // 버튼을 비활성화
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