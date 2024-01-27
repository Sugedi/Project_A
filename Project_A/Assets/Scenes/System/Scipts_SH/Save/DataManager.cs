using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int soul = 0;

    // ���� ����
    public int maxHP = 100; // �ִ�ü�¸� �������ְ�, ����ġ ���� ���� ü���� �ִ� ü������ ȸ����Ű��

    // ��ų ������ �ҷ��� �����ؾ� ��.
    public List<Skill> skillHave;

    // ������ʹ� �������� 1�� ����
    // �ͼ� ������ ȹ�� ���� - true���� �Ǹ�, ���ڸ� ���� �̹����� �ٲٰų�, ������Ʈ ����
    public bool stage1ItemBox1 = false; // ����(�ִ�ü�� ����)
    public bool stage1ItemBox2 = false; 
    public bool stage1ItemBox3 = false; 

    // ����Ʈ ���� ��Ȳ
    public int stage1MainQuest = 0; // 0�� �̼���, 1�� �̿�, 2�� �Ϸ�/����̼��� 3�� �������
    // public int stage1Quest2Pro = 0; //���� �̻��

    // ���� ���� óġ ���� (���� ���޿�)
    public bool stage1BossClear = false;

    // Ʃ�丮�� �Ϸ� ����
    public int stage1Tutorial = 0;

    // ���̺� ��ġ
    public Vector3 savePos = new Vector3(0, 0, 0); // ����ġ���� �� ������ �ҷ��� �ٲ۴�.
    public string saveSceneName = "Backstage_0114"; //�ϴ� ������ �� ��� �ʱⰪ

}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    public Datas datas;  // ----------------------------------------------- A
        
    // �⺻ ��
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    //public GameObject stat;

    private void Awake()
    {
        // ������ ������ �ƹ� ������ ���ٸ�, ������ ���� ����
        if (!ES3.FileExists(fileName))
        {
            // �ʱ� ���� ������ ����
            DataSave();
        }
        DataLoad();
    }

    public void Start()
    {
        instance = this;
    }


    public void DataSave() 
    {
        // �⺻ ��
        // Datas Ŭ������ �ִ� ��� �������� ���� - �� ȿ�������δ� Datas�� �Ϻθ� �����ϸ� �����ѵ�...
        // ����� Ŭ������ �����ϱ� ���� ���� ���� ����
        var settings = new ES3Settings { memberReferenceMode = ES3.ReferenceMode.ByRefAndValue };
        ES3.Save(KeyName, datas, settings);
        
        // ���� ��, ĳ���� ������Ʈ�� �ش� ������ ������Ʈ
        GameObject.Find("Player").GetComponent<Player>().ChangeScene();

    }
    public void DataLoad()
    {
        // ���� Ŀ���� Ŭ������ ���� �ҷ����Ⱑ �����մϴٸ�, 
        // Skill �̸��� �� �ҷ�����. ��Ȯ�� ������ ���ִ� �� ��
        var settings = new ES3Settings { memberReferenceMode = ES3.ReferenceMode.ByRefAndValue };
        ES3.LoadInto(KeyName, datas, settings);
        // ES3.Load(KeyName, datas);
        // Load, Loadinto�� �������� �� �𸣰ڴ�.

        // ĳ���� ���ȿ� ����ȭ
        GameObject.Find("Player").GetComponent<Player>().ChangeScene();

    }

    // ������ �ʱ�ȭ �Ǵ� �� ���� ��ɿ� ���� �� ����. �ɼǿ��� ������ �� �ְ� �ұ�? - �׽�Ʈ �뵵�ε� ����.
    public void DataRemove()
    {
        ES3.DeleteFile(fileName);
    }

    // ���� ���Ǵ� �� ����. �ٵ�, ���� ����� ���ؼ� ���ܳ���.
    public List<Skill> tempList;
}
