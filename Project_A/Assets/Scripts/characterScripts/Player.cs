using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class Player : MonoBehaviour
{
    // 플레이어 속성값
    // 기본값을 적어줘야 할 듯
    public int gem;
    public float speed = 5; // 이동 속도 - 저장 안 함.
    public GameObject[] weapons; // 무기 배열 - 일단 저장해(Start에서 Equip해서 상관은 없을듯)
    public bool[] hasWeapons; // 보유한 무기 여부 배열 - 이것도 애매
    public Camera followCamera; // 카메라 - 안함
    public int ammo = 9999999; // 현재 총알 수량 - 이게 현재 총알인가?
    public int health; // 현재 체력 - 어차피 저장하면 회복 줌
    public int maxAmmo = 9999999; // 최대 총알 수량 - 스킬로 변동
    public int maxHealth; // 최대 체력 - 저장 해
    public Image gameOverScreen;

    // 플레이어 스킬 저장
    public List<Skill> activeSkills; // 활성화된 스킬들을 저장하는 리스트   

    // 투입 변수
    float hAxis; // 가로 축 입력값
    float vAxis; // 세로 축 입력값
    bool wDown; // 걷기 입력 여부
    bool jDown; // 점프 입력 여부
    bool fDown; // 공격 입력 여부
    bool rDown; // 재장전 입력 여부

    // 상태 변수
    bool isDodge; // 회피 상태 여부    
    bool isReload; // 재장전 상태 여부
    bool isFireReady = true; // 공격 가능 여부
    bool isBorder; // 벽과 충돌 여부
    public bool isDamage; // 데미지를 받은 상태 여부
    public bool isDead = false;

    Vector3 moveVec; // 이동 벡터
    Vector3 dodgeVec; // 회피 벡터

    // 구성 요소
    Rigidbody rigid; // Rigidbody 컴포넌트
    public Animator anim; // Animator 컴포넌트
    public SkinnedMeshRenderer[] meshs; // SkinnedMeshRenderer 배열

    // 장비 값
    GameObject nearObject; // 주변 오브젝트
    public Weapon equipWeapon; // 현재 장착된 무기
    int equipWeaponIndex = -1; // 현재 장착된 무기의 인덱스
    float fireDelay; // 공격 딜레이

    // 데이터 매니저에서 저장된 데이터를 불러오기 위한 초석
    public Datas datas;
    private string KeyName = "Datas";
    public SaveSwitch save;

    // UI ON/OFF 담당
    public CanvasGroup SaveCanvas;
    public FloatingJoystick joystick; //0122

    public CanvasGroup joy;
    public CanvasGroup continueBtn;

    // 퀘스트 아이템 획득 여부
    public GameObject systemMessagePanel; // 시스템 메시지를 포함하는 패널
    public TextMeshProUGUI systemMessageText; // 'TextMeshProUGUI' 컴포넌트
    public bool hasThreadItem = false;
    public ObjectInteraction objectInteraction;

    // 초기화 시키는 거
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        anim = GetComponentInChildren<Animator>(); // Animator 컴포넌트 가져오기
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>(); // MeshRenderer 배열 가져오기
        //EquipWeapon(0);
    }

    // 승호 추가 시작
    public void SaveHeal()
    {
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        health = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;

    }

    private void Start()
    {
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
        health = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        hasThreadItem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1BossClear;
        EquipWeapon(0);


        // 스테이지에 입장하면 위치를 저장된 세이브 위치 혹은 초기 위치로 복귀
        if (SceneManager.GetActiveScene().name == "Stage")
        {
            transform.position = datas.savePos;
            SaveCanvas = GameObject.Find("SaveSwitchCanvas").GetComponent<CanvasGroup>();
        }

        // 백스테이지에 돌아오면 저장 위치는 백스테이지 0,0,0
        else if (SceneManager.GetActiveScene().name == "Backstage_0114")
        {

            GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName = "Backstage_0114";
            transform.position = new Vector3(0f, 0f, 0f);
            DataManager.instance.DataSave();
        }
    }

    public void ChangeScene()
    {
        // 씬이 바뀌었을 때, 플레이어의 스탯을 마지막으로 저장된 값으로 변경
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
    }

    public void SkillGet()
    {
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
        health = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        EquipWeapon(0);
    }
    public void BoxGet()
    {
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
        EquipWeapon(0);
    }

    void SpaceFunction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.01f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("NPC"))
            {
                if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest == 0)
                {

                }
                else
                {
                    // 비활성화된 게임 오브젝트 찾아오기 왤케 어려움?
                    //GameObject.Find("Skill").transform.Find("SkillCanvas").gameObject.SetActive(true);
                    CanvasGroupOff(joy);
                    GameObject.Find("Workbench baked").GetComponent<SkillNPC>().Interact();

                    // 이거 비활성화 말고, 메인 메뉴에서 썼던 캔버스 그룹으로 껐다 켜는 게 나을 듯 하다.
                    // 웬만하면 비활성화 안 시키는 게 좋을지도..?
                    // 아직 잘 모르겠네
                    // + UI 창 켜졌을 때, 키마는 움직일 수 있게 되어있음. 모바일은 걱정 없겠지만...
                    // UI 뜨면 유저 이동, 공격 등 못하도록 하는 게 좋을 듯

                    //GameObject mainUI = GameObject.Find("SaveCanvas");
                }

            }

            if (collider.CompareTag("Door"))
            {
                if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest == 0)
                {

                }
                else
                {
                    GameObject.Find("door-house-simple").GetComponent<SceneMove>().Portal();
                    SoundManager.instance.PlayAudio("Door2", "SE");

                }
            }

            if (collider.CompareTag("Switch"))
            {
                if (collider.gameObject.name == "SwitchDoor")
                {
                    SoundManager.instance.PlayAudio("Door2", "SE");

                    CanvasGroupOff(joy);
                    //GameObject.Find("Switch").GetComponent<SaveSwitch>().SwitchFunc();
                    GameObject.Find("SwitchDoor").GetComponent<SaveSwitch>().SwitchFunc();
                    DataManager.instance.DataSave();
                    Time.timeScale = 0;
                    CanvasGroupOn(SaveCanvas);

                }
                if (collider.gameObject.name == "SwitchDoor2")
                {
                    CanvasGroupOff(joy);
                    //GameObject.Find("Switch").GetComponent<SaveSwitch>().SwitchFunc();
                    GameObject.Find("SwitchDoor2").GetComponent<SaveSwitch>().SwitchFunc();
                    DataManager.instance.DataSave();
                    Time.timeScale = 0;
                    CanvasGroupOn(SaveCanvas);

                }
                if (collider.gameObject.name == "SwitchDoor3")
                {
                    CanvasGroupOff(joy);
                    //GameObject.Find("Switch").GetComponent<SaveSwitch>().SwitchFunc();
                    GameObject.Find("SwitchDoor3").GetComponent<SaveSwitch>().SwitchFunc();
                    DataManager.instance.DataSave();
                    Time.timeScale = 0;
                    CanvasGroupOn(SaveCanvas);

                }
                else if (collider.gameObject.name == "SwitchDoor_Tuto")
                {
                    SoundManager.instance.PlayAudio("Door2", "SE");

                    if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial == 3)
                    {
                        CanvasGroupOff(continueBtn);
                        CanvasGroupOff(joy);
                        //GameObject.Find("Switch").GetComponent<SaveSwitch>().SwitchFunc();
                        GameObject.Find("SwitchDoor_Tuto").GetComponent<SaveSwitch>().SwitchFunc();
                        DataManager.instance.DataSave();
                        Time.timeScale = 0;
                        CanvasGroupOn(SaveCanvas);

                    }
                    else if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1Tutorial >= 4)
                    {
                        CanvasGroupOff(joy);
                        //GameObject.Find("Switch").GetComponent<SaveSwitch>().SwitchFunc();
                        GameObject.Find("SwitchDoor_Tuto").GetComponent<SaveSwitch>().SwitchFunc();
                        DataManager.instance.DataSave();
                        Time.timeScale = 0;
                        CanvasGroupOn(SaveCanvas);
                    }

                }



            }

            if (collider.CompareTag("Treasure"))
            {
                if (collider.gameObject.name == "TreasureBox1")
                {
                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }
                if (collider.gameObject.name == "TreasureBox2")
                {

                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }
                if (collider.gameObject.name == "TreasureBox3")
                {

                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }
                if (collider.gameObject.name == "TreasureBox4")
                {

                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }
                if (collider.gameObject.name == "TreasureBox5")
                {

                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }
            }

            if (collider.CompareTag("PyramidEnter"))
            {
                GameObject.Find("Player").transform.position = new Vector3(-395f, 1f, 75f);
            }

            if (collider.CompareTag("PyramidExit"))
            {
                GameObject.Find("Player").transform.position = new Vector3(-87f, 0.2f, 83f);
            }

            if (collider.CompareTag("Mirror"))
            {
                CanvasGroupOff(joy);
                GameObject.Find("MirrorGod").GetComponent<ObjectInteraction>().MirrorInteraction();
            }

            if (collider.CompareTag("Lever"))
            {
                if (collider.gameObject.name == "Lever1")
                {
                    GameObject.Find("Lever1").GetComponent<LeverController>().Lever();
                }
                if (collider.gameObject.name == "Lever2")
                {
                    GameObject.Find("Lever2").GetComponent<LeverController>().Lever();
                }
                if (collider.gameObject.name == "Lever3")
                {
                    GameObject.Find("Lever3").GetComponent<LeverController>().Lever();
                }
                if (collider.gameObject.name == "Lever4")
                {
                    GameObject.Find("Lever4").GetComponent<LeverController>().Lever();
                }
            }
            if (collider.CompareTag("Story"))
            {
                if (collider.gameObject.name == "StoryItem1")
                {
                    GameObject.Find("StoryItem1").GetComponent<StoryHintManager>().ShowHint();
                }
                if (collider.gameObject.name == "StoryItem2")
                {
                    GameObject.Find("StoryItem2").GetComponent<StoryHintManager>().ShowHint();
                }
            }

        }

    }


    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }

    //승호 추가 끝


    public void Update()
    {
        if (health <= 0 && !isDead)
        {
            Die();

            return; // 사망했으므로 여기서 Update 메서드를 종료합니다.
        }
        if (!isDead) // 사망하지 않았을 때만 입력과 액션을 처리합니다.
        {
            // 입력 처리
            GetInput();
            // 플레이어 액션
            Move();
            Turn();

            Reload();
            Dodge();

            // 모바일 조작

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.01f);
            foreach (Collider collider in hitColliders)
            {

                // 승호 - skill NPC 상호작용
                if (collider.CompareTag("NPC") || collider.CompareTag("Door") || collider.CompareTag("Switch") || collider.CompareTag("Treasure") || collider.CompareTag("Mirror") || collider.CompareTag("PyramidEnter") || collider.CompareTag("PyramidExit")|| collider.CompareTag("Lever")|| collider.CompareTag("Story"))
                {
                    // 여기 부분에 공격 버튼 이미지 -> 상호작용 버튼 이미지로 바꾸는 거 넣으면 됨.

                    if (fDown == true)
                    {
                        SpaceFunction();
                    }
                    fDown = false;
                }

            }

            // PC 조작

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    SpaceFunction();
            //}

            Attack();
        }

    }



    void GetInput()
    {
        // PC 조작 (유니티 테스트용으로 사용)

        //hAxis = Input.GetAxisRaw("Horizontal"); // 가로 축 입력값 받기
        //vAxis = Input.GetAxisRaw("Vertical"); // 세로 축 입력값 받기
        //wDown = Input.GetButton("Walk"); // 걷기 입력 여부 받기
        //jDown = Input.GetButtonDown("Jump"); // 점프 입력 여부 받기
        //fDown = Input.GetButton("Fire1"); // 공격 입력 여부 받기
        //rDown = Input.GetButtonDown("Reload"); // 재장전 입력 여부 받기

        // 모바일 조작

        // 사용법: 1. 바로 위 PC 조작 부분 주석 처리 & 모바일 조작부분 주석 제거
        //         2. 스테이지 씬(현재는 여기만 넣었음)에서 Joystick 오브젝트를 활성화하면 됨.

        hAxis = joystick.Horizontal;
        vAxis = joystick.Vertical;

    }

    public void TouchAttack()
    {
        fDown = true;

    }
    public void TouchReload()
    {
        rDown = true;

    }
    public void TouchRoll()
    {
        jDown = true;

    }

    private void LateUpdate()
    {
        fDown = false;
        rDown = false;
        jDown = false;
    }

    // 플레이어 이동
    void Move()
    {
        Vector3 cameraForward = followCamera.transform.forward;
        Vector3 cameraRight = followCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        moveVec = (cameraForward * vAxis) + (cameraRight * hAxis);

        if (isDodge)
            moveVec = dodgeVec;       

        if (!isBorder)
        {
            // Calculate the ray positions for top, middle, and bottom of the player collider
            List<Vector3> rayPositions = new List<Vector3>();
            rayPositions.Add(transform.position + Vector3.up * 0.9f); // Top
            rayPositions.Add(transform.position + Vector3.up * 0.5f); // Middle
            //rayPositions.Add(transform.position + Vector3.up * 0.1f); // Bottom

            // Check if any of the rays hit a wall within a certain scope
            if (CheckHitWall(rayPositions, moveVec))
                moveVec = Vector3.zero;
        }

        // Rest of the Move method remains unchanged
        rigid.MovePosition(rigid.position + moveVec * speed * Time.deltaTime);

        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
            rigid.MoveRotation(moveQuat);
        }

        anim.SetBool("isRun", moveVec != Vector3.zero && !isReload);
        anim.SetBool("isWalk", wDown);
    }

    public float Wallscope = 1f;
    bool CheckHitWall(List<Vector3> rayPositions, Vector3 movement)
    {


        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * Wallscope, Color.red);

            if (Physics.Raycast(pos, movement, out RaycastHit hit, Wallscope))
            {
                if (hit.collider.CompareTag("Wall"))
                    return true;
            }
        }

        return false;
    }


    // 다 돌려놔~
    void Turn()
    {
        // #1. 키보드에 의한 회전
        transform.LookAt(transform.position + moveVec); // 이동 방향으로 플레이어 회전

        // #2. 마우스에 의한 회전
        //if (fDown)
        //{
        //    Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit rayHit;
        //    if (Physics.Raycast(ray, out rayHit, 100))
        //    {
        //        Vector3 nextVec = rayHit.point - transform.position;
        //        nextVec.y = 0;
        //        transform.LookAt(transform.position + nextVec); // 마우스 위치로 플레이어 회전
        //    }
        //}
    }
    public Transform FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        EnemyWorm[] enemyWorms = FindObjectsOfType<EnemyWorm>();
        EnemyPadakmon[] enemyPadakmon = FindObjectsOfType<EnemyPadakmon>();
        EnemyDragon[] enemyDragons = FindObjectsOfType<EnemyDragon>();
        EnemyBoss[] enemyBosses = FindObjectsOfType<EnemyBoss>();
        InMonsterLongAttack[] inMonsterLongAttacks = FindObjectsOfType<InMonsterLongAttack>();
        InMonsterLongAttackPoison[] inMonsterLongAttackPoisons = FindObjectsOfType<InMonsterLongAttackPoison>();

        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        float bulletRangeSqr = Mathf.Pow(equipWeapon.bulletSpeed * 1.3f, 2);
        foreach (var enemyArray in new MonoBehaviour[][] { enemies, enemyWorms, enemyPadakmon, enemyDragons, enemyBosses, inMonsterLongAttacks, inMonsterLongAttackPoisons })
        {
            foreach (MonoBehaviour potentialTarget in enemyArray)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr && dSqrToTarget <= bulletRangeSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestTarget = potentialTarget.transform;
                }
            }
        }

        return closestTarget;
    }
    void OnDrawGizmos()
    {
        if (equipWeapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, equipWeapon.bulletSpeed * 1.3f);
        }
    }

    // 플레이어 공격 처리
    void Attack()
    {
        // 현재 장착된 무기가 없으면 공격을 수행하지 않습니다.
        if (equipWeapon == null)
            return;

        // 공격 딜레이를 증가시킵니다.
        fireDelay += Time.deltaTime;

        // 현재 딜레이가 공격 속도를 넘었는지 확인합니다. 넘었다면 공격할 준비가 된 것입니다.
        isFireReady = (1f / (equipWeapon.baseAttackSpeed * equipWeapon.attackSpeedMultiplier)) < fireDelay; // 공격 딜레이 체크

        if (fDown && isFireReady)
        {
            if (isFireReady)
            {
                CancelAttack();
            }

            if (isReload) // 재장전 중이면 재장전을 취소합니다.
            {
                CancelReload();
            }

            // 가장 가까운 적을 찾습니다.
            Transform target = FindClosestTarget();
            if (target != null)
            {
                // 가장 가까운 적을 바라봅니다.
                Vector3 targetPosition = target.position;
                transform.LookAt(targetPosition);
            }
            equipWeapon.Use(); // 무기 사용
            anim.SetTrigger("doShot"); // 무기 종류에 따라 애니메이션 설정

            SoundManager.instance.PlayAudio("Shoot2", "SE");

            fireDelay = 0; // 딜레이 초기화
        }
    }
    void CancelAttack()
    {
        // 공격 애니메이션을 취소하고 공격 가능 상태로 복귀
        isFireReady = false;
        anim.ResetTrigger("doShot");
    }

    void CancelReload()
    {
        if (isReload)
        {
            anim.SetBool("isReload", false); // 재장전 애니메이션 비활성화

            isReload = false; // 재장전 상태 해제
                              // 재장전이 취소되면 캐릭터의 속도를 원래대로 복구합니다.
            speed = 5;
            CancelInvoke("ReloadOut"); // 이미 예약된 ReloadOut 호출을 취소합니다.
        }
    }
    // 재장전
    public void Reload()
    {
        if (equipWeapon == null || ammo == 0 || isReload || equipWeapon.curAmmo == equipWeapon.maxAmmo)
            return;

        if (rDown && !isDodge && isFireReady)
        {
            anim.SetBool("isReload", true);

            SoundManager.instance.PlayAudio("Reload1", "SE");

            isReload = true; // 재장전 중 상태 설정
            speed = 2;
            float reloadTime = 1.5f * GetReloadTimeMultiplier(); // 재장전 시간을 계산합니다.
            Invoke("ReloadOut", reloadTime); // 2초 후 재장전 완료 처리
        }
    }
    float GetReloadTimeMultiplier()
    {
        // 기본적으로는 1 (즉, 재장전 시간에 변화가 없음).
        float reloadMultiplier = 1f;
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                reloadMultiplier *= skill.reloadTimeMultiplier;
            }
        }
        return reloadMultiplier;
    }
    // 재장전 처리 완료
    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo;
        ammo -= reAmmo;
        isReload = false; // 재장전 완료 후 상태 설정
        anim.SetBool("isReload", false);

        speed = 5;
        anim.SetBool("isRun", moveVec != Vector3.zero);


    }

    // 회피 구현
    void Dodge()
    {
        CancelAttack();

        if (jDown && !isDodge)
        {
            // 회피 경로에 벽이나 "RedTag" 태그가 있는지 레이캐스트로 확인
            if (Physics.Raycast(transform.position, moveVec, out RaycastHit hit, 1f))
            {
                // 벽 또는 "RedTag" 태그가 있다면 회피 취소
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("RedTag"))
                {
                    return;
                }
            }
            // 공격 중 회피 버튼이 눌리면 회피 애니메이션으로 전환합니다.
            if (jDown && isFireReady)
            {
                Dodge();
            }

            // 공격 또는 재장전 상태를 취소합니다.
            if (isFireReady)
            {
                CancelAttack();
            }

            if (isReload)
            {
                CancelReload();
            }

            dodgeVec = moveVec; // 회피 방향 설정
            speed *= 1.6f; // 이동 속도 증가
            anim.SetTrigger("doDodge"); // 회피 애니메이션 활성화

            SoundManager.instance.PlayAudio("PlayerRoll1", "SE");


            isDodge = true; // 회피 중 상태 설정
            isDamage = true; // 무적 상태 활성화

            Invoke("DodgeOut", 0.5f); // 0.5초 후 회피 완료 처리
            Invoke("EndInvulnerability", 0.3f); // 0.3초 후 무적 상태 해제
        }
    }
    
    // 무적 상태 해제
    void EndInvulnerability()
    {
        isDamage = false;
    }
    // 회피 완료 처리
    void DodgeOut()
    {
        speed /= 1.6f; // 이동 속도 원래대로 감소
        isDodge = false; // 회피 완료 후 상태 설정

        // 무적 상태가 아직 해제되지 않았다면 해제
        if (isDamage)
            isDamage = false;
    }

    // 회전 고정 함수
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    // 벽에서 멈추게 하는 함수
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        // 벽이 감지되면 isBorder를 true로 설정
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    // 물리 업데이트를 담당하는 함수
    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    // 무기 장착 메서드
    void EquipWeapon(int weaponIndex)
    {
        if (weapons[weaponIndex] != null)
        {
            // 현재 장착된 무기가 있으면 비활성화
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }

            // 새 무기 장착
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            equipWeaponIndex = weaponIndex;

            // 무기 습득 처리
            hasWeapons[weaponIndex] = true;

            // 무기에 따른 스킬 적용
            ApplySkills();

            // 현재 탄약을 최대 탄약으로 설정
            equipWeapon.curAmmo = equipWeapon.maxAmmo;

        }
    }

    // 트리거와 충돌했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 아이템과 충돌했을 때 처리
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            HeartItem heartItem = other.GetComponent<HeartItem>();
            ThreadItem threadItem = other.GetComponent<ThreadItem>();
            // 아이템 종류에 따라 처리
            if (heartItem != null)
            {
                // 체력을 회복하고 아이템을 파괴
                health = Mathf.Min(health + heartItem.healAmount, maxHealth); // 체력은 최대 체력을 넘지 않도록 합니다.
                Destroy(other.gameObject);
            }
            else if (threadItem != null)
            {
                string message = "실타래 아이템을 획득 하였습니다.";
                ActivateSystemMessagePanel(message);
                Destroy(other.gameObject);
            }
            else if (item != null)
            {
                switch (item.type)
                {
                    case Item.Type.Ammo:
                        // 총알 아이템인 경우, 총알 수량 증가
                        ammo += item.value;

                        // 총알 수가 최대치를 넘지 않도록 합니다.
                        if (ammo > maxAmmo)
                            ammo = maxAmmo;
                        break;

                    case Item.Type.Gem:
                        gem += item.value;
                        GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul = gem;
                        //DataManager.instance.DataSave();
                        break;
                }
                // 파괴하는 거
                Destroy(item.gameObject);
            }
        }
        else if (other.tag == "Weapon")
        {
            Item item = other.GetComponent<Item>();
            int weaponIndex = item.value;
            // 무기 습득 처리를 합니다.
            hasWeapons[weaponIndex] = true; // 무기 획득 처리

            // 이미 장착된 무기가 있다면 비활성화합니다.
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }
            // 새로운 무기를 장착합니다.
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            equipWeaponIndex = weaponIndex;

            ApplySkills(); // 무기를 장착했으므로, 스킬을 적용합니다.

            // 현재 탄약을 최대 탄약으로 설정합니다.
            equipWeapon.curAmmo = equipWeapon.maxAmmo;

            Destroy(other.gameObject); // 주변 오브젝트 파괴
        }

        // 적 총알과 충돌했을 때 처리
        else if (other.tag == "EnemyBullet")
        {
            if (isDead) return;

            if (!isDamage)
            {
                // 적 총알에 맞았을 때 체력 감소 및 총알 파괴
                EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
                health -= enemyBullet.damage;
                if (health < 0)
                {
                    health = 0;
                    Die();
                }
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

                // 데미지 표시 코루틴 실행
                StartCoroutine(OnDamage());


            }
        }
        else if (other.tag == "Boom")
        {
            if (isDead) return;

            if (!isDamage)
            {                
                MeteorBoom meteorBoom = other.GetComponent<MeteorBoom>();
                health -= meteorBoom.damage;
                if (health < 0)
                {
                    health = 0;
                    Die();
                }                

                // 데미지 표시 코루틴 실행
                StartCoroutine(OnDamage());


            }
        }
        // 적 총알과 충돌했을 때 처리
        else if (other.tag == "EnemyLong")
        {
            if (isDead) return;

            if (!isDamage)
            {
                // 적 총알에 맞았을 때 체력 감소 및 총알 파괴
                EnemyLongAttack enemyLong = other.GetComponent<EnemyLongAttack>();
                BoomBullet boomBullet = other.GetComponent<BoomBullet>();
                BoomBulletMeteor boomBulletMeteor = other.GetComponent<BoomBulletMeteor>();

                if (enemyLong != null)
                {
                    health -= enemyLong.damage;
                }
                else if (boomBullet != null)
                {
                    health -= boomBullet.damage;
                }
                else if (boomBulletMeteor != null)
                {
                    health -= boomBulletMeteor.damage;
                }

                if (health < 0)
                {
                    health = 0;
                    Die();
                }
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

                // 데미지 표시 코루틴 실행
                StartCoroutine(OnDamage());
            }
        }

    }
    // 넉백과 함께 피해 처리를 위해 피해량(damage) 인자를 추가합니다.
    public void GetKnockedBack(Vector3 direction, float force, int damage)
    {
        if (isDead) return;
        // 먼저 피해를 적용합니다.
        if (!isDamage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
                Die();
            }
            StartCoroutine(OnDamage());

            if (anim != null)
            {
                anim.SetTrigger("doSlip");
            }
        }

        // 그 후에 넉백 효과를 적용합니다.
        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }

    // 죽음 처리 메서드
    public void Die()
    {
        // 사망 상태를 true로 설정합니다.
        isDead = true;

        // Animator에 isDead 파라미터 설정
        anim.SetBool("isDead", isDead);

        // 사망 애니메이션 재생
        anim.SetTrigger("Die");
        SoundManager.instance.PlayAudio("Die", "SE");


        StartCoroutine(DieSequence());

    }

    IEnumerator DieSequence()
    {
        // 사망 애니메이션이 재생되는 동안 대기
        yield return new WaitForSeconds(3f);

        // 게임 오버 화면을 활성화하고 회색으로 만듭니다.
        CanvasGroupOff(joy);
        gameOverScreen.gameObject.SetActive(true);

        DataManager.instance.datas.soul /= 2;

        // 씬의 모든 움직임을 멈춥니다. (필요한 경우)
        Time.timeScale = 0;

    }
    // 데미지 표시 코루틴
    public IEnumerator OnDamage()
    {
        if (!isDead)
        {
            anim.SetTrigger("doTakeDamage");
        }
        // 피격 상태를 활성화하고 메쉬들의 색상을 파란색으로 변경
        isDamage = true; // 피격 상태 활성화

        // 깜빡임 효과를 위한 변수 설정
        float blinkDuration = 1f; // 깜빡이는 총 시간
        float blinkRate = 0.1f; // 깜빡임 간격
        float elapsedTime = 0f; // 경과 시간

        // 깜빡이는 동안의 반복 루프
        while (elapsedTime < blinkDuration)
        {
            // 메쉬들의 색상을 파란색으로 변경
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.blue;
            }

            // 깜빡임 간격만큼 대기
            yield return new WaitForSeconds(blinkRate);

            // 메쉬들의 색상을 흰색으로 변경
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }

            // 다시 깜빡임 간격만큼 대기
            yield return new WaitForSeconds(blinkRate);

            // 경과 시간 업데이트
            elapsedTime += blinkRate * 9;
        }

        // 피격 상태를 비활성화하고 메쉬들의 색상을 흰색으로 변경
        isDamage = false;
        foreach (SkinnedMeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
    }

    // 스킬을 적용하는 메서드입니다.
    public void ApplySkills()
    {
        // 장착된 무기가 없거나, 활성화된 스킬이 없는 경우 스킬을 적용하지 않습니다.
        if (equipWeapon == null || activeSkills == null || activeSkills.Count == 0)
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
        bool pierceShotActive = false; // 관통샷 스킬 활성화 여부
        bool boomShotActive = false; // 붐샷 스킬 활성화 여부
        bool sideShotActive = false; // 사이드샷 스킬 활성화 여부
        bool lightningActive = false; // 라이트닝 스킬 활성화 여부
        float lightningDamage = 0f;

        int shotGun1Bullets = 0; // 샷건1 시 발사될 추가 총알 수
        float shotGun1Angle = 0f; // 샷건1의 총알 간 각도
        int shotGun2Bullets = 0; // 샷건2 스킬 시 발사될 추가 총알 수
        float shotGun2Angle = 0f; // 샷건2 스킬의 총알 간 각도
        int shotGun3Bullets = 0; // 샷건3 스킬 시 발사될 추가 총알 수
        float shotGun3Angle = 0f; // 샷건3 스킬의 총알 간 각도
        int shotGun4Bullets = 0; // 샷건4 스킬 시 발사될 추가 총알 수
        float shotGun4Angle = 0f; // 샷건4 스킬의 총알 간 각도
        int totalAmmoIncrease = 0; // 최대 탄창 증가량을 기본 0으로 설정합니다.
        float boomShotRadius = 0f; // 붐샷 스킬 피해 범위
        float boomShotDamage = 0f; // 붐샷 스킬 피해량


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

                // 피어스샷 스킬 적용 로직
                if (skill.isPierceShot)
                {
                    pierceShotActive = true;
                }

                // 붐샷 스킬 적용 로직
                if (skill.isBoomShot)
                {
                    boomShotActive = true;
                    boomShotRadius = skill.boomShotRadius;
                    boomShotDamage = skill.boomShotDamage;
                }
                // SideShot 스킬 적용 로직
                if (skill != null && skill.isSideShot)
                {
                    sideShotActive = true;
                }
                totalAmmoIncrease += skill.ammoIncrease;

                if (skill.isLightning)
                {
                    lightningActive = true;
                    lightningDamage = skill.lightningDamage;
                }
            }
            equipWeapon.UpdateMaxAmmo(totalAmmoIncrease);
        }

        // 계산된 최대 탄창 증가량을 장착된 무기의 baseMaxAmmo에 적용합니다.
        // 이를 위해 Weapon 클래스에 baseMaxAmmo와 UpdateMaxAmmo 메서드가 정의되어 있어야 합니다.
        equipWeapon.UpdateMaxAmmo(totalAmmoIncrease);
        // 계산된 배율을 장착된 무기에 적용합니다.
        equipWeapon.damageMultiplier = totalDamageMultiplier;
        equipWeapon.attackSpeedMultiplier = totalAttackSpeedMultiplier;

        // 샷건1 스킬이 활성화되었다면, 장착된 무기에 샷건1 설정을 적용함
        equipWeapon.isShotGun1Active = shotGun1Active;
        equipWeapon.isShotGun2Active = shotGun2Active;
        equipWeapon.isShotGun3Active = shotGun3Active;
        equipWeapon.isShotGun4Active = shotGun4Active;

        // 관통샷 스킬의 활성화 여부를 무기에 전달
        equipWeapon.isPierceShotActive = pierceShotActive;

        // 장착된 무기에 붐샷 스킬 속성 설정
        equipWeapon.isBoomShotActive = boomShotActive;
        equipWeapon.boomShotRadius = boomShotRadius;
        equipWeapon.boomShotDamage = boomShotDamage;

        // 장착된 무기에 SideShot 스킬 속성 설정
        equipWeapon.isSideShotActive = sideShotActive;

        // 장착된 무기에 Lightning 스킬 속성 설정
        equipWeapon.isLightningActive = lightningActive;
        equipWeapon.lightningDamage = lightningDamage;

        if (shotGun1Active)
        {
            equipWeapon.shotGun1Bullets = shotGun1Bullets; // 발사될 총알 수
            equipWeapon.shotGun1SpreadAngle = shotGun1Angle; // 총알 간의 각도
        }
        else
        {   // 샷건1 스킬이 비활성화되면 기본값으로 재설정합니다.         
            equipWeapon.shotGun1Bullets = 0;
            equipWeapon.shotGun1SpreadAngle = 0f;
        }

        if (shotGun2Active)
        {
            equipWeapon.shotGun2Bullets = shotGun2Bullets;
            equipWeapon.shotGun2SpreadAngle = shotGun2Angle;
        }
        else
        {
            // 샷건2 스킬이 비활성화되면 기본값으로 재설정합니다.
            equipWeapon.shotGun2Bullets = 0;
            equipWeapon.shotGun2SpreadAngle = 0f;
        }

        if (shotGun3Active)
        {
            equipWeapon.shotGun3Bullets = shotGun3Bullets;
            equipWeapon.shotGun3SpreadAngle = shotGun3Angle;
        }
        else
        {
            // 샷건2 스킬이 비활성화되면 기본값으로 재설정합니다.
            equipWeapon.shotGun3Bullets = 0;
            equipWeapon.shotGun3SpreadAngle = 0f;
        }

        if (shotGun4Active)
        {
            equipWeapon.shotGun4Bullets = shotGun4Bullets;
            equipWeapon.shotGun4SpreadAngle = shotGun4Angle;
        }
        else
        {
            // 샷건2 스킬이 비활성화되면 기본값으로 재설정합니다.
            equipWeapon.shotGun4Bullets = 0;
            equipWeapon.shotGun4SpreadAngle = 0f;
        }

    }

    // 새로운 스킬을 추가하는 메서드입니다.
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

    // 트리거에 머무를 때 호출되는 함수
    void OnTriggerStay(Collider other)
    {
        // 무기와 트리거에 머물러있는 경우, 가까운 오브젝트로 설정
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    // 트리거에서 벗어날 때 호출되는 함수
    void OnTriggerExit(Collider other)
    {
        // 무기 트리거에서 벗어날 경우, 가까운 오브젝트를 null로 설정
        if (other.tag == "Weapon")
            nearObject = null;
    }
    private void ActivateSystemMessagePanel(string message)
    {
        if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest <= 2)
        {
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = 2;
            DataManager.instance.DataSave();
        }
        else if (GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest == 3)
        {
            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = 3;
            DataManager.instance.DataSave();
        }

        systemMessagePanel.SetActive(true);
        systemMessageText.text = message;
        StartCoroutine(ShowSystemMessage(3f)); // 코루틴을 사용하여 3초 후 패널 비활성화
    }

    // 시스템 메시지 패널을 표시하는 코루틴
    IEnumerator ShowSystemMessage(float displayTime)
    {
        yield return new WaitForSeconds(displayTime); // 지정된 시간 동안 대기
        systemMessagePanel.SetActive(false); // 패널 비활성화
    }
    // 아이템 값 증가 메서드
    //public void IncreaseItemValue()
    //{
    //    itemValue++;
    //    if (itemValue == 1)
    //    {
    //        objectInteraction.RefreshItemCounter(itemValue);

    //        // ObjectInteraction의 mainQuest 값을 증가
    //        if (objectInteraction != null)
    //        {
    //            objectInteraction.mainQuest++;
    //            GameObject.Find("DataManager").GetComponent<DataManager>().datas.stage1MainQuest = objectInteraction.mainQuest;
    //            DataManager.instance.DataSave();

    //            Debug.Log("mainQuest 값: " + objectInteraction.mainQuest);
    //        }
    //    }
    //}
}