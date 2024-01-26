using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyDragon : MonoBehaviour
{
    public float maxHealth; // 최대 체력
    public float curHealth; // 현재 체력
    public Transform target; // 플레이어의 Transform
    public GameObject bullet; // 원거리 공격에 사용되는 총알
    public bool isChase; // 추격 상태 여부
    public bool isAttack; // 공격 상태 여부   
    public int minDropCount; // 드랍 아이템의 최소 개수
    public int maxDropCount; // 드랍 아이템의 최대 개수
    public float targetRange = 0;
    public float targetRadius = 0;
    public float attackDuration = 1f; // 공격이 지속되는 시간
    private bool isAttackHit = false; // 일반 공격이 성공적으로 적중했는지 여부
    public float attackHitCooldown = 0.5f; // 다음 일반 공격이 적중할 수 있는 쿨다운 시간
    public Camera followCamera;

    public float sightRange = 10f; // 타겟이 유저 인식

    //==============================================================
    public GameObject itemPrefab; // 드랍 아이템 프리펩 등록
    [SerializeField] Transform dropPosition; // 드랍 아이템을 생성 시킬 위치
    //==============================================================
    public Slider healthBarSlider;
    public GameObject healthBarUI;
    public float HealthBarRange = 15f; // 체력바가 활성화될 플레이어와의 거리
    Rigidbody rigid; // Rigidbody 컴포넌트
    BoxCollider boxCollider; // BoxCollider 컴포넌트
    Material mat; // Material 컴포넌트
    NavMeshAgent nav; // NavMeshAgent 컴포넌트
    Animator anim; // Animator 컴포넌트

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        nav = GetComponent<NavMeshAgent>();
        healthBarSlider.maxValue = maxHealth; // Slider의 최대값을 보스의 최대 체력으로 설정합니다.
        healthBarSlider.value = curHealth; // Slider의 현재값을 보스의 현재 체력으로 설정합니다.
        healthBarUI.SetActive(false);
    }

    void ChaseStart()
    {
        if (curHealth > 0)
        {
            isChase = true;
            anim.SetBool("isWalk", true);

            StartCoroutine(ChasePlayer());
        }
    }

    IEnumerator ChasePlayer()
    {
        while (isChase)
        {
            // NavMeshAgent가 멈춰있는 경우에만 재개
            if (!nav.isActiveAndEnabled || !nav.isOnNavMesh)
            {
                yield return null;
                continue;
            }

            // NavMeshAgent의 상태가 올바른 경우에만 목적지 설정
            nav.SetDestination(target.position);
            transform.LookAt(target);

            yield return null;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (curHealth > 0 && distanceToPlayer <= HealthBarRange)
        {
            if (!healthBarUI.activeSelf)
            {
                // 플레이어가 체력바 활성화 범위 내에 들어오면 체력바를 활성화합니다.
                healthBarUI.SetActive(true);
            }
        }
        else
        {
            if (healthBarUI.activeSelf)
            {
                // 플레이어가 체력바 활성화 범위 밖으로 나가면 체력바를 비활성화합니다.
                healthBarUI.SetActive(false);
            }
        }

        if (distanceToPlayer <= sightRange)
        {
            if (!isChase)
            {
                ChaseStart();
            }
        }
        else
        {
            if (isChase)
            {
                StopChase();
            }
        }
    }

    void StopChase()
    {
        isChase = false;
        anim.SetBool("isAttack", false);
        anim.SetBool("isWalk", false);

        isChase = false;
        anim.SetBool("isAttack2", false);
        anim.SetBool("isWalk", false);

        isChase = false;
        anim.SetBool("isAttack3", false);
        anim.SetBool("isWalk", false);

        isChase = false;
        anim.SetBool("isAttack4", false);
        anim.SetBool("isWalk", false);
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targerting()
    {
        /*

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // 플레이어가 근접 공격 범위 안에 있으면 Attack 코루틴 호출
        if (distanceToPlayer <= targetRadius)
        {
            if (!isAttack)
            {
                StartCoroutine(Attack());
            }
        }
        */

        // 플레이어를 감지하면 공격 시작
        RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position,
                targetRadius, transform.forward, targetRange,
                LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = followCamera.transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            followCamera.transform.position = new Vector3(followCamera.transform.position.x + x, followCamera.transform.position.y + y, originalPosition.z);


            elapsedTime += Time.deltaTime;

            yield return null;
        }

        followCamera.transform.position = originalPosition;
    }

    // IEnumerator를 사용한 Attack 코루틴 함수
    IEnumerator Attack()
    {
        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {           

            // 부채꼴 형태로 총알 15개 발사
            for (int i = 0; i < 15; i++)
            {
                // 총알 발사 각도를 계산합니다.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;
            }

            isAttackHit = true; // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit()); // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(1.5f);
        }

        // 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);

        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack2", true);

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // 부채꼴 형태로 총알 25개 발사
            for (int i = 0; i < 25; i++)
            {
                // 총알 발사 각도를 계산합니다.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 10), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;

                // 다음 총알 발사 전에 잠시 대기
                // yield return new WaitForSeconds(0.3f); // 이 값을 조절하여 총알 발사 간격을 변경할 수 있습니다.
            }

            isAttackHit = true; // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit()); // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(1.5f);
        }

        // 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack2", false);

        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack3", true);

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // 총알 60개를 발사하기 전에 카메라를 흔듭니다.
            StartCoroutine(ShakeCamera(0.5f, 0.5f));

            // 부채꼴 형태로 총알 50개 발사
            for (int i = 0; i < 50; i++)
            {
                // 총알 발사 각도를 계산합니다.
                Quaternion bulletRotation = Quaternion.Euler(0, -30 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;

                // 다음 총알 발사 전에 잠시 대기
                yield return new WaitForSeconds(0.15f); // 이 값을 조절하여 총알 발사 간격을 변경할 수 있습니다.
            }

            isAttackHit = true; // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit()); // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(1.5f);
        }

        // 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack3", false);

        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack4", true);

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // 부채꼴 형태로 총알 100개 발사
            for (int i = 0; i < 100; i++)
            {
                // 총알 발사 각도를 계산합니다.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;
            }

            isAttackHit = true; // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit()); // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(1.5f);
        }

        // 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack4", false);
    }

    IEnumerator ResetAttackHit()
    {
        yield return new WaitForSeconds(attackHitCooldown); // 설정한 시간 동안 대기
        isAttackHit = false; // 공격 적중 상태 초기화
    }

    // FixedUpdate 함수
    void FixedUpdate()
    {
        // 목표를 향해 이동하는 함수 호출
        Targerting();
        // 움직임을 억제하는 함수 호출
        FreezeVelocity();
    }    

    // 받는 피해 관리하는 함수

    public void TakeDamage(Bullet bullet, Vector3 hitPoint)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return;
        }

        // 라이트닝 스킬이 활성화되어 있으면 기본 데미지에 라이트닝 데미지를 합산합니다.
        float totalDamage = bullet.damage + (bullet.isLightningActive ? bullet.lightningDamage : 0);

        // 폭발 효과가 있는지 확인하고, 있으면 해당 데미지를 적용합니다.
        float damageToApply = bullet.isExplosion ? bullet.boomShotDamage : totalDamage;
        
        curHealth -= damageToApply; // Apply damage
        
        // 체력바 업데이트
        healthBarSlider.value = curHealth;
        
        // 체력이 0 이하이면 체력바 UI를 비활성화합니다.
        if (curHealth <= 0)
        {
            healthBarUI.SetActive(false);  // 체력바 비활성화
        }


        Debug.Log(gameObject.name + "가 데미지를 받았습니다. 데미지: " + damageToApply + ", 남은 체력: " + curHealth);

        Vector3 reactVec = transform.position - hitPoint;
        StartCoroutine(OnDamage(reactVec));

        if (!bullet.isPenetrating) // 관통 총알이 아니면 총알을 오브젝트 풀로 반환합니다.
        {
            bullet.ReturnToPool(); // 총알 반환
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // 몬스터가 죽었는지 확인합니다.
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return; // 몬스터가 죽었으면 아무것도 하지 않습니다.
        }

        if (other.tag == "Player")
        {
            isChase = true;
            anim.SetBool("isWalk", true);

            // 죽지 않았을 때만 체력바를 활성화합니다.
            if (curHealth > 0)
            {
                healthBarUI.SetActive(true);
            }
            else if (other.tag == "Bullet") // "Bullet"은 플레이어의 총알에 붙인 태그를 입력해주세요.
            {
                TakeDamage(other.GetComponent<Bullet>(), other.transform.position);
            }
        }        
    }

    /*

    private void OnTriggerExit(Collider other)
    {
       if (other.tag == "Player")
        {
            isChase = false;
            anim.SetBool("isWalk", false);
        }
    }

    */

    // 피격 시 발생하는 코루틴 함수
    IEnumerator OnDamage(Vector3 reactVec)
    {
        // 피격 시 일시적으로 캐릭터 색상을 빨간색으로 변경
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // 현재 체력이 0보다 큰 경우 doGetHit 애니메이션 재생
        if (curHealth > 0)
        {
            mat.color = Color.white;
            isChase = false;
            nav.enabled = false;
            // yield return new WaitForSeconds(0.5f);

            // Play doGetHit animation
            anim.SetBool("doGetHit", true);

            // Wait for the doGetHit animation to finish
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

            // Reset the doGetHit animation state
            anim.SetBool("doGetHit", false);

            isChase = true;
            nav.enabled = true;
        }
        // 현재 체력이 0 이하인 경우
        else
        {
            // 체력바를 0으로 설정
            healthBarSlider.value = 0;

            // 색상을 회색으로 변경하고 레이어를 "Dead"로 설정
            // mat.color = Color.gray;
            StopAllCoroutines();
            isChase = true;
            nav.enabled = false;

            gameObject.layer = 12; // 레이어를 변경하여 다시 공격을 받지 않도록 설정

            // 추격 중지, 네비게이션 비활성화, 죽음 애니메이션 재생
            isChase = false;
            anim.SetTrigger("doDie");
            // nav.enabled = false;

            // 피격된 방향 벡터를 정규화하고 위로 조금 이동시켜줌
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // 리지드바디에 피격 방향으로의 작은 힘을 가하고
            // rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item이라는 게임 오브젝트 변수 선언 + itemPrefab을 생성해서 _item에 할당
            GameObject _item;
            int dropCount = Random.Range(minDropCount, maxDropCount + 1); // minDropCount부터 maxDropCount까지의 랜덤한 수량
            for (int i = 0; i < dropCount; i++) // dropCount만큼 아이템을 생성합니다.
            {
                _item = Instantiate(itemPrefab); // 아이템 생성
                _item.transform.position = dropPosition.position; // 아이템 위치 설정
            }

            // 2초 뒤 몹 사망
            Destroy(gameObject, 10);
        }
    }
}