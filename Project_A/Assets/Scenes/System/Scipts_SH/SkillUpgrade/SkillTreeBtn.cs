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
        // foreach로 돌려서 리스트에 버튼이 있으면, 버튼 색을 회색으로
        // 첫 설명 화면은 딱총 설명화면으로

        // 데이터 매니저에서 바로 불러오는 방식 - 장기적으로는 이게 유리할 수 밖에 없음 - 근데 리스트에 스킬 애드를 못해서 못씀
        ES3.LoadInto(KeyName, datas);
        unlockedSkillList = datas.skillHave;

        // 캐릭터가 소지한 스킬을 불러오는 방식 
        // 아니 AddSkill 어케 쓰는 건데 ~~~
        //GameObject skillLoad = GameObject.Find("Player_SH");
        //unlockedSkillList = skillLoad.GetComponent<Move_SH>().activeSkills;

        // 버튼 비활성화 하는 거 -> 이건 강화에 적용해야 함.
        foreach (var Skill in datas.skillHave)
        {
            if (Skill.skillName == "Basic")
            {
                //Button cg = transform.Find("Basic").GetComponent<Button>(); //여기 오류 뜸 정신차리게 해주자. 
                // 지금 자식이 없는 버튼에 넣어서 오류가 나는 것.여러 데에 다 넣을 거라
                //cg.interactable = false;
            }
        }

    }

    void UnlockedSkillColor()
    {
        // 이거 배껴서 다른 스크립트에 쓰고, Skills 패널에 달아야 함.
        foreach (var Skill in datas.skillHave)
        {
            Image cg = transform.Find(Skill.skillName).GetComponent<Image>(); //여기 오류 뜸 정신차리게 해주자. 
            //cg.color = ();
        }
    }

    void SkillLV()
    {
        //스킬 좌하단에 텍스트메쉬프로로 0, 1, 2, 3, 4 로 스킬 레벨 표현할 것
        //강화 또는 초기화 시에 해당 변수를 0으로 변경
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
                        Debug.Log("더 이상 강화 불가");
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
                        Debug.Log("더 이상 강화 불가");
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
                        Debug.Log("더 이상 강화 불가");
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
                        Debug.Log("더 이상 강화 불가");
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

        // 1.스킬스 오브젝트의 하위 스킬들 이름을 모두 불러와, skill + n 값의 형식으로 불러오기?
        // 2.그냥 노가다로 버튼 모두 불러오기? 그럼 스킬이 많아지면 어카농 -구조가 별론뎅

        // 훗날, 또 다시 오류가 발생하면 고개를 들어 여기를 보라. 그동안의 고행이 담겨 있음. (완성 전까지 삭제 금지)

        //Skill skillName = Resources.Load<Skill>("Basic");      // "Prefabs/MyPrefab" 리소스 하위 경로 "Resources/Skills/스킬 이름" 으로 정리하면 될 듯
        //unlockedSkillList.Add(skillName);
        //DataManager.instance.datas.skillHave.Add(skillName);
        //DataManager.instance.DataSave();
        //GameObject.Find("Player").GetComponent<Player>().SkillGet(); // 커스텀 클래스를 저장, 불러오기 하려면 해당 씬에 easy save 3 manager가 있어야 하는 듯, 삽질 ㅈㄴ 했다.


        // 캐릭터에 직접 안 들어가고 datas에는 들어가는 것 확인
        // 캐릭터에 직접 넣어야 할듯
        // 추가적으로 저기 누를 때마다 계속 스킬 같은거 추가되니까
        // if 문 bool 값으로 1회 제한 두고, 초기화에는 되돌리기      +      강화하면, 버튼 비활성화 시키기 두개 하면 될 듯 하다.
        // 제엔장, 스킬 초기화 버튼도 넣어야 하네;;

        // ---
        // 대위기, 직접 만든 클래스를 불러오지를 못하네? 하핫 분명 해결법은 있다 이건. 말도 안 돼 안 될 리가 없는데 어려운 것 뿐이야.
        // ---
    }
}