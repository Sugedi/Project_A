using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SaveSwitchNumber
{
    SaveSwitch_1,
    SaveSwitch_2,
    SaveSwitch_9,
}
public class SaveSwitch : MonoBehaviour
{
    public SaveSwitchNumber switchNumber;

    static public Vector3 checkPoint_1 = new Vector3(-32f, 0.5f, 36f);
    static public Vector3 checkPoint_2 = new Vector3(0f, 0.5f, 36f); // ���� ����
    static public Vector3 checkPoint_9 = new Vector3(0f, 0f, 0f);
    static public string checkScene_1 = "Stage";
    static public int saveNumber = 1;

    private void Start()
    {
        if (switchNumber == SaveSwitchNumber.SaveSwitch_1)
        {
            saveNumber = 1;
        }
        else if (switchNumber == SaveSwitchNumber.SaveSwitch_2)
        {
            saveNumber = 2;
        }
        // �׽�Ʈ�� �� ������. 0,0,0���� �ʱ�ȭ�ϴ� ��ư
        else if (switchNumber == SaveSwitchNumber.SaveSwitch_9)
        {
            saveNumber = 9;
        }

    }

    public static void SwitchFunc()
    {
        if (saveNumber == 1)
        {
            DataManager.instance.datas.savePos = checkPoint_1;
            DataManager.instance.datas.saveSceneName = checkScene_1;
            DataManager.instance.DataSave();
        }
        else if (saveNumber == 2)
        {
            DataManager.instance.datas.savePos = checkPoint_2;
            DataManager.instance.datas.saveSceneName = checkScene_1;
            DataManager.instance.DataSave();
        }
        else if (saveNumber == 9)
        {
            DataManager.instance.datas.savePos = checkPoint_9;
            DataManager.instance.datas.saveSceneName = checkScene_1;
            DataManager.instance.DataSave();
        }
        Debug.Log("����Ǿ����ϴ�.");
        GameObject.Find("Player").GetComponent<Player>().SaveHeal();
    }
}

// ���̺�_1 (-32f, 0.5f, 36f)
//- �ͼ� ������
//- ���漺 ��ų
//- ���̺� �� & ���̺� ��ġ
//- �ذ��� ����? �� ������� ���·� ����? - �ϴ� �ļ����� �̷���
//- ������ ���� ����
//- ���� óġ ����? - ���� 1���������� ����� �ʿ� ����

// UI 
//- �齺��������
//- ��� �ϱ�
