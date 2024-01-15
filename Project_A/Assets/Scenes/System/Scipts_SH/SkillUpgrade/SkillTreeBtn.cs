using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeBtn : MonoBehaviour
{
    public SkillBtn skillBtn;

    public List<Skill> unlockedSkillList;
    public Player player;

    public Datas datas;
    private string KeyName = "Datas";


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

    public void OnBtnClick()
    {
        switch (skillBtn)
        {
            case SkillBtn.Skill_1:

                break;

            case SkillBtn.Skill_1Up:
                
                foreach (var Skill in unlockedSkillList)
                {
                    if (Skill.skillName == "Basic")
                    {
                        Debug.Log(Skill.skillName);
                        // ���⿡ ��ư ��Ȱ��ȭ ������ �ɵ�
                    }
                }
                foreach (var Skill in datas.skillHave)
                {
                    if(Skill.skillName == "Basic")
                    {
                        CanvasGroup cg = transform.Find("Basic").GetComponent<CanvasGroup>(); //���� ���� �� ���������� ������. 
                        cg.alpha = 0;
                        cg.interactable = false;
                        cg.blocksRaycasts = false;
                    }
                }

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