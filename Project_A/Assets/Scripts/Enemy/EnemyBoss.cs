using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{

    public float bmaxHealth; // 최대 체력
    public float bcurHealth; // 현재 체력
    public Transform btarget; // 플레이어의 Transform
    public BoxCollider bmeleeArea; // 근접 공격 범위 Collider
    public GameObject bbullet; // 원거리 공격에 사용되는 총알
    bool bisChase; // 추격 상태 여부
    bool bisAttack; // 공격 상태 여부   
    public int bminDropCount; // 드랍 아이템의 최소 개수
    public int bmaxDropCount; // 드랍 아이템의 최대 개수
    public float btargetRange = 0f;
    public float btargetRadius = 0f;
    public float bsightRange = 10f; // 타겟이 유저 인식
    public float battackinterval = 2f; // 원거리공격 간격
                                       
    // 넉백 효과 관련 변수
    public float knockbackForce = 100f; // 넉백의 강도
    public float knockbackDuration = 0.5f; // 넉백 지속 시간


    //==============================================================
    public GameObject bitemPrefab; // 드랍 아이템 프리펩 등록
    [SerializeField] Transform bdropPosition; // 드랍 아이템을 생성 시킬 위치
    //==============================================================

    Rigidbody brigid; // Rigidbody 컴포넌트
    BoxCollider bboxCollider; // BoxCollider 컴포넌트
    Material bmat; // Material 컴포넌트
    NavMeshAgent bnav; // NavMeshAgent 컴포넌트
    Animator banim; // Animator 컴포넌트


    //==============================================================
    //돌진 공격

    bool isCharging;
    bool chargeOnCooldown;
    public float chargeCooldownDuration = 8f;

    public float chargeSpeed = 1f; // 돌진 강도
    public float chargeDuration = 0.3f; // 돌진 시간
    // Reference to the arrow prefab


    void Start()
    {
        StartCoroutine(ChargeCooldown());
    }


    IEnumerator ChargeAttack()
    {
        bisChase = false;
        yield return new WaitForSeconds(0.1f);

        // 충전 방향 계산
        Vector3 chargeDirection = (btarget.position - transform.position).normalized;
        brigid.AddForce(chargeDirection * chargeSpeed * Time.deltaTime, ForceMode.Impulse);
        banim.SetTrigger("doAttack02");

        // 충전 공격이 플레이어에게 닿는 순간을 가정한 시점
        if (Vector3.Distance(btarget.position, transform.position) < 2f)
        {
            // 플레이어의 넉백 메서드를 호출
            btarget.GetComponent<Player>().GetKnockedBack(-chargeDirection, 150f);
        }

        yield return new WaitForSeconds(chargeDuration);
        brigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        bisChase = true;

        StartCoroutine(ChargeCooldown());
    }

    IEnumerator ChargeCooldown()
    {
        chargeOnCooldown = true;
        yield return new WaitForSeconds(chargeCooldownDuration);
        chargeOnCooldown = false;
    }




    //==============================================================











    void Awake()
    {
        brigid = GetComponent<Rigidbody>();
        bboxCollider = GetComponent<BoxCollider>();
        bmat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        bnav = GetComponent<NavMeshAgent>();
        banim = GetComponentInChildren<Animator>();

        btarget = FindObjectOfType<Player>().GetComponent<Transform>();


        // Invoke("ChaseStart", 1);  // 1초 뒤에 추격 시작
    }

 

    
    void bChaseStart()
    {
        if (bcurHealth > 0)  // Only start chasing if health is greater than 0
        {
            bisChase = true;
            //banim.SetBool("isWalk", true);

            StartCoroutine(bChasePlayer());
        }
    }

    IEnumerator bChasePlayer()
    {
        while (bisChase)
        {
            if (bisChase == true)
            {
             bnav.SetDestination(btarget.position);
             bnav.isStopped = !bisChase;
             transform.LookAt(btarget);
            }
            yield return null;  // Yielding null allows the coroutine to continue indefinitely
        }
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, btarget.position);

        if (distanceToPlayer <= bsightRange)
        {
            if (!bisChase)
            {
                bChaseStart();
            }

        }
        //else
        //{
        //    bisChase = false;

        //    //banim.SetBool("isAttack", false);
        //    //banim.SetBool("isWalk", false);
        //





        if (!isCharging && !chargeOnCooldown && distanceToPlayer <= btargetRadius)
        {
            StartCoroutine(ChargeAttack());
        }

    }



    void bFreezeVelocity()
    {
        if (bisChase)
        {
            brigid.velocity = Vector3.zero;
            brigid.angularVelocity = Vector3.zero;
        }
    }

    void bTargerting()
    {


        // 플레이어를 감지하면 공격 시작
        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
            btargetRadius, transform.forward, btargetRange,
            LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !bisAttack)
        {
            StartCoroutine(bAttack());
        }
    }

    // IEnumerator를 사용한 Attack 코루틴 함수
    IEnumerator bAttack()
    {
        // 추격 중지 및 공격 상태로 전환
        bisChase = false;
        bisAttack = true;
        banim.SetTrigger("doAttack03");

        if (bcurHealth > 0)
        {


            // 0.5초 대기 후 총알 생성 및 발사
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bbullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            // 2초 대기
            yield return new WaitForSeconds(2f);

        }


            // 추격 상태로 전환 및 공격 상태 종료
        bisChase = true;
        bisAttack = false;
        //banim.SetBool("isAttack", false);
        yield break;
    }

    // FixedUpdate 함수
    void FixedUpdate()
    {
        // 목표를 향해 이동하는 함수 호출
        bTargerting();
        // 움직임을 억제하는 함수 호출
        bFreezeVelocity();
    }

    // 받는 피해 관리하는 함수

    public void bTakeDamage(Bullet bullet, Vector3 hitPoint)
    {
        // Check if the monster is in the 'Enemydead' layer
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            // If in the 'Enemydead' layer, do nothing (or handle it as needed)
            return;
        }

        float damageToApply = bullet.isExplosion ? bullet.boomShotDamage : bullet.damage;
        bcurHealth -= damageToApply; // Apply damage

    // 공격으로 받은 위치 벡터 계산
    Vector3 reactVec = transform.position - hitPoint;

        // OnDamage 코루틴 실행
        StartCoroutine(bOnDamage(reactVec));

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
                bTakeDamage(bullet, collision.contacts[0].point);
            }
        }

        if (collision.collider.CompareTag("Player") && !bisChase)
        {
            // 플레이어에게 넉백 효과 적용
            Rigidbody playerRigidbody = collision.collider.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                float knockbackForce = 150; // 적절한 넉백 강도로 조정하세요
                playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
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
                bTakeDamage(bullet, other.transform.position);
            }
        }
        else if (other.CompareTag("Player"))
        {
           
            bisChase = true;
            // banim.SetBool("isWalk", true); // 필요하다면 애니메이션 상태도 변경할 수 있습니다.
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //   if (other.tag == "Player")
    //    {
    //        isChase = false;
    //        anim.SetBool("isWalk", false);
    //    }
    //}

    // 피격 시 발생하는 코루틴 함수
    IEnumerator bOnDamage(Vector3 reactVec)
    {

        // 피격 시 일시적으로 캐릭터 색상을 빨간색으로 변경
        // mat.color = Color.red;
        // yield return new WaitForSeconds(0.1f);

        // 현재 체력이 0보다 큰 경우 doGetHit 애니메이션 재생
        if (bcurHealth > 0)
        {


            // mat.color = Color.white;
            //isChase = false;
            //nav.enabled = false;

            // Play doGetHit animation
            banim.SetTrigger("getHit");


            // Wait for the doGetHit animation to finish
            yield return new WaitForSeconds(banim.GetCurrentAnimatorStateInfo(0).length);

            // Reset the doGetHit animation state
            //banim.SetBool("doGetHit", false);
 
          

        }
        // 현재 체력이 0 이하인 경우
        else
        {
            // 색상을 회색으로 변경하고 레이어를 "Dead"로 설정
            // mat.color = Color.gray;
            StopAllCoroutines();
            //bisChase = true;
            //nav.enabled = false;
            //anim.SetBool("doGetHit", true);

            gameObject.layer = 12; // 레이어를 변경하여 다시 공격을 받지 않도록 설정

            // 추격 중지, 네비게이션 비활성화, 죽음 애니메이션 재생
            bisChase = false;
            // nav.enabled = false;
            brigid.velocity = Vector3.zero;
            brigid.angularVelocity = Vector3.zero;
            banim.SetTrigger("doDie");

            // 피격된 방향 벡터를 정규화하고 위로 조금 이동시켜줌
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // 리지드바디에 피격 방향으로의 작은 힘을 가하고
            //brigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item이라는 게임 오브젝트 변수 선언 + itemPrefab을 생성해서 _item에 할당
            GameObject _item;
            int dropCount = Random.Range(bminDropCount, bmaxDropCount + 1); // minDropCount부터 maxDropCount까지의 랜덤한 수량
            for (int i = 0; i < dropCount; i++) // dropCount만큼 아이템을 생성합니다.
            {
                _item = Instantiate(bitemPrefab); // 아이템 생성
                _item.transform.position = bdropPosition.position; // 아이템 위치 설정
            }

            // 2초 뒤 몹 사망
            Destroy(gameObject, 10);
        }
    }

}