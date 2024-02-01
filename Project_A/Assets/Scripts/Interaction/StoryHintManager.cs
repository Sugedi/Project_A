using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StoryHintManager : MonoBehaviour
{
    public CanvasGroup storyHint; // StoryHint1�� Inspector���� �������ּ���.
    public CanvasGroup[] hints;
    public CanvasGroup joy;

    private int currentHintIndex = 0;

    public CinemachineVirtualCamera originalCamera; // Inspector���� ������ ī�޶� ����
    public CinemachineVirtualCamera transitionCamera; // Inspector���� CM vcam4 ����

    public void ShowHint()
    {
        CanvasGroupOn(storyHint);
    }

    public void NextHint()
    {
        CanvasGroupOff(hints[currentHintIndex]); // ���� ��Ʈ ��Ȱ��ȭ
        currentHintIndex++; // �ε��� ����

        if (currentHintIndex < hints.Length) // ���� ��Ʈ�� ����������
        {
            CanvasGroupOn(hints[currentHintIndex]); // ���� ��Ʈ Ȱ��ȭ

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
        else // ��� ��Ʈ�� ����������
        {
            CanvasGroupOff(storyHint);
            CanvasGroupOn(hints[0]);
            CanvasGroupOn(joy);

            currentHintIndex = 0; // ���� ������ ���� �ε��� �ʱ�ȭ

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
