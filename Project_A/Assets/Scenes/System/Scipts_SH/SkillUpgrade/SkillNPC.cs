using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNPC : MonoBehaviour
{
    // UI�� ���� �ѱ� ���� ĵ���� �׷��� ����
    public CanvasGroup skillCanvas;
    public CanvasGroup mainCanvas;

    public void Interact()
    {
        CanvasGroupOn(skillCanvas);
        CanvasGroupOff(mainCanvas);
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
}
