using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager_Puzzle : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 버추얼 카메라 설정
    public Camera defaultCamera; // 기본 카메라 설정
    public LayerMask triggerLayer; // 트리거 레이어 설정

    private void Start()
    {
        // 초기화: 버추얼 카메라를 비활성화하고, 기본 카메라를 활성화
        virtualCamera.Priority = 0;
        defaultCamera.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 트리거 레이어에 속한 오브젝트와 충돌한 경우
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            SwitchCameras(); // 카메라 전환 메소드 호출
        }
    }

    private void SwitchCameras()
    {
        // 기본 카메라 비활성화, 버추얼 카메라 활성화
        defaultCamera.enabled = false;
        virtualCamera.Priority = 10;

        // 일정 시간 후에 다시 기본 카메라로 전환
        StartCoroutine(SwitchBackAfterDelay());
    }

    private IEnumerator SwitchBackAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        // 버추얼 카메라 비활성화, 기본 카메라 활성화
        virtualCamera.Priority = 0;
        defaultCamera.enabled = true;
    }
}