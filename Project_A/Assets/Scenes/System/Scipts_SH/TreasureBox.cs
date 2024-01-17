using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Treasure
{
    BuckShot1,
    PierceShot,
}

public class TreasureBox : MonoBehaviour
{
    public Treasure treasure;

    static public int treasureNo = 1;
    static public bool treasure_1 = false;
    static public bool treasure_2 = false;

    private void Start()
    {
        if (treasure == Treasure.BuckShot1)
        {
            treasureNo = 1;
        }
        else if (treasure == Treasure.PierceShot)
        {
            treasureNo = 2;
        }

    }

    public static void TreasureFind()
    {
        if (treasureNo == 1)
        {
            if (treasure_1 == false)
            {
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("BuckShot1"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
                treasure_1 = true;
            }

        }

        if (treasureNo == 2)
        {
            if (treasure_2 == false)
            {
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("PierceShot"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
                treasure_2 = true;
            }

        }
    }
}
