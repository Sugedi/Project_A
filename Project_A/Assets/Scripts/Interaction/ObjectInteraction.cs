using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// =============================================================================================================================
// NPC ������Ʈ�� ���̴� ��ũ��Ʈ
// ��ȣ�ۿ� Ʈ���� ���� ���� �÷��̾ ���� ��, '��ȣ�ۿ� ��ư'�� ������
// 
// =============================================================================================================================

public class ObjectInteraction : MonoBehaviour
{
    // ó������ ���� ��ư�� ��ȣ�ۿ� ��ư�� ���� ���� ����Ϸ��� ������
    // ���� ���� �ʿ䰡 �ֳ�? �齺������������ ���� ��ư�� �������� ������ �ʰ� ����� ���ݾ�.
    // �� �׷��� �׳� interactionButton�� ����� �� ���� ���� ��ư������ �ذ� �����ϵ��� ������ֽ� (by professor)
    // public GameObject attackButton; // (�⺻)���� ��ư ������ ����
    // public GameObject interactionButton; // (����)��ȣ�ۿ� ��ư ������ ����

    DialogueManager dialogueManager; // DialogueManager Ŭ���� ���� ���� ����

    public GameObject button; // UI ��ư�� �Ҵ��� ����

    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����

    public GameObject dialogue1; // ���ο� UI �г� ������Ʈ
    public GameObject _button;   // ���� ��ư ������Ʈ (���� button ������ ���� �����̳�, �ٸ� ��� �������� �ʿ��� ������ _����)

    private void OnEnable() // �� ������Ʈ�� Ȱ��ȭ�� �� ȣ��Ǵ� �Լ�
    {
        if (dialogueManager == null) // dialogueManager ������ null�� ���
        {
            dialogueManager = FindObjectOfType<DialogueManager>(); // Scene���� DialogueManager Ÿ���� ������Ʈ�� ã�� ����
        }
    }


    void OnTriggerEnter(Collider other) // ������Ʈ�� Ʈ���ſ� ������ �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ������ ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = true; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
            button.SetActive(true); // ��ư�� Ȱ��ȭ
            // ���� ���� �κп��� �ڷ����� Button���� GameObject�� �������־�� SetActive�� �� �� ����
        }
    }


    void OnTriggerExit(Collider other) // ������Ʈ�� Ʈ���Ÿ� �������� �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���Ÿ� �������� ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��
            button.SetActive(false); // ��ư�� ��Ȱ��ȭ

        }
    }

    // ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnButtonPressed()
    {
        dialogue1.SetActive(true); // ���ο� �г��� Ȱ��ȭ
        _button.SetActive(false);  // ��ư�� ��Ȱ��ȭ
    }
}

// �Ʒ� �ּ�ó���� ����(ChangeButtoneState)�� GPT�� ������ ���� ���̾����� �����̰� ���� ���ʿ��ϴٰ� �˷��־� �ּ�ó����
// Sphere Collider Ʈ���ſ� ������ ���� ���� �� ���� 'ChangeButtonsState' �Լ��� ȣ���Ͽ� ��ư�� Ȱ��ȭ ���θ� �����ϴ� �Լ�
// attackButton�� ���� ��ư��, interactionButton�� ��ȣ�ۿ� ��ư�� ��Ÿ��
/* void ChangeButtonsState(bool attackButtonState, bool interactionButtonState)
{
    if (attackButton != null) 
    {
        attackButton.interactable = attackButtonState;
    }

    if (interactionButton != null) 
    {
        interactionButton.interactable = interactionButtonState;
    }
} */