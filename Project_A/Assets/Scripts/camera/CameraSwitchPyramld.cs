using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


//갈리오가 죽고 나서 문이 열리고 들어가는 유저는 메인 (우선순위50번) -> 2/3번 블랜드리스트 카메라(우선순위47번) on
//그런데 갑자기 맘바껴서 돌아오는 유저는 저 트리거에 닿았을때 2/3번 블랜드 리스트카메라 -> 메인카메라
// 즉 한번 트리거에 닿이면 메인->2/3번 블랜드리스트 카메라 on
//트리거 한번 닿인 유저는 2/3번 블랜드 리스트카메라 -> 메인카메라


//반대로 피라미드 내부에서 나오는 유저는 피라미드 이와 같은 메커니즘로 된 트리거를 피라미드 문 바로 앞에 배치
//문 바로 앞에 깔려있는 트리거에 닿인 유저는 3/4번 블랜드 카메라 on



public class CameraSwitchPyramld : MonoBehaviour
{
    public CinemachineVirtualCamera mainVirtualCamera;         // 우선 순위가 가장 높은 메인 카메라
    public CinemachineBlendListCamera blendCamera;             // 우선 순위가 높은 블렌딩 카메라 (2/3번)
    private bool hasEnteredTrigger = false;                    // 트리거에 진입했는지 여부를 나타내는 플래그




    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 진입했을 때
        if (other.CompareTag("Player"))
        {
            // 이미 트리거에 진입한 유저라면
            if (hasEnteredTrigger)
            {
                // 블렌딩 카메라를 비활성화하고 메인 카메라를 활성화
                blendCamera.enabled = false;
                mainVirtualCamera.enabled = true;
            }
            else
            {
                // 트리거에 진입한 유저가 아니라면
                // 블렌딩 카메라를 활성화하고 메인 카메라를 비활성화
                blendCamera.enabled = true;
                mainVirtualCamera.enabled = false;

                hasEnteredTrigger = true; // 트리거에 진입한 상태로 플래그 설정
            }
        }
    }
}