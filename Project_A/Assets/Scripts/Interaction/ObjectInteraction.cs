using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// =============================================================================================================================
// NPC 오브젝트에 붙이는 스크립트
// 상호작용 트리거 범위 내에 플레이어가 닿을 시, '상호작용 버튼'이 생성됨
// Main Quest 진행 과정도 같이 작성됨 (+ 승호에게 알려줄 변수 포함)
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    public GameObject button1; // 'Dialogue1 StartBtn: 거울에 손을 뻗는다'
    public GameObject button2; // 'Dialogue2 StartBtn: 거울에 손을 뻗는다'

    public bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부

    public GameObject dialogue1; // 새로운 UI 패널 오브젝트




    void OnTriggerEnter(Collider other) // 오브젝트가 트리거에 진입할 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 진입한 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = true; // 플레이어가 상호작용 범위 내에 있음을 표시
            button1.SetActive(true); // 버튼을 활성화
            // 위에 변수 부분에서 자료형을 Button에서 GameObject로 변경해주어야 SetActive를 쓸 수 있음
        }
    }


    void OnTriggerExit(Collider other) // 오브젝트가 트리거를 빠져나갈 때 호출되는 함수
    {
        if (other.CompareTag("Player")) // 트리거를 빠져나간 오브젝트의 태그가 Player인 경우
        {
            isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 없음을 표시
            button1.SetActive(false); // 버튼을 비활성화

        }
    }

    // 버튼이 눌렸을 때 호출되는 메서드
    public void OnButtonPressed()
    {
        dialogue1.SetActive(true); // 새로운 패널을 활성화
        button1.SetActive(false);  // 버튼을 비활성화
    }








}

