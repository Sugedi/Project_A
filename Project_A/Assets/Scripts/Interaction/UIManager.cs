using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ����� ����ϱ� ���� �� ���ӽ����̽��� �����մϴ�
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject questIconButton; // ����Ʈ ������ ��ư
    public GameObject dialogue1StartBtn;
    public GameObject dialogue1UI;
    public GameObject nextButtonUI;

    public GameObject systemMessagePanel; // �ý��� �޽����� �����ϴ� �г�
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' ������Ʈ

    // MainQuest Panels ����
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;


    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        questIconButton.SetActive(false); // ��ȭ �˾��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
        systemMessagePanel.SetActive(false); // �ý��� �޽��� �г��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
    }


    public void OnNextButtonClicked()
    {
        dialogue1StartBtn.SetActive(false);
        dialogue1UI.SetActive(false);
        nextButtonUI.SetActive(false); // Next ��ư�� ��Ȱ��ȭ
        questIconButton.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ
        ActivateSystemMessagePanel("�ֿ� �뺻�� Ȱ��ȭ �Ǿ����ϴ�."); // �޽����� ��Ȳ�� �°� ���� ����
    }

    // �ý��� �޽��� �г��� Ȱ��ȭ�ϰ�, 3�� �Ŀ� ��Ȱ��ȭ�ϴ� �޼���
    private void ActivateSystemMessagePanel(string message)
    {
        systemMessagePanel.SetActive(true);
        systemMessageText.text = message; // �޽��� ����
        StartCoroutine(ShowSystemMessagePanel(3f)); // �ڷ�ƾ ����
    }

    // �ý��� �޽��� �г��� ǥ���ϴ� �ڷ�ƾ
    private IEnumerator ShowSystemMessagePanel(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // ������ �ð� ���� ���
        systemMessagePanel.SetActive(false); // �г� ��Ȱ��ȭ
    }


    // ������ ī���͸� ������Ʈ�ϴ� �޼���
    public void RefreshItemCounter(int itemValue)
    {
        if (itemValue == 1)
        {
            mainQuestPanel1.SetActive(false);
            mainQuestPanel2.SetActive(true);
        }

        if (itemValue == 2)
        {
            mainQuestPanel2.SetActive(false);
            mainQuestPanel3.SetActive(true);
        }

    }
}