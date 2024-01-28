using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// =============================================================================================================================
// NPC 오브젝트에 붙이는 스크립트
// 상호작용 트리거 범위 내에 플레이어가 닿을 시, '상호작용 버튼'이 생성됨
// Main Quest 진행 과정도 같이 작성됨 (+ 승호에게 알려줄 변수 포함)
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    public GameObject talk1Panel; //
    public GameObject talk2Panel; //
    public GameObject talkCanvas; //

    public GameObject stageUI; //

    public GameObject dialoguePanel1; //
    public GameObject dialoguePanel2; //
    public GameObject dialoguePanel3; //

    // MainQuest Panels 참조
    public GameObject mainQuestPanel1; //
    public GameObject mainQuestPanel2; //
    public GameObject mainQuestPanel3; //

    public GameObject talkNextBtn; // 1-1부터 1-4까지 넘겨주는 버튼
    public GameObject nextButton1; //
    public GameObject nextButton2; //
    public GameObject nextButton3; //

    public GameObject questIcon; // 퀘스트 아이콘
    public Button questBtn; //

    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부

    public int mainQuest;

    // mainQuest 변수에 대한 public 접근자 메서드
    public int GetMainQuest()
    {
        return mainQuest;
    }

    // mainQuest 변수 값을 설정하는 public 메서드
    public void SetMainQuest(int value)
    {
        mainQuest = value;
    }

    public GameObject systemMessagePanel; // 시스템 메시지를 포함하는 패널
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' 컴포넌트

    public CanvasGroup joy;

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }

    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        systemMessagePanel.SetActive(false); // 시스템 메시지 패널을 비활성화 상태로 설정
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;

        if (mainQuest == 0) // 게임 시작 시 talk1Panel과 talk2Panel을 활성화 BUT, mainQuest가 0일 때만!
        {
            questIcon.SetActive(false); // 퀘스트 아이콘 버튼을 비활성화 상태로 설정

            talk1Panel.SetActive(true); // 처음에 주인공 독백 대사 2개 나오는 거! 대사 나올 때는 컨트롤러 다 비활성화 (아래 5개)
            CanvasGroupOff(joy);

            dialoguePanel1.SetActive(false); // 질서의 신 첫 번째 대사창을 비활성화 상태로 설정
        }

        if (mainQuest == 1)
        {
            questIcon.SetActive(true); // 퀘스트 아이콘 버튼을 활성화 상태로 설정

            mainQuestPanel1.SetActive(true);
            mainQuestPanel2.SetActive(false);
            mainQuestPanel3.SetActive(false);
        }
        if (mainQuest == 2)
        {
            mainQuestPanel2.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel3.SetActive(false);

        }
        if (mainQuest == 3)
        {
            mainQuestPanel3.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(false);
        }

        questBtn.onClick.AddListener(ShowMainQuestPanel); // questBtn에 클릭 이벤트 리스너 추가

    }


    // questIcon 버튼을 클릭했을 때 호출될 메서드
    public void ShowMainQuestPanel()
    {
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;
        if (mainQuest == 1)
        {
            mainQuestPanel1.SetActive(true); 
            mainQuestPanel2.SetActive(false);
            mainQuestPanel3.SetActive(false);
        }
        if (mainQuest == 2)
        {
            mainQuestPanel2.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel3.SetActive(false);

        }
        if (mainQuest == 3)
        {
            mainQuestPanel3.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(false);
        }

    }


    public void MirrorInteraction() 
    {
        if (mainQuest == 0)
        {
            dialoguePanel1.SetActive(true);
        }
        if (mainQuest == 1)
        {
            dialoguePanel2.SetActive(true);
        }
        if (mainQuest == 2)
        {
            dialoguePanel3.SetActive(true);
        }
    }

    public void OnNextButton1Clicked()
    {
        SoundManager.instance.PlayAudio("Button2", "SE");

        Debug.Log("OnNextButton1Clicked() 메서드 호출됨"); // 로그 추가

        dialoguePanel1.SetActive(false);
        questIcon.SetActive(true); // 퀘스트 아이콘 버튼을 활성화
        mainQuestPanel1.SetActive(true); // 첫 번째 메인 퀘스트 패널을 활성화 상태로 설정

        CanvasGroupOn(joy);

        // 여기에 실제로 전달하고 싶은 메시지를 입력하세요.
        string message = "대본집이 활성화 되었습니다.";
        ActivateSystemMessagePanel(message);

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // 로그 추가

    }

    public void OnNextButton2Clicked()
    {
        SoundManager.instance.PlayAudio("Button2", "SE");

        dialoguePanel2.SetActive(false);
        questIcon.SetActive(true);

        CanvasGroupOn(joy);
    }

    public void OnNextButton3Clicked()
    {
        SoundManager.instance.PlayAudio("Button2", "SE");

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // 로그 추가

        dialoguePanel3.SetActive(false);
        questIcon.SetActive(true); // questIcon을 활성화

        mainQuestPanel2.SetActive(false);
        mainQuestPanel3.SetActive(true);

        CanvasGroupOn(joy);


        // 여기에 실제로 전달하고 싶은 메시지를 입력하세요.
        string message = "대본집을 완성하였습니다.";
        ActivateSystemMessagePanel(message);
    }

    public void JoyON()
    {
        CanvasGroupOn(joy);
    }

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

    // 'Stage' 씬으로 전환하려고 할 때 호출되는 메서드
    public void TryEnterStageScene()
    {
        // mainQuest가 1 이상일 때만 'Stage' 씬으로 넘어갈 수 있도록 합니다.
        if (mainQuest >= 1)
        {
            // 'Stage' 씬으로 넘어가는 코드
            SceneManager.LoadScene("Stage");
        }
        else
        {
            // 사용자에게 'Stage' 씬으로 넘어갈 수 없음을 알리는 메시지를 표시
            ActivateSystemMessagePanel("지금은 입장할 수 없습니다. 먼저 거울과 대화하세요.");
        }
    }

    // 버튼 클릭 등을 통해 위 메서드를 호출할 수 있도록 UI에 연결합니다.

}

