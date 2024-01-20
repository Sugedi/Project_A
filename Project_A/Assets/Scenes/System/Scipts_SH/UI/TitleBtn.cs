using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleBtn : MonoBehaviour
{
    // 버튼의 타입을 선언했는데... MainUI에 있는 열거형이 적용된 것 같다? 신기해
    public ButtonType currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup DataResetGroup;
    public CanvasGroup NO1;
    public CanvasGroup NO2;
    bool isSound;

    // instance로 불러오면서 필요 없어짐. 혹시 기능 동작 안 할 것을 대비하여 살려놓을게
    // public Datas datas;
    // private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    public Datas datas;
    private Player player;

    private void Start()
    {

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

    }

    // UI 버튼 온클릭에 적용할 것


    public void OnBtnClick()
    {
        
        switch (currentType)
        {
            // MainUI에서 New, Continue를 GameStart로 통합하고, 여기에도 반영 시킵니다. 
            // 디테일을 주면, TMPro를 (!ES3.FileExists(fileName))에 따라 스타트에서 변경합니다.
            case ButtonType.New:

                // 걍 데이터 있을 때에는 영영 다시 시작할 수 없게 만들자. 오류생기는 거 열받음 시발 ㅓㅁ노이ㅏ러몬아ㅣ퍼ㅗ뮤내
                // 첫 스테이지의 위치로 이동
                // DontDestroy 때문에 플레이어를 못 읽어와서 추가해줘야 함.


                // 1번 방법
                //GameObject.Find("Player ").transform.position = datas.savePos; //new Vector3(10, 10, 10);
                //SceneManager.LoadScene("Backstage_0114");

                //2번 방법
                //Vector3 lastPos = datas.savePos;
                string lastScene = GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName;
                SceneManager.LoadScene(lastScene); // 저장된 씬으로 이동
                
                // 맵 위치 저장 즉, 세이브 스위치에서 해당 코드 주기
                // string a = SceneManager.GetActiveScene().name;

                break;

            case ButtonType.Option:

                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("옵션");
                break;

            case ButtonType.Sound:
                // 온 오프 기능 말고, 사운드 별로 슬라이드를 만들 것
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

            case ButtonType.DataResetUI:
                CanvasGroupOn(DataResetGroup); 

                break;
            case ButtonType.DataReset:
                // 1- 진짜 삭제?
                // 2- 삭제 되었습니다.
                if (NO1.alpha == 1) // 1만 켜짐, 2는 꺼짐
                {
                    CanvasGroupOn(NO2);
                    CanvasGroupOff(NO1);
                    DataManager.instance.DataRemove();
                }
                else if (NO1.alpha == 0) // 삭제되었고, 이제 창 끄기
                {
                    CanvasGroupOff(DataResetGroup);
                    CanvasGroupOff(NO2);
                    CanvasGroupOn(NO1);
                }


                break;
            case ButtonType.DataResetBack:
                CanvasGroupOff(DataResetGroup);
                CanvasGroupOff(NO2);
                CanvasGroupOn(NO1);

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

}
