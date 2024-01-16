using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemCounter : MonoBehaviour
{

    public TextMeshProUGUI questValue;
    public int itemValue;

    public TextMeshProUGUI systemMessageUI; // �ý��� �޽����� ǥ���� UI
    public float messageDisplayTime = 5f; // �޽��� ǥ�� �ð� (��)


    // public GameObject systemMessageUI; // Inspector���� �Ҵ��� �ý��� �޽��� UI ������Ʈ // �޼����� �����°Ÿ� ������Ʈ�� ������ �� ��...?


    private void OnTriggerEnter(Collider other)
    {
        itemValue++;

        // �ý��� �޽��� ǥ��
        StartCoroutine(DisplaySystemMessage($"'{gameObject.name}'��(��) ȹ���Ͽ����ϴ�."));

        if (itemValue == 1) // itemValue�� ��� �������� �ö󰡳ĸ� �����غ��� ��(++�� ���� ���Ŀ� ������ �ǰ� �����ϱ�!!) // Ư�� ���ǿ� ���� �߰� �۾��� ����
        {
            Debug.Log("����Ʈ Ŭ����");
        }
        else
        {
            questValue.text = itemValue.ToString(); // SetActive���� false�ؼ� �ؿ� update�� ���� ���� ī���ø� �ǰ� uiâ�� ǥ��X
            gameObject.SetActive(false);
            Debug.Log("�Ծ���~");

        }

        IEnumerator DisplaySystemMessage(string message)
        {
            systemMessageUI.text = message; // �޽��� �ؽ�Ʈ ����
            systemMessageUI.gameObject.SetActive(true); // �޽��� UI Ȱ��ȭ

            // ������ �ð���ŭ ���
            yield return new WaitForSeconds(messageDisplayTime);

            systemMessageUI.gameObject.SetActive(false); // �޽��� UI ��Ȱ��ȭ
        }

        // �� ��ũ��Ʈ�� �÷��̾ Ư�� ������Ʈ(����Ʈ ������)�� �浹�� ��(`OnTriggerEnter`)���� �۵��մϴ�.
        // �������� ������ ������ `itemValue`�� ������Ű��, `UIManager`�� `RefreshItemCounter` �޼��带 ȣ���Ͽ� ����Ʈ ���� ���¸� ������Ʈ�մϴ�.
        // ����, "Ư�� ������Ʈ�� �̸��� ȹ���Ͽ����ϴ�."��� �޽����� �ý��� �޽��� UI�� 5�ʰ� ǥ���մϴ�.
        //`DisplaySystemMessage` �ڷ�ƾ�� ������ �ð�(���⼭�� 5��) ���� �޽����� ǥ���� �� �ڵ����� �޽����� ����ϴ�.


    }

}

