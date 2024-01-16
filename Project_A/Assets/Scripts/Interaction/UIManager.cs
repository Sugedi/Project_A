using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 기능을 사용하기 위해 이 네임스페이스를 포함합니다

public class UIManager : MonoBehaviour
{
    public GameObject questIconButton; // 퀘스트 아이콘 버튼
    public GameObject uiElement1;
    public GameObject uiElement2;
    public GameObject uiElement3;

    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        questIconButton.SetActive(false); // 대화 팝업을 초기 상태(비활성화 상태)로 설정
    }


    // 'Next Button'이 클릭되었을 때 호출되는 메서드
    public void ShowQuestIcon()
    {
        questIconButton.SetActive(true); // 퀘스트 아이콘 버튼을 활성화
    }

    public void DestroyUIElements()
    {
        // 각 UI 요소를 파괴한다.
        Destroy(uiElement1);
        Destroy(uiElement2);
        Destroy(uiElement3);
    }

}