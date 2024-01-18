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
    public GameObject startBtn; // 'Dialogue StartBtn: �ſ￡ ���� ���´�'

    public GameObject dialogue1; // ù ��° ������ �� ��ȭâ
    public GameObject dialogue2; // �� ��° ������ �� ��ȭâ
    public GameObject dialogue3; // �� ��° ������ �� ��ȭâ

    // MainQuest Panels ����
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;


    public GameObject questIcon; // ����Ʈ ������

    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����

    public int mainQuest = 0;

    public GameObject systemMessagePanel; // �ý��� �޽����� �����ϴ� �г�
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' ������Ʈ


    
    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        questIcon.SetActive(false); // ����Ʈ ������ ��ư�� ��Ȱ��ȭ ���·� ����
        systemMessagePanel.SetActive(false); // �ý��� �޽��� �г��� ��Ȱ��ȭ ���·� ����
        mainQuestPanel1.SetActive(true); // ù ��° ���� ����Ʈ �г��� Ȱ��ȭ ���·� ����
    }


    void OnTriggerEnter(Collider other) // ������Ʈ�� Ʈ���ſ� ������ �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ������ ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = true; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��

            if (mainQuest == 0)
            {
                startBtn.SetActive(true);

            }

            if (mainQuest == 1)
            {
                Dialogue_2On();
            }

            if (mainQuest == 2)
            {
                Dialogue_3On();
            }

        }
    }


    void OnTriggerExit(Collider other) // ������Ʈ�� Ʈ���Ÿ� �������� �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���Ÿ� �������� ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
        }
    }


    // ��ȭâ �ݱ� ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void NextButtonOn()
    {
        if (mainQuest == 0)
        {
            Dialogue_1On();
        }

        if (mainQuest == 1)
        {
            Dialogue_2Off();
        }

        if (mainQuest == 2)
        {
            Dialogue_3Off();
        }

    }


    public void Dialogue_1On() // ��ȭâ 1�� ���� ��
    {
        dialogue1.SetActive(true); // ������ �� ù ��° ��ȭâ Ȱ��ȭ
        startBtn.SetActive(false); // ��ȣ�ۿ� ��ư ��Ȱ��ȭ
    }

    void Dialogue_1Off() // ��ȭâ 1�� ���� ��
    {
        dialogue1.SetActive(false); // ������ �� ù ��° ��ȭâ ��Ȱ��
        questIcon.SetActive(true);
        ActivateSystemMessagePanel("�ֿ� �뺻�� Ȱ��ȭ �Ǿ����ϴ�."); // �޽����� ��Ȳ�� �°� ���� ����
        mainQuest++; // ���� ����Ʈ ���� ������Ʈ

    }

    void Dialogue_2On() // ��ȭâ 2�� ���� ��
    {
        dialogue2.SetActive(true); // ������ �� �� ��° ��ȭâ Ȱ��ȭ
    }

    void Dialogue_2Off() // ��ȭâ 2�� ���� ��
    {
        dialogue2.SetActive(false); // ������ �� �� ��° ��ȭâ ��Ȱ��
    }


    void Dialogue_3On() // ��ȭâ 3�� ���� ��
    {
        dialogue3.SetActive(true); // ������ �� �� ��° ��ȭâ Ȱ��ȭ
    }

    void Dialogue_3Off() // ��ȭâ 3�� ���� ��
    {
        dialogue3.SetActive(false); // ������ �� �� ��° ��ȭâ ��Ȱ��
    }

    // �ý��� �޽��� �г��� Ȱ��ȭ�ϰ�, ������ �ð� �Ŀ� ��Ȱ��ȭ�ϴ� �޼���
    private void ActivateSystemMessagePanel(string message)
    {
        systemMessagePanel.SetActive(true);
        systemMessageText.text = message;
        StartCoroutine(ShowSystemMessage(3f)); // �ڷ�ƾ�� ����Ͽ� 3�� �� �г� ��Ȱ��ȭ
    }

    // �ý��� �޽��� �г��� ǥ���ϴ� �ڷ�ƾ
    private IEnumerator ShowSystemMessage(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // ������ �ð� ���� ���
        systemMessagePanel.SetActive(false); // �г� ��Ȱ��ȭ
    }


}

