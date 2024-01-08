using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ============================================================================
    // 해당 스크립트는 UI 관리를 위한 스크립트입니다.
    // 상호작용 버튼을 제어하고, 대화창을 띄우는 역할을 합니다.
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
