using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// =============================================================================================================================
// NPC ������Ʈ�� ���̴� ��ũ��Ʈ
// ��ȣ�ۿ� Ʈ���� ���� ���� �÷��̾ ���� ��, '��ȣ�ۿ� ��ư'�� ������
// Main Quest ���� ������ ���� �ۼ��� (+ ��ȣ���� �˷��� ���� ����)
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    public GameObject talk1Panel; //
    public GameObject talk2Panel; //
    public GameObject talkCanvas; //

    public GameObject stageUI; //

    public GameObject dialoguePanel1; //
    public GameObject dialoguePanel2; //
    public GameObject dialoguePanel3; //

    // MainQuest Panels ����
    public GameObject mainQuestPanel1; //
    public GameObject mainQuestPanel2; //
    public GameObject mainQuestPanel3; //

    public GameObject talkNextBtn; // 1-1���� 1-4���� �Ѱ��ִ� ��ư
    public GameObject nextButton1; //
    public GameObject nextButton2; //
    public GameObject nextButton3; //

    public GameObject questIcon; // ����Ʈ ������
    public Button questBtn; //

    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����

    public int mainQuest;

    public GameObject systemMessagePanel; // �ý��� �޽����� �����ϴ� �г�
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' ������Ʈ

    public CanvasGroup joy;

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }

    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        systemMessagePanel.SetActive(false); // �ý��� �޽��� �г��� ��Ȱ��ȭ ���·� ����
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;

        if (mainQuest == 0) // ���� ���� �� talk1Panel�� talk2Panel�� Ȱ��ȭ BUT, mainQuest�� 0�� ����!
        {
            questIcon.SetActive(false); // ����Ʈ ������ ��ư�� ��Ȱ��ȭ ���·� ����

            talk1Panel.SetActive(true); // ó���� ���ΰ� ���� ��� 2�� ������ ��! ��� ���� ���� ��Ʈ�ѷ� �� ��Ȱ��ȭ (�Ʒ� 5��)
            CanvasGroupOff(joy);

            //dialoguePanel1.SetActive(false); // ������ �� ù ��° ���â�� ��Ȱ��ȭ ���·� ����
        }

        if (mainQuest == 1)
        {
            questIcon.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ ���·� ����

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

    }


    public void MirrorInteraction() 
    {
        mainQuest = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest;
        if (mainQuest == 0)
        {
            //GameObject.Find("MirrorTalk Canvas").transform.Find("Dialogue Panel (1)").gameObject.SetActive(true);
            dialoguePanel1.SetActive(true);
        }
        if (mainQuest == 1)
        {
            dialoguePanel2.SetActive(true);
        }
        if (mainQuest == 2)
        {
            dialoguePanel3.SetActive(true);
        }
        if (mainQuest >= 3)
        {
            CanvasGroupOn(joy);
        }
    }

    public void OnNextButton1Clicked()
    {
        SoundManager.instance.PlayAudio("Button2", "SE");

        Debug.Log("OnNextButton1Clicked() �޼��� ȣ���"); // �α� �߰�

        dialoguePanel1.SetActive(false);
        questIcon.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ
        mainQuestPanel1.SetActive(true); // ù ��° ���� ����Ʈ �г��� Ȱ��ȭ ���·� ����

        CanvasGroupOn(joy);

        // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
        string message = "����Ʈ â�� Ȱ��ȭ �Ǿ����ϴ�.";
        ActivateSystemMessagePanel(message);

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // �α� �߰�

        //// mainQuest�� 1�� ��, MainQuest Panel (1)�� Ȱ��ȭ ���·� ����
        //if (mainQuest == 1)
        //{
        //    mainQuestPanel1.SetActive(true);
        //    // �ٸ� UI ����� ���¸� ���⼭ ������ �� �ֽ��ϴ�.
        //}

    }

    public void OnNextButton2Clicked()
    {
        SoundManager.instance.PlayAudio("Button2", "SE");

        dialoguePanel2.SetActive(false);
        questIcon.SetActive(true);

        CanvasGroupOn(joy);


        //// mainQuest�� 2�� ��, MainQuest Panel (2)�� Ȱ��ȭ ���·� ����
        //if (mainQuest == 2)
        //{
        //    mainQuestPanel2.SetActive(true);
        //    // �ٸ� UI ����� ���¸� ���⼭ ������ �� �ֽ��ϴ�.
        //}

    }

    public void OnNextButton3Clicked()
    {
        SoundManager.instance.PlayAudio("Button2", "SE");

        mainQuest++;
        GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = mainQuest;
        DataManager.instance.DataSave();
        Debug.Log("mainQuest: " + mainQuest); // �α� �߰�

        dialoguePanel3.SetActive(false);
        questIcon.SetActive(true); // questIcon�� Ȱ��ȭ

        mainQuestPanel2.SetActive(false);
        mainQuestPanel3.SetActive(true);

        CanvasGroupOn(joy);


        // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
        string message = "����Ʈ�� �Ϸ��Ͽ����ϴ�.";
        ActivateSystemMessagePanel(message);

        SceneManager.LoadScene("EndingVideo");

        //// mainQuest�� 3�� ��, MainQuest Panel (3)�� Ȱ��ȭ ���·� ����
        //if (mainQuest == 3)
        //{
        //    mainQuestPanel3.SetActive(true);
        //    // �ٸ� UI ����� ���¸� ���⼭ ������ �� �ֽ��ϴ�.
        //}

    }

    public void JoyON()
    {
        CanvasGroupOn(joy);
    }

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

