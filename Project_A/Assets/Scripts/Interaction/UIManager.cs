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


    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        questIconButton.SetActive(false); // 대화 팝업을 초기 상태(비활성화 상태)로 설정
        systemMessagePanel.SetActive(false); // 시스템 메시지 패널을 초기 상태(비활성화 상태)로 설정
    }


    public void OnNextButtonClicked()
    {
        dialogue1StartBtn.SetActive(false);
        dialogue1UI.SetActive(false);
        nextButtonUI.SetActive(false); // Next 버튼을 비활성화
        questIconButton.SetActive(true); // 퀘스트 아이콘 버튼을 활성화
        ActivateSystemMessagePanel("주요 대본이 활성화 되었습니다."); // 메시지는 상황에 맞게 변경 가능
    }

    // 시스템 메시지 패널을 활성화하고, 3초 후에 비활성화하는 메서드
    private void ActivateSystemMessagePanel(string message)
    {
        systemMessagePanel.SetActive(true);
        systemMessageText.text = message; // 메시지 설정
        StartCoroutine(ShowSystemMessagePanel(3f)); // 코루틴 시작
    }

    // 시스템 메시지 패널을 표시하는 코루틴
    private IEnumerator ShowSystemMessagePanel(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // 지정된 시간 동안 대기
        systemMessagePanel.SetActive(false); // 패널 비활성화
    }
}