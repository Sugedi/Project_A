using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class bossKillDoor : MonoBehaviour
{
    public AnimationClip bossDoorOpenAnimation; // 보스가 죽으면 문이 열리는 애니메이션

    public CinemachineVirtualCamera mainVirtualCamera; // 메인카메라
    public CinemachineBlendListCamera blendListCamera;  // 블랜드 리스트 카메라

    private void Start()
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

        // 만약 '갈리오'를 처음으로 클리어했다면
        if (DataManager.instance.datas.galioFirstClear == true)
        {
            // 이 게임 오브젝트를 비활성화합니다.
            gameObject.SetActive(false);
        }
    }

    public void BossKillDoorOpen()
    {
        // 만약 문의 애니메이션이 지정되어 있다면
        if (bossDoorOpenAnimation != null)
        {
            // '보스를 죽이고 문을 열기' 애니메이션을 실행합니다.
            if(DataManager.instance.datas.galioFirstClear == false)
            {
                bossPlayDoorAnimation(bossDoorOpenAnimation);
            }

        }
        else
        {
            // 경고: 보스 킬 도어 스크립트에 문 애니메이션이 지정되지 않았습니다.
            Debug.LogError("Door Animation not assigned to BossKillDoor script.");
        }
    }

    void bossPlayDoorAnimation(AnimationClip animationClip)
    {
        // 문이 열릴 때 사운드를 재생합니다.
        SoundManager.instance.PlayAudio("Gate1", "SE");

        // Animation 컴포넌트에 지정된 애니메이션 클립을 설정합니다.
        GetComponent<Animation>().clip = animationClip;

        // 애니메이션을 한 번 재생합니다.
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();

        // 문이 열리는 동안 블랜드 리스트 카메라를 활성화하고 메인 카메라를 비활성화
        mainVirtualCamera.enabled = false;
        blendListCamera.enabled = true;

        // 애니메이션 재생 후에 스크립트를 비활성화하는 메서드를 호출합니다.
        StartCoroutine(DisableAfterAnimation(animationClip.length));

    }

    IEnumerator DisableAfterAnimation(float duration)
    {
        // 애니메이션이 끝날 때까지 기다립니다.
        yield return new WaitForSeconds(duration);

        // 문이 닫히는 동안 메인 카메라를 활성화하고 블랜드 리스트 카메라를 비활성화
        mainVirtualCamera.enabled = true;
        blendListCamera.enabled = false;

        // 스크립트를 비활성화합니다.
        DisableScript();
    }

    void DisableScript()
    {
        // '갈리오'를 처음으로 클리어했다고 표시합니다.
        DataManager.instance.datas.galioFirstClear = true;

        // 로그에 메시지를 남깁니다.
        Debug.Log("갈리오를 물리쳤습니다!");

        // 이 게임 오브젝트를 비활성화합니다.
        gameObject.SetActive(false);

        // 데이터를 저장합니다.
        DataManager.instance.DataSave();
    }
}
