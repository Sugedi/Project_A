using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialManager : MonoBehaviour
{
    // 조이스틱 & UI ON/OOF용
    public CanvasGroup joy;
    public CanvasGroup stageUI;

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
    public CanvasGroup UI_8;
    public CanvasGroup UI_9;
    public CanvasGroup Quest;
    public CanvasGroup Move_0;
    public CanvasGroup Treasure_0;

    public int PanelNum = 0;

    void Start()
    {
        tutorial = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial;
        PanelNum = GameObject.Find("DataManager").GetComponent<DataManager>().datas.tutorPanelNum;

        if(tutorial == 0)
        {
            CanvasGroupOff(joy);
            CanvasGroupOn(UI_0); // 첫 UI 튜토리얼 켜주기 // UI_1은 켜져있는 상태
        }
        else if(tutorial == 1)
        {
            GameObject.Find("MoveGoal").transform.Find("Portal").gameObject.SetActive(true);
        }
        else if(tutorial == 2)
        {
            
        }

        else if (tutorial == 99)
        {
            //실행 안 함.
            GameObject.Find("TutorWall").transform.Find("TurtorWall_1").gameObject.SetActive(false);
        }
    }

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
        else if(PanelNum == 3)
        {
            CanvasGroupOff(UI_1);
            CanvasGroupOn(UI_2);
            PanelNum++;
        }
        else if(PanelNum == 4)
        {
            CanvasGroupOff(UI_2);
            CanvasGroupOff(NO_4);
            CanvasGroupOn(NO_5);
            CanvasGroupOff(Quest);
            CanvasGroupOn(UI_3);

            PanelNum++;
        }
        else if(PanelNum == 5)
        {
            CanvasGroupOff(UI_3);
            CanvasGroupOn(Quest);
            CanvasGroupOn(UI_2);
            PanelNum++;
        }    

        else if(PanelNum == 6)
        {
            CanvasGroupOff(UI_2);
            CanvasGroupOn(UI_4);
            PanelNum++; 
        }
        else if(PanelNum == 7)
        {
            CanvasGroupOff(UI_4);
            CanvasGroupOn(UI_5);
            CanvasGroupOff(stageUI);
            PanelNum++; 
        }
        else if(PanelNum == 8)
        {
            CanvasGroupOff(UI_5);
            CanvasGroupOn(UI_6);
            PanelNum++; 
        }
        else if(PanelNum == 9)
        {
            CanvasGroupOff(UI_6);
            CanvasGroupOn(UI_7);
            PanelNum++; 
        }
        else if(PanelNum == 10)
        {
            CanvasGroupOff(UI_7);
            CanvasGroupOn(UI_8);
            PanelNum++; 
        }
        else if(PanelNum == 11)
        {
            CanvasGroupOff(UI_8);
            CanvasGroupOn(UI_9);
            GameObject.Find("MoveGoal").transform.Find("Portal").gameObject.SetActive(true);
            PanelNum++; 
        }
        else if(PanelNum == 12)
        {
            CanvasGroupOff(UI_9);
            CanvasGroupOn(joy);
            CanvasGroupOn(stageUI);

            PanelNum++; 
            tutorial = 1;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial = tutorial;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.tutorPanelNum = PanelNum;
            DataManager.instance.DataSave();
        }
        else if (PanelNum == 13)
        {
            CanvasGroupOff(NO_6);
            CanvasGroupOn(NO_7);
            PanelNum++;
        }
        else if (PanelNum == 14)
        {
            CanvasGroupOff(NO_7);
            CanvasGroupOn(NO_8);
            PanelNum++;
        }
        else if (PanelNum == 15)
        {
            CanvasGroupOff(NO_8);
            CanvasGroupOff(Move_0);
            CanvasGroupOn(joy);
            PanelNum++;
            tutorial = 2;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial = tutorial;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.tutorPanelNum = PanelNum;
            DataManager.instance.DataSave();
        }
        // 보물 상자 먹었을 때, 즉발 - 이건 버튼 아님
        else if (PanelNum == 16)
        {
            Time.timeScale = 1f;
            CanvasGroupOff(joy);
            CanvasGroupOn(Treasure_0);
            PanelNum++;
        }        
        else if (PanelNum == 17)
        {
            CanvasGroupOff(NO_9);
            CanvasGroupOn(NO_10);
            PanelNum++;
        }           
        else if (PanelNum == 18)
        {
            CanvasGroupOff(NO_10);
            CanvasGroupOn(NO_11);
            PanelNum++;
        }        
        else if (PanelNum == 19)
        {
            CanvasGroupOff(Treasure_0);
            CanvasGroupOn(joy);
            PanelNum++;
            tutorial = 3;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial = tutorial;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.tutorPanelNum = PanelNum;
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
