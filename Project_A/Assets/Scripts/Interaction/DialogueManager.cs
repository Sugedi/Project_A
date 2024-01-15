using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class DialogueManager : MonoBehaviour
{

    // =============================================================================================================
    // 게임 시작 시 CSV 파일에서 대화 데이터를 읽어와서 UI에 표시하는 기본적인 구조
    // =============================================================================================================

    // 
    List<Dictionary<string, object>> data_Dialog; // CSV 파일에서 읽어온 대화 데이터를 저장할 리스트

    // UI 요소들을 연결할 TextMeshProUGUI 변수들
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI name;

    public GameObject dialogPopupBtn; // Attack Button으로 설정되어 있음
    public GameObject joystick; // 조이스틱 UI 요소
    public GameObject dialogPopup; // 대화 팝업 UI 요소

    [SerializeField]
    int id; // 현재 대화의 ID

    // 데이터 (아래 두 줄의 코드는 이름과 대화 내용을 저장하기 위한 두 개의 리스트를 생성하는 것)
    public List<string> names = new List<string>(); // 대화 참가자의 이름을 저장하는데 사용됨
    public List<string> dialogs = new List<string> (); // 대화 내용을 저장하는데 사용됨

    public int dialogCount = 0; // 현재 대화 진행 상태를 나타내는 카운트 변수


    public int endTalk = 7;

    private QuestManager questManager; // 퀘스트 관리자

    public GameObject questCanvas; // Quest Canvas에 대한 참조


    private void Awake() // 게임 시작 시 호출되는 Awake 함수
    {
        // "dialogue" 부분은 Reasources 폴더에 넣어둔 CSV 파일의 이름을 넣어주면 됨
        data_Dialog = CSVReader.Read("maindialogue"); // CSVReader를 사용하여 대화 데이터를 읽어옴 -> data_Dialog로 저장!


        questManager = FindObjectOfType<QuestManager>(); // 퀘스트 관리자 찾기
        if (questManager != null)
        {
            // CSV 파일에서 퀘스트 데이터를 로드하고 QuestManager에 전달
            List<Dictionary<string, object>> questData = CSVReader.Read("mainquest"); // 가정: mainquestdata.csv 파일에서 읽어옴
            questManager.LoadQuests(questData);
        }
    }

    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        // 게임 내부 객체들의 기본 값(최초 상태)를 여기다가 전부 설정
        id = 0; // 초기 대화 ID 설정

        // 오류 발생(24-01-10) 했었는데 고침: 두 가지 방법이 있음
        // 1. 아래와 같이 게임이 시작될 때 최초로 1번 호출되는 Start 메소드에, 대화팝업창을 기본적으로 false 상태로 세팅해놓기
        // 2. 세팅 값 없이도 하이어라키 창에서 부모 객체인 talk canvas는 놔두고, 자식 객체인 dialogue만 인스펙터창에서 활성화 해제시켜놓기
        dialogPopup.SetActive(false); // 대화 팝업을 초기 상태(비활성화 상태)로 설정
    }

    // ####### 그룹 안 쓰기로 했으니까 일단 주석처리 함

    // 대화를 표시하는 메서드
    //public void ChangeTalkId(int _id)
    //{
    //    id = _id; // 대화 ID를 새로운 _id로 변경
    //    dialogCount = 0; // 대화 카운트를 초기화
    //    DialogSetting(); // 대화 설정 메서드 호출
    //}
    public void SettingUI(bool type) // UI를 설정하는 메서드 (버튼 키기)
    {
        dialogPopupBtn.gameObject.SetActive(type); // 대화 팝업 버튼의 활성화 상태를 type 값에 따라 변경
    }

    // 대화 팝업을 표시하는 메서드
    public void ShowDialog()
    {
        //DialogSetting();
        // 현재 대화 카운트(dialogCount)에 해당하는 이름과 대화 내용을 각각 name과 dialog 텍스트 UI에 설정
        //name.text = names[dialogCount];
        //dialog.text = dialogs[dialogCount];

        // 대화 팝업을 활성화하고, 조이스틱과 대화 팝업 버튼을 비활성화
        dialogPopup.SetActive(true);
        joystick.SetActive(false);
        dialogPopupBtn.SetActive(false);

        if (data_Dialog[id].ContainsKey("MainTalkID") && int.Parse(data_Dialog[id]["MainTalkID"].ToString()) == 4)
        {
            // MainTalkID가 4일 때 Quest Canvas를 활성화합니다.
            questCanvas.SetActive(true);
        }

        NextTalk();

    }

    // ####### 그룹 안 쓰기로 했으니까 일단 주석처리 함

    //void DialogSetting() // 대화 설정을 하는 메서드
    //{
    //    // 이름과 대화 리스트 초기화
    //    //names = new List<string>();
    //    //dialogs = new List<string>();


    //    // 대화 데이터에서 현재 대화 ID에 해당하는 대화를 찾아 이름과 대화 리스트에 추가
    //    for (int i = 0; i < data_Dialog.Count; i++) // for문은 (시작; 조건; 단계)로 작성됨
    //    {
    //        if ((int)data_Dialog[i]["talkID"] == id)
    //        {
    //            names.Add(data_Dialog[i]["chaName"].ToString());
    //            dialogs.Add(data_Dialog[i]["description"].ToString());
    //        }
    //    }
    //}

    // ####### 그룹 안 쓰기로 했으니까 일단 주석처리 함

    // 다음 대화를 표시하는 메서드
    //public void NextDialog()
    //{
    //    dialogCount++; // 대화 카운트 증가

    //    if (dialogCount >= names.Count)
    //    {
    //        // 모든 대화를 표시했을 경우, 대화 팝업을 비활성화하고 대화 카운트를 초기화
    //        dialogPopup.SetActive(false);
    //        dialogCount = 0;

    //        //대화 ID에 따라 다음 대화를 설정 -> 이걸 지우면 2번 그룹의 대사가 끝난 후 3번 그룹의 대사가 안뜸
    //        if (id == 2)
    //        {
    //            ChangeTalkId(3); // 대화 ID를 3으로 변경하여 대화 3을 시작
    //            ShowDialog(); // 대화 팝업을 표시
    //        }
    //    }
    //    else
    //    {
    //        // 다음 대화를 표시
    //        name.text = names[dialogCount];
    //        dialog.text = dialogs[dialogCount];
    //    }
    //}

    // 대화 ID와 대화 카운트를 검사하여 대화가 모두 끝났는지 확인하는 메서드
    public bool CheckIfDialogueIsComplete(int dialogueID)
    {
        // 대화 ID가 매개변수로 받은 dialogueID와 일치하고,
        // 대화 카운트가 현재 대화 ID의 대화 수(names 리스트의 길이) 이상이면 true를 반환합니다.
        return id == dialogueID && dialogCount >= names.Count;
    }


    public void NextTalk() // 다음 대사 나오는 함수
    {
        // 현재 대화 ID가 endTalk ID를 초과하는지 확인합니다.
        // id가 endTalk와 같으면 해당 대화가 표시되지 않고 종료되므로, '>' 연산자를 사용합니다.
        if (id > endTalk)
        {
            // 대화가 끝났으면 대화 팝업을 비활성화합니다.
            dialogPopup.SetActive(false);
            // 플레이어의 움직임이나 상호작용을 위해 조이스틱을 활성화합니다.
            joystick.SetActive(true);
            // 대화 팝업을 다시 여는 버튼을 활성화합니다(가정).
            dialogPopupBtn.SetActive(true);
        }
        else
        {
            if (id < data_Dialog.Count)  // 현재 ID가 데이터 리스트의 범위 안에 있는지 확인합니다.
            {
                // 대화에 등장하는 캐릭터 이름과 대화 텍스트를 UI에 설정합니다.
                name.text = data_Dialog[id]["MainChaName"].ToString();
                dialog.text = data_Dialog[id]["MainDescription"].ToString();

                // 다음 대화 조각이 퀘스트를 트리거하는지 확인합니다.
                // 'MainKind' 필드가 'quest'이고 대화 ID가 4일 때 첫 번째 퀘스트를 활성화합니다.
                if (data_Dialog[id].ContainsKey("MainKind") && data_Dialog[id]["MainKind"].ToString() == "quest" && id == 4)
                {
                    questManager.ActivateQuest(0); // 첫 번째 퀘스트 활성화
                }

                id++; // 다음 대화 ID로 이동
            }
            else
            {
                // 범위를 벗어났을 때의 처리를 추가합니다.
                Debug.LogError("Dialogue ID is out of range. No more dialogue entries to show.");
            }            

            // 디버깅 목적으로 현재 대화 ID를 로그에 기록합니다.
            Debug.Log("Current dialogue ID: " + id);
        }
    }
}

//else
//{
//    // 대화가 아직 끝나지 않았으면 다음 대화 조각을 표시합니다:
//    // 대화에 등장하는 캐릭터 이름을 설정합니다.
//    name.text = data_Dialog[id]["MainChaName"].ToString();
//    // 실제 대화 텍스트를 설정합니다.
//    dialog.text = data_Dialog[id]["MainDescription"].ToString();
//    // 다음 대화 조각으로 진행하기 위해 대화 ID를 증가시킵니다.
//    id++;
//}

//// 다음 대화 조각이 퀘스트를 트리거하는지 확인합니다.
//if (data_Dialog[id]["MainKind"].ToString() == "quest" &&
//    int.TryParse(data_Dialog[id]["MainquestID"].ToString(), out int mainquestID))
//{
//    // 주어진 퀘스트 ID로 퀘스트를 활성화합니다.
//    questManager.ActivateQuest(mainquestID);
//}