using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera defaultVirtualCamera; // 기본 버추얼 카메라 설정 (우선순위 10)
    public CinemachineVirtualCamera switchVirtualCamera; // 전환 버추얼 카메라 설정 (우선순위 1)
    public MonoBehaviour MatchingPuzzle; // 충돌을 감지할 스크립트 설정

    private void Start()
    {
        // 초기화: 버추얼 카메라를 비활성화하고, 기본 카메라를 활성화
        // 초기화: 기본 버추얼 카메라를 활성화하고, 전환 버추얼 카메라를 비활성화
        defaultVirtualCamera.Priority = 10;
        switchVirtualCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트에 특정 스크립트가 적용되어 있는 경우
        if (other.GetComponent(MatchingPuzzle.GetType()) != null)
        {
            SwitchCameras(); // 카메라 전환 메소드 호출
        }
    }

    private void SwitchCameras()
    {

        // 게임 타임을 일시정지 (Time.timeScale = 0)
        //Time.timeScale = 0;

        // 기본 버추얼 카메라 비활성화, 전환 버추얼 카메라 활성화
        defaultVirtualCamera.Priority = 0;
        switchVirtualCamera.Priority = 9;

        // 일정 시간 후에 다시 전환 버추얼 카메라를 비활성화하고 기본 버추얼 카메라를 활성화
        StartCoroutine(SwitchBackAfterDelay());


    }


    private IEnumerator SwitchBackAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        // 플레이어 움직임 재개 (Time.timeScale = 1)
       // Time.timeScale = 1;

        // 전환 버추얼 카메라 비활성화, 기본 버추얼 카메라 활성화
        switchVirtualCamera.Priority = 0;
        defaultVirtualCamera.Priority = 10;
    }
}