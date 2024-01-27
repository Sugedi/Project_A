using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SaveSwitchNumber
{
    SaveSwitch_1, // ���ƽý� ���̺�
    SaveSwitch_2,
    SaveSwitch_Tutor, //Ʃ�丮�� ���̺�
    SaveSwitch_9,
}
public class SaveSwitch : MonoBehaviour
{
    public SaveSwitchNumber switchNumber;

    public Vector3 checkPoint_1 = new Vector3(-32f, 0.5f, 36f);
    public Vector3 checkPoint_2 = new Vector3(0f, 0.5f, 36f); // ���� ����
    public Vector3 checkPoint_Tutor = new Vector3(0f, 0f, 37f);
    public Vector3 checkPoint_9 = new Vector3(0f, 0f, 0f);

    public string checkScene_1 = "Stage";
    public int saveNumber = 1;

    public void Start()
    {
        if (switchNumber == SaveSwitchNumber.SaveSwitch_1)
        {
            saveNumber = 1;
        }
        else if (switchNumber == SaveSwitchNumber.SaveSwitch_2)
        {
            saveNumber = 2;
        }
        else if (switchNumber == SaveSwitchNumber.SaveSwitch_Tutor)
        {
            saveNumber = 3;
        }

        else if (switchNumber == SaveSwitchNumber.SaveSwitch_9)
        {
            saveNumber = 9;
        }

    }

    public void SwitchFunc()
    {
        if (saveNumber == 1)
        {
            DataManager.instance.datas.savePos = checkPoint_1;
            DataManager.instance.datas.saveSceneName = checkScene_1;

        }
        else if (saveNumber == 2)
        {
            DataManager.instance.datas.savePos = checkPoint_2;
            DataManager.instance.datas.saveSceneName = checkScene_1;

        }
        else if (saveNumber == 3)
        {
            DataManager.instance.datas.savePos = checkPoint_Tutor;
            DataManager.instance.datas.saveSceneName = checkScene_1;

        }

        else if (saveNumber == 9)
        {
            DataManager.instance.datas.savePos = checkPoint_9;
            DataManager.instance.datas.saveSceneName = checkScene_1;

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
