using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    // ó������ ���� ��ư�� ��ȣ�ۿ� ��ư�� ���� ���� ����Ϸ��� ������
    // ���� ���� �ʿ䰡 �ֳ�? + �齺������������ ���� ��ư�� �������� ������ �ʰ� ����� ���ݾ� + ���� ������ ��ȣ�ۿ� ������ ������Ʈ�� �� ������ �� �ǰ�?
    // �� �׷��� �׳� interactionButton�� ����� �� ���� ���� ��ư������ �ذᰡ���ϵ��� ������ֽ�(by professor)
    // public GameObject attackButton; // (�⺻)���� ��ư ������ ����
    // public GameObject interactionButton; // (����)��ȣ�ۿ� ��ư ������ ����

    public int talkId; // ��ȭ ID ����

    DialogueManager dialogueManager; // DialogueManager Ŭ���� ���� ���� ����

    // �Ʒ� 2���� GPT ��信���� ���� �ֵ��ε� �ϴ� ���ֺ�
    // ����: Sphere Collider ����(���� ����) ���� �÷��̾ ������ �� <- �� �����̴ϱ� ���� ������ �־������ ������?
    // public float interactionRange = 2f; // ��ȣ�ۿ� ������ ����
    // �� �ּ�ó�� �� ����: ������Ʈ�� �ݶ��̴� �����߰�, �Ʒ��� OnTrigger �Լ��� �����ϰ� �ֱ� ����
    public bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� ����



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

            // ���� ���� �κп��� �ڷ����� Button���� GameObject�� �������־�� SetActive�� �� �� ����
            // �Ʒ� 1���� GPT���� �˷��ش�� �ڷ����� Button���� ���� ���� �Լ�����
            // ChangeButtonsState(false, true); // ���� ��ư�� ��Ȱ��ȭ�ϰ�, ��ȣ�ۿ� ��ư�� Ȱ��ȭ
            // �Ʒ� 2���� �����̰� �˷��ش�� �ڷ����� GameObject�� �������ְ�, SetActive ��� ����� ��
            // ����� ���� ���� ���߿� �� ���� ����� �����ϴٴ� ���� �˰� �ٸ� ������� �ٲ� �� �־ �������
            //dialogueManager.ChangeTalkId(talkId); // ��ȭ ID ����
            dialogueManager.SettingUI(true); // UI ����

        }
    }

    void OnTriggerExit(Collider other) // ������Ʈ�� Ʈ���Ÿ� �������� �� ȣ��Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���Ÿ� �������� ������Ʈ�� �±װ� Player�� ���
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� ������ ǥ��

            //ChangeButtonsState(true, false); // ���� ��ư�� Ȱ��ȭ�ϰ�, ��ȣ�ۿ� ��ư�� ��Ȱ��ȭ => ���� ���� ������ ����

            dialogueManager.SettingUI(false); // UI ����

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

}
