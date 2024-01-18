using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// =============================================================================================================================
// NPC 오브젝트에 붙이는 스크립트
// 상호작용 트리거 범위 내에 플레이어가 닿을 시, '상호작용 버튼'이 생성됨
// Main Quest 진행 과정도 같이 작성됨 (+ 승호에게 알려줄 변수 포함)
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    public GameObject startBtn; // 'Dialogue StartBtn: 거울에 손을 뻗는다'

    public GameObject dialogue1; // 첫 번째 질서의 신 대화창
    public GameObject dialogue2; // 두 번째 질서의 신 대화창
    public GameObject dialogue3; // 세 번째 질서의 신 대화창

    // MainQuest Panels 참조
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;


    public GameObject questIcon; // 퀘스트 아이콘

    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부

    public int mainQuest = 0;

    public GameObject systemMessagePanel; // 시스템 메시지를 포함하는 패널
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' 컴포넌트


    
    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        questIcon.SetActive(false); // 퀘스트 아이콘 버튼을 비활성화 상태로 설정
        systemMessagePanel.SetActive(false); // 시스템 메시지 패널을 비활성화 상태로 설정
        mainQuestPanel1.SetActive(true); // 첫 번째 메인 퀘스트 패널을 활성화 상태로 설정
    }


    void OnTriggerEnter(Collider other) // 오브젝트가 트리거에 진입할 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 진입한 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = true; // 플레이어가 상호작용 범위 내에 있음을 표시

            if (mainQuest == 0)
            {
                startBtn.SetActive(true);

            }

            if (mainQuest == 1)
            {
                Dialogue_2On();
            }

            if (mainQuest == 2)
            {
                Dialogue_3On();
            }

        }
    }


    void OnTriggerExit(Collider other) // 오브젝트가 트리거를 빠져나갈 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거를 빠져나간 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 없음을 표시
        }
    }


    // 대화창 닫기 버튼이 눌렸을 때 호출되는 메서드
    public void NextButtonOn()
    {
        if (mainQuest == 0)
        {
            Dialogue_1On();
        }

        if (mainQuest == 1)
        {
            Dialogue_2Off();
        }

        if (mainQuest == 2)
        {
            Dialogue_3Off();
        }

    }


    public void Dialogue_1On() // 대화창 1이 켜질 때
    {
        dialogue1.SetActive(true); // 질서의 신 첫 번째 대화창 활성화
        startBtn.SetActive(false); // 상호작용 버튼 비활성화
    }

    void Dialogue_1Off() // 대화창 1이 꺼질 때
    {
        dialogue1.SetActive(false); // 질서의 신 첫 번째 대화창 비활성
        questIcon.SetActive(true);
        ActivateSystemMessagePanel("주요 대본이 활성화 되었습니다."); // 메시지는 상황에 맞게 변경 가능
        mainQuest++; // 메인 퀘스트 상태 업데이트

    }

    void Dialogue_2On() // 대화창 2가 켜질 때
    {
        dialogue2.SetActive(true); // 질서의 신 두 번째 대화창 활성화
    }

    void Dialogue_2Off() // 대화창 2가 꺼질 때
    {
        dialogue2.SetActive(false); // 질서의 신 두 번째 대화창 비활성
    }


    void Dialogue_3On() // 대화창 3이 켜질 때
    {
        dialogue3.SetActive(true); // 질서의 신 세 번째 대화창 활성화
    }

    void Dialogue_3Off() // 대화창 3이 꺼질 때
    {
        dialogue3.SetActive(false); // 질서의 신 세 번째 대화창 비활성
    }

    // 시스템 메시지 패널을 활성화하고, 지정된 시간 후에 비활성화하는 메서드
    private void ActivateSystemMessagePanel(string message)
    {
        systemMessagePanel.SetActive(true);
        systemMessageText.text = message;
        StartCoroutine(ShowSystemMessage(3f)); // 코루틴을 사용하여 3초 후 패널 비활성화
    }

    // 시스템 메시지 패널을 표시하는 코루틴
    private IEnumerator ShowSystemMessage(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // 지정된 시간 동안 대기
        systemMessagePanel.SetActive(false); // 패널 비활성화
    }


}

