using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class DialogueManager : MonoBehaviour
{

    // =============================================================================================================
    // ���� ���� �� CSV ���Ͽ��� ��ȭ �����͸� �о�ͼ� UI�� ǥ���ϴ� �⺻���� ����
    // =============================================================================================================

    // 
    List<Dictionary<string, object>> data_Dialog; // CSV ���Ͽ��� �о�� ��ȭ �����͸� ������ ����Ʈ

    // UI ��ҵ��� ������ TextMeshProUGUI ������
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI name;

    public GameObject dialogPopupBtn; // Attack Button���� �����Ǿ� ����
    public GameObject joystick; // ���̽�ƽ UI ���
    public GameObject dialogPopup; // ��ȭ �˾� UI ���

    [SerializeField]
    int id; // ���� ��ȭ�� ID

    // ������ (�Ʒ� �� ���� �ڵ�� �̸��� ��ȭ ������ �����ϱ� ���� �� ���� ����Ʈ�� �����ϴ� ��)
    public List<string> names = new List<string>(); // ��ȭ �������� �̸��� �����ϴµ� ����
    public List<string> dialogs = new List<string> (); // ��ȭ ������ �����ϴµ� ����

    public int dialogCount = 0; // ���� ��ȭ ���� ���¸� ��Ÿ���� ī��Ʈ ����


    private void Awake() // ���� ���� �� ȣ��Ǵ� Awake �Լ�
    {
        // "dialogue" �κ��� Reasources ������ �־�� CSV ������ �̸��� �־��ָ� ��
        data_Dialog = CSVReader.Read("dialogue"); // CSVReader�� ����Ͽ� ��ȭ �����͸� �о�� -> data_Dialog�� ����!
    }

    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        // ���� ���� ��ü���� �⺻ ��(���� ����)�� ����ٰ� ���� ����
        id = 0; // �ʱ� ��ȭ ID ����

        // ���� �߻�(24-01-10) �߾��µ� ��ħ: �� ���� ����� ����
        // 1. �Ʒ��� ���� ������ ���۵� �� ���ʷ� 1�� ȣ��Ǵ� Start �޼ҵ忡, ��ȭ�˾�â�� �⺻������ false ���·� �����س���
        // 2. ���� �� ���̵� ���̾��Ű â���� �θ� ��ü�� talk canvas�� ���ΰ�, �ڽ� ��ü�� dialogue�� �ν�����â���� Ȱ��ȭ �������ѳ���
        dialogPopup.SetActive(false); // ��ȭ �˾��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
    }

    // ��ȭ�� ǥ���ϴ� �޼���
    public void ChangeTalkId(int _id)
    {
        id = _id; // ��ȭ ID�� ���ο� _id�� ����
        dialogCount = 0; // ��ȭ ī��Ʈ�� �ʱ�ȭ
        DialogSetting(); // ��ȭ ���� �޼��� ȣ��
    }
    public void SettingUI(bool type) // UI�� �����ϴ� �޼��� (��ư Ű��)
    {
        dialogPopupBtn.gameObject.SetActive(type); // ��ȭ �˾� ��ư�� Ȱ��ȭ ���¸� type ���� ���� ����
    }

    // ��ȭ �˾��� ǥ���ϴ� �޼���
    public void ShowDialog()
    {
        // ���� ��ȭ ī��Ʈ(dialogCount)�� �ش��ϴ� �̸��� ��ȭ ������ ���� name�� dialog �ؽ�Ʈ UI�� ����
        name.text = names[dialogCount];
        dialog.text = dialogs[dialogCount];

        // ��ȭ �˾��� Ȱ��ȭ�ϰ�, ���̽�ƽ�� ��ȭ �˾� ��ư�� ��Ȱ��ȭ
        dialogPopup.SetActive(true);
        joystick.SetActive(false);
        dialogPopupBtn.SetActive(false) ;

    }


    void DialogSetting() // ��ȭ ������ �ϴ� �޼���
    {
        // �̸��� ��ȭ ����Ʈ �ʱ�ȭ
        names = new List<string>();
        dialogs = new List<string>();

        // ��ȭ �����Ϳ��� ���� ��ȭ ID�� �ش��ϴ� ��ȭ�� ã�� �̸��� ��ȭ ����Ʈ�� �߰�
        for (int i = 0; i < data_Dialog.Count; i++) // for���� (����; ����; �ܰ�)�� �ۼ���
        {
            if ((int)data_Dialog[i]["talkID"] == id)
            {
                names.Add(data_Dialog[i]["chaName"].ToString());
                dialogs.Add(data_Dialog[i]["description"].ToString());
            }
        }
        Debug.Log(names.Count);
    }

    // ���� ��ȭ�� ǥ���ϴ� �޼���
    public void NextDialog()
    {
        dialogCount++; // ��ȭ ī��Ʈ ����

        if (dialogCount >= names.Count)
        {
            // ��� ��ȭ�� ǥ������ ���, ��ȭ �˾��� ��Ȱ��ȭ�ϰ� ��ȭ ī��Ʈ�� �ʱ�ȭ
            dialogPopup.SetActive(false);
            dialogCount = 0;

            //��ȭ ID�� ���� ���� ��ȭ�� ���� -> �̰� ����� 2�� �׷��� ��簡 ���� �� 3�� �׷��� ��簡 �ȶ�
            if (id == 2)
            {
                ChangeTalkId(3); // ��ȭ ID�� 3���� �����Ͽ� ��ȭ 3�� ����
                ShowDialog(); // ��ȭ �˾��� ǥ��
            }
        }
        else
        {
            // ���� ��ȭ�� ǥ��
            name.text = names[dialogCount];
            dialog.text = dialogs[dialogCount];
        }
    }

    // ��ȭ ID�� ��ȭ ī��Ʈ�� �˻��Ͽ� ��ȭ�� ��� �������� Ȯ���ϴ� �޼���
    public bool CheckIfDialogueIsComplete(int dialogueID)
    {
        // ��ȭ ID�� �Ű������� ���� dialogueID�� ��ġ�ϰ�,
        // ��ȭ ī��Ʈ�� ���� ��ȭ ID�� ��ȭ ��(names ����Ʈ�� ����) �̻��̸� true�� ��ȯ�մϴ�.
        return id == dialogueID && dialogCount >= names.Count;
    }


}
