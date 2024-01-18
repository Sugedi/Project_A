using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ����� ����ϱ� ���� �� ���ӽ����̽��� �����մϴ�
using System.Collections;
using UnityEditor.VersionControl;

public class UIManager : MonoBehaviour
{
    public GameObject dialogue1StartBtn;
    public GameObject questIconButton; // ����Ʈ ������ ��ư
    public GameObject dialogue1UI;
    public GameObject nextButton1UI;

    public GameObject systemMessagePanel; // �ý��� �޽����� �����ϴ� �г�
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' ������Ʈ
    public ObjectInteraction objectInteraction;

    // MainQuest Panels ����
    public GameObject mainQuestPanel1;
    public GameObject mainQuestPanel2;
    public GameObject mainQuestPanel3;


    //private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    //{
    //    questIconButton.SetActive(false); // ��ȭ �˾��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
    //    systemMessagePanel.SetActive(false); // �ý��� �޽��� �г��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
    //    mainQuestPanel1.SetActive(true); // ù ��° ���� ����Ʈ �г��� Ȱ��ȭ ���·� ����
    //}


    //public void OnNextButton1Clicked()
    //{
    //    dialogue1StartBtn.SetActive(false);
    //    dialogue1UI.SetActive(false);
    //    nextButton1UI.SetActive(false); // Next ��ư�� ��Ȱ��ȭ
    //    questIconButton.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ
                                         
    //    // ���⿡ ������ �����ϰ� ���� �޽����� �Է��ϼ���.
    //    string message = "�ֿ� �뺻�� Ȱ��ȭ �Ǿ����ϴ�.";

    //    objectInteraction.ActivateSystemMessagePanel(message);
    //}



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