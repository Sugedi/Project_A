using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// =============================================================================================================
// �� ��ũ��Ʈ�� �÷��̾ Ư�� ������Ʈ(����Ʈ ������)�� �浹�� ��(`OnTriggerEnter`)���� �۵��մϴ�.
// �������� ������ ������ `itemValue`�� ������Ű��,
// `UIManager`�� `RefreshItemCounter` �޼��带 ȣ���Ͽ� ����Ʈ ���� ���¸� ������Ʈ�մϴ�.
// =============================================================================================================

public class QuestItemCounter : MonoBehaviour
{
    // �÷��̾ �����ؾ� �ϴ� �������� ���� ����
    public int itemValue = 0;

    private bool alreadyCollected = false; // �������� �̹� �����Ǿ����� ���θ� Ȯ���ϴ� �÷���

    // �÷��̾ ����Ʈ �����۰� �浹�� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        // ���� �̹� �������� �����ߴٸ�, �� �̻� �ڵ带 �������� ����
        if (alreadyCollected)
        {
            return;
        }

        // �������� ������ ������ itemValue�� 1 ������Ŵ
        itemValue++;

        // ���� �÷��̾ ù ��° �������� �����ߴٸ� ����Ʈ�� Ŭ�����ߴٰ� �α׸� ���
        // itemValue�� ��� �������� �ö󰡳ĸ� �����غ��� ��(++�� ���� ���Ŀ� ������ �ǰ� �����ϱ�!!)
        // Ư�� ���ǿ� ���� �߰� �۾��� ����
        if (itemValue == 1)
        {
            Debug.Log("����Ʈ Ŭ����");
            alreadyCollected = true; // �÷��׸� true�� �����Ͽ� �� �̻� OnTriggerEnter�� �۵����� �ʵ��� ��
        }
            // ���� ���� ������Ʈ(����Ʈ ������)�� ��Ȱ��ȭ�Ͽ� �� �̻� �浹���� �ʵ��� ��
            gameObject.SetActive(false);
            Debug.Log("�Ծ���~");
    }

}

