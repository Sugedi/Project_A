using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    // �Һ��ϴ� �÷��̾� ����
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;

    // ������ �Ŵ������� �ҷ����� �÷��̾� ����
    public int maxHP;
    public float attackDamage;

    // ������ �Ŵ������� �����͸� �ҷ����� ���� �ʼ�
    public Datas datas;
    private string KeyName = "Datas";



    private void Start()
    {
        // ���� ���� ��, ĳ���Ͱ� ����� �ڽ��� ������ �ҷ���
        ES3.LoadInto(KeyName, datas);
        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
        Debug.Log(datas.maxHP);
        Debug.Log(datas.attackDamage);
        Debug.Log(maxHP);

    }


    //public StageName stage;
    public bool stage_1 = true;
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(moveAmount);

        // ��ȣ�ۿ� Ű
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceFunction();
        }
    }

    void SpaceFunction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (Collider collider in hitColliders)
        {

            if (collider.CompareTag("Enemy"))
            {
                if (stage_1 == true)
                {
                    SceneManager.LoadScene("Title");
                    stage_1 = false;
                }

                else if (stage_1 == false)
                {
                    SceneManager.LoadScene("SaveTest");
                    stage_1 = true;
                }
                /*
                switch (stage)
                {
                    case StageName.Stage1:
                        SceneManager.LoadScene("Title");
                        break;
                }
                */
                // "enemy" �±׸� ���� ������Ʈ�� ��ȣ�ۿ��ϴ� �ڵ� �߰�
                //SceneManager.LoadScene("Title");

            }

            if (collider.CompareTag("NPC"))
            {

                // ��Ȱ��ȭ�� ���� ������Ʈ ã�ƿ��� ���� �����?
                //GameObject.Find("Skill").transform.Find("SkillCanvas").gameObject.SetActive(true);

                GameObject.Find("SkillNPC").GetComponent<SkillNPC>().Interact();

                // �̰� ��Ȱ��ȭ ����, ���� �޴����� ��� ĵ���� �׷����� ���� �Ѵ� �� ���� �� �ϴ�.
                // �����ϸ� ��Ȱ��ȭ �� ��Ű�� �� ��������..?
                // ���� �� �𸣰ڳ�
                // + UI â ������ ��, Ű���� ������ �� �ְ� �Ǿ�����. ������� ���� ��������...
                // UI �߸� ���� �̵�, ���� �� ���ϵ��� �ϴ� �� ���� ��

                //GameObject mainUI = GameObject.Find("SaveCanvas");
            }
        }

        // ��ȣ�ۿ� �ø��� �����͸� �ҷ��� �÷��̾� ���� �������ش�. ����~
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
    }

    public void ChangeScene()
    {
        // ���� �ٲ���� ��, �÷��̾��� ������ ���������� ����� ������ ����
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
    }

    // ��ų ���� �׽�Ʈ
    public List<Skill> activeSkills;


    public void ApplySkills()
    {
        // ������ ���Ⱑ ���ų�, Ȱ��ȭ�� ��ų�� ���� ��� ��ų�� �������� �ʽ��ϴ�.
        if (activeSkills == null || activeSkills.Count == 0)
        {
            return;
        }

        // �������� ���� �ӵ� ������ �⺻ 1�� �����մϴ�.
        float totalDamageMultiplier = 1f;
        float totalAttackSpeedMultiplier = 1f;
        bool shotGun1Active = false; // ����1 ��ų Ȱ��ȭ ����
        bool shotGun2Active = false; // ����2 ��ų Ȱ��ȭ ����
        bool shotGun3Active = false; // ����3 ��ų Ȱ��ȭ ����
        bool shotGun4Active = false; // ����4 ��ų Ȱ��ȭ ����
        int shotGun1Bullets = 0; // ����1 �� �߻�� �߰� �Ѿ� ��
        float shotGun1Angle = 0f; // ����1�� �Ѿ� �� ����
        int shotGun2Bullets = 0; // ����2 ��ų �� �߻�� �߰� �Ѿ� ��
        float shotGun2Angle = 0f; // ����2 ��ų�� �Ѿ� �� ����
        int shotGun3Bullets = 0; // ����3 ��ų �� �߻�� �߰� �Ѿ� ��
        float shotGun3Angle = 0f; // ����3 ��ų�� �Ѿ� �� ����
        int shotGun4Bullets = 0; // ����4 ��ų �� �߻�� �߰� �Ѿ� ��
        float shotGun4Angle = 0f; // ����4 ��ų�� �Ѿ� �� ����

        // Ȱ��ȭ�� ��ų���� ��ȸ�ϸ� ������ �� ���� �ӵ� ������ ����մϴ�.
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                totalDamageMultiplier *= skill.damageMultiplier;
                totalAttackSpeedMultiplier *= skill.attackSpeedMultiplier;

                // ����1 ��ų Ȱ��ȭ ���θ� üũ�ϰ� ���� ���� ������
                if (skill.isShotGun1)
                {
                    shotGun1Active = true;
                    shotGun1Bullets = skill.shotGun1Count;
                    shotGun1Angle = skill.shotGun1SpreadAngle;
                }

                if (skill.isShotGun2)
                {
                    shotGun2Active = true; // ����2 ��ų�� Ȱ��ȭ ���·� ����
                    shotGun2Bullets = skill.shotGun2Count; // �߻�� �Ѿ� ��
                    shotGun2Angle = skill.shotGun2SpreadAngle; // �Ѿ� ���� ����
                }

                if (skill.isShotGun3)
                {
                    shotGun3Active = true; // ����3 ��ų�� Ȱ��ȭ ���·� ����
                    shotGun3Bullets = skill.shotGun3Count; // �߻�� �Ѿ� ��
                    shotGun3Angle = skill.shotGun3SpreadAngle; // �Ѿ� ���� ����
                }

                if (skill.isShotGun4)
                {
                    shotGun4Active = true; // ����4 ��ų�� Ȱ��ȭ ���·� ����
                    shotGun4Bullets = skill.shotGun4Count; // �߻�� �Ѿ� ��
                    shotGun4Angle = skill.shotGun4SpreadAngle; // �Ѿ� ���� ����
                }

            }
        }


    }

    public void AddSkill(Skill newSkill)
    {
        activeSkills.Add(newSkill); // ��ų ����Ʈ�� �� ��ų�� �߰��մϴ�.
        ApplySkills(); // ��ų �߰� �� ��ų�� �����մϴ�.

    }

    // ��ų�� �����ϴ� �޼����Դϴ�.
    public void RemoveSkill(Skill skillToRemove)
    {
        activeSkills.Remove(skillToRemove); // ��ų ����Ʈ���� ������ ��ų�� �����մϴ�.
        ApplySkills(); // ��ų ���� �� ��ų�� �ٽ� �����մϴ�.
    }

}
