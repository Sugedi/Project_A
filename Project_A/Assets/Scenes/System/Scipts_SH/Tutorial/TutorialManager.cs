using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialManager : MonoBehaviour
{
    // ���̽�ƽ & UI ON/OOF��
    public CanvasGroup joy;
    public CanvasGroup stageUI;
    public CanvasGroup questIcon;

    // Ʃ�丮�� �ܰ�
    // 0�� UI Ʃ�丮��
    public int tutorial;

    // UI Ʃ�丮��
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
            CanvasGroupOn(UI_0); // ù UI Ʃ�丮�� ���ֱ� // UI_1�� �����ִ� ����
        }

        else if (tutorial == 99)
        {
            //���� �� ��.
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
            // no1,2,3.�ϰ� UI_0 �� �Ŀ� UI_1 �ѱ�

            CanvasGroupOff(NO_1);
            CanvasGroupOn(NO_2);
            PanelNum++;
        }
        else if (PanelNum == 1)
        {
            // no1,2,3.�ϰ� UI_0 �� �Ŀ� UI_1 �ѱ�

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
            PanelNum++; // ���� 7
        }
        else if(PanelNum == 7)
        {
            CanvasGroupOff(UI_5);
            CanvasGroupOn(UI_6);
            PanelNum++; // ���� 7
        }
        else if(PanelNum == 8)
        {
            CanvasGroupOff(UI_6);
            CanvasGroupOn(UI_7);
            PanelNum++; // ���� 7
        }
        else if(PanelNum == 9)
        {
            CanvasGroupOff(UI_7);
            CanvasGroupOn(joy);
            CanvasGroupOn(stageUI);
            CanvasGroupOn(questIcon);

            PanelNum++; // ���� 7
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
