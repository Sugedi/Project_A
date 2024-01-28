using UnityEngine;
using Cinemachine;
using System.Collections;

public class leverDoorOpen : MonoBehaviour
{
    public GameObject lever1;  // 레버 1 게임 오브젝트
    public GameObject lever2;  // 레버 2 게임 오브젝트
    public GameObject lever3;  // 레버 3 게임 오브젝트
    public GameObject lever4;  // 레버 4 게임 오브젝트

    public AnimationClip doorOpenAnimation;  // 문 열림 애니메이션 클립

    private bool hasOpened = false;  // 문이 열렸는지 여부를 추적하는 변수

    public CinemachineVirtualCamera mainVirtualCamera; // 메인카메라
    public CinemachineBlendListCamera blendListCamera;  // 블랜드 리스트 카메라

    void Start()
    {
        // 기본적으로는 메인카메라 활성화
        if (mainVirtualCamera != null)
        {
            mainVirtualCamera.enabled = true;
        }

        // 블렌딩 카메라는 비활성화 상태로 시작
        if (blendListCamera != null)
        {
            blendListCamera.enabled = false;
        }
    }

    void Update()
    {
        // 모든 레버가 퍼즐 답을 맞추었는지 확인
        bool allLeversActivated = CheckAllLevers();

        // 퍼즐 답이 올바르고 문이 아직 열리지 않았으면 문 열림 애니메이션을 재생
        if (allLeversActivated && !hasOpened)
        {
            // 문이 열리는 동안 블렌드 리스트 카메라를 활성화하고 메인 카메라를 비활성화
            mainVirtualCamera.enabled = false;
            blendListCamera.enabled = true;

            // 2초 후에 문 열림 애니메이션을 재생하기 위해 코루틴 호출
            StartCoroutine(PlayDoorAnimationWithDelay(2f));

            hasOpened = true;  // 문을 열린 상태로 표시
        }
    }

    bool CheckAllLevers()
    {
        // 각 레버의 퍼즐 답을 가져와서 모든 레버가 활성화되었는지 확인
        bool lever1Activated = lever1.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever2Activated = lever2.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever3Activated = lever3.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever4Activated = lever4.GetComponent<LeverController>().isPuzzleAnswer;

        return lever1Activated && lever2Activated && lever3Activated && lever4Activated;
    }

    // 문 열림 애니메이션을 재생하는 코루틴
    IEnumerator PlayDoorAnimationWithDelay(float delay)
    {
        // 대기 시간 후에 문 열림 애니메이션을 재생
        yield return new WaitForSeconds(delay);

        // 지정된 애니메이션 클립을 Animation 컴포넌트에 설정
        GetComponent<Animation>().clip = doorOpenAnimation;
        // 애니메이션을 한 번 재생
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();

        // 문이 열리는 동안 블랜드 리스트 카메라를 비활성화하고 메인 카메라를 활성화
        yield return new WaitForSeconds(doorOpenAnimation.length); // 애니메이션 재생 시간만큼 대기
        mainVirtualCamera.enabled = true;
        blendListCamera.enabled = false;
    }
}
