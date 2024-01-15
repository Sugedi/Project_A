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
    public static int Soul;

    // 유저 스탯
    public int maxHP = 3; // 최대체력만 저장해주고, 스위치 때는 현재 체력을 최대 체력으로 회복시키기
    // public float attackDamage; 
    // public float attackSpeed;
    // public float moveSpeed;

    // 총기 & 스킬 - 스킬에서 저장해서 쓸모가 없는듯
    //public int maxBullet;
    //public float reloadTime;
    //public float range;

    // 스킬 에셋을 불러와 저장해야 함.
    public List<Skill> skillHave;

    // 여기부터는 스테이지 1만 적용
    // 귀속 아이템 획득 여부 - true값이 되면, 상자를 열린 이미지로 바꾸거나, 오브젝트 삭제
    public bool stage1ItemBox1 = false; // 심장(최대체력 증가)
    public bool stage1ItemBox2 = false; // 전시장 아이템 - true가 되면 

    // 퀘스트 진행 상황
    public int stage1Quest1Pro = 0; // 0은 미수주, 1은 미완, 2는 완료/보상미수령 3은 보상수령
    public int stage1Quest2Pro = 0;

    // 퍼즐 진행 상황 (문을 여는 등) / 숏컷이랑 통합하자
    public bool stage1Puzzle1 = false;
    public bool stage1Puzzle2 = false;

    // 보스 최초 처치 여부 (보상 지급용)
    public bool stage1BossClear = false;

    // 튜토리얼 완료 여부
    public bool stage1Tutorial = false;

    // 세이브 위치
    public Vector3 savePos = new Vector3(0, 0, 0); // 스위치에서 이 변수를 불러와 바꾼다.
    public Scene saveScene;
    public string saveSceneName = "Backstage_0114"; //일단 저장한 게 없어서 초기값

    //나중에 다 합치면 엄청 길어질 듯? 스테이지별로 쪼개거나 방법을 생각해야겠는데??
    //저장 중 원형 슬라이더 넣으면 좋을 것 같다. 로딩창처럼
    

}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    public Datas datas;  // ----------------------------------------------- A
        
    // 기본 형
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    //public GameObject stat;

    void Start()
    {
        instance = this;

        // 게임 접속 시 저장된 데이터 로드
        DataLoad();
    }


    public void DataSave() 
    {
        // 기본 형
        // Datas 클래스에 있는 모든 변수값을 저장
        var settings = new ES3Settings { memberReferenceMode = ES3.ReferenceMode.ByRefAndValue };
        ES3.Save(KeyName,datas, settings);  // <-------------------------------------- A
        //stat = GameObject.Find("Player");
        //stat.GetComponent<Player>().ChangeScene();
        GameObject.Find("Player").GetComponent<Player>().ChangeScene();
        

        // 캐릭터 세이브 위치 저장 테스트 - 이건 스위치에다가 삽입하면 될듯
        Transform playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 playerPos = playerTrans.position;

        // 자동저장 형
        //ES3AutoSaveMgr.Current.Save();
    }
    public void DataLoad()
    {
        // 이제 커스텀 클래스도 저장 불러오기가 가능합니다만, 
        // Skill 이름은 못 불러오네. 정확히 파일은 들어가있는 듯 해
        var settings = new ES3Settings { memberReferenceMode = ES3.ReferenceMode.ByRefAndValue };
        ES3.LoadInto(KeyName, datas, settings);
        //ES3.Load(KeyName, datas);

        // 캐릭터 스탯에 동기화
        //stat = GameObject.Find("Player");
        //stat.GetComponent<Player>().ChangeScene();
        GameObject.Find("Player").GetComponent<Player>().ChangeScene();

    }

    public void DataRemove()
    {
        ES3.DeleteFile(fileName);
    }


}
