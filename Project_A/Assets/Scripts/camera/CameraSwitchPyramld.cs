using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchPyramld : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;      // 우선 순위 10번인 버추얼 카메라
    public CinemachineClearShot clearShotCamera;   // 우선 순위 2번인 블렌딩 카메라
    public float blendingDuration = 5f; // 블렌딩 카메라 활성화 기간 (초)

    //private bool hasTriggered = false;   // 트리거가 한 번 발생했는지 여부를 나타내는 플래그
    private bool isBlending = false;     // 블렌딩 중인지 여부를 나타내는 플래그

    void Start()
    {
        // 기본적으로는 우선 순위 10번인 버추얼 카메라를 활성화
        if (virtualCamera != null)
        {
            virtualCamera.enabled = true;
        }

        // 블렌딩 카메라는 비활성화 상태로 시작
        if (clearShotCamera != null)
        {
            clearShotCamera.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {


        // 특정 오브젝트에 플레이어 태그가 트리거되면 블렌딩 시작
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SwitchToBlendingCamera());
            //hasTriggered = true; // 트리거가 발생했음을 표시
        }
    }

    IEnumerator SwitchToBlendingCamera()
    {
        // 블렌딩 중이 아닐 때만 실행
        if (!isBlending)
        {
            isBlending = true;

            // 우선 순위 10번인 버추얼 카메라 비활성화
            if (virtualCamera != null)
            {
                virtualCamera.enabled = false;
            }

            // 우선 순위 2번인 블렌딩 카메라 활성화
            if (clearShotCamera != null)
            {
                clearShotCamera.enabled = true;
                yield return new WaitForSeconds(blendingDuration);
            }

            // 우선 순위 2번인 블렌딩 카메라 비활성화
            if (clearShotCamera != null)
            {
                clearShotCamera.enabled = false;
            }

            // 우선 순위 10번인 버추얼 카메라 활성화
            if (virtualCamera != null)
            {
                virtualCamera.enabled = true;
            }

            isBlending = false;
        }
       // yield break; // 값을 반환하지 않음을 명시적으로 표현
    }
}

