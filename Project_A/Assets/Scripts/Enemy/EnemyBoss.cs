using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public float btargetRange = 0.5f;
    public float btargetRadius = 10f;
    public float bsightRange = 15f; // 타겟이 유저 인식
    public float battackinterval = 2f; // 원거리공격 간격
    private bool isAttackHit = false; // 일반 공격이 성공적으로 적중했는지 여부
    public float attackHitCooldown = 0.5f; // 다음 일반 공격이 적중할 수 있는 쿨다운 시간

    // 넉백 효과 관련 변수
    public float knockbackForce = 95f; // 넉백의 강도
    public float knockbackDuration = 0.5f; // 넉백 지속 시간

    public Vector3 homePosition; // 몬스터의 초기 위치
    public float chaseRange = 20f;
    public bool bisReturn;

    //==============================================================
    public GameObject bitemPrefab; // 드랍 아이템 프리펩 등록
    [SerializeField] Transform bdropPosition; // 드랍 아이템을 생성 시킬 위치
    //==============================================================
    public Slider healthBarSlider;
    public GameObject healthBarUI;
    public float bHealthBarRange = 15f; // 체력바가 활성화될 플레이어와의 거리
    Rigidbody brigid; // Rigidbody 컴포넌트
    BoxCollider bboxCollider; // BoxCollider 컴포넌트
    Material bmat; // Material 컴포넌트
    NavMeshAgent bnav; // NavMeshAgent 컴포넌트
    Animator banim; // Animator 컴포넌트


    //==============================================================
    //돌진 공격

    bool isCharging = false;
    bool chargeOnCooldown;
    public float chargeCooldownDuration = 8f;

    public float chargeSpeed = 10f; // 돌진 강도
    public float chargeDuration = 1f; // 돌진 시간
    public float chargeRange = 1f; // 공격 범위

    // Reference to the arrow prefab
    private bool isChargeDamage = false;
    public ParticleSystem chargeAreaEffectPrefab;
    private ParticleSystem chargeAreaEffectInstance;

    void Start()
    {
        homePosition = transform.position;
        chargeAreaEffectInstance = Instantiate(chargeAreaEffectPrefab, transform.position, Quaternion.identity);
        chargeAreaEffectInstance.Stop();
        StartCoroutine(ChargeCooldown());
    }
    // 돌진 범위를 시각적으로 표시하는 메소드
    void ShowChargeArea()
    {
        Debug.Log("범위 보여줘");
        // 파티클 시스템의 위치와 크기를 돌진 범위에 맞게 설정합니다.
        chargeAreaEffectInstance.transform.position = transform.position + (btarget.position - transform.position).normalized * 1f;
        chargeAreaEffectInstance.transform.localScale = new Vector3(chargeRange, chargeAreaEffectInstance.transform.localScale.y, chargeRange);
        chargeAreaEffectInstance.transform.rotation = Quaternion.LookRotation(btarget.position - transform.position);

        // 파티클 시스템을 활성화합니다.
        chargeAreaEffectInstance.Play();
    }    

    IEnumerator ChargeAttack()
    {
        Debug.Log("ChargeAttack 시작됨");
        isCharging = true;        

        ShowChargeArea(); // 돌진 범위를 표시합니다.

        // 돌진 방향을 고정시킵니다.
        Vector3 chargeDirection = (btarget.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(chargeDirection);

        yield return new WaitForSeconds(1f); // 1초 동안 기다립니다.

        // 파티클 시스템을 비활성화합니다.
        chargeAreaEffectInstance.Stop();

        //bisChase = false;        
        isChargeDamage = false;

        yield return new WaitForSeconds(0.1f);

        brigid.velocity = chargeDirection * chargeSpeed; // 돌진 시작 시점에 속도를 설정합니다.
        banim.SetTrigger("doAttack02");

        yield return new WaitForSeconds(chargeDuration);

        brigid.velocity = Vector3.zero;
        isCharging = false;        
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
    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = Camera.main.transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + x, Camera.main.transform.position.y + y, originalPosition.z);


            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.position = originalPosition;
    }

    void Awake()
    {
        
        brigid = GetComponent<Rigidbody>();
        bboxCollider = GetComponent<BoxCollider>();
        bmat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        bnav = GetComponent<NavMeshAgent>();
        banim = GetComponentInChildren<Animator>();

        btarget = FindObjectOfType<Player>().GetComponent<Transform>();

        healthBarSlider.maxValue = bmaxHealth; // Slider의 최대값을 보스의 최대 체력으로 설정합니다.
        healthBarSlider.value = bcurHealth; // Slider의 현재값을 보스의 현재 체력으로 설정합니다.
        healthBarUI.SetActive(false);
    }

 

    
    void bChaseStart()
    {
        if (bcurHealth > 0)  // Only start chasing if health is greater than 0
        {
            bisChase = true;
            //banim.SetBool("isWalk", true);
            bnav.isStopped = false;

            StartCoroutine(bChasePlayer());
        }
    }

    IEnumerator bChasePlayer()
    {
        while (bisChase)
        {
            if (bnav.isActiveAndEnabled && bnav.isOnNavMesh)
            {
                bnav.SetDestination(btarget.position);
                // 돌진 중이 아닐 때만 캐릭터가 플레이어를 바라보도록 합니다.
                if (!isCharging)
                {
                    transform.LookAt(btarget);
                }
            }
            yield return null;  // Yielding null allows the coroutine to continue indefinitely
        }
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, btarget.position);

        if (bcurHealth > 0 && distanceToPlayer <= bHealthBarRange)
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

        if (!isCharging && !chargeOnCooldown && distanceToPlayer <= btargetRadius)
        {
            StartCoroutine(ChargeAttack());
        }
        else if (!isCharging && distanceToPlayer <= bsightRange)
        {
            if (!bisChase && !isCharging)
            {
                bChaseStart();
            }
            if (!isCharging)
            {
                bTargerting();
            }
        }

        // 플레이어가 일정 범위 내에 있을 때 추격 시작
        if (distanceToPlayer <= bsightRange && !bisChase && !bisReturn && !isCharging)
        {
            bChaseStart();
        }
        // 플레이어가 추격 범위를 벗어났을 때 추격 중지 및 제자리로 복귀
        else if (distanceToPlayer > chaseRange && bisChase && !isCharging)
        {
            StopChase();
            Return();
        }
    }
    void StopChase()
    {
        bisChase = false;
        banim.SetBool("isAttack", false);
        //banim.SetBool("isWalk", false);
        bnav.isStopped = true;
    }

    void Return()
    {
        bboxCollider.enabled = false;
        // 제자리로 돌아가는 상태로 설정
        bisReturn = true;

        // 원래 위치로 이동
        //banim.SetBool("isWalk", true);
        bnav.isStopped = false;
        bnav.SetDestination(homePosition);

        // 원래 위치에 도착했는지 확인
        if (Vector3.Distance(transform.position, homePosition) <= 2f)
        {
            //banim.SetBool("isWalk", false);
            bnav.isStopped = true;
            bisReturn = false; // 제자리로 돌아간 상태를 해제
            bboxCollider.enabled = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        // 추격 범위를 빨간색 원으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
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
        if (rayHits.Length > 0 && !bisAttack && !isCharging) // 돌진 중이 아닐 때만 공격 시작
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

        if (bcurHealth > 0 && !isAttackHit)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bbullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            isAttackHit = true;  // 공격이 성공적으로 적중했다고 표시
            StartCoroutine(ResetAttackHit());  // 공격 적중 상태 초기화 코루틴 실행
            yield return new WaitForSeconds(2f);
        }

        // 추격 상태로 전환 및 공격 상태 종료
        bisChase = true;
        bisAttack = false;
        //banim.SetBool("isAttack", false);
        yield break;
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
        if (!isCharging)
        {
            bTargerting();
        }
        // 움직임을 억제하는 함수 호출
        //bFreezeVelocity();
    }
    private void StopCharging()
    {
        if (isCharging)
        {
            isCharging = false;
            brigid.velocity = Vector3.zero;
            bisChase = true;
        }
    }
    

    // 받는 피해 관리하는 함수
    public void bTakeDamage(Bullet bullet, Vector3 hitPoint)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return;
        }

        // 라이트닝 스킬이 활성화되어 있으면 기본 데미지에 라이트닝 데미지를 합산합니다.
        float totalDamage = bullet.damage + (bullet.isLightningActive ? bullet.lightningDamage : 0);

        // 폭발 효과가 있는지 확인하고, 있으면 해당 데미지를 적용합니다.
        float damageToApply = bullet.isExplosion ? bullet.boomShotDamage : totalDamage;
        bcurHealth -= damageToApply; // Apply damage

        if (bcurHealth < 0)
        {
            bcurHealth = 0;
        }
        // 체력바 업데이트
        healthBarSlider.value = bcurHealth;

        // 체력이 0 이하이면 체력바 UI를 비활성화합니다.
        if (bcurHealth <= 0)
        {
            healthBarUI.SetActive(false);  // 체력바 비활성화
        }

        Debug.Log(gameObject.name + "가 데미지를 받았습니다. 데미지: " + damageToApply + ", 남은 체력: " + bcurHealth);

        Vector3 reactVec = transform.position - hitPoint;
        StartCoroutine(bOnDamage(reactVec));

        if (!bullet.isPenetrating) // 관통 총알이 아니면 총알을 오브젝트 풀로 반환합니다.
        {
            bullet.ReturnToPool(); // 총알 반환
        }
        StartCoroutine(KnockbackResistance());
    }
    IEnumerator KnockbackResistance()
    {
        brigid.isKinematic = true; // 물리적 영향 제한
        yield return new WaitForSeconds(0.2f); // 경직 지속 시간
        brigid.isKinematic = false; // 물리적 영향 다시 허용
    }
    void OnCollisionEnter(Collision collision)
    {
        // 'Wall' 태그를 가진 벽 또는 'Player' 태그를 가진 플레이어와 충돌했는지 확인
        if (isCharging && (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player"))
        {
            StopCharging();

            if (collision.gameObject.tag == "Player" && !isChargeDamage)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
                    player.GetKnockedBack(knockbackDirection, knockbackForce, 20);
                    isChargeDamage = true;

                    // 돌진 공격에 맞았을 때 카메라 흔들림 효과를 줍니다.
                    StartCoroutine(ShakeCamera(0.5f, 0.5f));
                }
            }
        }        

    }

    void OnTriggerEnter(Collider other)
    {
        // 몬스터가 죽었는지 확인합니다.
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return; // 몬스터가 죽었으면 아무것도 하지 않습니다.
        }

        if (other.CompareTag("Player"))
        {
           
            bisChase = true;
            // banim.SetBool("isWalk", true); // 필요하다면 애니메이션 상태도 변경할 수 있습니다.
            
            // 죽지 않았을 때만 체력바를 활성화합니다.
            if (bcurHealth > 0)
            {
                healthBarUI.SetActive(true);
            }
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
            // 체력바를 0으로 설정
            healthBarSlider.value = 0;

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