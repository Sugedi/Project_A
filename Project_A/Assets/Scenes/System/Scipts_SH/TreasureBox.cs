using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    public Image image;
    public Sprite imageAttack;

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

            if (treasure_4 == false)
            {
                image.sprite = imageAttack;
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox4").SetActive(false);
                GameObject.Find("TreasureBox4After").transform.Find("TreasureBox4_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "벅샷 스킬을 획득하였습니다!\n기본 공격이 세 갈래로 발사됩니다.";
                CanvasGroupOn(treasureBox);
                // System MSG 라는 캔버스를 활성화하는 것으로
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("BuckShot2"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_4 = true; // false일 때는 열린 상자, true일 때는 닫힌 상자 
                DataManager.instance.datas.stage1ItemBox4 = true;
                DataManager.instance.DataSave();
            }

        }

        if (treasure == Treasure.PierceShot)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_1 == false)
            {
                image.sprite = imageAttack;
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox1").SetActive(false);
                GameObject.Find("TreasureBox1After").transform.Find("TreasureBox1_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "관통샷 스킬을 획득하였습니다!\n기본 공격이 몬스터를 관통해서 지속됩니다.";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("PierceShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_1 = true;
                DataManager.instance.datas.stage1ItemBox1 = true;
                DataManager.instance.DataSave();
            }

        }

        if (treasure == Treasure.Money)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_3 == false)
            {
                image.sprite = imageAttack;
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox3").SetActive(false);
                GameObject.Find("TreasureBox3After").transform.Find("TreasureBox3_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                //tresaureMSG.text = "영혼 조각 30개를 획득하였습니다!";
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

            if (treasure_5 == false)
            {
                image.sprite = imageAttack;
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox5").SetActive(false);
                GameObject.Find("TreasureBox5After").transform.Find("TreasureBox5_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "유도탄 스킬을 획득하였습니다!\n작은 유도탄들을 추가로 발사합니다.";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("SideShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_5 = true;
                DataManager.instance.datas.stage1ItemBox5 = true;
                DataManager.instance.DataSave();
            }

        }
        if (treasure == Treasure.Lighting)
        {
            SoundManager.instance.PlayAudio("BoxOpen1", "SE");

            if (treasure_2 == false)
            {
                image.sprite = imageAttack;
                CanvasGroupOff(joy);
                GameObject.Find("TreasureBox2").SetActive(false);
                GameObject.Find("TreasureBox2After").transform.Find("TreasureBox2_After").gameObject.SetActive(true);
                Time.timeScale = 0;
                tresaureMSG.text = "라이트닝 스킬을 획득하였습니다!\n총알이 몬스터에 닿으면 번개가 내려쳐 추가 피해를 입힙니다.";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("Lightning"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().BoxGet();
                treasure_2 = true;
                DataManager.instance.datas.stage1ItemBox2 = true;
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
