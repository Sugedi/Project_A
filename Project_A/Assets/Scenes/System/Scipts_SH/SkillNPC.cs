using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNPC : MonoBehaviour
{
    // UI를 껐다 켜기 위한 캔버스 그룹을 선언
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
