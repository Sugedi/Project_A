using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;

    public int maxHP;
    public float attackDamage;

    // 데이터 묶음?을 저장하는 공간 선언
    public Datas datas;
    private string KeyName = "Datas";

    //public GameObject skillUI;
    //public GameObject mainUI;

    private void Awake()
    {
        
        // 현재 저장된 플레이어 스탯을 불러오고 싶어
        //playerStat = GameObject.Find("DataManager");
        //datas = playerStat.GetComponent<DataManager>().datas;
    }
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
                Debug.Log("여기까지1");

                // 비활성화된 게임 오브젝트 찾아오기 왤케 어려움?
                GameObject.Find("Skill").transform.Find("SkillCanvas").gameObject.SetActive(true);
                Debug.Log("여기까지2");

                // 이거 비활성화 말고, 메인 메뉴에서 썼던 캔버스 그룹으로 껐다 켜는 게 나을 듯 하다.
                // 웬만하면 비활성화 안 시키는 게 좋을지도..?
                // 아직 잘 모르겠네
                // + UI 창 켜졌을 때, 키마는 움직일 수 있게 되어있음. 모바일은 걱정 없겠지만...
                // UI 뜨면 유저 이동, 공격 등 못하도록 하는 게 좋을 듯

                GameObject mainUI = GameObject.Find("SaveCanvas");
                mainUI.gameObject.SetActive(false);
                Debug.Log("여기까지3");
            }
        }

        // 상호작용 시마다 데이터를 불러와 플레이어 몸에 적용해준다. 개꿀~
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
    }
}
