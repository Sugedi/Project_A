using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/*
-----------------------------------------------------------

*1번 트리거에 들어갈 내용

start
항상 main카메라가 true
그외 카메라는 false
 
조건 1 (맵 초기화 x + 지금 트리거에 닿이면)
카메라 - pyramid blendlist camera1(문방향 정면카메라) 로 전환

조건 2 (맵 초기화 x + 2번 트리거에 닿였다 + 지금 트리거에 닿이면)
카메라 - 다시 main camera 로 전환

조건 3 (맵 초기화 o + 지금 트리거에 닿이면)
카메라 - 다시 main camera 로 전환
문- 열린다

-----------------------------------------------------------

    */

public class Camerapyramid_Trigger1 : MonoBehaviour
{
    public CinemachineVirtualCamera mainVirtualCamera; // 메인 가상 카메라
    public CinemachineVirtualCamera virtualCamera; // 전환할 가상 카메라

    public bool isMapInitialized = false; // 맵 초기화 여부
    public bool isTrigger2Entered = false; // 2번 트리거에 닿았는지 여부
    public bool isThisTriggerEntered = false; // 현재 트리거에 닿았는지 여부

    void Start()
    {
        // 메인 가상 카메라는 항상 켜져 있도록 설정
        if (mainVirtualCamera != null)
        {
            mainVirtualCamera.gameObject.SetActive(true);
        }

        // 할당한 가상 카메라는 초기에 비활성화
        if (virtualCamera != null)
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }



    // 트리거에 진입했을 때 호출되는 함수
    void OnTriggerEnter(Collider other)
    {
        // 태그가 "Player"인 객체가 트리거에 닿았을 때
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거에 닿았습니다.");

            // 맵 초기화 안된 상태이고, 현재 트리거에 닿았을 때
            if (!isMapInitialized && isThisTriggerEntered)
            {
                // 가상 카메라를 활성화하고 메인 가상 카메라를 비활성화
                if (virtualCamera != null && mainVirtualCamera != null)
                {
                    virtualCamera.gameObject.SetActive(true);
                    mainVirtualCamera.gameObject.SetActive(false);
                    Debug.Log("가상 카메라로 전환되었습니다.");
                }
            }


            // 맵 초기화 안된 상태이고, 두 번째 트리거에 닿았고, 현재 트리거에 닿았을 때
            else if (!isMapInitialized && isTrigger2Entered && isThisTriggerEntered)
            {
                // 메인 가상 카메라를 다시 활성화하고 가상 카메라를 비활성화
                if (mainVirtualCamera != null && virtualCamera != null)
                {
                    mainVirtualCamera.gameObject.SetActive(true);
                    virtualCamera.gameObject.SetActive(false);
                    Debug.Log("메인 가상 카메라로 전환되었습니다.");
                }
            }


            // 맵 초기화된 상태이고, 현재 트리거에 닿았을 때
            else if (isMapInitialized && isThisTriggerEntered)
            {
                // 메인 가상 카메라를 다시 활성화하고 가상 카메라를 비활성화
                if (mainVirtualCamera != null && virtualCamera != null)
                {
                    mainVirtualCamera.gameObject.SetActive(true);
                    virtualCamera.gameObject.SetActive(false);
                    Debug.Log("맵 초기화 상태에서 메인 가상 카메라로 전환되었습니다.");
                }
            }
        }
    }

    // SwitchButton 스크립트에서 호출되는 맵 초기화 함수
    public void ResetMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
