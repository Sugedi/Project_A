using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class DialogueManager : MonoBehaviour
{
    // =============================================================================================================
    // 
    // =============================================================================================================

    public GameObject dialogPopupBtn; // Attack Button으로 설정되어 있음
    public GameObject joystick; // 조이스틱 UI
    public GameObject dialogPopup; // 대화 팝업창 UI

    public GameObject questWindowCanvas; // Hierarchy 창에 있는 QuestWindow Canvas에 대한 참조


    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        // 게임 내부 객체들의 기본 값(최초 상태)를 여기다가 전부 설정

        // 오류 발생(24-01-10) 했었는데 고침: 두 가지 방법이 있음
        // 1. 아래와 같이 게임이 시작될 때 최초로 1번 호출되는 Start 메소드에, 대화팝업창을 기본적으로 false 상태로 세팅해놓기
        // 2. 세팅 값 없이도 하이어라키 창에서 부모 객체인 talk canvas는 놔두고, 자식 객체인 dialogue만 인스펙터창에서 활성화 해제시켜놓기
        dialogPopup.SetActive(false); // 대화 팝업을 초기 상태(비활성화 상태)로 설정
    }

}
