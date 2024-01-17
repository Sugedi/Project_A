using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

public class SaveSwitch : MonoBehaviour
{
    
    public Vector3 checkPoint_1 = new Vector3(-32f, 0.5f, 36f);
    public Vector3 checkPoint_2 = new Vector3(0f, 0.5f, 36f); // ���� ����
    public string checkScene_1 = "Stage";
    public int saveNumber = 1;
    //public SavePoint savePoint = SavePoint.SavePoint_1;

    public void SaveData(int saveNumber)
    {
        DataManager dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        if (saveNumber == 1)
        {
            dataManager.datas.savePos = checkPoint_1;
            dataManager.datas.saveSceneName = checkScene_1;
            dataManager.DataSave();
        }
        else if (saveNumber == 2)
        {
            dataManager.datas.savePos = checkPoint_2;
            dataManager.datas.saveSceneName = checkScene_1;
            dataManager.DataSave();
        }


        Debug.Log("����Ǿ����ϴ�.");
    }

}
