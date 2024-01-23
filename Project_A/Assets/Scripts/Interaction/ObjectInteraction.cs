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
    public GameObject talk1Panel;
    public GameObject talk2Panel;
    public GameObject talkCanvas;

    public GameObject stageUI;

    public GameObject startBtn; // 'Dialogue StartBtn: 거울에 손을 뻗는다'
    public GameObject dialoguePanel1;
    public GameObject dialoguePanel2;
    public GameObject dialoguePanel3;

    // MainQuest Panels 참조
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;

    public GameObject talkNextBtn; // 1-1부터 1-4까지 넘겨주는 버튼
    public GameObject nextButton1;
    public GameObject nextButton2;
    public GameObject nextButton3;

    public GameObject questIcon; // 퀘스트 아이콘

    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부

    public int mainQuest;

    public GameObject systemMessagePanel; // 시스템 메시지를 포함하는 패널
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' 컴포넌트


    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        questIcon.SetActive(false); // 퀘스트 아이콘 버튼을 비활성화 상태로 설정
        systemMessagePanel.SetActive(false); // 시스템 메시지 패널을 비활성화 상태로 설정
        mainQuestPanel1.SetActive(true); // 첫 번째 메인 퀘스트 패널을 활성화 상태로 설정
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;


        if (mainQuest == 0) // 게임 시작 시 talk1Panel과 talk2Panel을 활성화 BUT, mainQuest가 0일 때만!
        {
            talk1Panel.SetActive(true);
            dialoguePanel1.SetActive(false); // 질서의 신 첫 번째 대사창을 비활성화 상태로 설정

        }

    }


    void OnTriggerEnter(Collider other) // 오브젝트가 트리거에 진입할 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 진입한 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = true; // 플레이어가 상호작용 범위 내에 있음을 표시

            if (mainQuest == 0)
            {
                talkCanvas.SetActive(false);
                startBtn.SetActive(true);
                dialoguePanel1.SetActive(false); // 질서의 신 첫 번째 대사창을 비활성화 상태로 설정
            }

            if (mainQuest == 1)
            {
                dialoguePanel2.SetActive(false);
                //questIcon.SetActive(false); // questIcon을 비활성화
            }

            if (mainQuest == 2)
            {
                dialoguePanel3.SetActive(false);
                //questIcon.SetActive(false); // questIcon을 비활성화
            }

        }
    }
    public void OnStartBtnClick()
    {
        Debug.Log("Start Button Clicked"); // 로그를 추가하여 이 메서드가 호출되는지 확인합니다
        if (mainQuest == 0)
        {
            dialoguePanel1.SetActive(true);
            startBtn.SetActive(false);

            stageUI.SetActive(true); // 체력, 총알, 젬 UI 활성화

        }
        if (mainQuest == 1)
        {
            dialoguePanel2.SetActive(true);
            questIcon.SetActive(false);
        }
        if (mainQuest == 2)
        {
            dialoguePanel3.SetActive(true);
            questIcon.SetActive(false);
        }
    }

    public void OnNextButton1Clicked()
    {
        Debug.Log("OnNextButton1Clicked() 메서드 호출됨"); // 로그 추가

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // 로그 추가

        startBtn.SetActive(false);
        dialoguePanel1.SetActive(false);
        questIcon.SetActive(true); // 퀘스트 아이콘 버튼을 활성화

        // 여기에 실제로 전달하고 싶은 메시지를 입력하세요.
        string message = "주요 대본 1이 활성화 되었습니다.";
        ActivateSystemMessagePanel(message);

    }

    public void OnNextButton2Clicked()
    {
        dialoguePanel2.SetActive(false);
        questIcon.SetActive(true); // questIcon을 활성화
    }

    public void OnNextButton3Clicked()
    {
        mainQuest++;
        dialoguePanel3.SetActive(false);
        questIcon.SetActive(true); // questIcon을 활성화

        mainQuestPanel2.SetActive(false);
        mainQuestPanel3.SetActive(true);

        // 여기에 실제로 전달하고 싶은 메시지를 입력하세요.
        string message = "주요 대본 1을 완성하였습니다.";
        ActivateSystemMessagePanel(message);
    }


    void OnTriggerExit(Collider other) // 오브젝트가 트리거를 빠져나갈 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거를 빠져나간 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 없음을 표시
            startBtn.SetActive(false);
        }
    }

    // 시스템 메시지 패널을 활성화하고, 지정된 시간 후에 비활성화하는 메서드
    private void ActivateSystemMessagePanel(string message)
    {
        systemMessagePanel.SetActive(true);
        systemMessageText.text = message;
        StartCoroutine(ShowSystemMessage(3f)); // 코루틴을 사용하여 3초 후 패널 비활성화
    }

    // 시스템 메시지 패널을 표시하는 코루틴
    IEnumerator ShowSystemMessage(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // 지정된 시간 동안 대기
        systemMessagePanel.SetActive(false); // 패널 비활성화
    }
    public void RefreshItemCounter(int itemValue)
    {
        if (itemValue == 1)
        {
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(true);
        }

    }

}

