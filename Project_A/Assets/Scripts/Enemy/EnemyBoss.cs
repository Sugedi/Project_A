using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

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
    public float btargetRange = 0f;
    public float btargetRadius = 0f;
    public float bsightRange = 10f; // Ÿ���� ���� �ν�
    public float battackinterval = 2f; // ���Ÿ����� ����
                                       
    // �˹� ȿ�� ���� ����
    public float knockbackForce = 5f; // �˹��� ����
    public float knockbackDuration = 0.5f; // �˹� ���� �ð�


    //==============================================================
    public GameObject bitemPrefab; // ��� ������ ������ ���
    [SerializeField] Transform bdropPosition; // ��� �������� ���� ��ų ��ġ
    //==============================================================

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

    public float chargeSpeed = 1f; // ���� ����
    public float chargeDuration = 0.3f; // ���� �ð�
    // Reference to the arrow prefab


    void Start()
    {
        StartCoroutine(ChargeCooldown());
    }


    IEnumerator ChargeAttack()
    {
        bisChase = false;

        

        yield return new WaitForSeconds(0.1f);

        // Add force for the charge attack
        Vector3 chargeDirection = (btarget.position - transform.position).normalized;
        // brigid.AddForce(transform.forward * chargeSpeed * Time.deltaTime, ForceMode.Impulse);
        brigid.AddForce(chargeDirection * chargeSpeed * Time.deltaTime, ForceMode.Impulse);
        banim.SetTrigger("doAttack02");

        yield return new WaitForSeconds(chargeDuration);

        // Stop the rigidbody velocity after the charge
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


        // Invoke("ChaseStart", 1);  // 1�� �ڿ� �߰� ����
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

        if (bcurHealth > 0)
        {


            // 0.5�� ��� �� �Ѿ� ���� �� �߻�
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bbullet, transform.position, transform.rotation);
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            rigidBullet.velocity = transform.forward * 20;

            // 2�� ���
            yield return new WaitForSeconds(2f);

        }


            // �߰� ���·� ��ȯ �� ���� ���� ����
        bisChase = true;
        bisAttack = false;
        //banim.SetBool("isAttack", false);
        yield break;
    }

    // FixedUpdate �Լ�
    void FixedUpdate()
    {
        // ��ǥ�� ���� �̵��ϴ� �Լ� ȣ��
        bTargerting();
        // �������� �����ϴ� �Լ� ȣ��
        bFreezeVelocity();
    }

    // �޴� ���� �����ϴ� �Լ�

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

    // �������� ���� ��ġ ���� ���
    Vector3 reactVec = transform.position - hitPoint;

        // OnDamage �ڷ�ƾ ����
        StartCoroutine(bOnDamage(reactVec));

        if (!bullet.isPenetrating) // ���� �Ѿ��� �ƴϸ� �Ѿ��� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        {
            bullet.ReturnToPool(); // �Ѿ� ��ȯ
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // TakeDamage �޼��带 ȣ���Ͽ� ������ ó���� �մϴ�.
                bTakeDamage(bullet, collision.contacts[0].point);
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
                // TakeDamage �޼��带 ȣ���Ͽ� ������ ó���� �մϴ�.
                bTakeDamage(bullet, other.transform.position);
            }
        }
        else if (other.CompareTag("Player"))
        {
            // ���� ���� ���̰� �˹� ��ٿ��� �ƴ� ���� �˹��� �����մϴ�.
            if (isCharging && !chargeOnCooldown)
            {
                Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    // �˹� ������ �������� �÷��̾ ���ϴ� �����Դϴ�.
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    knockbackDirection.y = 0; // ���Ϲ��� �˹� ���� (�ʿ信 ���� ����)

                    // �˹� ȿ���� �÷��̾�� �����մϴ�.
                    playerRigidbody.velocity = Vector3.zero; // �÷��̾��� ���� � ���¸� �ʱ�ȭ�մϴ�.
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }
            }

            bisChase = true;
            // banim.SetBool("isWalk", true); // �ʿ��ϴٸ� �ִϸ��̼� ���µ� ������ �� �ֽ��ϴ�.
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