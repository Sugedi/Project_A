using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 세이브_1 (-32f, 0.5f, 36f)
//- 귀속 아이템
//- 습득성 스킬
//- 세이브 씬 & 세이브 위치
//- 해결한 퍼즐? 문 열어놓은 상태로 유지? - 일단 후순위로 미루자
//- 숏컷은 지금 없음
//- 보스 처치 보상? - 지금 1스테이지만 만들면 필요 없음

// UI 
//- 백스테이지로
//- 계속 하기

public class SaveSwitch : MonoBehaviour
{
    
    public Vector3 checkPoint_1 = new Vector3(-32f, 0.5f, 36f);
    public Vector3 checkPoint_2 = new Vector3(0f, 0.5f, 36f); // 아직 미정
    public string checkScene_1 = "Stage";
    public int saveNumber = 1;
    //public SavePoint savePoint = SavePoint.SavePoint_1;

    public void SaveData(int saveNumber)
    {
        DataManager dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        if (saveNumber == 1)
        {
            dataManager.datas.savePos = checkPoint_1;
            dataManager.datas.saveSceneName = checkScene_1;
            dataManager.DataSave();
        }
        else if (saveNumber == 2)
        {
            dataManager.datas.savePos = checkPoint_2;
            dataManager.datas.saveSceneName = checkScene_1;
            dataManager.DataSave();
        }


        Debug.Log("저장되었습니다.");
    }

}
