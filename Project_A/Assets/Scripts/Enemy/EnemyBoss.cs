using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    public float bmaxHealth; // �ִ� ü��
    public float bcurHealth; // ���� ü��
    public Transform btarget; // �÷��̾��� Transform
    public BoxCollider bmeleeArea; // ���� ���� ���� Collider
    public GameObject bbullet; // ���Ÿ� ���ݿ� ���Ǵ� �Ѿ�
    bool bisChase; // �߰� ���� ����
    bool bisAttack; // ���� ���� ����   
    public int bminDropCount; // ��� �������� �ּ� ����
    public int bmaxDropCount; // ��� �������� �ִ� ����
    public float btargetRange = 0.5f;
    public float btargetRadius = 15f;
    public float bsightRange = 15f; // Ÿ���� ���� �ν�
    public float battackinterval = 2f; // ���Ÿ����� ����
    private bool isAttackHit = false; // �Ϲ� ������ ���������� �����ߴ��� ����
    public float attackHitCooldown = 0.5f; // ���� �Ϲ� ������ ������ �� �ִ� ��ٿ� �ð�

    // �˹� ȿ�� ���� ����
    public float knockbackForce = 95f; // �˹��� ����
    public float knockbackDuration = 0.5f; // �˹� ���� �ð�


    //==============================================================
    public GameObject bitemPrefab; // ��� ������ ������ ���
    [SerializeField] Transform bdropPosition; // ��� �������� ���� ��ų ��ġ
    //==============================================================
    public Slider healthBarSlider;
    public GameObject healthBarUI;
    public float bHealthBarRange = 15f; // ü�¹ٰ� Ȱ��ȭ�� �÷��̾���� �Ÿ�
    Rigidbody brigid; // Rigidbody ������Ʈ
    BoxCollider bboxCollider; // BoxCollider ������Ʈ
    Material bmat; // Material ������Ʈ
    NavMeshAgent bnav; // NavMeshAgent ������Ʈ
    Animator banim; // Animator ������Ʈ


    //==============================================================
    //���� ����

    bool isCharging;
    bool chargeOnCooldown;
    public float chargeCooldownDuration = 8f;

    public float chargeSpeed = 10f; // ���� ����
    public float chargeDuration = 1f; // ���� �ð�
    // Reference to the arrow prefab
    private bool isChargeDamage = false;

    void Start()
    {
        StartCoroutine(ChargeCooldown());
    }


    IEnumerator ChargeAttack()
    {
        Debug.Log("ChargeAttack ���۵�");
        //bisChase = false;        
        isChargeDamage = false;
        isCharging = true;

        yield return new WaitForSeconds(0.1f);

        Vector3 chargeDirection = (btarget.position - transform.position).normalized * chargeSpeed;
        brigid.velocity = chargeDirection; // ���� ���� ������ �ӵ��� �����մϴ�.
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

        healthBarSlider.maxValue = bmaxHealth; // Slider�� �ִ밪�� ������ �ִ� ü������ �����մϴ�.
        healthBarSlider.value = bcurHealth; // Slider�� ���簪�� ������ ���� ü������ �����մϴ�.
        healthBarUI.SetActive(false);
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

        if (bcurHealth > 0 && distanceToPlayer <= bHealthBarRange)
        {
            if (!healthBarUI.activeSelf)
            {
                // �÷��̾ ü�¹� Ȱ��ȭ ���� ���� ������ ü�¹ٸ� Ȱ��ȭ�մϴ�.
                healthBarUI.SetActive(true);
            }
        }
        else
        {
            if (healthBarUI.activeSelf)
            {
                // �÷��̾ ü�¹� Ȱ��ȭ ���� ������ ������ ü�¹ٸ� ��Ȱ��ȭ�մϴ�.
                healthBarUI.SetActive(false);
            }
        }

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
        else if (!isCharging && distanceToPlayer <= bsightRange)
        {
            if (!bisChase)
            {
                bChaseStart();
            }
            bTargerting();
        }
    }



    //void bFreezeVelocity()
    //{
    //    if (bisChase)
    //    {
    //        brigid.velocity = Vector3.zero;
    //        brigid.angularVelocity = Vector3.zero;
    //    }
    //}

    void bTargerting()
    {


        // �÷��̾ �����ϸ� ���� ����
        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
            btargetRadius, transform.forward, btargetRange,
            LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !bisAttack)
        {
            StartCoroutine(bAttack());
        }
    }

    // IEnumerator�� ����� Attack �ڷ�ƾ �Լ�
    IEnumerator bAttack()
    {
        // �߰� ���� �� ���� ���·� ��ȯ
        bisChase = false;
        bisAttack = true;
        banim.SetTrigger("doAttack03");

        if (bcurHealth > 0 && !isAttackHit)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bbullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            isAttackHit = true;  // ������ ���������� �����ߴٰ� ǥ��
            StartCoroutine(ResetAttackHit());  // ���� ���� ���� �ʱ�ȭ �ڷ�ƾ ����
            yield return new WaitForSeconds(2f);
        }

        // �߰� ���·� ��ȯ �� ���� ���� ����
        bisChase = true;
        bisAttack = false;
        //banim.SetBool("isAttack", false);
        yield break;
    }
    IEnumerator ResetAttackHit()
    {
        yield return new WaitForSeconds(attackHitCooldown); // ������ �ð� ���� ���
        isAttackHit = false; // ���� ���� ���� �ʱ�ȭ
    }
    // FixedUpdate �Լ�
    void FixedUpdate()
    {
        // ��ǥ�� ���� �̵��ϴ� �Լ� ȣ��
        bTargerting();
        // �������� �����ϴ� �Լ� ȣ��
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

    

    // �޴� ���� �����ϴ� �Լ�
    public void bTakeDamage(Bullet bullet, Vector3 hitPoint)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return;
        }

        // ����Ʈ�� ��ų�� Ȱ��ȭ�Ǿ� ������ �⺻ �������� ����Ʈ�� �������� �ջ��մϴ�.
        float totalDamage = bullet.damage + (bullet.isLightningActive ? bullet.lightningDamage : 0);

        // ���� ȿ���� �ִ��� Ȯ���ϰ�, ������ �ش� �������� �����մϴ�.
        float damageToApply = bullet.isExplosion ? bullet.boomShotDamage : totalDamage;
        bcurHealth -= damageToApply; // Apply damage

        // ü�¹� ������Ʈ
        healthBarSlider.value = bcurHealth;

        // ü���� 0 �����̸� ü�¹� UI�� ��Ȱ��ȭ�մϴ�.
        if (bcurHealth <= 0)
        {
            healthBarUI.SetActive(false);  // ü�¹� ��Ȱ��ȭ
        }

        Debug.Log(gameObject.name + "�� �������� �޾ҽ��ϴ�. ������: " + damageToApply + ", ���� ü��: " + bcurHealth);

        Vector3 reactVec = transform.position - hitPoint;
        StartCoroutine(bOnDamage(reactVec));

        if (!bullet.isPenetrating) // ���� �Ѿ��� �ƴϸ� �Ѿ��� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        {
            bullet.ReturnToPool(); // �Ѿ� ��ȯ
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 'Wall' �±׸� ���� �� �Ǵ� 'Player' �±׸� ���� �÷��̾�� �浹�ߴ��� Ȯ��
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

                    // ���� ���ݿ� �¾��� �� ī�޶� ��鸲 ȿ���� �ݴϴ�.
                    StartCoroutine(ShakeCamera(0.5f, 0.5f));
                }
            }
        }        

    }

    void OnTriggerEnter(Collider other)
    {
        // ���Ͱ� �׾����� Ȯ���մϴ�.
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return; // ���Ͱ� �׾����� �ƹ��͵� ���� �ʽ��ϴ�.
        }

        if (other.CompareTag("Player"))
        {
           
            bisChase = true;
            // banim.SetBool("isWalk", true); // �ʿ��ϴٸ� �ִϸ��̼� ���µ� ������ �� �ֽ��ϴ�.
            
            // ���� �ʾ��� ���� ü�¹ٸ� Ȱ��ȭ�մϴ�.
            if (bcurHealth > 0)
            {
                healthBarUI.SetActive(true);
            }
        }

        else if (other.tag == "Bullet")
        {
            bTakeDamage(other.GetComponent<Bullet>(), other.transform.position);
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

    // �ǰ� �� �߻��ϴ� �ڷ�ƾ �Լ�
    IEnumerator bOnDamage(Vector3 reactVec)
    {

        // �ǰ� �� �Ͻ������� ĳ���� ������ ���������� ����
        // mat.color = Color.red;
        // yield return new WaitForSeconds(0.1f);

        // ���� ü���� 0���� ū ��� doGetHit �ִϸ��̼� ���
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
        // ���� ü���� 0 ������ ���
        else
        {
            // ü�¹ٸ� 0���� ����
            healthBarSlider.value = 0;

            // ������ ȸ������ �����ϰ� ���̾ "Dead"�� ����
            // mat.color = Color.gray;
            StopAllCoroutines();
            //bisChase = true;
            //nav.enabled = false;
            //anim.SetBool("doGetHit", true);

            gameObject.layer = 12; // ���̾ �����Ͽ� �ٽ� ������ ���� �ʵ��� ����

            // �߰� ����, �׺���̼� ��Ȱ��ȭ, ���� �ִϸ��̼� ���
            bisChase = false;
            // nav.enabled = false;
            brigid.velocity = Vector3.zero;
            brigid.angularVelocity = Vector3.zero;
            banim.SetTrigger("doDie");

            // �ǰݵ� ���� ���͸� ����ȭ�ϰ� ���� ���� �̵�������
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // ������ٵ� �ǰ� ���������� ���� ���� ���ϰ�
            //brigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item�̶�� ���� ������Ʈ ���� ���� + itemPrefab�� �����ؼ� _item�� �Ҵ�
            GameObject _item;
            int dropCount = Random.Range(bminDropCount, bmaxDropCount + 1); // minDropCount���� maxDropCount������ ������ ����
            for (int i = 0; i < dropCount; i++) // dropCount��ŭ �������� �����մϴ�.
            {
                _item = Instantiate(bitemPrefab); // ������ ����
                _item.transform.position = bdropPosition.position; // ������ ��ġ ����
            }

            // 2�� �� �� ���
            Destroy(gameObject, 10);
        }
    }

}