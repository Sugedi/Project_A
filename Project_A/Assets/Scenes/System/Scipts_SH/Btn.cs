using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 버튼의 타입을 선언했는데... MainUI에 있는 열거형이 적용된 것 같다? 신기해
    public ButtonType currentType;
    public Transform buttonScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup ContinueGroup;
    Vector3 defaltScale;
    bool isSound;

    public Datas datas;
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    private void Start()
    {
        defaltScale = buttonScale.localScale;
        ES3.LoadInto(KeyName, datas);

        // 파일이 없으면, 이어하기 탭이 뜨지 않는다.
        if (!ES3.FileExists(fileName))
        {
            CanvasGroupOff(ContinueGroup);
            // 아직 연결 안 해 놓음.
        }
    }

    // UI 버튼 온클릭에 적용할 것
    // 버튼의 컴포넌트에 on click에 적용하면 되겠죠?
    public void OnBtnClick()
    {
        
        switch (currentType)
        {
            case ButtonType.New:

                // GameObject initialPlayerStat = GameObject.Find("DataManager");
                // 새 게임일 때에는 그냥 데이터매니저에서 기본값을 불러오면 됨
                if (ES3.FileExists(fileName))
                {
                    // 팝업창 - "저장된 데이터가 삭제됩니다. 그래도 새로 시작하시겠습니까?
                    // 예 - 팝업창 비활성
                    // 아니오 - 뉴게임
                }
                ES3.DeleteFile(fileName); // easy save 파일 데이터 삭제가 어떻게 작용하는지 모르겠음. 안 됨.
                //ES3.Save(KeyName, datas);
                //ES3.LoadInto(KeyName, datas);
                SceneManager.LoadScene("SaveTest");

                // 데이터 매니저의 기능 훔쳐오기 - 이렇게 간단한 방법이
                DataManager.instance.DataSave();

                break;
            case ButtonType.Continue:

                ES3.LoadInto(KeyName, datas);
                SceneManager.LoadScene("SaveTest");
                // 이어하기일 때에는 저장된 데이터를 불러와서 시작

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
