using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// =============================================================================================================================
// NPC ������Ʈ�� ���̴� ��ũ��Ʈ
// ��ȣ�ۿ� Ʈ���� ���� ���� �÷��̾ ���� ��, '��ȣ�ۿ� ��ư'�� ������
// Main Quest ���� ������ ���� �ۼ��� (+ ��ȣ���� �˷��� ���� ����)
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    public GameObject button1; // 'Dialogue1 StartBtn: �ſ￡ ���� ���´�'
    public GameObject button2; // 'Dialogue2 StartBtn: �ſ￡ ���� ���´�'

    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����

    public GameObject dialogue1; // ���ο� UI �г� ������Ʈ




    void OnTriggerEnter(Collider other) // ������Ʈ�� Ʈ���ſ� ������ �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ������ ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = true; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
            button1.SetActive(true); // ��ư�� Ȱ��ȭ
            // ���� ���� �κп��� �ڷ����� Button���� GameObject�� �������־�� SetActive�� �� �� ����
        }
    }


    void OnTriggerExit(Collider other) // ������Ʈ�� Ʈ���Ÿ� �������� �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���Ÿ� �������� ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
            button1.SetActive(false); // ��ư�� ��Ȱ��ȭ

        }
    }

    // ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnButtonPressed()
    {
        dialogue1.SetActive(true); // ���ο� �г��� Ȱ��ȭ
        button1.SetActive(false);  // ��ư�� ��Ȱ��ȭ
    }








}

