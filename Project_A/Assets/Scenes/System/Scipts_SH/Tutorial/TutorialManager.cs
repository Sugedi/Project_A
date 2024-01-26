using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialManager : MonoBehaviour
{
    // 조이스틱 & UI ON/OOF용
    public CanvasGroup joy;
    public CanvasGroup stageUI;
    public CanvasGroup questIcon;

    // 튜토리얼 단계
    // 0은 UI 튜토리얼
    public int tutorial;

    // UI 튜토리얼
    public CanvasGroup UI_0;
    public CanvasGroup UI_1;
    public CanvasGroup UI_2;
    public CanvasGroup UI_3;
    public CanvasGroup UI_4;
    public CanvasGroup UI_5;
    public CanvasGroup UI_6;
    public CanvasGroup UI_7;

    public int PanelNum = 0;

    void Start()
    {
        tutorial = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial;

        if(tutorial == 0)
        {
            CanvasGroupOff(joy);
            CanvasGroupOn(UI_0); // 첫 UI 튜토리얼 켜주기 // UI_1은 켜져있는 상태
        }

        else if (tutorial == 99)
        {
            //실행 안 함.
        }
    }

    public void UINext()
    {
        CanvasGroup NO_1 = GameObject.Find("UITutor").transform.Find("UI_0").transform.Find("Image").transform.Find("NO1").GetComponent<CanvasGroup>();
        CanvasGroup NO_2 = GameObject.Find("UITutor").transform.Find("UI_0").transform.Find("Image").transform.Find("NO2").GetComponent<CanvasGroup>();
        CanvasGroup NO_3 = GameObject.Find("UITutor").transform.Find("UI_0").transform.Find("Image").transform.Find("NO3").GetComponent<CanvasGroup>();

        // UI_0
        if (PanelNum == 0)
        {
            // no1,2,3.하고 UI_0 끈 후에 UI_1 켜기

            CanvasGroupOff(NO_1);
            CanvasGroupOn(NO_2);
            PanelNum++;
        }
        else if (PanelNum == 1)
        {
            // no1,2,3.하고 UI_0 끈 후에 UI_1 켜기

            CanvasGroupOff(NO_2);
            CanvasGroupOn(NO_3);
            PanelNum++;
        }
        else if(PanelNum == 2)
        {
            CanvasGroupOff(NO_3);
            CanvasGroupOn(NO_1);
            CanvasGroupOff(UI_0);
            CanvasGroupOn(UI_1);
            PanelNum++;
        }

        // UI_1
        else if(PanelNum == 3)
        {
            CanvasGroupOff(UI_1);
            CanvasGroupOn(UI_2);
            PanelNum++;
        }
        // UI_2
        else if(PanelNum == 4)
        {
            CanvasGroupOff(UI_2);
            CanvasGroupOn(UI_3);
            CanvasGroupOff(stageUI);
            CanvasGroupOff(questIcon);
            PanelNum++;
        }
        // UI_1
        else if(PanelNum == 5)
        {
            CanvasGroupOff(UI_3);
            CanvasGroupOn(UI_4);
            PanelNum++;
        }
        else if(PanelNum == 6)
        {
            CanvasGroupOff(UI_4);
            CanvasGroupOn(UI_5);
            PanelNum++; // 지금 7
        }
        else if(PanelNum == 7)
        {
            CanvasGroupOff(UI_5);
            CanvasGroupOn(UI_6);
            PanelNum++; // 지금 7
        }
        else if(PanelNum == 8)
        {
            CanvasGroupOff(UI_6);
            CanvasGroupOn(UI_7);
            PanelNum++; // 지금 7
        }
        else if(PanelNum == 9)
        {
            CanvasGroupOff(UI_7);
            CanvasGroupOn(joy);
            CanvasGroupOn(stageUI);
            CanvasGroupOn(questIcon);

            PanelNum++; // 지금 7
            tutorial = 1;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial = tutorial;
            DataManager.instance.DataSave();
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
