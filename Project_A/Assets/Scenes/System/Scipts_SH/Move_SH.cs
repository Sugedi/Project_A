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


}
