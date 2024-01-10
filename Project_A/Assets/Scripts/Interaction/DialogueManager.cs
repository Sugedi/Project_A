using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{

    // =============================================================================================================
    // 게임 시작 시 CSV 파일에서 대화 데이터를 읽어와서 UI에 표시하는 기본적인 구조
    // =============================================================================================================

    List<Dictionary<string, object>> data_Dialog; // CSV 파일에서 읽어온 대화 데이터를 저장할 리스트

    // UI 요소들을 연결할 TextMeshProUGUI 변수들
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI name;

    public Button dialogPopupBtn;

    public GameObject joystick;
    public GameObject dialogPopup;
    
    int id; // 현재 대화의 ID

    // 데이터
    public List<string> names = new List<string>();
    public List<string> dialogs = new List<string> ();


    private void Awake() // 게임 시작 시 호출되는 Awake 함수
    {
        // "dialogue" 부분은 Reasources 폴더에 넣어둔 CSV 파일의 이름을 넣어주면 됨
        data_Dialog = CSVReader.Read("dialogue"); // CSVReader를 사용하여 대화 데이터를 읽어옴
    }

    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        id = 0;
    }

    // 대화를 표시하는 메서드
    public void CangeTalkId(int _id)
    {
        id = _id;
        dialogCount = 0;
        DialogSetting();
    }
    public void SettingUI(bool type) // 버튼 키기
    {
        dialogPopupBtn.gameObject.SetActive(type);
        
    }

    public void ShowDialog()
    {
        name.text = names[id];
        dialog.text = dialogs[id];

        dialogPopup.SetActive(true);
        joystick.SetActive(false);
    }


    void DialogSetting()
    {
        names = new List<string>();
        dialogs = new List<string>();

        for(int i = 0; i < data_Dialog.Count; i++)
        {
            if ((int)data_Dialog[i]["talkID"] == id)
            {
                names.Add(data_Dialog[i]["chaName"].ToString());
                dialogs.Add(data_Dialog[i]["description"].ToString());
            }
        }
    }

    public int dialogCount = 0;

    public void NextDialog()
    {
        dialogCount++;
        if(dialogCount >= names.Count)
        {
            dialogPopup.SetActive(false);
            dialogCount = 0;
        }
        else
        {
            name.text = names[dialogCount];
            dialog.text = dialogs[dialogCount];
        }
    }


}