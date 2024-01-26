using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{

    public int tutorial = 0;
    public CanvasGroup UITutorial;

    // Start is called before the first frame update
    void Start()
    {
        if(tutorial == 0)
        {
            Time.timeScale = 0f;
            CanvasGroupOn(UITutorial); // 첫 UI 튜토리얼 켜주기

        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
