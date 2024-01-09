using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class DataManager_hyunji: MonoBehaviour
{

    // =============================================================================================================
    // 게임 시작 시 CSV 파일에서 대화 데이터를 읽어와서 UI에 표시하는 기본적인 구조
    // =============================================================================================================

   /* List<Dictionary<string, object>> data_Dialog; // CSV 파일에서 읽어온 대화 데이터를 저장할 리스트

    // UI 요소들을 연결할 TextMeshProUGUI 변수들
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI name;
    int id; // 현재 대화의 ID

    private void Awake() // 게임 시작 시 호출되는 Awake 함수
    {
        data_Dialog = CSVReader.Read("dialogue"); // CSVReader를 사용하여 대화 데이터를 읽어옴
    }

    private void Start() // 게임 시작 시 호출되는 Start 함수
    {
        // 대화 데이터를 확인하기 위한 주석 처리된 코드
        //for (int i = 0; i < data_Dialog.Count; i++)
        //{
        //    print(data_Dialog[i]["Content"].ToString());
        //}

        id = 0; // 초기 대화 ID를 0으로 설정

        // 대화와 캐릭터 이름을 UI 텍스트에 표시
        dialog.text = (string)data_Dialog[id]["dialogue"];
        name.text = (string)data_Dialog[id]["chaName"];

        id++; // 다음 대화 ID로 이동

        // 다음 대화와 캐릭터 이름을 UI 텍스트에 표시
        dialog.text = (string)data_Dialog[id]["dialogue"];
        name.text = (string)data_Dialog[id]["chaName"];
    } */
}