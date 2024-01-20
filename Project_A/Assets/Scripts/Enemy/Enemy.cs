using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C };
    public Type enemyType; // 적 종류
    public float maxHealth; // 최대 체력
    public float curHealth; // 현재 체력
    public Transform target; // 플레이어의 Transform
    public BoxCollider meleeArea; // 근접 공격 범위 Collider
    public GameObject bullet; // 원거리 공격에 사용되는 총알
    public bool isChase; // 추격 상태 여부
    public bool isAttack; // 공격 상태 여부   
    public int minDropCount; // 드랍 아이템의 최소 개수
    public int maxDropCount; // 드랍 아이템의 최대 개수
    public float targetRange = 0;
    
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

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();
        initialPosition = transform.position; // 초기 위치 저장

        // Invoke("ChaseStart", 1);  // 1초 뒤에 추격 시작
    }

    void Start()
    {

    }

    void ChaseStart()
    {
        if (curHealth > 0)  // Only start chasing if health is greater than 0
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
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
            transform.LookAt(target);

            yield return null;  // Yielding null allows the coroutine to continue indefinitely
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

        // 추가된 부분: 초기 위치로 돌아가기
        StartCoroutine(ReturnToInitialPosition());
    }

    IEnumerator ReturnToInitialPosition()
    {
        isReturningToInitialPosition = true; // 초기 위치로 돌아가는 중임을 표시

        yield return null;

        // 추가된 부분: isWalk 애니메이션 재생
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

        // 추가된 부분: isWalk 애니메이션 종료
        anim.SetBool("isWalk", false);

        // 초기 위치로 돌아갔을 때 플레이어가 다시 인식 범위 안으로 들어오면 추격 재개
        while (Vector3.Distance(transform.position, target.position) > sightRange)
        {
            yield return null;
        }

        // 추가된 부분: 플레이어가 인식 범위 안으로 들어왔을 때 추격 재개
        ChaseStart();

        isReturningToInitialPosition = false; // 초기 위치로 돌아가는 동작이 끝났음을 표시
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
        float targetRadius = 0;

        // 적 종류에 따라 탐지 범위 조정
        switch (enemyType)
        {
            case Type.A:
                targetRadius = 1f;
                targetRange = 0.8f;
                break;
            case Type.B:
                targetRadius = 1f;
                targetRange = 12f;
                break;
            case Type.C:
                targetRadius = 0.5f;
                targetRange = 8f;
                break;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // 플레이어가 근접 공격 범위 안에 있으면 Attack 코루틴 호출
        if (distanceToPlayer <= targetRadius)
        {
            if (!isAttack)
            {
                StartCoroutine(Attack());
            }
        }

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

        // 적 유형에 따른 공격 동작 처리
        switch (enemyType)
        {
            case Type.A:
                // 0.2초 대기 후 근접 공격 범위 활성화
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                // 1초 후 근접 공격 범위 비활성화
                 yield return new WaitForSeconds(0.7f);
                meleeArea.enabled = false;

                // 1초 대기
                yield return new WaitForSeconds(0f);
                break;
            case Type.B:
                // 0.1초 대기 후 앞으로 이동하는 힘을 가하고 근접 공격 범위 활성화
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                // 0.5초 후 움직임을 제거하고 근접 공격 범위 비활성화
                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                // 2초 대기
                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                // 0.5초 대기 후 총알 생성 및 발사
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 10;

                // 2초 대기
                yield return new WaitForSeconds(1.5f);
                break;
        }

        // 추격 상태로 전환 및 공격 상태 종료
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
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
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // TakeDamage 메서드를 호출하여 데미지 처리를 합니다.
                TakeDamage(bullet, collision.contacts[0].point);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                // TakeDamage 메서드를 호출하여 데미지 처리를 합니다.
                TakeDamage(bullet, other.transform.position);
            }
        }
        else if (other.tag == "Player")
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
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item이라는 게임 오브젝트 변수 선언 + itemPrefab을 생성해서 _item에 할당
            GameObject _item;
            int dropCount = Random.Range(minDropCount, maxDropCount + 1); // minDropCount부터 maxDropCount까지의 랜덤한 수량
            for (int i = 0; i < dropCount; i++) // dropCount만큼 아이템을 생성합니다.
            {
                _item = Instantiate(itemPrefab); // 아이템 생성
                _item.transform.position = dropPosition.position; // 아이템 위치 설정
            }

            // 2초 뒤 몹 사망
            Destroy(gameObject, 1);
        }
    }
}