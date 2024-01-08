using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��ư�� Ÿ���� �����ߴµ�... MainUI�� �ִ� �������� ����� �� ����? �ű���
    public ButtonType currentType;
    public Transform buttonScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    Vector3 defaltScale;
    bool isSound;

    private void Start()
    {
        defaltScale = buttonScale.localScale;
    }

    // UI ��ư ��Ŭ���� ������ ��
    // ��ư�� ������Ʈ�� on click�� �����ϸ� �ǰ���?
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.New:
                Debug.Log("�� ����");
                break;
            case ButtonType.Continue:
                Debug.Log("�̾� �ϱ�");
                break;
            case ButtonType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("�ɼ�");
                break;
            case ButtonType.Sound:
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("���� OFF");
                }
                else
                {
                    isSound = true;
                    Debug.Log("���� ON");
                }
                break;
            case ButtonType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                Debug.Log("�ڷ� ����");
                break;
            case ButtonType.Quit:
                Application.Quit();
                Debug.Log("���� ����");
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaltScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaltScale;
    }
}
