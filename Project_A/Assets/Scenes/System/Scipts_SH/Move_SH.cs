using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    // 불변하는 플레이어 스탯
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;

    // 데이터 매니저에서 불러오는 플레이어 스탯
    public int maxHP;
    public float attackDamage;

    // 데이터 매니저에서 데이터를 불러오기 위한 초석
    public Datas datas;
    private string KeyName = "Datas";



    private void Start()
    {
        // 게임 시작 시, 캐릭터가 저장된 자신의 스탯을 불러옴
        ES3.LoadInto(KeyName, datas);
        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
        Debug.Log(datas.maxHP);
        Debug.Log(datas.attackDamage);
        Debug.Log(maxHP);

    }


    //public StageName stage;
    public bool stage_1 = true;
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(moveAmount);

        // 상호작용 키
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceFunction();
        }
    }

    void SpaceFunction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (Collider collider in hitColliders)
        {

            if (collider.CompareTag("Enemy"))
            {
                if (stage_1 == true)
                {
                    SceneManager.LoadScene("Title");
                    stage_1 = false;
                }

                else if (stage_1 == false)
                {
                    SceneManager.LoadScene("SaveTest");
                    stage_1 = true;
                }
                /*
                switch (stage)
                {
                    case StageName.Stage1:
                        SceneManager.LoadScene("Title");
                        break;
                }
                */
                // "enemy" 태그를 가진 오브젝트와 상호작용하는 코드 추가
                //SceneManager.LoadScene("Title");

            }

            if (collider.CompareTag("NPC"))
            {

                // 비활성화된 게임 오브젝트 찾아오기 왤케 어려움?
                //GameObject.Find("Skill").transform.Find("SkillCanvas").gameObject.SetActive(true);

                GameObject.Find("SkillNPC").GetComponent<SkillNPC>().Interact();

                // 이거 비활성화 말고, 메인 메뉴에서 썼던 캔버스 그룹으로 껐다 켜는 게 나을 듯 하다.
                // 웬만하면 비활성화 안 시키는 게 좋을지도..?
                // 아직 잘 모르겠네
                // + UI 창 켜졌을 때, 키마는 움직일 수 있게 되어있음. 모바일은 걱정 없겠지만...
                // UI 뜨면 유저 이동, 공격 등 못하도록 하는 게 좋을 듯

                //GameObject mainUI = GameObject.Find("SaveCanvas");
            }
        }

        // 상호작용 시마다 데이터를 불러와 플레이어 몸에 적용해준다. 개꿀~
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
    }

    public void ChangeScene()
    {
        // 씬이 바뀌었을 때, 플레이어의 스탯을 마지막으로 저장된 값으로 변경
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
    }

    // 스킬 적용 테스트
    public List<Skill> activeSkills;


    public void ApplySkills()
    {
        // 장착된 무기가 없거나, 활성화된 스킬이 없는 경우 스킬을 적용하지 않습니다.
        if (activeSkills == null || activeSkills.Count == 0)
        {
            return;
        }

        // 데미지와 공격 속도 배율을 기본 1로 설정합니다.
        float totalDamageMultiplier = 1f;
        float totalAttackSpeedMultiplier = 1f;
        bool shotGun1Active = false; // 샷건1 스킬 활성화 여부
        bool shotGun2Active = false; // 샷건2 스킬 활성화 여부
        bool shotGun3Active = false; // 샷건3 스킬 활성화 여부
        bool shotGun4Active = false; // 샷건4 스킬 활성화 여부
        int shotGun1Bullets = 0; // 샷건1 시 발사될 추가 총알 수
        float shotGun1Angle = 0f; // 샷건1의 총알 간 각도
        int shotGun2Bullets = 0; // 샷건2 스킬 시 발사될 추가 총알 수
        float shotGun2Angle = 0f; // 샷건2 스킬의 총알 간 각도
        int shotGun3Bullets = 0; // 샷건3 스킬 시 발사될 추가 총알 수
        float shotGun3Angle = 0f; // 샷건3 스킬의 총알 간 각도
        int shotGun4Bullets = 0; // 샷건4 스킬 시 발사될 추가 총알 수
        float shotGun4Angle = 0f; // 샷건4 스킬의 총알 간 각도

        // 활성화된 스킬들을 순회하며 데미지 및 공격 속도 배율을 계산합니다.
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                totalDamageMultiplier *= skill.damageMultiplier;
                totalAttackSpeedMultiplier *= skill.attackSpeedMultiplier;

                // 샷건1 스킬 활성화 여부를 체크하고 관련 값을 설정함
                if (skill.isShotGun1)
                {
                    shotGun1Active = true;
                    shotGun1Bullets = skill.shotGun1Count;
                    shotGun1Angle = skill.shotGun1SpreadAngle;
                }

                if (skill.isShotGun2)
                {
                    shotGun2Active = true; // 샷건2 스킬을 활성화 상태로 설정
                    shotGun2Bullets = skill.shotGun2Count; // 발사될 총알 수
                    shotGun2Angle = skill.shotGun2SpreadAngle; // 총알 간의 각도
                }

                if (skill.isShotGun3)
                {
                    shotGun3Active = true; // 샷건3 스킬을 활성화 상태로 설정
                    shotGun3Bullets = skill.shotGun3Count; // 발사될 총알 수
                    shotGun3Angle = skill.shotGun3SpreadAngle; // 총알 간의 각도
                }

                if (skill.isShotGun4)
                {
                    shotGun4Active = true; // 샷건4 스킬을 활성화 상태로 설정
                    shotGun4Bullets = skill.shotGun4Count; // 발사될 총알 수
                    shotGun4Angle = skill.shotGun4SpreadAngle; // 총알 간의 각도
                }

            }
        }


    }

    public void AddSkill(Skill newSkill)
    {
        activeSkills.Add(newSkill); // 스킬 리스트에 새 스킬을 추가합니다.
        ApplySkills(); // 스킬 추가 후 스킬을 적용합니다.

    }

    // 스킬을 제거하는 메서드입니다.
    public void RemoveSkill(Skill skillToRemove)
    {
        activeSkills.Remove(skillToRemove); // 스킬 리스트에서 지정된 스킬을 제거합니다.
        ApplySkills(); // 스킬 제거 후 스킬을 다시 적용합니다.
    }

}
