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
                //int listCount = datas.skillHave.Count;
                //Debug.Log("����");
                //for (int i = 0; i < listCount; i++)
                //{
                //    Debug.Log("����");
                //    Skill skill = datas.skillHave[i];
                //    Debug.Log(skill.skillName);
                //    if (skill.skillName == "PowerUp1")
                //    {
                //        Debug.Log(i + skill.name);

                //    }
                //}

                // ������ => ����� �� �̸� ���ư��� ��, �̸��� ���о �ߺ��� ����
                // �̸� ����� ���� ���� �������� �۵���.
                List<Skill> skillList = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
                
                foreach (Skill skill in skillList )
                {
                    Skill powerUp1 = Resources.Load<Skill>("PowerUp1");
                    Skill powerUp2 = Resources.Load<Skill>("PowerUp2");
                    Skill powerUp3 = Resources.Load<Skill>("PowerUp3");
                    Skill powerUp4 = Resources.Load<Skill>("PowerUp4");

                    Debug.Log(skill.skillName);

                    if (skill.skillName == "PowerUp1")
                    {
                        DataManager.instance.datas.skillHave.Add(powerUp2);
                        DataManager.instance.datas.skillHave.Remove(powerUp1);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 300;
                        Debug.Log("2");
                        powerSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "PowerUp2")
                    {
                        DataManager.instance.datas.skillHave.Add(powerUp3);
                        DataManager.instance.datas.skillHave.Remove(powerUp2);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 500;
                        Debug.Log("3");
                        powerSkillCheck += 1;
                        break;
                    }
                    else if (skill.skillName == "PowerUp3")
                    {
                        DataManager.instance.datas.skillHave.Add(powerUp4);
                        DataManager.instance.datas.skillHave.Remove(powerUp3);
                        DataManager.instance.DataSave();
                        GameObject.Find("Player").GetComponent<Player>().SkillGet();
                        // GameObject.Find("Player").GetComponent<Player>().gem -= 700;
                        Debug.Log("3");
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

            case SkillBtn.Skill_1:

                // ����� ������ ��ų ����â�� �ٲٴ� �� TMPro �̿��ؼ� ���� �ٲ� ��
                // ��ȭ ��ư�� �ش� ��ų�ε� �ٲ����. �̰� �� ��ưڴµ�?

                break;

            case SkillBtn.Skill_1Up:

                Skill skillName = Resources.Load<Skill>("Basic");      // "Prefabs/MyPrefab" ���ҽ� ���� ��� "Resources/Skills/��ų �̸�" ���� �����ϸ� �� ��
                unlockedSkillList.Add(skillName);
                DataManager.instance.datas.skillHave.Add(skillName);
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet(); // Ŀ���� Ŭ������ ����, �ҷ����� �Ϸ��� �ش� ���� easy save 3 manager�� �־�� �ϴ� ��, ���� ���� �ߴ�.


                // ĳ���Ϳ� ���� �� ���� datas���� ���� �� Ȯ��
                // ĳ���Ϳ� ���� �־�� �ҵ�
                // �߰������� ���� ���� ������ ��� ��ų ������ �߰��Ǵϱ�
                // if �� bool ������ 1ȸ ���� �ΰ�, �ʱ�ȭ���� �ǵ�����      +      ��ȭ�ϸ�, ��ư ��Ȱ��ȭ ��Ű�� �ΰ� �ϸ� �� �� �ϴ�.
                // ������, ��ų �ʱ�ȭ ��ư�� �־�� �ϳ�;;

                // ---
                // ������, ���� ���� Ŭ������ �ҷ������� ���ϳ�? ���� �и� �ذ���� �ִ� �̰�. ���� �� �� �� �� ���� ���µ� ����� �� ���̾�.
                // ---

                break;

            case SkillBtn.Skill_2:
                break;
            case SkillBtn.Skill_2Up:
                break;
            case SkillBtn.Skill_3:
                break;
            case SkillBtn.Skill_3Up:
                break;
            case SkillBtn.Skill_4:
                break;
            case SkillBtn.Skill_4Up:
                break;
            case SkillBtn.Skill_5:
                break;
            case SkillBtn.Skill_5Up:
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

        }

        // 1.��ų�� ������Ʈ�� ���� ��ų�� �̸��� ��� �ҷ���, skill + n ���� �������� �ҷ�����?
        // 2.�׳� �밡�ٷ� ��ư ��� �ҷ�����? �׷� ��ų�� �������� ��ī�� -������ ���е�

    }
}