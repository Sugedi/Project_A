using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    // 불변하는 플레이어 스탯
    public float moveSpeed = 5f; // 이동 속도 저장 안함
    public float interactionDistance = 2f;

    // 데이터 매니저에서 불러오는 플레이어 스탯
    public int maxHP;
    public float attackDamage;

    // 데이터 매니저에서 데이터를 불러오기 위한 초석
    public Datas datas;
    private string KeyName = "Datas";

    public List<Skill> activeSkills;


    private void Start()
    {
        // 게임 시작 시, 캐릭터가 저장된 자신의 스탯을 불러옴
        ES3.LoadInto(KeyName, datas);
        maxHP = datas.maxHP;
        Debug.Log(datas.maxHP);
        Debug.Log(maxHP);

        activeSkills = datas.skillHave;

        // 제에발 제발 스킬 이렇게 넣는 거여라
        // 줘패놨죠? ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ 다 된다 이거야
        // 리스트에 하나하나 추가하는 맛 미쳤다.

        /* 스킬 삽입 테스트
        Skill skillName = Resources.Load<Skill>("SpeedUp1");
        activeSkills.Add(skillName);
        Skill skillName1 = Resources.Load<Skill>("SpeedUp2");
        activeSkills.Add(skillName1);
        */
    }

    //public StageName stage; 뭐야? 내가 쓴 주석 아닌데 이거
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

    }

    public void ChangeScene()
    {
        // 씬이 바뀌었을 때, 플레이어의 스탯을 마지막으로 저장된 값으로 변경
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;

    }
    
    public void SkillGet()
    {
        ES3.LoadInto(KeyName, datas);
        Debug.Log(datas.skillHave[0]);
        activeSkills = datas.skillHave;
    }
    
}
