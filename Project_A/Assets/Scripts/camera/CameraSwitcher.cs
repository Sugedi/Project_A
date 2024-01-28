using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;      // 우선 순위 10번인 버추얼 카메라
    public CinemachineBlendListCamera blendingCamera;   // 우선 순위 2번인 블렌딩 카메라
    public float blendingDuration = 3f; // 블렌딩 카메라 활성화 기간 (초)

    private bool hasTriggered = false;   // 트리거가 한 번 발생했는지 여부를 나타내는 플래그
    private bool isBlending = false;     // 블렌딩 중인지 여부를 나타내는 플래그

    public string bossBGMName; // Elite boss BGM의 이름

    void Start()
    {
        // 기본적으로는 우선 순위 10번인 버추얼 카메라를 활성화
        if (virtualCamera != null)
        {
            virtualCamera.enabled = true;
        }

        // 블렌딩 카메라는 비활성화 상태로 시작
        if (blendingCamera != null)
        {
            blendingCamera.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 한 번 트리거가 이미 발생했으면 더 이상 실행하지 않음
        if (hasTriggered)
        {
            return;
        }

        // 특정 오브젝트에 플레이어 태그가 트리거되면 블렌딩과 BGM 재생 시작
        if (other.CompareTag("Player"))
        {
            // 현재 재생 중인 배경음악을 정지합니다 (필요한 경우).
            SoundManager.instance.StopAudio("BGM");

            // Elite boss BGM을 재생합니다.
            SoundManager.instance.PlayAudio("Boss", "BGM");

            StartCoroutine(SwitchToBlendingCamera());
            hasTriggered = true; // 트리거가 발생했음을 표시
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
            if (blendingCamera != null)
            {
                blendingCamera.enabled = true;
                yield return new WaitForSeconds(blendingDuration);
            }

            // 우선 순위 2번인 블렌딩 카메라 비활성화
            if (blendingCamera != null)
            {
                blendingCamera.enabled = false;
            }

            // 우선 순위 10번인 버추얼 카메라 활성화
            if (virtualCamera != null)
            {
                virtualCamera.enabled = true;
            }

            isBlending = false;
        }
    }
}
