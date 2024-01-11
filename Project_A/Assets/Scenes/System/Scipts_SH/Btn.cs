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
    //public CanvasGroup newGroup;
    //public CanvasGroup continueGroup; // 이어하기 버튼 상위에 캔버스 그룹 만들어주자.
    Vector3 defaltScale;
    bool isSound;

    // instance로 불러오면서 필요 없어짐. 혹시 기능 동작 안 할 것을 대비하여 살려놓을게
    // public Datas datas;
    // private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    private void Start()
    {
        defaltScale = buttonScale.localScale;

        // 데이터 매니저의 기능 훔쳐오기 - 이렇게 간단한 방법이

        // 파일이 없으면, 이어하기 탭이 뜨지 않는다.
        if (!ES3.FileExists(fileName))
        {
            // 초기 저장 데이터 생성
            DataManager.instance.DataSave();
            //CanvasGroupOff(continueGroup);
            // 아직 연결 안 해 놓음.
        }
        else
        {
            //CanvasGroupOff(newGroup);
        }
    }

    // UI 버튼 온클릭에 적용할 것
    // 버튼의 컴포넌트에 on click에 적용하면 되겠죠?
    public void OnBtnClick()
    {
        
        switch (currentType)
        {
            // MainUI에서 New, Continue를 GameStart로 통합하고, 여기에도 반영 시킵니다. 
            // 디테일을 주면, TMPro를 (!ES3.FileExists(fileName))에 따라 스타트에서 변경합니다.
            case ButtonType.New:

                // 걍 데이터 있을 때에는 영영 다시 시작할 수 없게 만들자. 오류생기는 거 열받음 시발 ㅓㅁ노이ㅏ러몬아ㅣ퍼ㅗ뮤내
                // 첫 스테이지의 위치로 이동
                SceneManager.LoadScene("SaveTest");
                break;

            case ButtonType.Continue:

                // DataManager.instance.DataLoad(); -> 데이터 매니저가 항상 로드합니다. 걱정마세요.
                // 모든 씬에는 데이터 매니저가 함께 합니다.
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
