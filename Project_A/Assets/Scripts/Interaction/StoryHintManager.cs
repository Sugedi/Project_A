using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StoryHintManager : MonoBehaviour
{
    public GameObject storyHint1; // StoryHint1을 Inspector에서 연결해주세요.
    public GameObject[] hints; // H_1, H_2, H_3를 Inspector에서 연결해주세요.
    private int currentHintIndex = 0;
    private bool isPlayerInTrigger = false; // 플레이어가 트리거 영역에 있는지를 나타내는 변수

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StoryItem1") // StoryItem1에 닿을 때
        {
            isPlayerInTrigger = true; // 플레이어가 트리거 영역에 들어갔음을 표시
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "StoryItem1") // StoryItem1에서 벗어날 때
        {
            isPlayerInTrigger = false; // 플레이어가 트리거 영역에서 벗어났음을 표시
        }
    }

    public void ShowHint()
    {
        if (isPlayerInTrigger) // 플레이어가 트리거 영역에 있을 때만
        {
            storyHint1.SetActive(true); // StoryHint1 활성화
            hints[currentHintIndex].SetActive(true); // 첫 번째 힌트 활성화
        }
    }

    public void NextHint()
    {
        hints[currentHintIndex].SetActive(false); // 현재 힌트 비활성화
        currentHintIndex++; // 인덱스 증가

        if (currentHintIndex < hints.Length) // 아직 힌트가 남아있으면
        {
            hints[currentHintIndex].SetActive(true); // 다음 힌트 활성화
        }
        else // 모든 힌트를 보여줬으면
        {
            storyHint1.SetActive(false); // StoryHint1 비활성화
            currentHintIndex = 0; // 다음 실행을 위해 인덱스 초기화
        }
    }
}
