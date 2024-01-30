using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryHintManager : MonoBehaviour
{
    public CanvasGroup storyHint; // StoryHint1�� Inspector���� �������ּ���.
    public CanvasGroup[] hints;
    private int currentHintIndex = 0;


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
        }
        else // ��� ��Ʈ�� ����������
        {
            CanvasGroupOff(storyHint);
            CanvasGroupOn(hints[0]);

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
