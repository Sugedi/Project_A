using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StoryHintManager : MonoBehaviour
{
    public CanvasGroup storyHint; // StoryHint1을 Inspector에서 연결해주세요.
    public CanvasGroup[] hints;
    public CanvasGroup joy;

    private int currentHintIndex = 0;

    public CinemachineVirtualCamera originalCamera; // Inspector에서 기존의 카메라 연결
    public CinemachineVirtualCamera transitionCamera; // Inspector에서 CM vcam4 연결

    public void ShowHint()
    {
        CanvasGroupOn(storyHint);
    }

    public void NextHint()
    {
        CanvasGroupOff(hints[currentHintIndex]); // 현재 힌트 비활성화
        currentHintIndex++; // 인덱스 증가

        if (currentHintIndex < hints.Length) // 아직 힌트가 남아있으면
        {
            CanvasGroupOn(hints[currentHintIndex]); // 다음 힌트 활성화

            if (currentHintIndex == 3) 
            {
                transitionCamera.Priority = 11;
                
            }

            if (currentHintIndex == 4) 
            {
                CanvasGroupOff(storyHint);
                CanvasGroupOff(joy);
                
            }

            if (currentHintIndex == 5) 
            {
                CanvasGroupOn(storyHint);
                originalCamera.Priority = 10;
                transitionCamera.Priority = 5;
            }

        }
        else // 모든 힌트를 보여줬으면
        {
            CanvasGroupOff(storyHint);
            CanvasGroupOn(hints[0]);
            CanvasGroupOn(joy);

            currentHintIndex = 0; // 다음 실행을 위해 인덱스 초기화

        }
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }
}
