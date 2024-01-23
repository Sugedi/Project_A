using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialoguePanels; // ��ȭ �гε��� ������ �迭
    public Button nextButton_A; // 1-2-3-4�� �Ѿ�� �ϴ� ���� ��ư
    public Button nextButton1; // 4���� ���� ���� ��ư�� ��Ȱ��ȭ�ϰ�, �� �ڸ��� �־��� ��ư
    private int currentPanelIndex = 0; // ���� Ȱ��ȭ�� ��ȭ �г��� �ε���

    void Start()
    {
        InitializeDialogue(); // ��ȭ�� �ʱ�ȭ�ϴ� �޼��带 ȣ��
        nextButton1.gameObject.SetActive(false); // nextButton1�� ������ �� ��Ȱ��ȭ�մϴ�.
        nextButton1.onClick.AddListener(EndDialogue); // nextButton1�� Ŭ�� �̺�Ʈ �����ʸ� �߰�
    }

    // ��� ��ȭ �г��� ��Ȱ��ȭ�ϰ� ù ��° �г��� Ȱ��ȭ�ϴ� �޼���
    void InitializeDialogue()
    {
        foreach (GameObject panel in dialoguePanels) // ��� ��ȭ �г��� ��ȸ�ϸ� ��Ȱ��ȭ
        {
            panel.SetActive(false);
        }
        if (dialoguePanels.Length > 0) // ��ȭ �г� �迭�� ��Ұ� �ִٸ�, ù ��° �г��� Ȱ��ȭ
        {
            dialoguePanels[0].SetActive(true);
        }
        currentPanelIndex = 0; // ���� �г� �ε����� 0���� ����
        nextButton_A.onClick.AddListener(ShowNextDialoguePanel); // ���� ��ư�� Ŭ�� �̺�Ʈ �����ʸ� �߰�
    }

    // 'Next' ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void ShowNextDialoguePanel()
    {
        if (currentPanelIndex < dialoguePanels.Length - 1) // ���� �г� �ε����� �迭�� ���̺��� �۴ٸ� ���� �гη� �Ѿ��
        {
            dialoguePanels[currentPanelIndex].SetActive(false); // ���� �г��� ��Ȱ��ȭ�ϰ�,
            currentPanelIndex++; // ���� �г��� Ȱ��ȭ
            dialoguePanels[currentPanelIndex].SetActive(true);
        }
        else
        {
            // ������ ��ȭ �г��̹Ƿ� ��ȭ�� �����ϰų� ���� ����Ʈ/�̺�Ʈ�� �Ѿ �� �ֽ��ϴ�.
            EndDialogue();
        }

        // ������ ��ȭ �гο� �������� ���� �����Դϴ�.
        if (currentPanelIndex == dialoguePanels.Length - 1)
        {
            nextButton_A.gameObject.SetActive(false); // '����' ȭ��ǥ ��ư�� ��Ȱ��ȭ�մϴ�.
            nextButton1.gameObject.SetActive(true); // nextButton1�� Ȱ��ȭ�մϴ�.
        }
    }

    // ��ȭ ���� �� ȣ��Ǵ� �޼���
    void EndDialogue()
    {
        if (currentPanelIndex == dialoguePanels.Length - 1)
        {
            dialoguePanels[currentPanelIndex].SetActive(false); // ������ ��ȭ �г��� ��Ȱ��ȭ�մϴ�.
            nextButton1.gameObject.SetActive(false); // nextButton1�� ��Ȱ��ȭ�մϴ�.
        }
        // ��ȭ ���� �� �߰����� ó���� �ʿ��ϸ� ���⿡ ������ �߰��մϴ�.
        // ��: �÷��̾� ���� Ȱ��ȭ, �� ����Ʈ ���� ��
    }
}
