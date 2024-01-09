using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 구현 목표
// 스위치에서만 저장을 줘. 스위치에 도달 못하면 그 전까지 정보는 그냥 없어.
// 하지만, 튜토리얼은 최초 1회로 하자. 튜토리얼이 끝나면(마지막 버튼?) 맵 내부 datamanager에서 여부 bool값을 true로 놓자.(얘는 백스테이지에는 필요 없으니까)

// 그럼 스위치 상호작용에다가 저장할 것을 할당

// 1. 재화 획득시 -> (이건 스위치& 사망시에서만 저장, 평소에는 씬 내부 변수에 담아두기)
// 2. 귀속 아이템 획득시 - 이를 담은 아이템 상자(지금으로서는 총 또는 전시장용 아이템 정도가 있을 듯)
// 3. 퀘스트 진행 상황
// 4. 퍼즐 해결 여부
// 5. 오픈된 숏컷
// 6. 보스 최초 처치 여부 -> 스테이지 오픈과도 연결
// 7. 스탯(심장을 발견한다든지)



public class SaveDatas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
