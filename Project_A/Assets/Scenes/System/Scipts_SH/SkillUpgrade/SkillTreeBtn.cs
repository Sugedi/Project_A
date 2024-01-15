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
        // foreach로 돌려서 리스트에 버튼이 있으면, 버튼 색을 회색으로
        // 첫 설명 화면은 딱총 설명화면으로

        // 데이터 매니저에서 바로 불러오는 방식 - 장기적으로는 이게 유리할 수 밖에 없음 - 근데 리스트에 스킬 애드를 못해서 못씀
        ES3.LoadInto(KeyName, datas);
        unlockedSkillList = datas.skillHave;

        // 캐릭터가 소지한 스킬을 불러오는 방식 
        // 아니 AddSkill 어케 쓰는 건데 ~~~
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
                        // 여기에 버튼 비활성화 넣으면 될듯
                    }
                }
                foreach (var Skill in datas.skillHave)
                {
                    if(Skill.skillName == "Basic")
                    {
                        CanvasGroup cg = transform.Find("Basic").GetComponent<CanvasGroup>(); //여기 오류 뜸 정신차리게 해주자. 
                        cg.alpha = 0;
                        cg.interactable = false;
                        cg.blocksRaycasts = false;
                    }
                }

                Skill skillName = Resources.Load<Skill>("Basic");      // "Prefabs/MyPrefab" 리소스 하위 경로 "Resources/Skills/스킬 이름" 으로 정리하면 될 듯
                unlockedSkillList.Add(skillName);
                DataManager.instance.datas.skillHave.Add(skillName);
                DataManager.instance.DataSave();
                GameObject.Find("Player").GetComponent<Player>().SkillGet(); // 커스텀 클래스를 저장, 불러오기 하려면 해당 씬에 easy save 3 manager가 있어야 하는 듯, 삽질 ㅈㄴ 했다.


                // 캐릭터에 직접 안 들어가고 datas에는 들어가는 것 확인
                // 캐릭터에 직접 넣어야 할듯
                // 추가적으로 저기 누를 때마다 계속 스킬 같은거 추가되니까
                // if 문 bool 값으로 1회 제한 두고, 초기화에는 되돌리기      +      강화하면, 버튼 비활성화 시키기 두개 하면 될 듯 하다.
                // 제엔장, 스킬 초기화 버튼도 넣어야 하네;;

                // ---
                // 대위기, 직접 만든 클래스를 불러오지를 못하네? 하핫 분명 해결법은 있다 이건. 말도 안 돼 안 될 리가 없는데 어려운 것 뿐이야.
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

        // 1.스킬스 오브젝트의 하위 스킬들 이름을 모두 불러와, skill + n 값의 형식으로 불러오기?
        // 2.그냥 노가다로 버튼 모두 불러오기? 그럼 스킬이 많아지면 어카농 -구조가 별론뎅

    }
}