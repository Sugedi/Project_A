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

    //public int treasureNo = 1;
    public bool treasure_1 = false;
    public bool treasure_2 = false;

    //private void Start()
    //{
    //    if (treasure == Treasure.BuckShot1)
    //    {
    //        treasureNo = 1;
    //    }
    //    else if (treasure == Treasure.PierceShot)
    //    {
    //        treasureNo = 2;
    //    }

    //}

    public void TreasureFind()
    {
        if (treasure == Treasure.BuckShot1)
        {
            if (treasure_1 == false)
            {
                DataManager.instance.datas.skillHave.Add(Resources.Load<Skill>("BuckShot1"));
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();
                treasure_1 = true;
            }

        }

        if (treasure == Treasure.PierceShot)
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
