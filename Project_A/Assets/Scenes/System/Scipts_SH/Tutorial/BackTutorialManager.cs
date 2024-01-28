using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTutorialManager : MonoBehaviour
{
    public CanvasGroup joy;
    public CanvasGroup stageUI;

    public int tutorial;
    public int PanelNum = 0;

    public bool SkillLVUp = false;

    public CanvasGroup Skill_0;
    public CanvasGroup Skill_1;
    public CanvasGroup Skill_2;
    public CanvasGroup SkillCanvas;

    void Start()
    {
        tutorial = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial;

        if(tutorial == 3)
        {
            CanvasGroupOff(joy);
            CanvasGroupOff(stageUI);
            CanvasGroupOn(Skill_0);
        }
    }

    void Update()
    {
        if(tutorial == 3)
        {
            if(SkillLVUp == false)
            {
                if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul == 0)
                {
                    DataManager.instance.DataSave();
                    CanvasGroupOff(SkillCanvas);
                    CanvasGroupOff(joy);
                    CanvasGroupOff(stageUI);
                    CanvasGroupOn(Skill_2);
                    SkillLVUp = true;
                }
            }

        }
    }

    public void Skill_Next()
    {
        CanvasGroup NO_1 = GameObject.Find("SkillTutor").transform.Find("Skill_0").transform.Find("Image").transform.Find("NO1").GetComponent<CanvasGroup>();
        CanvasGroup NO_2 = GameObject.Find("SkillTutor").transform.Find("Skill_0").transform.Find("Image").transform.Find("NO2").GetComponent<CanvasGroup>();
        CanvasGroup NO_3 = GameObject.Find("SkillTutor").transform.Find("Skill_1").transform.Find("Image").transform.Find("NO3").GetComponent<CanvasGroup>();
        CanvasGroup NO_4 = GameObject.Find("SkillTutor").transform.Find("Skill_1").transform.Find("Image").transform.Find("NO4").GetComponent<CanvasGroup>();
        CanvasGroup NO_5 = GameObject.Find("SkillTutor").transform.Find("Skill_2").transform.Find("Image").transform.Find("NO5").GetComponent<CanvasGroup>();
        CanvasGroup NO_6 = GameObject.Find("SkillTutor").transform.Find("Skill_2").transform.Find("Image").transform.Find("NO6").GetComponent<CanvasGroup>();

        if (PanelNum == 0)
        {
            CanvasGroupOff(NO_1);
            CanvasGroupOn(NO_2);
            PanelNum++;
        }
        else if (PanelNum == 1)
        {
            CanvasGroupOff(Skill_0);
            CanvasGroupOn(Skill_1);
            PanelNum++;
        }
        else if (PanelNum == 2)
        {
            CanvasGroupOff(NO_3);
            CanvasGroupOn(NO_4);
            PanelNum++;
        }
        else if (PanelNum == 3)
        {
            CanvasGroupOff(Skill_1);
            CanvasGroupOn(joy);
            CanvasGroupOn(stageUI);
            PanelNum++;
        }
        else if (PanelNum == 4)
        {
            CanvasGroupOff(NO_5);
            CanvasGroupOn(NO_6);
            PanelNum++;
        }
        else if (PanelNum == 5)
        {
            CanvasGroupOn(joy);
            CanvasGroupOn(stageUI);
            CanvasGroupOff(Skill_2);
            PanelNum++;
            tutorial = 99;
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial = tutorial;
            DataManager.instance.DataSave();
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
