using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Treasure
{
    BuckShot1,
    PierceShot,
    SideShot,
    Lighting,
    Money
}

public class TreasureBox : MonoBehaviour
{
    public Treasure treasure;

    public bool treasure_1; 
    public bool treasure_2; 
    public bool treasure_3; 
    public bool treasure_4; 
    public bool treasure_5; 

    public CanvasGroup treasureBox;
    public TextMeshProUGUI tresaureMSG;

    public CanvasGroup joy;

    private void Start()
    {
        treasure_1 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox1;
        treasure_2 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox2;
        treasure_3 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox3;
        treasure_4 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox4;
        treasure_5 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox5;

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
        if (this.name == "TreasureBox4")
        {
            if (treasure_4 == false)
            {
                GameObject.Find("TreasureBox4").SetActive(true);
                GameObject.Find("TreasureBox4After").transform.Find("TreasureBox4_After").gameObject.SetActive(false);
            }
            else if (treasure_4 == true)
            {
                GameObject.Find("TreasureBox4").SetActive(false);
                GameObject.Find("TreasureBox4After").transform.Find("TreasureBox4_After").gameObject.SetActive(true);
            }
        }
        if (this.name == "TreasureBox5")
        {
            if (treasure_5 == false)
            {
                GameObject.Find("TreasureBox5").SetActive(true);
                GameObject.Find("TreasureBox5After").transform.Find("TreasureBox5_After").gameObject.SetActive(false);
            }
            else if (treasure_5 == true)
            {
                GameObject.Find("TreasureBox5").SetActive(false);
                GameObject.Find("TreasureBox5After").transform.Find("TreasureBox5_After").gameObject.SetActive(true);
            }
        }

    }

    public void TreasureFind()
    {
        if (treasure == Treasure.BuckShot1)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_1 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox1").SetActive(false);
                GameObject.Find("TreasureBox1After").transform.Find("TreasureBox1_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "¹÷¼¦ ½ºÅ³À» È¹µæÇÏ¿´½À´Ï´Ù!";
                CanvasGroupOn(treasureBox);
                // System MSG ¶ó´Â Äµ¹ö½º¸¦ È°¼ºÈ­ÇÏ´Â °ÍÀ¸·Î
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("BuckShot2"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_1 = true; // falseÀÏ ¶§´Â ¿­¸° »óÀÚ, trueÀÏ ¶§´Â ´ÝÈù »óÀÚ 
                DataManager.instance.datas.stage1ItemBox1 = true;
                DataManager.instance.DataSave();
            }

        }

        if (treasure == Treasure.PierceShot)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_2 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox2").SetActive(false);
                GameObject.Find("TreasureBox2After").transform.Find("TreasureBox2_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "°üÅë¼¦ ½ºÅ³À» È¹µæÇÏ¿´½À´Ï´Ù!";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("PierceShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_2 = true;
                DataManager.instance.datas.stage1ItemBox2 = true;
                DataManager.instance.DataSave();
            }

        }

        if (treasure == Treasure.Money)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_3 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox3").SetActive(false);
                GameObject.Find("TreasureBox3After").transform.Find("TreasureBox3_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                //tresaureMSG.text = "¿µÈ¥ Á¶°¢ 30°³¸¦ È¹µæÇÏ¿´½À´Ï´Ù!";
                //CanvasGroupOn(treasureBox);
                GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul += 30;
                treasure_3 = true;
                DataManager.instance.datas.stage1ItemBox3 = true;
                DataManager.instance.DataSave();
                GameObject.Find("TutorialManager").GetComponent<TutorialManager>().UINext();
            }

        }

        if (treasure == Treasure.SideShot)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_4 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox4").SetActive(false);
                GameObject.Find("TreasureBox4After").transform.Find("TreasureBox4_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "À¯µµÅº ½ºÅ³À» È¹µæÇÏ¿´½À´Ï´Ù!";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("SideShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_4 = true;
                DataManager.instance.datas.stage1ItemBox4 = true;
                DataManager.instance.DataSave();
            }

        }
        if (treasure == Treasure.Lighting)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_5 == false)
            {
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox5").SetActive(false);
                GameObject.Find("TreasureBox5After").transform.Find("TreasureBox5_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "¶óÀÌÆ®´× ½ºÅ³À» È¹µæÇÏ¿´½À´Ï´Ù!";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("Lightning"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_5 = true;
                DataManager.instance.datas.stage1ItemBox5 = true;
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
