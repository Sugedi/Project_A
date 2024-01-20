using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C };
    public Type enemyType; // �� ����
    public float maxHealth; // �ִ� ü��
    public float curHealth; // ���� ü��
    public Transform target; // �÷��̾��� Transform
    public BoxCollider meleeArea; // ���� ���� ���� Collider
    public GameObject bullet; // ���Ÿ� ���ݿ� ���Ǵ� �Ѿ�
    public bool isChase; // �߰� ���� ����
    public bool isAttack; // ���� ���� ����   
    public int minDropCount; // ��� �������� �ּ� ����
    public int maxDropCount; // ��� �������� �ִ� ����
    public float targetRange = 0;
    
    public float sightRange = 10f; // Ÿ���� ���� �ν�

    bool isReturningToInitialPosition; // ���Ͱ� �ʱ� ��ġ�� ���ư��� �ִ��� ���θ� ��Ÿ���� ����

    //==============================================================
    public GameObject itemPrefab; // ��� ������ ������ ���
    [SerializeField] Transform dropPosition; // ��� �������� ���� ��ų ��ġ
    //==============================================================

    Rigidbody rigid; // Rigidbody ������Ʈ
    BoxCollider boxCollider; // BoxCollider ������Ʈ
    Material mat; // Material ������Ʈ
    NavMeshAgent nav; // NavMeshAgent ������Ʈ
    Animator anim; // Animator ������Ʈ

    Vector3 initialPosition; // ������ �ʱ� ��ġ ���� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();
        initialPosition = transform.position; // �ʱ� ��ġ ����

        // Invoke("ChaseStart", 1);  // 1�� �ڿ� �߰� ����
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

        // �߰��� �κ�: �ʱ� ��ġ�� ���ư���
        StartCoroutine(ReturnToInitialPosition());
    }

    IEnumerator ReturnToInitialPosition()
    {
        isReturningToInitialPosition = true; // �ʱ� ��ġ�� ���ư��� ������ ǥ��

        yield return null;

        // �߰��� �κ�: isWalk �ִϸ��̼� ���
        if (!anim.GetBool("isWalk") && !anim.GetCurrentAnimatorStateInfo(0).IsName("isWalk"))
        {
            // "isWalk" �ִϸ��̼��� ��� ���� �ƴ϶�� ���
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

        // �߰��� �κ�: isWalk �ִϸ��̼� ����
        anim.SetBool("isWalk", false);

        // �ʱ� ��ġ�� ���ư��� �� �÷��̾ �ٽ� �ν� ���� ������ ������ �߰� �簳
        while (Vector3.Distance(transform.position, target.position) > sightRange)
        {
            yield return null;
        }

        // �߰��� �κ�: �÷��̾ �ν� ���� ������ ������ �� �߰� �簳
        ChaseStart();

        isReturningToInitialPosition = false; // �ʱ� ��ġ�� ���ư��� ������ �������� ǥ��
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

        // �� ������ ���� Ž�� ���� ����
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

        // �÷��̾ ���� ���� ���� �ȿ� ������ Attack �ڷ�ƾ ȣ��
        if (distanceToPlayer <= targetRadius)
        {
            if (!isAttack)
            {
                StartCoroutine(Attack());
            }
        }

    // �÷��̾ �����ϸ� ���� ����
    RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
            targetRadius, transform.forward, targetRange,
            LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    // IEnumerator�� ����� Attack �ڷ�ƾ �Լ�
    IEnumerator Attack()
    {
        // �߰� ���� �� ���� ���·� ��ȯ
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        // �� ������ ���� ���� ���� ó��
        switch (enemyType)
        {
            case Type.A:
                // 0.2�� ��� �� ���� ���� ���� Ȱ��ȭ
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                // 1�� �� ���� ���� ���� ��Ȱ��ȭ
                 yield return new WaitForSeconds(0.7f);
                meleeArea.enabled = false;

                // 1�� ���
                yield return new WaitForSeconds(0f);
                break;
            case Type.B:
                // 0.1�� ��� �� ������ �̵��ϴ� ���� ���ϰ� ���� ���� ���� Ȱ��ȭ
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                // 0.5�� �� �������� �����ϰ� ���� ���� ���� ��Ȱ��ȭ
                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                // 2�� ���
                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                // 0.5�� ��� �� �Ѿ� ���� �� �߻�
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 10;

                // 2�� ���
                yield return new WaitForSeconds(1.5f);
                break;
        }

        // �߰� ���·� ��ȯ �� ���� ���� ����
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    // FixedUpdate �Լ�
    void FixedUpdate()
    {
        // ��ǥ�� ���� �̵��ϴ� �Լ� ȣ��
        Targerting();
        // �������� �����ϴ� �Լ� ȣ��
        FreezeVelocity();
    }

    // �޴� ���� �����ϴ� �Լ�

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

        // �������� ���� ��ġ ���� ���
        Vector3 reactVec = transform.position - hitPoint;

        // OnDamage �ڷ�ƾ ����
        StartCoroutine(OnDamage(reactVec));

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
                // TakeDamage �޼��带 ȣ���Ͽ� ������ ó���� �մϴ�.
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

    // �ǰ� �� �߻��ϴ� �ڷ�ƾ �Լ�
    IEnumerator OnDamage(Vector3 reactVec)
    {
        // �ǰ� �� �Ͻ������� ĳ���� ������ ���������� ����
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // ���� ü���� 0���� ū ��� doGetHit �ִϸ��̼� ���
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
        // ���� ü���� 0 ������ ���
        else
        {
            // ������ ȸ������ �����ϰ� ���̾ "Dead"�� ����
            // mat.color = Color.gray;
            StopAllCoroutines();
            isChase = true;
            nav.enabled = false;

            gameObject.layer = 12; // ���̾ �����Ͽ� �ٽ� ������ ���� �ʵ��� ����

            // �߰� ����, �׺���̼� ��Ȱ��ȭ, ���� �ִϸ��̼� ���
            isChase = false;
            anim.SetTrigger("doDie");
            // nav.enabled = false;

            // �ǰݵ� ���� ���͸� ����ȭ�ϰ� ���� ���� �̵�������
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // ������ٵ� �ǰ� ���������� ���� ���� ���ϰ�
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item�̶�� ���� ������Ʈ ���� ���� + itemPrefab�� �����ؼ� _item�� �Ҵ�
            GameObject _item;
            int dropCount = Random.Range(minDropCount, maxDropCount + 1); // minDropCount���� maxDropCount������ ������ ����
            for (int i = 0; i < dropCount; i++) // dropCount��ŭ �������� �����մϴ�.
            {
                _item = Instantiate(itemPrefab); // ������ ����
                _item.transform.position = dropPosition.position; // ������ ��ġ ����
            }

            // 2�� �� �� ���
            Destroy(gameObject, 1);
        }
    }
}