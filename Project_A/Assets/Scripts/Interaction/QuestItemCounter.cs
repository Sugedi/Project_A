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

    //UIManager�� ���� ���� : �ش� ��ũ��Ʈ���� ������ ���� �������� �� 'UIManager'�� �޼��带 ȣ���Ͽ� ����Ʈ ����
    public UIManager uiManager;

    // �������� �̹� �����Ǿ����� ���θ� Ȯ���ϴ� �÷���
    // �ۼ��� ����: �÷��̾ �ݶ��̴��� 2�� �����Ͽ� �������� 1�� ������ 2���� ī���� ��
    private bool alreadyCollected = false; 

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

        // itemValue�� ��� �������� �ö󰡳ĸ� �����غ��� ��(++�� ���� ���Ŀ� ������ �ǰ� �����ϱ�!!)
        // Ư�� ���ǿ� ���� �߰� �۾��� ����
        if (itemValue == 1)
        {
            alreadyCollected = true; // �÷��׸� true�� �����Ͽ� �� �̻� OnTriggerEnter�� �۵����� �ʵ��� ��
            gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            Debug.Log("�Ծ���~");

            uiManager.RefreshItemCounter(itemValue); // UIManager�� �޼��� ȣ��

        }
        //// ���� ���� ������Ʈ(����Ʈ ������)�� ��Ȱ��ȭ�Ͽ� �� �̻� �浹���� �ʵ��� ��
        //gameObject.SetActive(false);
        //    Debug.Log("�Ծ���~");
    }

}

