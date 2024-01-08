using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRat : MonoBehaviour
{
    public enum Type { A, B, C };
    public Type enemyType; // 적 종류
    public int maxHealth; // 최대 체력
    public int curHealth; // 현재 체력
    public Transform target; // 플레이어의 Transform
    public BoxCollider meleeArea; // 근접 공격 범위 Collider
    public GameObject bullet; // 원거리 공격에 사용되는 총알
    public bool isChase; // 추격 상태 여부
    public bool isAttack; // 공격 상태 여부

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
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Invoke("ChaseStart", 1);  // 1초 뒤에 추격 시작
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    void Update()
    {
        if(nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
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
        float targetRange = 0;

        // 적 종류에 따라 탐지 범위 조정
        switch (enemyType)
        {
            case Type.A:
                targetRadius = 3f;
                targetRange = 1.5f;
                break;
            case Type.B:
                targetRadius = 1f;
                targetRange = 12f;
                break;
            case Type.C:
                targetRadius = 0.5f;
                targetRange = 25f;
                break;
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
                yield return new WaitForSeconds(0.01f);
                meleeArea.enabled = true;

                // 1초 후 근접 공격 범위 비활성화
                yield return new WaitForSeconds(0f);
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
                rigidBullet.velocity = transform.forward * 20;

                // 2초 대기
                yield return new WaitForSeconds(2f);
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

    // 피격 시 발생하는 충돌 이벤트 처리 함수
    void OnTriggerEnter(Collider other)
    {
        // 만약 충돌한 오브젝트의 태그가 "Melee"인 경우
        if (other.tag == "Melee")  // 근접 공격을 받았을 때
        {
            // 충돌한 오브젝트에서 Weapon 컴포넌트 획득
            Weapon weapon = other.GetComponent<Weapon>();

            // 현재 체력에서 무기의 데미지만큼 감소
            curHealth -= weapon.damage;

            // 공격으로 받은 위치 벡터 계산
            Vector3 reactVec = transform.position - other.transform.position;

            // OnDamage 코루틴 실행
            StartCoroutine(OnDamage(reactVec));
        }
        // 만약 충돌한 오브젝트의 태그가 "Bullet"인 경우
        else if (other.tag == "Bullet") // 원거리 공격을 받았을 때
        {
            // 충돌한 오브젝트에서 Bullet 컴포넌트 획득
            Bullet bullet = other.GetComponent<Bullet>();

            // 현재 체력에서 총알의 데미지만큼 감소
            curHealth -= bullet.damage;

            // 공격으로 받은 위치 벡터 계산
            Vector3 reactVec = transform.position - other.transform.position;

            // 충돌한 총알 오브젝트 파괴
            Destroy(other.gameObject);

            // OnDamage 코루틴 실행
            StartCoroutine(OnDamage(reactVec));
        }
    }

    // 피격 시 발생하는 코루틴 함수
    IEnumerator OnDamage(Vector3 reactVec)
    {
        // 피격 시 일시적으로 캐릭터 색상을 빨간색으로 변경
        // mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // 현재 체력이 0보다 큰 경우 색상을 원래대로 되돌림
        if (curHealth > 0)
        {
            // mat.color = Color.white;
            isChase = true;
            // nav.enabled = false;
            anim.SetBool("doGetHit", true);
        }
        // 현재 체력이 0 이하인 경우
        else
        {
            // 색상을 회색으로 변경하고 레이어를 "Dead"로 설정
            // mat.color = Color.gray;
            isChase = true;
            nav.enabled = false;
            anim.SetBool("doGetHit", true);

            gameObject.layer = 14; // 레이어를 변경하여 다시 공격을 받지 않도록 설정

            // 추격 중지, 네비게이션 비활성화, 죽음 애니메이션 재생
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");

            // 피격된 방향 벡터를 정규화하고 위로 조금 이동시켜줌
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // 리지드바디에 피격 방향으로의 작은 힘을 가하고, 추락 애니메이션 재생 후 4초 후에 게임 오브젝트 파괴
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            Destroy(gameObject, 4);
        }
    }
}