using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPyramidSound : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 특정 오브젝트에 플레이어 태그가 트리거되면 블렌딩 재생 시작
        if (other.CompareTag("Player"))
        {
            // 현재 재생 중인 배경음악을 정지합니다 (필요한 경우).
            SoundManager.instance.StopAudio("BGM");

            // Pyramid BGM을 재생합니다.
            SoundManager.instance.PlayAudio("Pyramid", "BGM");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pyramid BGM을 정지합니다.
            SoundManager.instance.StopAudio("BGM");

            // 원래의 배경음악을 다시 재생합니다 (원래 BGM 이름을 알아야 함).
            SoundManager.instance.PlayAudio("Stage", "BGM");
        }
    }
}
