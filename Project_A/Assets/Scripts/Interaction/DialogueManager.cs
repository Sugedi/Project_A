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


    public int endTalk = 7;

    private QuestManager questManager; // ����Ʈ ������

    public GameObject questCanvas; // Quest Canvas�� ���� ����


    private void Awake() // ���� ���� �� ȣ��Ǵ� Awake �Լ�
    {
        // "dialogue" �κ��� Reasources ������ �־�� CSV ������ �̸��� �־��ָ� ��
        data_Dialog = CSVReader.Read("maindialogue"); // CSVReader�� ����Ͽ� ��ȭ �����͸� �о�� -> data_Dialog�� ����!


        questManager = FindObjectOfType<QuestManager>(); // ����Ʈ ������ ã��
        if (questManager != null)
        {
            // CSV ���Ͽ��� ����Ʈ �����͸� �ε��ϰ� QuestManager�� ����
            List<Dictionary<string, object>> questData = CSVReader.Read("mainquest"); // ����: mainquestdata.csv ���Ͽ��� �о��
            questManager.LoadQuests(questData);
        }
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

    // ####### �׷� �� ����� �����ϱ� �ϴ� �ּ�ó�� ��

    // ��ȭ�� ǥ���ϴ� �޼���
    //public void ChangeTalkId(int _id)
    //{
    //    id = _id; // ��ȭ ID�� ���ο� _id�� ����
    //    dialogCount = 0; // ��ȭ ī��Ʈ�� �ʱ�ȭ
    //    DialogSetting(); // ��ȭ ���� �޼��� ȣ��
    //}
    public void SettingUI(bool type) // UI�� �����ϴ� �޼��� (��ư Ű��)
    {
        dialogPopupBtn.gameObject.SetActive(type); // ��ȭ �˾� ��ư�� Ȱ��ȭ ���¸� type ���� ���� ����
    }

    // ��ȭ �˾��� ǥ���ϴ� �޼���
    public void ShowDialog()
    {
        //DialogSetting();
        // ���� ��ȭ ī��Ʈ(dialogCount)�� �ش��ϴ� �̸��� ��ȭ ������ ���� name�� dialog �ؽ�Ʈ UI�� ����
        //name.text = names[dialogCount];
        //dialog.text = dialogs[dialogCount];

        // ��ȭ �˾��� Ȱ��ȭ�ϰ�, ���̽�ƽ�� ��ȭ �˾� ��ư�� ��Ȱ��ȭ
        dialogPopup.SetActive(true);
        joystick.SetActive(false);
        dialogPopupBtn.SetActive(false);

        if (data_Dialog[id].ContainsKey("MainTalkID") && int.Parse(data_Dialog[id]["MainTalkID"].ToString()) == 4)
        {
            // MainTalkID�� 4�� �� Quest Canvas�� Ȱ��ȭ�մϴ�.
            questCanvas.SetActive(true);
        }

        NextTalk();

    }

    // ####### �׷� �� ����� �����ϱ� �ϴ� �ּ�ó�� ��

    //void DialogSetting() // ��ȭ ������ �ϴ� �޼���
    //{
    //    // �̸��� ��ȭ ����Ʈ �ʱ�ȭ
    //    //names = new List<string>();
    //    //dialogs = new List<string>();


    //    // ��ȭ �����Ϳ��� ���� ��ȭ ID�� �ش��ϴ� ��ȭ�� ã�� �̸��� ��ȭ ����Ʈ�� �߰�
    //    for (int i = 0; i < data_Dialog.Count; i++) // for���� (����; ����; �ܰ�)�� �ۼ���
    //    {
    //        if ((int)data_Dialog[i]["talkID"] == id)
    //        {
    //            names.Add(data_Dialog[i]["chaName"].ToString());
    //            dialogs.Add(data_Dialog[i]["description"].ToString());
    //        }
    //    }
    //}

    // ####### �׷� �� ����� �����ϱ� �ϴ� �ּ�ó�� ��

    // ���� ��ȭ�� ǥ���ϴ� �޼���
    //public void NextDialog()
    //{
    //    dialogCount++; // ��ȭ ī��Ʈ ����

    //    if (dialogCount >= names.Count)
    //    {
    //        // ��� ��ȭ�� ǥ������ ���, ��ȭ �˾��� ��Ȱ��ȭ�ϰ� ��ȭ ī��Ʈ�� �ʱ�ȭ
    //        dialogPopup.SetActive(false);
    //        dialogCount = 0;

    //        //��ȭ ID�� ���� ���� ��ȭ�� ���� -> �̰� ����� 2�� �׷��� ��簡 ���� �� 3�� �׷��� ��簡 �ȶ�
    //        if (id == 2)
    //        {
    //            ChangeTalkId(3); // ��ȭ ID�� 3���� �����Ͽ� ��ȭ 3�� ����
    //            ShowDialog(); // ��ȭ �˾��� ǥ��
    //        }
    //    }
    //    else
    //    {
    //        // ���� ��ȭ�� ǥ��
    //        name.text = names[dialogCount];
    //        dialog.text = dialogs[dialogCount];
    //    }
    //}

    // ��ȭ ID�� ��ȭ ī��Ʈ�� �˻��Ͽ� ��ȭ�� ��� �������� Ȯ���ϴ� �޼���
    public bool CheckIfDialogueIsComplete(int dialogueID)
    {
        // ��ȭ ID�� �Ű������� ���� dialogueID�� ��ġ�ϰ�,
        // ��ȭ ī��Ʈ�� ���� ��ȭ ID�� ��ȭ ��(names ����Ʈ�� ����) �̻��̸� true�� ��ȯ�մϴ�.
        return id == dialogueID && dialogCount >= names.Count;
    }


    public void NextTalk() // ���� ��� ������ �Լ�
    {
        // ���� ��ȭ ID�� endTalk ID�� �ʰ��ϴ��� Ȯ���մϴ�.
        // id�� endTalk�� ������ �ش� ��ȭ�� ǥ�õ��� �ʰ� ����ǹǷ�, '>' �����ڸ� ����մϴ�.
        if (id > endTalk)
        {
            // ��ȭ�� �������� ��ȭ �˾��� ��Ȱ��ȭ�մϴ�.
            dialogPopup.SetActive(false);
            // �÷��̾��� �������̳� ��ȣ�ۿ��� ���� ���̽�ƽ�� Ȱ��ȭ�մϴ�.
            joystick.SetActive(true);
            // ��ȭ �˾��� �ٽ� ���� ��ư�� Ȱ��ȭ�մϴ�(����).
            dialogPopupBtn.SetActive(true);
        }
        else
        {
            if (id < data_Dialog.Count)  // ���� ID�� ������ ����Ʈ�� ���� �ȿ� �ִ��� Ȯ���մϴ�.
            {
                // ��ȭ�� �����ϴ� ĳ���� �̸��� ��ȭ �ؽ�Ʈ�� UI�� �����մϴ�.
                name.text = data_Dialog[id]["MainChaName"].ToString();
                dialog.text = data_Dialog[id]["MainDescription"].ToString();

                // ���� ��ȭ ������ ����Ʈ�� Ʈ�����ϴ��� Ȯ���մϴ�.
                // 'MainKind' �ʵ尡 'quest'�̰� ��ȭ ID�� 4�� �� ù ��° ����Ʈ�� Ȱ��ȭ�մϴ�.
                if (data_Dialog[id].ContainsKey("MainKind") && data_Dialog[id]["MainKind"].ToString() == "quest" && id == 4)
                {
                    questManager.ActivateQuest(0); // ù ��° ����Ʈ Ȱ��ȭ
                }

                id++; // ���� ��ȭ ID�� �̵�
            }
            else
            {
                // ������ ����� ���� ó���� �߰��մϴ�.
                Debug.LogError("Dialogue ID is out of range. No more dialogue entries to show.");
            }            

            // ����� �������� ���� ��ȭ ID�� �α׿� ����մϴ�.
            Debug.Log("Current dialogue ID: " + id);
        }
    }
}

//else
//{
//    // ��ȭ�� ���� ������ �ʾ����� ���� ��ȭ ������ ǥ���մϴ�:
//    // ��ȭ�� �����ϴ� ĳ���� �̸��� �����մϴ�.
//    name.text = data_Dialog[id]["MainChaName"].ToString();
//    // ���� ��ȭ �ؽ�Ʈ�� �����մϴ�.
//    dialog.text = data_Dialog[id]["MainDescription"].ToString();
//    // ���� ��ȭ �������� �����ϱ� ���� ��ȭ ID�� ������ŵ�ϴ�.
//    id++;
//}

//// ���� ��ȭ ������ ����Ʈ�� Ʈ�����ϴ��� Ȯ���մϴ�.
//if (data_Dialog[id]["MainKind"].ToString() == "quest" &&
//    int.TryParse(data_Dialog[id]["MainquestID"].ToString(), out int mainquestID))
//{
//    // �־��� ����Ʈ ID�� ����Ʈ�� Ȱ��ȭ�մϴ�.
//    questManager.ActivateQuest(mainquestID);
//}