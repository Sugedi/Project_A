using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가
using Cinemachine;

public class CameraInsidePyramid : MonoBehaviour
{
    public CinemachineVirtualCamera mainvirtualCamera;          // 우선 순위 10번인 버추얼 카메라
    public CinemachineBlendListCamera outsidevirtualCameraPyramid;   // 우선 순위 2번인 블렌딩 카메라
    public Button attackButton;                                  // 공격 버튼
    private bool isInsideCollider = false;                      // 콜라이더 안에 있는지 여부

    void OnTriggerEnter(Collider other)
    {
        // 특정 오브젝트의 콜라이더에 들어갔을 때
        if (other.CompareTag("Player"))
        {
            isInsideCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 특정 오브젝트의 콜라이더에서 나왔을 때
        if (other.CompareTag("Player"))
        {
            isInsideCollider = false;
        }
    }

    void Start()
    {
        // 공격 버튼에 onClick 이벤트 추가
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackButtonClick);
        }
    }

    void OnAttackButtonClick()
    {
        // 특정 오브젝트의 콜라이더 안에 있고, 버튼을 클릭했을 때
        if (isInsideCollider)
        {
            // mainvirtualCamera를 활성화하고 outsidevirtualCameraPyramid를 비활성화
            if (mainvirtualCamera != null && outsidevirtualCameraPyramid != null)
            {
                mainvirtualCamera.enabled = true;
                outsidevirtualCameraPyramid.enabled = false;
            }
        }
    }
}