using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeBtn : MonoBehaviour
{
    public SkillBtn skillBtn;

    public Datas datas;
    private string KeyName = "Datas";

    public CanvasGroup powerInfoCanvas;
    public CanvasGroup ammoInfoCanvas;
    public CanvasGroup speedInfoCanvas;
    public CanvasGroup reloadInfoCanvas;
    public CanvasGroup healthInfoCanvas;

    public CanvasGroup powerUpEndCanvas;
    public CanvasGroup ammoUpEndCanvas;
    public CanvasGroup speedUpEndCanvas;
    public CanvasGroup reloadUpEndCanvas;
    public CanvasGroup healthUpEndCanvas;

    public CanvasGroup powerGemCanvas;
    public CanvasGroup ammoGemCanvas;
    public CanvasGroup speedGemCanvas;
    public CanvasGroup reloadGemCanvas;
    public CanvasGroup healthGemCanvas;

    public CanvasGroup powerBtnCanvas;
    public CanvasGroup ammoBtnCanvas;
    public CanvasGroup speedBtnCanvas;
    public CanvasGroup reloadBtnCanvas;
    public CanvasGroup healthBtnCanvas;

    public TextMeshProUGUI powerInfo;
    public TextMeshProUGUI ammoInfo;
    public TextMeshProUGUI speedInfo;
    public TextMeshProUGUI reloadInfo;
    public TextMeshProUGUI healthInfo;
    public TextMeshProUGUI gem;

    public TextMeshProUGUI powerLevel;
    public TextMeshProUGUI ammoLevel;
    public TextMeshProUGUI speedLevel;
    public TextMeshProUGUI reloadLevel;
    public TextMeshProUGUI healthLevel;

    public int LV1 = 1;
    public int LV2 = 2;
    public int LV3 = 3;
    public int LV4 = 4;

    public int soul;

    public CanvasGroup joy;

    void LevelNotice(TextMeshProUGUI text1, int level)
    {
        text1.text = $"LV.{level}";
    }
    // Start is called before the first frame update
    void Start()
    {
        // foreach�� ������ ����Ʈ�� ��ư�� ������, ��ư ���� ȸ������
        // ù ���� ȭ���� ���� ����ȭ������

        // ������ �Ŵ������� �ٷ� �ҷ����� ��� - ��������δ� �̰� ������ �� �ۿ� ���� - �ٵ� ����Ʈ�� ��ų �ֵ带 ���ؼ� ����
        ES3.LoadInto(KeyName, datas);
        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
        gem.text = $"{soul}";

        // ĳ���Ͱ� ������ ��ų�� �ҷ����� ��� 
        // �ƴ� AddSkill ���� ���� �ǵ� ~~~
        //GameObject skillLoad = GameObject.Find("Player_SH");
        //unlockedSkillList = skillLoad.GetComponent<Move_SH>().activeSkills;

        // ��ư ��Ȱ��ȭ �ϴ� �� -> �̰� ��ȭ�� �����ؾ� ��.
        foreach (var Skill in datas.skillHave)
        {
            if (Skill.skillName == "Basic")
            {
                //Button cg = transform.Find("Basic").GetComponent<Button>(); //���� ���� �� ���������� ������. 
                // ���� �ڽ��� ���� ��ư�� �־ ������ ���� ��.���� ���� �� ���� �Ŷ�
                //cg.interactable = false;
            }
        }

        List<Skill> powerList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        foreach (Skill skill in powerList)
        {

            if (skill.skillName == "PowerUp1")
            {
                powerInfo.text = "50";
                LevelNotice(powerLevel, LV1);
            }
            else if (skill.skillName == "PowerUp2")
            {
                powerInfo.text = "70";
                LevelNotice(powerLevel, LV2);
            }
            else if (skill.skillName == "PowerUp3")
            {
                powerInfo.text = "90";
                LevelNotice(powerLevel, LV3);
            }
            else if (skill.skillName == "PowerUp4")
            {
                powerInfo.text = "";
                LevelNotice(powerLevel, LV4);
                CanvasGroupOff(powerGemCanvas);
                CanvasGroupOff(powerBtnCanvas);
                CanvasGroupOn(powerUpEndCanvas);
            }
        }
        List<Skill> ammoList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        foreach (Skill skill in ammoList)
        {

            if (skill.skillName == "AmmoUp1")
            {
                LevelNotice(ammoLevel, LV1);
            }
            else if (skill.skillName == "AmmoUp2")
            {
                LevelNotice(ammoLevel, LV2);
            }
            else if (skill.skillName == "AmmoUp3")
            {
                LevelNotice(ammoLevel, LV3);
            }
            else if (skill.skillName == "AmmoUp4")
            {
                LevelNotice(ammoLevel, LV4);
            }
        }
        List<Skill> speedList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        foreach (Skill skill in speedList)
        {

            if (skill.skillName == "SpeedUp1")
            {
                LevelNotice(speedLevel, LV1);
            }
            else if (skill.skillName == "SpeedUp2")
            {
                LevelNotice(speedLevel, LV2);
            }
            else if (skill.skillName == "SpeedUp3")
            {
                LevelNotice(speedLevel, LV3);
            }
            else if (skill.skillName == "SpeedUp4")
            {
                LevelNotice(speedLevel, LV4);
            }
        }
        List<Skill> reloadList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        foreach (Skill skill in reloadList)
        {

            if (skill.skillName == "ReloadUp1")
            {
                LevelNotice(reloadLevel, LV1);
            }
            else if (skill.skillName == "ReloadUp2")
            {
                LevelNotice(reloadLevel, LV2);
            }
            else if (skill.skillName == "ReloadUp3")
            {
                LevelNotice(reloadLevel, LV3);
            }
            else if (skill.skillName == "ReloadUp4")
            {
                LevelNotice(reloadLevel, LV4);
            }
        }
        int charHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;

        if (charHealth == 130)
        {
            LevelNotice(healthLevel, LV1);
        }
        else if (charHealth == 160)
        {
            LevelNotice(healthLevel, LV2);
        }
        else if (charHealth == 200)
        {
            LevelNotice(healthLevel, LV3);
        }
        else if (charHealth == 240)
        {
            LevelNotice(healthLevel, LV4);
        }


    }

    void UnlockedSkillColor()
    {
        // �̰� �貸�� �ٸ� ��ũ��Ʈ�� ����, Skills �гο� �޾ƾ� ��.
        foreach (var Skill in datas.skillHave)
        {
            Image cg = transform.Find(Skill.skillName).GetComponent<Image>(); //���� ���� �� ���������� ������. 
            //cg.color = ();
        }
    }


    void SkillLV()
    {
        //��ų ���ϴܿ� �ؽ�Ʈ�޽����η� 0, 1, 2, 3, 4 �� ��ų ���� ǥ���� ��
        //��ȭ �Ǵ� �ʱ�ȭ �ÿ� �ش� ������ 0���� ����
    }
    /*
    public void UnlockSkill(Skill skill)
    {
        unlockedSkillList.Add(skill);
    }

    public bool IsSkillUnlocked(Skill skill)
    {
        return unlockedSkillList.Contains(skill);
    }
    */

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

    public void OnBtnClick()
    {


        switch (skillBtn)
        {
            case SkillBtn.Power:

                List<Skill> powerList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                if (powerInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(powerInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                    CanvasGroupOff(healthInfoCanvas);


                    foreach (Skill skill in powerList)
                    {

                        if (skill.skillName == "PowerUp1")
                        {
                            powerInfo.text = "50";
                            LevelNotice(powerLevel, LV1);
                            break;
                        }
                        else if (skill.skillName == "PowerUp2")
                        {
                            powerInfo.text = "70";
                            LevelNotice(powerLevel, LV2);
                            break;
                        }
                        else if (skill.skillName == "PowerUp3")
                        {
                            powerInfo.text = "90";
                            LevelNotice(powerLevel, LV3);
                            break;
                        }
                        else if (skill.skillName == "PowerUp4")
                        {
                            powerInfo.text = "";
                            LevelNotice(powerLevel, LV4);
                            CanvasGroupOff(powerGemCanvas);
                            CanvasGroupOff(powerBtnCanvas);
                            CanvasGroupOn(powerUpEndCanvas);
                            break;
                        }
                    }
                }
                break;

            case SkillBtn.Ammo:

                List<Skill> ammoList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                if (ammoInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(ammoInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                    CanvasGroupOff(healthInfoCanvas);

                    foreach (Skill skill in ammoList)
                    {

                        if (skill.skillName == "AmmoUp1")
                        {
                            ammoInfo.text = "50";
                            LevelNotice(ammoLevel, LV1);
                            break;
                        }
                        else if (skill.skillName == "AmmoUp2")
                        {
                            ammoInfo.text = "70";
                            LevelNotice(ammoLevel, LV2);
                            break;
                        }
                        else if (skill.skillName == "AmmoUp3")
                        {
                            ammoInfo.text = "90";
                            LevelNotice(ammoLevel, LV3);
                            break;
                        }
                        else if (skill.skillName == "AmmoUp4")
                        {
                            ammoInfo.text = "";
                            LevelNotice(ammoLevel, LV4);
                            CanvasGroupOff(ammoGemCanvas);
                            CanvasGroupOff(ammoBtnCanvas);
                            CanvasGroupOn(ammoUpEndCanvas);
                            break;
                        }
                    }
                }
                break;

            case SkillBtn.Speed:

                List<Skill> speedList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                if (speedInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(speedInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                    CanvasGroupOff(healthInfoCanvas);


                    foreach (Skill skill in speedList)
                    {

                        if (skill.skillName == "SpeedUp1")
                        {
                            speedInfo.text = "50";
                            LevelNotice(speedLevel, LV1);
                            break;
                        }
                        else if (skill.skillName == "SpeedUp2")
                        {
                            speedInfo.text = "70";
                            LevelNotice(speedLevel, LV2);
                            break;
                        }
                        else if (skill.skillName == "SpeedUp3")
                        {
                            speedInfo.text = "90";
                            LevelNotice(speedLevel, LV3);
                            break;
                        }
                        else if (skill.skillName == "SpeedUp4")
                        {
                            speedInfo.text = "";
                            LevelNotice(speedLevel, LV4);
                            CanvasGroupOff(speedGemCanvas);
                            CanvasGroupOff(speedBtnCanvas);
                            CanvasGroupOn(speedUpEndCanvas);
                            break;
                        }
                    }
                }
                break;

            case SkillBtn.Reload:

                List<Skill> reloadList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                if (reloadInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(reloadInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);
                    CanvasGroupOff(healthInfoCanvas);


                    foreach (Skill skill in reloadList)
                    {

                        if (skill.skillName == "ReloadUp1")
                        {
                            reloadInfo.text = "50";
                            LevelNotice(reloadLevel, LV1);
                            break;
                        }
                        else if (skill.skillName == "ReloadUp2")
                        {
                            reloadInfo.text = "70";
                            LevelNotice(reloadLevel, LV2);
                            break;
                        }
                        else if (skill.skillName == "ReloadUp3")
                        {
                            reloadInfo.text = "90";
                            LevelNotice(reloadLevel, LV3);
                            break;
                        }
                        else if (skill.skillName == "ReloadUp4")
                        {
                            reloadInfo.text = "";
                            LevelNotice(reloadLevel, LV4);
                            CanvasGroupOff(reloadGemCanvas);
                            CanvasGroupOff(reloadBtnCanvas);
                            CanvasGroupOn(reloadUpEndCanvas);
                            break;
                        }
                    }
                }
                break;
            case SkillBtn.Health:

                int nowHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
                if (healthInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(healthInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);

                    if (nowHealth == 130)
                    {
                        healthInfo.text = "50";
                        LevelNotice(healthLevel, LV1);
                    }
                    else if (nowHealth == 160)
                    {
                        healthInfo.text = "70";
                        LevelNotice(healthLevel, LV2);
                    }
                    else if (nowHealth == 200)
                    {
                        healthInfo.text = "90";
                        LevelNotice(healthLevel, LV3);
                    }
                    else if (nowHealth == 240)
                    {
                        healthInfo.text = "";
                        LevelNotice(healthLevel, LV4);
                        CanvasGroupOff(healthGemCanvas);
                        CanvasGroupOff(healthBtnCanvas);
                        CanvasGroupOn(healthUpEndCanvas);
                    }
                }
                break;


            case SkillBtn.PowerUp:

                int powerSkillCheck = 0;
                List<Skill> powerskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
                
                foreach (Skill skill in powerskillList )
                {
                    soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                    Skill powerUp2 = Resources.Load<Skill>("PowerUp2");
                    Skill powerUp3 = Resources.Load<Skill>("PowerUp3");
                    Skill powerUp4 = Resources.Load<Skill>("PowerUp4");

                    if (skill.skillName == "PowerUp1")
                    {
                        if (soul < 50)
                        {
                            powerSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            powerskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(powerUp2);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 50;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            powerInfo.text = "70";
                            LevelNotice(powerLevel, LV2);
                            powerSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "PowerUp2")
                    {
                        if (soul < 70)
                        {
                            powerSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            powerskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(powerUp3);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 70;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            powerInfo.text = "90";
                            LevelNotice(powerLevel, LV3);
                            powerSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "PowerUp3")
                    {
                        if (soul < 90)
                        {
                            powerSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            powerskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(powerUp4);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 90;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            powerInfo.text = "";
                            LevelNotice(powerLevel, LV4);
                            CanvasGroupOff(powerGemCanvas);
                            CanvasGroupOff(powerBtnCanvas);
                            CanvasGroupOn(powerUpEndCanvas);
                            powerSkillCheck += 1;
                            break;
                        }
                    }
                    else if(skill.skillName == "PowerUp4")
                    {
                        Debug.Log("�� �̻� ��ȭ �Ұ�");
                        powerSkillCheck += 1;
                        break;
                    }
                    
                }
                if (powerSkillCheck == 0)
                {
                    if (soul < 30)
                    {
                        break;
                    }
                    else
                    {
                        Skill powerUp1 = Resources.Load<Skill>("PowerUp1");
                        Debug.Log(powerUp1);
                        DataManager.instance.datas.skillHave.Add(powerUp1);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 30;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        powerInfo.text = "50";
                        LevelNotice(powerLevel, LV1);
                        break;
                    }
                }
                break;

            case SkillBtn.AmmoUp:

                int ammoSkillCheck = 0;
                List<Skill> ammoskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                foreach (Skill skill in ammoskillList)
                {
                    soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                    Skill ammoUp2 = Resources.Load<Skill>("AmmoUp2");
                    Skill ammoUp3 = Resources.Load<Skill>("AmmoUp3");
                    Skill ammoUp4 = Resources.Load<Skill>("AmmoUp4");

                    if (skill.skillName == "AmmoUp1")
                    {

                        if (soul < 50)
                        {
                            ammoSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            ammoskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(ammoUp2);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 50;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            ammoInfo.text = "70";
                            LevelNotice(ammoLevel, LV2);
                            ammoSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "AmmoUp2")
                    {
                        if (soul < 70)
                        {
                            ammoSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            ammoskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(ammoUp3);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 70;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            ammoInfo.text = "90";
                            LevelNotice(ammoLevel, LV3);
                            ammoSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "AmmoUp3")
                    {
                        if (soul < 90)
                        {
                            ammoSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            ammoskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(ammoUp4);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 90;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            ammoInfo.text = "";
                            LevelNotice(ammoLevel, LV4);
                            CanvasGroupOff(ammoGemCanvas);
                            CanvasGroupOff(ammoBtnCanvas);
                            CanvasGroupOn(ammoUpEndCanvas);
                            ammoSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "AmmoUp4")
                    {
                        Debug.Log("�� �̻� ��ȭ �Ұ�");
                        ammoSkillCheck += 1;
                        break;
                    }

                }
                if (ammoSkillCheck == 0)
                {
                    if (soul < 30)
                    {
                        break;
                    }
                    else
                    {
                        Skill ammoUp1 = Resources.Load<Skill>("AmmoUp1");
                        Debug.Log(ammoUp1);
                        DataManager.instance.datas.skillHave.Add(ammoUp1);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 30;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        ammoInfo.text = "50";
                        LevelNotice(ammoLevel, LV1);
                        break;
                    }
                }
                break;

            case SkillBtn.ReloadUp:

                int reloadSkillCheck = 0;
                List<Skill> reloadskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                foreach (Skill skill in reloadskillList)
                {
                    soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                    Skill reloadUp2 = Resources.Load<Skill>("ReloadUp2");
                    Skill reloadUp3 = Resources.Load<Skill>("ReloadUp3");
                    Skill reloadUp4 = Resources.Load<Skill>("ReloadUp4");

                    Debug.Log(skill.skillName);

                    if (skill.skillName == "ReloadUp1")
                    {
                        if (soul < 50)
                        {
                            reloadSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            reloadskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(reloadUp2);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 50;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            reloadInfo.text = "70";
                            LevelNotice(reloadLevel, LV2);
                            reloadSkillCheck += 1;
                            break;
                        }
                        
                    }
                    else if (skill.skillName == "ReloadUp2")
                    {
                        if (soul < 70)
                        {
                            reloadSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            reloadskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(reloadUp3);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 70;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            reloadInfo.text = "90";
                            LevelNotice(reloadLevel, LV3);
                            reloadSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "ReloadUp3")
                    {
                        if (soul < 90)
                        {
                            reloadSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            reloadskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(reloadUp4);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 90;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            reloadInfo.text = "";
                            LevelNotice(reloadLevel, LV4);
                            CanvasGroupOff(reloadGemCanvas);
                            CanvasGroupOff(reloadBtnCanvas);
                            CanvasGroupOn(reloadUpEndCanvas);

                            reloadSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "ReloadUp4")
                    {
                        Debug.Log("�� �̻� ��ȭ �Ұ�");
                        reloadSkillCheck += 1;
                        break;
                    }

                }
                if (reloadSkillCheck == 0)
                {
                    if (soul < 30)
                    {
                        break;
                    }
                    else
                    {
                        Skill reloadUp1 = Resources.Load<Skill>("ReloadUp1");
                        DataManager.instance.datas.skillHave.Add(reloadUp1);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 30;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        reloadInfo.text = "50";
                        LevelNotice(reloadLevel, LV1);
                        break;
                    }
                }
                break;

            case SkillBtn.SpeedUp:

                int speedSkillCheck = 0;
                List<Skill> speedskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                foreach (Skill skill in speedskillList)
                {
                    soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                    Skill speedUp2 = Resources.Load<Skill>("SpeedUp2");
                    Skill speedUp3 = Resources.Load<Skill>("SpeedUp3");
                    Skill speedUp4 = Resources.Load<Skill>("SpeedUp4");

                    Debug.Log(skill.skillName);

                    if (skill.skillName == "SpeedUp1")
                    {
                        //���⼭ ���� ���� ������ �𸣰ڳ�? ���� �κи� �� �����ص� ����ع�����
                        if (soul < 50)
                        {
                            speedSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            speedskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(speedUp2);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 50;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            speedInfo.text = "70";
                            LevelNotice(speedLevel, LV2);
                            speedSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "SpeedUp2")
                    {
                        if (soul < 70)
                        {
                            speedSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            speedskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(speedUp3);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 70;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            speedInfo.text = "90";
                            LevelNotice(speedLevel, LV3);
                            speedSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "SpeedUp3")
                    {
                        if (soul < 90)
                        {
                            speedSkillCheck += 1;
                            break;
                        }
                        else
                        {
                            speedskillList.Remove(skill);
                            DataManager.instance.datas.skillHave.Add(speedUp4);
                            DataManager.instance.DataSave();
                            GameObject.Find("Player").GetComponent<Player>().SkillGet();
                            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 90;
                            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                            gem.text = $"{soul}";
                            DataManager.instance.DataSave();
                            speedInfo.text = "";
                            LevelNotice(speedLevel, LV4);
                            CanvasGroupOff(speedGemCanvas);
                            CanvasGroupOff(speedBtnCanvas);
                            CanvasGroupOn(speedUpEndCanvas);
                            speedSkillCheck += 1;
                            break;
                        }
                    }
                    else if (skill.skillName == "SpeedUp4")
                    {
                        Debug.Log("�� �̻� ��ȭ �Ұ�");
                        speedSkillCheck += 1;
                        break;
                    }

                }
                if (speedSkillCheck == 0)
                {
                    if (soul < 30)
                    {
                        break;
                    }
                    else
                    {
                        Skill speedUp1 = Resources.Load<Skill>("SpeedUp1");
                        DataManager.instance.datas.skillHave.Add(speedUp1);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 30;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        speedInfo.text = "50";
                        LevelNotice(speedLevel, LV1);
                        break;
                    }
                }
                break;

            case SkillBtn.HealthUp:

                int curHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
                soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;

                if (curHealth == 100)
                {
                    if(soul < 30)
                    {

                    }
                    else
                    {
                        DataManager.instance.datas.maxHP = 130;
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 30;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        healthInfo.text = "50";
                        LevelNotice(healthLevel, LV1);
                    }
                }
                else if (curHealth == 130)
                {
                    if(soul < 50)
                    {

                    }
                    else
                    {
                        DataManager.instance.datas.maxHP = 160;
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 50;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        healthInfo.text = "70";
                        LevelNotice(healthLevel, LV2);
                    }
                }
                else if (curHealth == 160)
                {
                    if(soul < 70)
                    {

                    }
                    else
                    {
                        DataManager.instance.datas.maxHP = 200;
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 70;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        healthInfo.text = "90";
                        LevelNotice(healthLevel, LV3);
                    }
                }
                else if (curHealth == 200)
                {
                    if(soul < 90)
                    {

                    }
                    else
                    {
                        DataManager.instance.datas.maxHP = 240;
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 90;
                        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
                        gem.text = $"{soul}";
                        DataManager.instance.DataSave();
                        healthInfo.text = "";
                        LevelNotice(healthLevel, LV4);
                        CanvasGroupOff(healthGemCanvas);
                        CanvasGroupOff(healthBtnCanvas);
                        CanvasGroupOn(healthUpEndCanvas);
                    }
                    
                }
                break;
            // ��ų��
            //int healthSkillCheck = 0;
            //List<Skill> healthskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

            //foreach (Skill skill in healthskillList)
            //{
            //    soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
            //    Skill healthUp2 = Resources.Load<Skill>("HealthUp2");
            //    Skill healthUp3 = Resources.Load<Skill>("HealthUp3");
            //    Skill healthUp4 = Resources.Load<Skill>("HealthUp4");

            //    Debug.Log(skill.skillName);

            //    if (skill.skillName == "HealthUp1")
            //    {
            //        if (soul < 50)
            //        {
            //            healthSkillCheck += 1;
            //            break;
            //        }
            //        else
            //        {
            //            healthskillList.Remove(skill);
            //            DataManager.instance.datas.skillHave.Add(healthUp2);
            //            DataManager.instance.DataSave();
            //            GameObject.Find("Player").GetComponent<Player>().SkillGet();
            //            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 50;
            //            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
            //            gem.text = $"��ȭ: {soul}";
            //            DataManager.instance.DataSave();
            //            healthInfo.text = "�ʿ� ��ȭ: 70";
            //            healthSkillCheck += 1;
            //            break;
            //        }
            //    }
            //    else if (skill.skillName == "HealthUp2")
            //    {
            //        if (soul < 70)
            //        {
            //            healthSkillCheck += 1;
            //            break;
            //        }
            //        else
            //        {
            //            healthskillList.Remove(skill);
            //            DataManager.instance.datas.skillHave.Add(healthUp3);
            //            DataManager.instance.DataSave();
            //            GameObject.Find("Player").GetComponent<Player>().SkillGet();
            //            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 70;
            //            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
            //            gem.text = $"��ȭ: {soul}";
            //            DataManager.instance.DataSave();
            //            healthInfo.text = "�ʿ� ��ȭ: 90";
            //            healthSkillCheck += 1;
            //            break;
            //        }
            //    }
            //    else if (skill.skillName == "HealthUp3")
            //    {
            //        if (soul < 90)
            //        {
            //            healthSkillCheck += 1;
            //            break;
            //        }
            //        else
            //        {
            //            healthskillList.Remove(skill);
            //            DataManager.instance.datas.skillHave.Add(healthUp4);
            //            DataManager.instance.DataSave();
            //            GameObject.Find("Player").GetComponent<Player>().SkillGet();
            //            GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 90;
            //            soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
            //            gem.text = $"��ȭ: {soul}";
            //            DataManager.instance.DataSave();
            //            healthInfo.text = "�ְ� ����";
            //            healthSkillCheck += 1;
            //            break;
            //        }
            //    }
            //    else if (skill.skillName == "HealthUp1")
            //    {
            //        Debug.Log("�� �̻� ��ȭ �Ұ�");
            //        healthSkillCheck += 1;
            //        break;
            //    }

            //}
            //if (healthSkillCheck == 0)
            //{
            //    if (soul < 30)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        Skill healthUp1 = Resources.Load<Skill>("HealthUp1");
            //        DataManager.instance.datas.skillHave.Add(healthUp1);
            //        DataManager.instance.DataSave();
            //        GameObject.Find("Player").GetComponent<Player>().SkillGet();
            //        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul -= 30;
            //        soul = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
            //        gem.text = $"��ȭ: {soul}";
            //        DataManager.instance.DataSave();
            //        healthInfo.text = "�ʿ� ��ȭ: 50";
            //        break;
            //    }
            //}
            //break;

            case SkillBtn.Quit:
                CanvasGroup skillTree = GameObject.Find("SkillCanvas").GetComponent<CanvasGroup>();
                skillTree.alpha = 0;
                skillTree.interactable = false;
                skillTree.blocksRaycasts = false;

                CanvasGroup MainUI = GameObject.Find("StageUI").GetComponent<CanvasGroup>();
                MainUI.alpha = 1;
                MainUI.interactable = true;
                MainUI.blocksRaycasts = true;
                CanvasGroupOn(joy);
                DataManager.instance.DataSave();
                DataManager.instance.DataLoad();

                break;

            case SkillBtn.Reset:

                // ���� ���� ����
                List<Skill> resetillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
                List<Skill> tempList = new List<Skill>();

                Skill buckShot1 = Resources.Load<Skill>("BuckShot1");
                Skill pierceShot = Resources.Load<Skill>("PierceShot");
                Skill boomShot1 = Resources.Load<Skill>("BoomShot1");
                Skill sideShot = Resources.Load<Skill>("SideShot");

                foreach (Skill skill in resetillList)
                {
                    if (skill.skillName == "BuckShot1")
                    {
                        tempList.Add(buckShot1);
                    }
                    else if (skill.skillName == "PierceShot")
                    {
                        tempList.Add(pierceShot);
                    }
                    else if (skill.skillName == "BoomShot1")
                    {
                        tempList.Add(boomShot1);
                    }
                    else if (skill.skillName == "SideShot")
                    {
                        tempList.Add(sideShot);
                    }
                }
                resetillList.Clear();

                foreach (Skill skill in tempList)
                {
                    if (skill.skillName == "BuckShot1")
                    {
                        resetillList.Add(buckShot1);
                    }
                    else if (skill.skillName == "PierceShot")
                    {
                        resetillList.Add(pierceShot);
                    }
                    else if (skill.skillName == "BoomShot1")
                    {
                        resetillList.Add(boomShot1);
                    }
                    else if (skill.skillName == "SideShot")
                    {
                        resetillList.Add(sideShot);
                    }
                }
                Skill basic = Resources.Load<Skill>("Basic");
                DataManager.instance.datas.skillHave.Add(basic);
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();

                powerInfo.text = "30";
                ammoInfo.text = "30";
                speedInfo.text = "30";
                reloadInfo.text = "30";



                break;
        }

        // 1.��ų�� ������Ʈ�� ���� ��ų�� �̸��� ��� �ҷ���, skill + n ���� �������� �ҷ�����?
        // 2.�׳� �밡�ٷ� ��ư ��� �ҷ�����? �׷� ��ų�� �������� ��ī�� -������ ���е�

        // �ʳ�, �� �ٽ� ������ �߻��ϸ� ���� ��� ���⸦ ����. �׵����� ������ ��� ����. (�ϼ� ������ ���� ����)

        //Skill skillName = Resources.Load<Skill>("Basic");      // "Prefabs/MyPrefab" ���ҽ� ���� ��� "Resources/Skills/��ų �̸�" ���� �����ϸ� �� ��
        //unlockedSkillList.Add(skillName);
        //DataManager.instance.datas.skillHave.Add(skillName);
        //DataManager.instance.DataSave();
        //GameObject.Find("Player").GetComponent<Player>().SkillGet(); // Ŀ���� Ŭ������ ����, �ҷ����� �Ϸ��� �ش� ���� easy save 3 manager�� �־�� �ϴ� ��, ���� ���� �ߴ�.


        // ĳ���Ϳ� ���� �� ���� datas���� ���� �� Ȯ��
        // ĳ���Ϳ� ���� �־�� �ҵ�
        // �߰������� ���� ���� ������ ��� ��ų ������ �߰��Ǵϱ�
        // if �� bool ������ 1ȸ ���� �ΰ�, �ʱ�ȭ���� �ǵ�����      +      ��ȭ�ϸ�, ��ư ��Ȱ��ȭ ��Ű�� �ΰ� �ϸ� �� �� �ϴ�.
        // ������, ��ų �ʱ�ȭ ��ư�� �־�� �ϳ�;;

        // ---
        // ������, ���� ���� Ŭ������ �ҷ������� ���ϳ�? ���� �и� �ذ���� �ִ� �̰�. ���� �� �� �� �� ���� ���µ� ����� �� ���̾�.
        // ---
    }
}