using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ������ ��, �÷��̾����� �����ϴ� ��� �ؾ���

// �������� 1 ������ �����ؾ��� �����͸� ���� ��ũ��Ʈ

// ���� ��ǥ
// ����ġ������ ������ ��. ����ġ�� ���� ���ϸ� �� ������ ������ �׳� ����.
// �ٵ� ����, ����Ʈ�� �ٽ��ϱ⿣ ������ �� ����. �� �� �����غ��� - ����Ʈ�� �Ϸ�ÿ� ���� ����. �ٽ��ϴ� �� ����

// ������, Ʃ�丮���� ���� 1ȸ�� ����. Ʃ�丮���� ������(������ ��ư?) �� ���� datamanager���� ���� bool���� true�� ����.(��� �齺���������� �ʿ� �����ϱ�)

// �׷� ����ġ ��ȣ�ۿ뿡�ٰ� ������ ���� �Ҵ�

// 1. ��ȭ ȹ��� -> (�̰� ����ġ& ����ÿ����� ����, ��ҿ��� �� ���� ������ ��Ƶα�)
// 2. ����(������ �߰��Ѵٵ���)
// 3. ��ų ����
// 4. �ͼ� ������ ȹ��� - �̸� ���� ������ ����(�������μ��� �� �Ǵ� ������� ������ ������ ���� ��)
// 5. ����Ʈ ���� ��Ȳ
// 6. ���� �ذ� ���� + 7. ���µ� ����
// 8. ���� ���� óġ ���� -> �������� ���°��� ����
// 9. Ʃ�丮�� �Ϸ� ����


// �� ��ȯ���� �� ����
// ���� ���� �ҷ����� �� ���� �޶�������?
// ���� ������ �ٽ� �ҷ����� �� �־�� ���ٵ�.
// ��� �ҷ��ñ�?



[System.Serializable]
public class Datas
{
    // ��ȭ
    public int Soul;

    // ���� ����
    public int maxHP = 3; // �ִ�ü�¸� �������ְ�, ����ġ ���� ���� ü���� �ִ� ü������ ȸ����Ű��
    public float attackDamage; 
    public float attackSpeed;
    public float moveSpeed;

    // �ѱ� & ��ų
    public int maxBullet;
    public float reloadTime;
    public float range;

    //public int skill_1LV = 0; // 0�� ��ȹ��, 1~3�� ��ų ���� // ���� �������� ������, 0 ~ 1�� booló�� �ᵵ ��
    //public int skill_2LV = 0; 
    //public int skill_3LV = 0; 

    // ��ų �ý��� �������� ���� ���� ����
    // ��ų ������ �ҷ��� �����ؾ� ��.
    public List<Skill> skillHave;


    // ������ʹ� �������� 1�� ����
    // �ͼ� ������ ȹ�� ���� - true���� �Ǹ�, ���ڸ� ���� �̹����� �ٲٰų�, ������Ʈ ����
    public bool stage1ItemBox1 = false; // ����(�ִ�ü�� ����)
    public bool stage1ItemBox2 = false; // ������ ������ - true�� �Ǹ� 

    // ����Ʈ ���� ��Ȳ
    public int stage1Quest1Pro = 0; // 0�� �̼���, 1�� �̿�, 2�� �Ϸ�/����̼��� 3�� �������
    public int stage1Quest2Pro = 0;

    // ���� ���� ��Ȳ (���� ���� ��) / �����̶� ��������
    public bool stage1Puzzle1 = false;
    public bool stage1Puzzle2 = false;

    // ���� ���� óġ ���� (���� ���޿�)
    public bool stage1BossClear = false;

    // Ʃ�丮�� �Ϸ� ����
    public bool stage1Tutorial = false;

    //���߿� �� ��ġ�� ��û ����� ��? ������������ �ɰ��ų� ����� �����ؾ߰ڴµ�??
    //���� �� ���� �����̴� ������ ���� �� ����. �ε�âó��
    

}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Datas datas;  // ----------------------------------------------- A
        
    // �⺻ ��
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    public GameObject stat;

    void Start()
    {
        instance = this;

        // ���� ���� �� ����� ������ �ε�
        DataLoad();
    }


    public void DataSave() 
    {
        // �⺻ ��
        // Datas Ŭ������ �ִ� ��� �������� ����
        ES3.Save(KeyName,datas);  // <-------------------------------------- A
        stat = GameObject.Find("Player_SH");
        stat.GetComponent<Move_SH>().ChangeScene();

        // �ڵ����� ��
        //ES3AutoSaveMgr.Current.Save();
    }
    public void DataLoad()
    {
        ES3.LoadInto(KeyName, datas);
        //ES3.Load(KeyName, datas);

        // ĳ���� ���ȿ� ����ȭ
        stat = GameObject.Find("Player_SH");
        stat.GetComponent<Move_SH>().ChangeScene();

    }

    public void DataRemove()
    {
        ES3.DeleteFile(fileName);
    }


}
