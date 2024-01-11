using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트는 스킬 데이터를 정의하기 위한 ScriptableObject입니다.
// ScriptableObject는 데이터 컨테이너 역할을 하여 게임 오브젝트에 붙지 않고도 데이터를 저장할 수 있습니다.
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]

// CreateAssetMenu :이 어트리뷰트를 사용하면 유니티 에디터의 메뉴에 새로운 옵션을 추가할 수 있습니다.
// fileName : 생성될 새 에셋의 기본 파일 이름을 지정합니다. 변경가능
// menuName : 유니티 에디터의 메뉴에서 이 에셋을 생성할 수 있는 옵션의 이름을 지정합니다.

public class Skill : ScriptableObject
{
    // 스킬의 이름을 저장합니다.
    // 예를 들어 "Power Boost", "Speed Surge" 등 스킬의 이름을 여기에 저장합니다.
    public string skillName;

    // 스킬이 적용될 때 플레이어 또는 무기의 데미지에 곱해지는 배율입니다.
    // 실제 스킬에 적용할 배율은 여기서 적는게 아니고 에셋을 만들어서 그거에 적음
    public float damageMultiplier = 1f;

    // 스킬이 적용될 때 플레이어 또는 무기의 공격 속도에 곱해지는 배율입니다.
    // 실제 스킬에 적용할 배율은 여기서 적는게 아니고 에셋을 만들어서 그거에 적음
    public float attackSpeedMultiplier = 1f;

    // 재장전 시간 배율을 추가합니다.
    public float reloadTimeMultiplier = 1f;

    // 산탄 사격 스킬을 위한 새로운 속성
    public bool isBuckShot = false; // 산탄 사격 스킬 활성화 여부입니다.
    public int buckShotCount = 3; // 한 번에 발사되는 총알의 수입니다.
    public float buckShotSpreadAngle = 30f; // 총알 사이의 각도입니다.

    // 다리부수기 스킬 속성
    public bool isLegBreak = false; // 다리부수기 스킬 활성화 여부
    public int legBreakCount = 5; // 발사될 총알의 수
    public float legBreakSpreadAngle = 45f; // 총알 사이의 각도    
}