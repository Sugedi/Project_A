using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;


    // �����͸� �����ϴ� ������Ʈ�� �ҷ���
    GameObject playerStat;

    // ������ ����?�� �����ϴ� ���� ����
    public Datas datas;

    public int maxHP;
    public float attackDamage;

    private string KeyName = "Datas";

    private void Awake()
    {
        ES3.LoadInto(KeyName, datas);
        // ���� ����� �÷��̾� ������ �ҷ����� �;�
        //playerStat = GameObject.Find("DataManager");
        //datas = playerStat.GetComponent<DataManager>().datas;
    }
    private void Start() 
    {
        // ������ �̰� ������ �ǰڴ�. ���ȴ�. ���� õ���ϱ�? ��������
        // �� ����? ������������������
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
        }
    }
}
