using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class DataManager_hyunji: MonoBehaviour
{

    // =============================================================================================================
    // ���� ���� �� CSV ���Ͽ��� ��ȭ �����͸� �о�ͼ� UI�� ǥ���ϴ� �⺻���� ����
    // =============================================================================================================

   /* List<Dictionary<string, object>> data_Dialog; // CSV ���Ͽ��� �о�� ��ȭ �����͸� ������ ����Ʈ

    // UI ��ҵ��� ������ TextMeshProUGUI ������
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI name;
    int id; // ���� ��ȭ�� ID

    private void Awake() // ���� ���� �� ȣ��Ǵ� Awake �Լ�
    {
        data_Dialog = CSVReader.Read("dialogue"); // CSVReader�� ����Ͽ� ��ȭ �����͸� �о��
    }

    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        // ��ȭ �����͸� Ȯ���ϱ� ���� �ּ� ó���� �ڵ�
        //for (int i = 0; i < data_Dialog.Count; i++)
        //{
        //    print(data_Dialog[i]["Content"].ToString());
        //}

        id = 0; // �ʱ� ��ȭ ID�� 0���� ����

        // ��ȭ�� ĳ���� �̸��� UI �ؽ�Ʈ�� ǥ��
        dialog.text = (string)data_Dialog[id]["dialogue"];
        name.text = (string)data_Dialog[id]["chaName"];

        id++; // ���� ��ȭ ID�� �̵�

        // ���� ��ȭ�� ĳ���� �̸��� UI �ؽ�Ʈ�� ǥ��
        dialog.text = (string)data_Dialog[id]["dialogue"];
        name.text = (string)data_Dialog[id]["chaName"];
    } */
}