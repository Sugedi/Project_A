using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 기능을 사용하기 위해 이 네임스페이스를 포함합니다
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject questIconButton; // 퀘스트 아이콘 버튼
    public GameObject dialogue1StartBtn;
    public GameObject dialogue1UI;
    public GameObject nextButtonUI;

    public GameObject systemMessagePanel; // 시스템 메시지를 포함하는 패널
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' 컴포넌트

    // MainQuest Panels 참조
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;


    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        questIconButton.SetActive(false); // 대화 팝업을 초기 상태(비활성화 상태)로 설정
        systemMessagePanel.SetActive(false); // 시스템 메시지 패널을 초기 상태(비활성화 상태)로 설정
        mainQuestPanel1.SetActive(true); // 첫 번째 메인 퀘스트 패널을 활성화 상태로 설정
    }


    public void OnNextButtonClicked()
    {
        dialogue1StartBtn.SetActive(false);
        dialogue1UI.SetActive(false);
        nextButtonUI.SetActive(false); // Next 버튼을 비활성화
        questIconButton.SetActive(true); // 퀘스트 아이콘 버튼을 활성화
    }



    // 아이템 카운터를 업데이트하는 메서드
    public void RefreshItemCounter(int itemValue)
    {
        if (itemValue == 1)
        {
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(true);
        }

        if (itemValue == 2)
        {
            mainQuestPanel2.SetActive(false);
            mainQuestPanel3.SetActive(true);
        }

    }
}