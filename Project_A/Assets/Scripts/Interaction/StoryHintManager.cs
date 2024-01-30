using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StoryHintManager : MonoBehaviour
{
    public GameObject storyHint1; // StoryHint1�� Inspector���� �������ּ���.
    public GameObject[] hints; // H_1, H_2, H_3�� Inspector���� �������ּ���.
    private int currentHintIndex = 0;
    private bool isPlayerInTrigger = false; // �÷��̾ Ʈ���� ������ �ִ����� ��Ÿ���� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StoryItem1") // StoryItem1�� ���� ��
        {
            isPlayerInTrigger = true; // �÷��̾ Ʈ���� ������ ������ ǥ��
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "StoryItem1") // StoryItem1���� ��� ��
        {
            isPlayerInTrigger = false; // �÷��̾ Ʈ���� �������� ������� ǥ��
        }
    }

    public void ShowHint()
    {
        if (isPlayerInTrigger) // �÷��̾ Ʈ���� ������ ���� ����
        {
            storyHint1.SetActive(true); // StoryHint1 Ȱ��ȭ
            hints[currentHintIndex].SetActive(true); // ù ��° ��Ʈ Ȱ��ȭ
        }
    }

    public void NextHint()
    {
        hints[currentHintIndex].SetActive(false); // ���� ��Ʈ ��Ȱ��ȭ
        currentHintIndex++; // �ε��� ����

        if (currentHintIndex < hints.Length) // ���� ��Ʈ�� ����������
        {
            hints[currentHintIndex].SetActive(true); // ���� ��Ʈ Ȱ��ȭ
        }
        else // ��� ��Ʈ�� ����������
        {
            storyHint1.SetActive(false); // StoryHint1 ��Ȱ��ȭ
            currentHintIndex = 0; // ���� ������ ���� �ε��� �ʱ�ȭ
        }
    }
}
