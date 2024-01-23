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

    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����

    public int mainQuest;

    public GameObject systemMessagePanel; // �ý��� �޽����� �����ϴ� �г�
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' ������Ʈ


    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        questIcon.SetActive(false); // ����Ʈ ������ ��ư�� ��Ȱ��ȭ ���·� ����
        systemMessagePanel.SetActive(false); // �ý��� �޽��� �г��� ��Ȱ��ȭ ���·� ����
        mainQuestPanel1.SetActive(true); // ù ��° ���� ����Ʈ �г��� Ȱ��ȭ ���·� ����
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;


        if (mainQuest == 0) // ���� ���� �� talk1Panel�� talk2Panel�� Ȱ��ȭ BUT, mainQuest�� 0�� ����!
        {
            talk1Panel.SetActive(true);
            dialoguePanel1.SetActive(false); // ������ �� ù ��° ���â�� ��Ȱ��ȭ ���·� ����

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
                startBtn.SetActive(true);
                dialoguePanel1.SetActive(false); // ������ �� ù ��° ���â�� ��Ȱ��ȭ ���·� ����
            }

            if (mainQuest == 1)
            {
                dialoguePanel2.SetActive(false);
                //questIcon.SetActive(false); // questIcon�� ��Ȱ��ȭ
            }

            if (mainQuest == 2)
            {
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

            stageUI.SetActive(true); // ü��, �Ѿ�, �� UI Ȱ��ȭ

        }
        if (mainQuest == 1)
        {
            dialoguePanel2.SetActive(true);
            questIcon.SetActive(false);
        }
        if (mainQuest == 2)
        {
            dialoguePanel3.SetActive(true);
            questIcon.SetActive(false);
        }
    }

    public void OnNextButton1Clicked()
    {
        Debug.Log("OnNextButton1Clicked() �޼��� ȣ���"); // �α� �߰�

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // �α� �߰�

        startBtn.SetActive(false);
        dialoguePanel1.SetActive(false);
        questIcon.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ

        // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
        string message = "�ֿ� �뺻 1�� Ȱ��ȭ �Ǿ����ϴ�.";
        ActivateSystemMessagePanel(message);

    }

    public void OnNextButton2Clicked()
    {
        dialoguePanel2.SetActive(false);
        questIcon.SetActive(true); // questIcon�� Ȱ��ȭ
    }

    public void OnNextButton3Clicked()
    {
        mainQuest++;
        dialoguePanel3.SetActive(false);
        questIcon.SetActive(true); // questIcon�� Ȱ��ȭ

        mainQuestPanel2.SetActive(false);
        mainQuestPanel3.SetActive(true);

        // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
        string message = "�ֿ� �뺻 1�� �ϼ��Ͽ����ϴ�.";
        ActivateSystemMessagePanel(message);
    }


    void OnTriggerExit(Collider other) // ������Ʈ�� Ʈ���Ÿ� �������� �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���Ÿ� �������� ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
            startBtn.SetActive(false);
        }
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

