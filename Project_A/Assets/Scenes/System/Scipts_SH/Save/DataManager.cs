using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 이제 데이터 저장한 걸, 플레이어한테 연결하는 기능 해야함

// 스테이지 1 내부의 저장해야할 데이터를 담은 스크립트

// 구현 목표
// 스위치에서만 저장을 줘. 스위치에 도달 못하면 그 전까지 정보는 그냥 없어.
// 근데 퍼즐, 퀘스트는 다시하기엔 지루할 것 같다. 좀 더 생각해볼것 - 퀘스트는 완료시에 저장 주자. 다시하는 거 별로

// 하지만, 튜토리얼은 최초 1회로 하자. 튜토리얼이 끝나면(마지막 버튼?) 맵 내부 datamanager에서 여부 bool값을 true로 놓자.(얘는 백스테이지에는 필요 없으니까)

// 그럼 스위치 상호작용에다가 저장할 것을 할당

// 1. 재화 획득시 -> (이건 스위치& 사망시에서만 저장, 평소에는 씬 내부 변수에 담아두기)
// 2. 스탯(심장을 발견한다든지)
// 3. 스킬 레벨
// 4. 귀속 아이템 획득시 - 이를 담은 아이템 상자(지금으로서는 총 또는 전시장용 아이템 정도가 있을 듯)
// 5. 퀘스트 진행 상황
// 6. 퍼즐 해결 여부 + 7. 오픈된 숏컷
// 8. 보스 최초 처치 여부 -> 스테이지 오픈과도 연결
// 9. 튜토리얼 완료 여부


// 씬 전환했을 때 연구
// 서로 씬을 불러왔을 때 뭐가 달라지는지?
// 가면 씬에서 다시 불러오는 게 있어야 할텐데.
// 어떻게 불러올까?



[System.Serializable]
public class Datas
{
    // 재화
    public int soul = 0;

    // 유저 스탯
    public int maxHP = 100; // 최대체력만 저장해주고, 스위치 때는 현재 체력을 최대 체력으로 회복시키기

    // 스킬 에셋을 불러와 저장해야 함.
    public List<Skill> skillHave;

    // 여기부터는 스테이지 1만 적용
    // 귀속 아이템 획득 여부 - true값이 되면, 상자를 열린 이미지로 바꾸거나, 오브젝트 삭제
    public bool stage1ItemBox1 = false; // 심장(최대체력 증가)
    public bool stage1ItemBox2 = false; 
    public bool stage1ItemBox3 = false; 

    // 퀘스트 진행 상황
    public int stage1MainQuest = 0; // 0은 미수주, 1은 미완, 2는 완료/보상미수령 3은 보상수령
    // public int stage1Quest2Pro = 0; //현재 미사용

    // 보스 최초 처치 여부 (보상 지급용)
    public bool stage1BossClear = false;

    // 튜토리얼 완료 여부
    public int stage1Tutorial = 0;

    // 세이브 위치
    public Vector3 savePos = new Vector3(0, 0, 0); // 스위치에서 이 변수를 불러와 바꾼다.
    public string saveSceneName = "Backstage_0114"; //일단 저장한 게 없어서 초기값

}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    public Datas datas;  // ----------------------------------------------- A
        
    // 기본 형
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    //public GameObject stat;

    private void Awake()
    {
        // 데이터 폴더에 아무 파일이 없다면, 데이터 파일 생성
        if (!ES3.FileExists(fileName))
        {
            // 초기 저장 데이터 생성
            DataSave();
        }
        DataLoad();
    }

    public void Start()
    {
        instance = this;
    }


    public void DataSave() 
    {
        // 기본 형
        // Datas 클래스에 있는 모든 변수값을 저장 - 더 효율적으로는 Datas의 일부를 저장하면 좋긴한데...
        // 사용자 클래스를 저장하기 위해 저장 세팅 변경
        var settings = new ES3Settings { memberReferenceMode = ES3.ReferenceMode.ByRefAndValue };
        ES3.Save(KeyName, datas, settings);
        
        // 저장 시, 캐릭터 오브젝트에 해당 정보를 업데이트
        GameObject.Find("Player").GetComponent<Player>().ChangeScene();

    }
    public void DataLoad()
    {
        // 이제 커스텀 클래스도 저장 불러오기가 가능합니다만, 
        // Skill 이름은 못 불러오네. 정확히 파일은 들어가있는 듯 해
        var settings = new ES3Settings { memberReferenceMode = ES3.ReferenceMode.ByRefAndValue };
        ES3.LoadInto(KeyName, datas, settings);
        // ES3.Load(KeyName, datas);
        // Load, Loadinto의 차이점을 잘 모르겠다.

        // 캐릭터 스탯에 동기화
        GameObject.Find("Player").GetComponent<Player>().ChangeScene();

    }

    // 데이터 초기화 또는 새 게임 기능에 넣을 수 있음. 옵션에서 선택할 수 있게 할까? - 테스트 용도로도 좋음.
    public void DataRemove()
    {
        ES3.DeleteFile(fileName);
    }

    // 현재 사용되는 곳 없음. 근데, 전수 조사는 못해서 남겨놓음.
    public List<Skill> tempList;
}
