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
        // foreach�� ������ ����Ʈ�� ��ư�� ������, ��ư ���� ȸ������
        // ù ���� ȭ���� ���� ����ȭ������
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

    // 1.��ų�� ������Ʈ�� ���� ��ų�� �̸��� ��� �ҷ���, skill + n ���� �������� �ҷ�����?
    // 2.�׳� �밡�ٷ� ��ư ��� �ҷ�����? �׷� ��ų�� �������� ��ī�� -������ ���е�
    
}
