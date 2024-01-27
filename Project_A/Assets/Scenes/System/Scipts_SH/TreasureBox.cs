using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Treasure
{
    BuckShot1,
    PierceShot,
    Money
}

public class TreasureBox : MonoBehaviour
{
    public Treasure treasure;

    public bool treasure_1; 
    public bool treasure_2; 
    public bool treasure_3; 

    public CanvasGroup treasureBox;
    public TextMeshProUGUI tresaureMSG;

    public CanvasGroup joy;

    private void Start()
    {
        treasure_1 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox1;
        treasure_2 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox2;
        treasure_3 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox3;

        if (this.name == "TreasureBox1")
        {
            if (treasure_1 == false)
            {
                GameObject.Find("TreasureBox1").SetActive(true);
                GameObject.Find("TreasureBox1After").transform.Find("TreasureBox1_After").gameObject.SetActive(false);
            }
            else if (treasure_1 == true)
            {
                GameObject.Find("TreasureBox1").SetActive(false);
                GameObject.Find("TreasureBox1After").transform.Find("TreasureBox1_After").gameObject.SetActive(true);
            }
        }

        if (this.name == "TreasureBox2")
        {
            if (treasure_2 == false)
            {
                GameObject.Find("TreasureBox2").SetActive(true);
                GameObject.Find("TreasureBox2After").transform.Find("TreasureBox2_After").gameObject.SetActive(false);
            }
            else if (treasure_2 == true)
            {
                GameObject.Find("TreasureBox2").SetActive(false);
                GameObject.Find("TreasureBox2After").transform.Find("TreasureBox2_After").gameObject.SetActive(true);
            }
        }

        if (this.name == "TreasureBox3")
        {
            if (treasure_3 == false)
            {
                GameObject.Find("TreasureBox3").SetActive(true);
                GameObject.Find("TreasureBox3After").transform.Find("TreasureBox3_After").gameObject.SetActive(false);
            }
            else if (treasure_3 == true)
            {
                GameObject.Find("TreasureBox3").SetActive(false);
                GameObject.Find("TreasureBox3After").transform.Find("TreasureBox3_After").gameObject.SetActive(true);
            }
        }

    }

    public void TreasureFind()
    {
        if (treasure == Treasure.BuckShot1)
        {
            if (treasure_1 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox1").SetActive(false);
                GameObject.Find("TreasureBox1After").transform.Find("TreasureBox1_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "���� ��ų�� ȹ���Ͽ����ϴ�!";
                CanvasGroupOn(treasureBox);
                // System MSG ��� ĵ������ Ȱ��ȭ�ϴ� ������
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("BuckShot2"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
                treasure_1 = true; // false�� ���� ���� ����, true�� ���� ���� ���� 
                DataManager.instance.datas.stage1ItemBox1 = true;
                DataManager.instance.DataSave();
            }

        }

        if (treasure == Treasure.PierceShot)
        {
            if (treasure_2 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox2").SetActive(false);
                GameObject.Find("TreasureBox2After").transform.Find("TreasureBox2_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "���뼦 ��ų�� ȹ���Ͽ����ϴ�!";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("PierceShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
                treasure_2 = true;
                DataManager.instance.datas.stage1ItemBox2 = true;
                DataManager.instance.DataSave();
            }

        }
        if (treasure == Treasure.Money)
        {
            if (treasure_3 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox3").SetActive(false);
                GameObject.Find("TreasureBox3After").transform.Find("TreasureBox3_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "��ȥ ���� 30���� ȹ���Ͽ����ϴ�!";
                CanvasGroupOn(treasureBox);
                GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul += 30;
                treasure_3 = true;
                DataManager.instance.datas.stage1ItemBox3 = true;
                DataManager.instance.DataSave();
            }

        }
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
