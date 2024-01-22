using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDragon : MonoBehaviour
{
    public float maxHealth; // 최대 체력
    public float curHealth; // 현재 체력
    public Transform target; // 플레이어의 Transform
    public GameObject bullet; // 원거리 공격에 사용되는 총알
    public GameObject bullet2; // 원거리 공격에 사용되는 총알
    public GameObject bullet3; // 원거리 공격에 사용되는 총알
    public bool isChase; // 추격 상태 여부
    public bool isAttack; // 공격 상태 여부   
    public int minDropCount; // 드랍 아이템의 최소 개수
    public int maxDropCount; // 드랍 아이템의 최대 개수
    public float targetRange = 0;
    public float targetRadius = 0;
    public float attackDuration = 1f; // 공격이 지속되는 시간
    private bool isAttackHit = false; // 일반 공격이 성공적으로 적중했는지 여부
    public float attackHitCooldown = 0.5f; // 다음 일반 공격이 적중할 수 있는 쿨다운 시간

    public GameObject projectilePrefab; // 투사체 프리팹
    public Transform firePoint; // 발사 위치
    public float projectileSpeed = 5f; // 투사체 속도
    public int numberOfProjectiles = 12; // 투사체 개수
    public float spreadAngle = 360f; // 투사체 각도 범위

    public float sightRange = 10f; // 타겟이 유저 인식

    bool isReturningToInitialPosition; // 몬스터가 초기 위치로 돌아가고 있는지 여부를 나타내는 변수

    //==============================================================
    public GameObject itemPrefab; // 드랍 아이템 프리펩 등록
    [SerializeField] Transform dropPosition; // 드랍 아이템을 생성 시킬 위치
    //==============================================================

    Rigidbody rigid; // Rigidbody 컴포넌트
    BoxCollider boxCollider; // BoxCollider 컴포넌트
    Material mat; // Material 컴포넌트
    NavMeshAgent nav; // NavMeshAgent 컴포넌트
    Animator anim; // Animator 컴포넌트

    Vector3 initialPosition; // 몬스터의 초기 위치 저장 변수

    void Start()
    {
        StartCoroutine(FireProjectiles());
    }

    IEnumerator FireProjectiles()
    {
        float angleStep = spreadAngle / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = rotation * Vector2.right * projectileSpeed;

            yield return new WaitForSeconds(0.1f); // 투사체 간 딜레이
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();
        initialPosition = transform.position; // 초기 위치 저장

        nav = GetComponent<NavMeshAgent>();

        // Invoke("ChaseStart", 1);  // 1초 뒤에 추격 시작
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

        // 초기 위치로 돌아가기
        // StartCoroutine(ReturnToInitialPosition());
    }

    /* IEnumerator ReturnToInitialPosition()
    {
        isReturningToInitialPosition = true; // 초기 위치로 돌아가는 중임을 표시

        if (!nav.enabled)
            nav.enabled = true;

        if (!nav.isOnNavMesh)
        {
            // NavMesh에 없는 경우 초기 위치를 목적지로 설정
            nav.Warp(initialPosition); // 이 부분 수정
        }

        // isWalk 애니메이션 재생
        if (!anim.GetBool("isWalk") && !anim.GetCurrentAnimatorStateInfo(0).IsName("isWalk"))
        {
            // "isWalk" 애니메이션이 재생 중이 아니라면 재생
            anim.SetBool("isWalk", true);
        }

        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            nav.SetDestination(initialPosition);
            yield return null;
        }

        nav.isStopped = true;
        transform.position = initialPosition;

        isChase = true;

        // isWalk 애니메이션 종료
        anim.SetBool("isWalk", false);

        // 초기 위치로 돌아갔을 때 플레이어가 다시 인식 범위 안으로 들어오면 추격 재개
        while (Vector3.Distance(transform.position, target.position) > sightRange)
        {
            yield return null;
        }

        // ChaseStart 메서드를 호출하여 플레이어 추격을 재개
        ChaseStart();

        // 초기 위치로 돌아가는 동작이 끝났음을 표시
        isReturningToInitialPosition = false;
    }
    */

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

    // IEnumerator를 사용한 Attack 코루틴 함수
    IEnumerator Attack()
    {
        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(attackDuration);

        // 추격 상태로 전환 및 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);

        if (curHealth > 0 && !isAttackHit)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            isAttackHit = true;  // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit());  // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(2f);
        }

        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack2", true);

        if (curHealth > 0 && !isAttackHit)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            isAttackHit = true;  // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit());  // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(2f);
        }

        // 추격 상태로 전환 및 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack2", false);

        // 추격 중지 및 공격 상태로 전환
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack3", true);

        {
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            isAttackHit = true;  // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit());  // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(2f);
        }

        // 추격 상태로 전환 및 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack3", false);
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
        // Check if the monster is in the 'Enemydead' layer
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            // If in the 'Enemydead' layer, do nothing (or handle it as needed)
            return;
        }

        float damageToApply = bullet.isExplosion ? bullet.boomShotDamage : bullet.damage;
        curHealth -= damageToApply; // Apply damage
        Debug.Log(gameObject.name + "가 데미지를 받았습니다. 데미지: " + damageToApply + ", 남은 체력: " + curHealth);

        // 공격으로 받은 위치 벡터 계산
        Vector3 reactVec = transform.position - hitPoint;

        // OnDamage 코루틴 실행
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

        if (other.tag == "Player")
        {
            isChase = true;
            anim.SetBool("isWalk", true);
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
            // anim.SetBool("doGetHit", true);

            // Wait for the doGetHit animation to finish
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

            // Reset the doGetHit animation state
            // anim.SetBool("doGetHit", false);

            isChase = true;
            nav.enabled = true;
        }
        // 현재 체력이 0 이하인 경우
        else
        {
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