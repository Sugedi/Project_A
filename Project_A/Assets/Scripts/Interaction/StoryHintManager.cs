using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryHintManager : MonoBehaviour
{
    // 조이스틱 & UI ON/OFF용
    public CanvasGroup joy;
    public CanvasGroup stageUI;


    // HINT UI창 변수 선언. 근데 이거를 따로따로 해놓는게 아니라 그룹으로 잡음
    public CanvasGroup HINT_0;
    public CanvasGroup HINT_1;
    public CanvasGroup HINT_2;
    public CanvasGroup HINT_3;
    public CanvasGroup HINT_4;
    public CanvasGroup HINT_5;

    public void UINext()
    {
        CanvasGroup NO_1 = GameObject.Find("UITutor").transform.Find("UI_0").transform.Find("Image").transform.Find("NO1").GetComponent<CanvasGroup>();
        CanvasGroup NO_2 = GameObject.Find("UITutor").transform.Find("UI_0").transform.Find("Image").transform.Find("NO2").GetComponent<CanvasGroup>();
        CanvasGroup NO_3 = GameObject.Find("UITutor").transform.Find("UI_0").transform.Find("Image").transform.Find("NO3").GetComponent<CanvasGroup>();
        CanvasGroup NO_4 = GameObject.Find("UITutor").transform.Find("UI_2").transform.Find("Image").transform.Find("NO4").GetComponent<CanvasGroup>();
        CanvasGroup NO_5 = GameObject.Find("UITutor").transform.Find("UI_2").transform.Find("Image").transform.Find("NO5").GetComponent<CanvasGroup>();
        CanvasGroup NO_6 = GameObject.Find("MoveTutor").transform.Find("Move_0").transform.Find("Image").transform.Find("NO6").GetComponent<CanvasGroup>();
        CanvasGroup NO_7 = GameObject.Find("MoveTutor").transform.Find("Move_0").transform.Find("Image").transform.Find("NO7").GetComponent<CanvasGroup>();
        CanvasGroup NO_8 = GameObject.Find("MoveTutor").transform.Find("Move_0").transform.Find("Image").transform.Find("NO8").GetComponent<CanvasGroup>();
        CanvasGroup NO_9 = GameObject.Find("TreasureTutor").transform.Find("Treasure_0").transform.Find("Image").transform.Find("NO9").GetComponent<CanvasGroup>();
        CanvasGroup NO_10 = GameObject.Find("TreasureTutor").transform.Find("Treasure_0").transform.Find("Image").transform.Find("NO10").GetComponent<CanvasGroup>();
        CanvasGroup NO_11 = GameObject.Find("TreasureTutor").transform.Find("Treasure_0").transform.Find("Image").transform.Find("NO11").GetComponent<CanvasGroup>();
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
