using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeBtn : MonoBehaviour
{

    public Skills skillBtn;
    private List<Skills> unlockedSkillList;

    // Start is called before the first frame update
    void Start()
    {
        // foreach로 돌려서 리스트에 버튼이 있으면, 버튼 색을 회색으로
        // 첫 설명 화면은 딱총 설명화면으로
    }

    public void UnlockSkill(Skills skills)
    {
        unlockedSkillList.Add(skills);
    }

    public bool IsSkillUnlocked(Skills skills)
    {
        return unlockedSkillList.Contains(skills);
    }

    public void OnBtnClick()
    {

        for (int i = 1;  i < 6; i++ )
        {
            
        }
    }

    // 1.스킬스 오브젝트의 하위 스킬들 이름을 모두 불러와, skill + n 값의 형식으로 불러오기?
    // 2.그냥 노가다로 버튼 모두 불러오기? 그럼 스킬이 많아지면 어카농 -구조가 별론뎅
    
}
