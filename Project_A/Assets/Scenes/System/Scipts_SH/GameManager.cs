using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SkillLevelUp()
    {
        DataManager.instance.datas.skill_1LV += 1;
    }
}
