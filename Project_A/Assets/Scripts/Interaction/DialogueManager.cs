using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialoguePanels; // 대화 패널들을 저장할 배열
    public Button nextButton_A; // 1-2-3-4로 넘어가게 하는 다음 버튼
    public Button nextButton1; // 4에서 기존 다음 버튼을 비활성화하고, 그 자리에 넣어줄 버튼
    public int currentPanelIndex = 0; // 현재 활성화된 대화 패널의 인덱스

    void Start()
    {
        InitializeDialogue(); // 대화를 초기화하는 메서드를 호출
        nextButton1.gameObject.SetActive(false); // nextButton1을 시작할 때 비활성화합니다.
    }

    // 모든 대화 패널을 비활성화하고 첫 번째 패널을 활성화하는 메서드
    void InitializeDialogue()
    {
        foreach (GameObject panel in dialoguePanels) // 모든 대화 패널을 순회하며 비활성화
        {
            panel.SetActive(false);
        }
        if (dialoguePanels.Length > 0) // 대화 패널 배열에 요소가 있다면, 첫 번째 패널을 활성화
        {
            dialoguePanels[0].SetActive(true);
        }
        currentPanelIndex = 0; // 현재 패널 인덱스를 0으로 설정
    }

    // 'Next' 버튼 클릭 시 호출되는 메서드
    public void ShowNextDialoguePanel()
    {
        if (currentPanelIndex == 0)
        {
            dialoguePanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++; // 다음 패널을 활성화
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else if (currentPanelIndex == 1)
        {
            dialoguePanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++; // 다음 패널을 활성화
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else if (currentPanelIndex == 2)
        {
            dialoguePanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++; // 다음 패널을 활성화
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else if (currentPanelIndex == 3)
        {
            dialoguePanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++; // 다음 패널을 활성화
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else if (currentPanelIndex == 4)
        {
            dialoguePanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++; // 다음 패널을 활성화
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else if (currentPanelIndex == 5)
        {
            dialoguePanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++; // 다음 패널을 활성화
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else if (currentPanelIndex == 6)
        {
            nextButton_A.gameObject.SetActive(false); // nextButton1을 비활성화합니다.
            dialoguePanels[currentPanelIndex].SetActive(false); // 마지막 대화 패널을 비활성화합니다.
            currentPanelIndex++; // 다음 패널을 활성화
            nextButton1.gameObject.SetActive(true); // nextButton1을 비활성화합니다.
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
    }

    // 대화 종료 시 호출되는 메서드
    void EndDialogue()
    {
        dialoguePanels[currentPanelIndex].SetActive(false); // 마지막 대화 패널을 비활성화합니다.
        nextButton_A.gameObject.SetActive(false); // nextButton1을 비활성화합니다.
        nextButton1.gameObject.SetActive(true); // nextButton1을 비활성화합니다.
        // 대화 종료 후 추가적인 처리가 필요하면 여기에 로직을 추가합니다.
        // 예: 플레이어 제어 활성화, 새 퀘스트 시작 등
    }
}
