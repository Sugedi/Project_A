using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ============================================================================
    // �ش� ��ũ��Ʈ�� UI ������ ���� ��ũ��Ʈ�Դϴ�.
    // ��ȣ�ۿ� ��ư�� �����ϰ�, ��ȭâ�� ���� ������ �մϴ�.
    // ============================================================================

    public static UIManager Instance;

    public Button interactionButton;
    public GameObject dialoguePanel;

    private void Awake()
    {
        Instance = this;
    }

    public void EnableInteractionButton() 
    {
        interactionButton.gameObject.SetActive(true);
    }

    public void DisableInteractionButton() 
    {
        interactionButton.gameObject.SetActive(false);
    }

    public void ShowDialoguePanel() 
    {
        dialoguePanel.SetActive(true); 
    }

    public void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

}
