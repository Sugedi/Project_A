using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeBtn : MonoBehaviour
{
    public SkillBtn skillBtn;

    public List<Skill> unlockedSkillList;
    public Player player;

    public Datas datas;
    private string KeyName = "Datas";

    public CanvasGroup powerInfoCanvas;
    public CanvasGroup ammoInfoCanvas;
    public CanvasGroup speedInfoCanvas;
    public CanvasGroup reloadInfoCanvas;


    // Start is called before the first frame update
    void Start()
    {
        // foreach�� ������ ����Ʈ�� ��ư�� ������, ��ư ���� ȸ������
        // ù ���� ȭ���� ���� ����ȭ������

        // ������ �Ŵ������� �ٷ� �ҷ����� ��� - ��������δ� �̰� ������ �� �ۿ� ���� - �ٵ� ����Ʈ�� ��ų �ֵ带 ���ؼ� ����
        ES3.LoadInto(KeyName, datas);
        unlockedSkillList = datas.skillHave;

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
                if(powerInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(powerInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                }
                break;

            case SkillBtn.Ammo:
                if(ammoInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(ammoInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                }
                break;

            case SkillBtn.Speed:
                if(speedInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(speedInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);
                    CanvasGroupOff(reloadInfoCanvas);
                }
                break;

            case SkillBtn.Reload:
                if(reloadInfoCanvas.alpha == 1)
                {

                }
                else
                {
                    CanvasGroupOn(reloadInfoCanvas);
                    CanvasGroupOff(ammoInfoCanvas);
                    CanvasGroupOff(speedInfoCanvas);
                    CanvasGroupOff(powerInfoCanvas);
                }
                break;


            case SkillBtn.PowerUp:

                int powerSkillCheck = 0;
                List<Skill> powerskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
                
                foreach (Skill skill in powerskillList )
                {
                    Skill powerUp2 = Resources.Load<Skill>("PowerUp2");
                    Skill powerUp3 = Resources.Load<Skill>("PowerUp3");
                    Skill powerUp4 = Resources.Load<Skill>("PowerUp4");

                    if (skill.skillName == "PowerUp1")
                    {
                        powerskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(powerUp2);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 300;
                        powerSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "PowerUp2")
                    {
                        powerskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(powerUp3);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 500;
                        powerSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "PowerUp3")
                    {
                        powerskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(powerUp4);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 700;
                        powerSkillCheck += 1;
                        break;
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
                    Skill powerUp1 = Resources.Load<Skill>("PowerUp1");
                    Debug.Log(powerUp1);
                    DataManager.instance.datas.skillHave.Add(powerUp1);
                    DataManager.instance.DataSave();
                    GameObject.Find("Player").GetComponent<Player>().SkillGet();
                    break;
                }
                break;

            case SkillBtn.AmmoUp:

                int ammoSkillCheck = 0;
                List<Skill> ammoskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                foreach (Skill skill in ammoskillList)
                {
                    Skill ammoUp2 = Resources.Load<Skill>("AmmoUp2");
                    Skill ammoUp3 = Resources.Load<Skill>("AmmoUp3");
                    Skill ammoUp4 = Resources.Load<Skill>("AmmoUp4");

                    if (skill.skillName == "AmmoUp1")
                    {
                        ammoskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(ammoUp2);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 300;
                        ammoSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "AmmoUp2")
                    {
                        ammoskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(ammoUp3);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 500;
                        ammoSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "AmmoUp3")
                    {
                        ammoskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(ammoUp4);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 700;
                        ammoSkillCheck += 1;
                        break;
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
                    Skill ammoUp1 = Resources.Load<Skill>("AmmoUp1");
                    Debug.Log(ammoUp1);
                    DataManager.instance.datas.skillHave.Add(ammoUp1);
                    DataManager.instance.DataSave();
                    GameObject.Find("Player").GetComponent<Player>().SkillGet();
                    break;
                }
                break;

            case SkillBtn.ReloadUp:

                int reloadSkillCheck = 0;
                List<Skill> reloadskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                foreach (Skill skill in reloadskillList)
                {
                    Skill reloadUp2 = Resources.Load<Skill>("ReloadUp2");
                    Skill reloadUp3 = Resources.Load<Skill>("ReloadUp3");
                    Skill reloadUp4 = Resources.Load<Skill>("ReloadUp4");

                    Debug.Log(skill.skillName);

                    if (skill.skillName == "ReloadUp1")
                    {
                        reloadskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(reloadUp2);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 300;
                        reloadSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "ReloadUp2")
                    {
                        reloadskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(reloadUp3);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 500;
                        reloadSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "ReloadUp3")
                    {
                        reloadskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(reloadUp4);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 700;
                        reloadSkillCheck += 1;
                        break;
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
                    Skill reloadUp1 = Resources.Load<Skill>("ReloadUp1");
                    DataManager.instance.datas.skillHave.Add(reloadUp1);
                    DataManager.instance.DataSave();
                    GameObject.Find("Player").GetComponent<Player>().SkillGet();
                    break;
                }
                break;

            case SkillBtn.SpeedUp:

                int speedSkillCheck = 0;
                List<Skill> speedskillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;

                foreach (Skill skill in speedskillList)
                {
                    Skill speedUp2 = Resources.Load<Skill>("SpeedUp2");
                    Skill speedUp3 = Resources.Load<Skill>("SpeedUp3");
                    Skill speedUp4 = Resources.Load<Skill>("SpeedUp4");

                    Debug.Log(skill.skillName);

                    if (skill.skillName == "SpeedUp1")
                    {
                        speedskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(speedUp2);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 300;
                        speedSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "SpeedUp2")
                    {
                        speedskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(speedUp3);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 500;
                        speedSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "SpeedUp3")
                    {
                        speedskillList.Remove(skill);
                        DataManager.instance.datas.skillHave.Add(speedUp4);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 700;
                        speedSkillCheck += 1;
                        break;
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
                    Skill speedUp1 = Resources.Load<Skill>("SpeedUp1");
                    DataManager.instance.datas.skillHave.Add(speedUp1);
                    DataManager.instance.DataSave();
                    GameObject.Find("Player").GetComponent<Player>().SkillGet();
                    break;
                }
                break;

            case SkillBtn.Quit:
                CanvasGroup skillTree = GameObject.Find("SkillCanvas").GetComponent<CanvasGroup>();
                skillTree.alpha = 0;
                skillTree.interactable = false;
                skillTree.blocksRaycasts = false;

                CanvasGroup MainUI = GameObject.Find("MainCanvas").GetComponent<CanvasGroup>();
                MainUI.alpha = 1;
                MainUI.interactable = true;
                MainUI.blocksRaycasts = true;

                break;

            case SkillBtn.Reset:

                List<Skill> resetillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
                resetillList.Clear();
                Skill basic = Resources.Load<Skill>("Basic");
                DataManager.instance.datas.skillHave.Add(basic);
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet();

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