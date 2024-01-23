using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Treasure
{
    BuckShot1,
    PierceShot,
}

public class TreasureBox : MonoBehaviour
{
    public Treasure treasure;

    public bool treasure_1; 
    public bool treasure_2; 

    public CanvasGroup treasureBox;
    public TextMeshProUGUI tresaureMSG;

    public CanvasGroup joy;
    public CanvasGroup joy2;

    private void Start()
    {
        treasure_1 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox1;
        treasure_2 = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1ItemBox2;
    }

    public void TreasureFind()
    {
        if (treasure == Treasure.BuckShot1)
        {
            if (treasure_1 == false)
            {
                CanvasGroupOff(joy);
                CanvasGroupOff(joy2);
                Time.timeScale = 0;
                tresaureMSG.text = "벅샷 스킬을 획득하였습니다!";
                CanvasGroupOn(treasureBox);
                // System MSG 라는 캔버스를 활성화하는 것으로
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("BuckShot2"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
                treasure_1 = true; // false일 때는 열린 상자, true일 때는 닫힌 상자 
                DataManager.instance.datas.stage1ItemBox1 = true;
                DataManager.instance.DataSave();
            }

        }

        if (treasure == Treasure.PierceShot)
        {
            if (treasure_2 == false)
            {
                CanvasGroupOff(joy);
                CanvasGroupOff(joy2);
                Time.timeScale = 0;
                tresaureMSG.text = "관통샷 스킬을 획득하였습니다!";
                CanvasGroupOn(treasureBox);
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("PierceShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
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
