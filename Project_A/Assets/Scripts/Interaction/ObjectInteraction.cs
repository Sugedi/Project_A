using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// =============================================================================================================================
// NPC ������Ʈ�� ���̴� ��ũ��Ʈ
// ��ȣ�ۿ� Ʈ���� ���� ���� �÷��̾ ���� ��, '��ȣ�ۿ� ��ư'�� ������
// Main Quest ���� ������ ���� �ۼ��� (+ ��ȣ���� �˷��� ���� ����)
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    public GameObject talk1Panel;
    public GameObject talk2Panel;
    public GameObject talkCanvas;

    public GameObject stageUI;
    public GameObject joystickBtn;
    public GameObject AttackBtn;
    public GameObject RollBtn;
    public GameObject ReloadBtn;

    public GameObject startBtn; // 'Dialogue StartBtn: �ſ￡ ���� ���´�'
    public GameObject dialoguePanel1;
    public GameObject dialoguePanel2;
    public GameObject dialoguePanel3;

    // MainQuest Panels ����
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;

    public GameObject talkNextBtn; // 1-1���� 1-4���� �Ѱ��ִ� ��ư
    public GameObject nextButton1;
    public GameObject nextButton2;
    public GameObject nextButton3;

    public GameObject questIcon; // ����Ʈ ������
    public Button questBtn;

    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����

    public int mainQuest;

    public GameObject systemMessagePanel; // �ý��� �޽����� �����ϴ� �г�
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' ������Ʈ


    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        systemMessagePanel.SetActive(false); // �ý��� �޽��� �г��� ��Ȱ��ȭ ���·� ����
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;

        if (mainQuest == 0) // ���� ���� �� talk1Panel�� talk2Panel�� Ȱ��ȭ BUT, mainQuest�� 0�� ����!
        {
            questIcon.SetActive(false); // ����Ʈ ������ ��ư�� ��Ȱ��ȭ ���·� ����

            talk1Panel.SetActive(true);
            startBtn.SetActive(false);
            joystickBtn.SetActive(false);
            AttackBtn.SetActive(false);
            RollBtn.SetActive(false);
            ReloadBtn.SetActive(false);

            dialoguePanel1.SetActive(false); // ������ �� ù ��° ���â�� ��Ȱ��ȭ ���·� ����
        }

        if (mainQuest == 1)
        {
            questIcon.SetActive(true); // ����Ʈ ������ ��ư�� ��Ȱ��ȭ ���·� ����

            mainQuestPanel1.SetActive(true);
            mainQuestPanel2.SetActive(false);
            mainQuestPanel3.SetActive(false);
        }
        if (mainQuest == 2)
        {
            mainQuestPanel2.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel3.SetActive(false);

        }
        if (mainQuest == 3)
        {
            mainQuestPanel3.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(false);
        }

        questBtn.onClick.AddListener(ShowMainQuestPanel); // questBtn�� Ŭ�� �̺�Ʈ ������ �߰�

    }


    // questIcon ��ư�� Ŭ������ �� ȣ��� �޼���
    public void ShowMainQuestPanel()
    {
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;
        if (mainQuest == 1)
        {
            mainQuestPanel1.SetActive(true); // MainQuest Panel (1) Ȱ��ȭ
            mainQuestPanel2.SetActive(false);
            mainQuestPanel3.SetActive(false);
        }
        if (mainQuest == 2)
        {
            mainQuestPanel2.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel3.SetActive(false);

        }
        if (mainQuest == 3)
        {
            mainQuestPanel3.SetActive(true);
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(false);
        }

    }


    void OnTriggerEnter(Collider other) // ������Ʈ�� Ʈ���ſ� ������ �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ������ ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = true; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��

            if (mainQuest == 0)
            {
                talkCanvas.SetActive(false);
                AttackBtn.SetActive(false);
                startBtn.SetActive(true);
                //dialoguePanel1.SetActive(false); // ������ �� ù ��° ���â�� ��Ȱ��ȭ ���·� ���� ########
            }

            if (mainQuest == 1)
            {
                AttackBtn.SetActive(false);
                startBtn.SetActive(true);
                dialoguePanel2.SetActive(false);
                //questIcon.SetActive(false); // questIcon�� ��Ȱ��ȭ
            }

            if (mainQuest == 2)
            {
                AttackBtn.SetActive(false);
                startBtn.SetActive(true);
                dialoguePanel3.SetActive(false);
                //questIcon.SetActive(false); // questIcon�� ��Ȱ��ȭ
            }

        }
    }
    public void OnStartBtnClick()
    {
        Debug.Log("Start Button Clicked"); // �α׸� �߰��Ͽ� �� �޼��尡 ȣ��Ǵ��� Ȯ���մϴ�
        if (mainQuest == 0)
        {
            dialoguePanel1.SetActive(true);
            startBtn.SetActive(false);
            joystickBtn.SetActive(false);
            AttackBtn.SetActive(false);
            RollBtn.SetActive(false);
            ReloadBtn.SetActive(false);

        }
        if (mainQuest == 1)
        {
            dialoguePanel2.SetActive(true);
            startBtn.SetActive(false);
            joystickBtn.SetActive(false);
            AttackBtn.SetActive(false);
            RollBtn.SetActive(false);
            ReloadBtn.SetActive(false);


        }
        if (mainQuest == 2)
        {
            dialoguePanel3.SetActive(true);
            startBtn.SetActive(false);
            joystickBtn.SetActive(false);
            AttackBtn.SetActive(false);
            RollBtn.SetActive(false);
            ReloadBtn.SetActive(false);


        }
    }

    public void OnNextButton1Clicked()
    {
        Debug.Log("OnNextButton1Clicked() �޼��� ȣ���"); // �α� �߰�


        //startBtn.SetActive(false);
        dialoguePanel1.SetActive(false);
        questIcon.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ
        mainQuestPanel1.SetActive(true); // ù ��° ���� ����Ʈ �г��� Ȱ��ȭ ���·� ����

        joystickBtn.SetActive(true);
        AttackBtn.SetActive(true);
        RollBtn.SetActive(true);
        ReloadBtn.SetActive(true);


        // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
        string message = "�뺻���� Ȱ��ȭ �Ǿ����ϴ�.";
        ActivateSystemMessagePanel(message);

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // �α� �߰�

        // mainQuest�� 1�� ��, MainQuest Panel (1)�� Ȱ��ȭ ���·� ����
        if (mainQuest == 1)
        {
            mainQuestPanel1.SetActive(true);
            // �ٸ� UI ����� ���¸� ���⼭ ������ �� �ֽ��ϴ�.
        }

    }

    public void OnNextButton2Clicked()
    {
        dialoguePanel2.SetActive(false);
        questIcon.SetActive(true); // questIcon�� Ȱ��ȭ

        joystickBtn.SetActive(true);
        AttackBtn.SetActive(true);
        RollBtn.SetActive(true);
        ReloadBtn.SetActive(true);


        // mainQuest�� 2�� ��, MainQuest Panel (2)�� Ȱ��ȭ ���·� ����
        if (mainQuest == 2)
        {
            mainQuestPanel2.SetActive(true);
            // �ٸ� UI ����� ���¸� ���⼭ ������ �� �ֽ��ϴ�.
        }

    }

    public void OnNextButton3Clicked()
    {
        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // �α� �߰�

        dialoguePanel3.SetActive(false);
        questIcon.SetActive(true); // questIcon�� Ȱ��ȭ

        mainQuestPanel2.SetActive(false);
        mainQuestPanel3.SetActive(true);

        joystickBtn.SetActive(true);
        AttackBtn.SetActive(true);
        RollBtn.SetActive(true);
        ReloadBtn.SetActive(true);


        // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
        string message = "�뺻���� �ϼ��Ͽ����ϴ�.";
        ActivateSystemMessagePanel(message);

        // mainQuest�� 3�� ��, MainQuest Panel (3)�� Ȱ��ȭ ���·� ����
        if (mainQuest == 3)
        {
            mainQuestPanel3.SetActive(true);
            // �ٸ� UI ����� ���¸� ���⼭ ������ �� �ֽ��ϴ�.
        }

    }


    void OnTriggerExit(Collider other) // ������Ʈ�� Ʈ���Ÿ� �������� �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���Ÿ� �������� ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
            startBtn.SetActive(false);
            AttackBtn.SetActive(true);
        }

        //if (mainQuest == 0)
        //{
        //    dialoguePanel1.SetActive(false); // ������ �� ù ��° ���â�� ��Ȱ��ȭ ���·� ���� ########
        //}

        //if (mainQuest == 1)
        //{
        //    dialoguePanel2.SetActive(false);
        //}

        //if (mainQuest == 2)
        //{
        //    dialoguePanel3.SetActive(false);
        //}

    }

    // �ý��� �޽��� �г��� Ȱ��ȭ�ϰ�, ������ �ð� �Ŀ� ��Ȱ��ȭ�ϴ� �޼���
    private void ActivateSystemMessagePanel(string message)
    {
        systemMessagePanel.SetActive(true);
        systemMessageText.text = message;
        StartCoroutine(ShowSystemMessage(3f)); // �ڷ�ƾ�� ����Ͽ� 3�� �� �г� ��Ȱ��ȭ
    }

    // �ý��� �޽��� �г��� ǥ���ϴ� �ڷ�ƾ
    IEnumerator ShowSystemMessage(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // ������ �ð� ���� ���
        systemMessagePanel.SetActive(false); // �г� ��Ȱ��ȭ
    }
    public void RefreshItemCounter(int itemValue)
    {
        if (itemValue == 1)
        {
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(true);
        }

    }

}

