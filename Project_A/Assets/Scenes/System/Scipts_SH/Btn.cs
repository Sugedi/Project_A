using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 버튼의 타입을 선언했는데... MainUI에 있는 열거형이 적용된 것 같다? 신기해
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

    // UI 버튼 온클릭에 적용할 것
    // 버튼의 컴포넌트에 on click에 적용하면 되겠죠?
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.New:
                Debug.Log("새 게임");
                break;
            case ButtonType.Continue:
                Debug.Log("이어 하기");
                break;
            case ButtonType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("옵션");
                break;
            case ButtonType.Sound:
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("사운드 OFF");
                }
                else
                {
                    isSound = true;
                    Debug.Log("사운드 ON");
                }
                break;
            case ButtonType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                Debug.Log("뒤로 가기");
                break;
            case ButtonType.Quit:
                Application.Quit();
                Debug.Log("게임 종료");
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
