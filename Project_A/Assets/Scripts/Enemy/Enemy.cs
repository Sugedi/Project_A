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
    public GameObject EnemyBullet; // EnemyBullet ������Ʈ
    public bool isChase; // �߰� ���� ����
    public bool isAttack; // ���� ���� ����
    public bool isReturn;
    public float chaseRange;
    public int minDropCount; // ��� �������� �ּ� ����
    public int maxDropCount; // ��� �������� �ִ� ����
    public float targetRange = 0;

    public float sightRange = 10f; // Ÿ���� ���� �ν�

    public Vector3 homePosition; // ������ �ʱ� ��ġ
    public float homeRange = 10f; // Ȩ ��ġ���� ���Ͱ� �̵��� �� �ִ� �ִ� �Ÿ�

    //==============================================================
    public GameObject itemPrefab; // ��� ������ ������ ���
    [SerializeField] Transform dropPosition; // ��� �������� ���� ��ų ��ġ
    //==============================================================

    //==============================================================
    public GameObject itemPrefabHeart; // ��� ������ ������ ���
    [SerializeField] Transform dropPositionHeart; // ��� �������� ���� ��ų ��ġ
    //==============================================================

    Rigidbody rigid; // Rigidbody ������Ʈ
    BoxCollider boxCollider; // BoxCollider ������Ʈ
    Material mat; // Material ������Ʈ
    NavMeshAgent nav; // NavMeshAgent ������Ʈ
    Animator anim; // Animator ������Ʈ

    public MonsterGroupManager groupManager;

    void Awake()
    {
        homePosition = transform.position;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        nav = GetComponent<NavMeshAgent>();

        EnemyBullet = transform.Find("EnemyBullet").gameObject;
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
            // NavMeshAgent�� �����ִ� ��쿡�� �簳
            if (!nav.isActiveAndEnabled || !nav.isOnNavMesh)
            {
                yield return null;
                continue;
            }

            // NavMeshAgent�� ���°� �ùٸ� ��쿡�� ������ ����
            nav.SetDestination(target.position);
            transform.LookAt(target);

            yield return null;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (isReturn)
        {
            Return();
            Targerting(); // �÷��̾� ���� �Լ� ȣ��
        }
        else
        {
            isReturn = chaseRange < Vector3.Distance(transform.position, homePosition);
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
    }

    void StopChase()
    {
        isChase = false;
        anim.SetBool("isAttack", false);
        anim.SetBool("isWalk", false);
    }

    void Return()
    {
        // BoxCollider ��Ȱ��ȭ
        boxCollider.enabled = false;

        isChase = false;
        anim.SetBool("isAttack", false);
        if (!nav.enabled)
            nav.enabled = true;

        if (!nav.isOnNavMesh)
        {
            // NavMesh�� ���� ��� �ʱ� ��ġ�� �������� ����
            nav.Warp(homePosition);
        }

        // isWalk �ִϸ��̼� ���
        if (!anim.GetBool("isWalk") && !anim.GetCurrentAnimatorStateInfo(0).IsName("isWalk"))
        {
            // "isWalk" �ִϸ��̼��� ��� ���� �ƴ϶�� ���
            anim.SetBool("isWalk", true);
        }

        nav.SetDestination(homePosition);

        if (Vector3.Distance(transform.position, homePosition) < 2f)
        {
            isReturn = false;
            anim.SetBool("isWalk", false);

            // BoxCollider Ȱ��ȭ
            boxCollider.enabled = true;
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
                // 0.8�� ��� �� �Ѿ� ���� �� �߻�
                yield return new WaitForSeconds(0.8f);
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
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return;
        }

        // ����Ʈ�� ��ų�� Ȱ��ȭ�Ǿ� ������ �⺻ �������� ����Ʈ�� �������� �ջ��մϴ�.
        float totalDamage = bullet.damage + (bullet.isLightningActive ? bullet.lightningDamage : 0);

        // ���� ȿ���� �ִ��� Ȯ���ϰ�, ������ �ش� �������� �����մϴ�.
        float damageToApply = bullet.isExplosion ? bullet.boomShotDamage : totalDamage;
        curHealth -= damageToApply; // Apply damage

        Debug.Log(gameObject.name + "�� �������� �޾ҽ��ϴ�. ������: " + damageToApply + ", ���� ü��: " + curHealth);

        Vector3 reactVec = transform.position - hitPoint;
        StartCoroutine(OnDamage(reactVec));

        if (!bullet.isPenetrating) // ���� �Ѿ��� �ƴϸ� �Ѿ��� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        {
            bullet.ReturnToPool(); // �Ѿ� ��ȯ
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

            // ���� Ȯ���� ���� ������ ����
            float dropChance = Random.Range(0f, 1f);
            if (dropChance <= 0.2f) // 20%�� Ȯ���� ������ ���
            {
                GameObject _health = Instantiate(itemPrefabHeart); // ������ ����
                _health.transform.position = dropPositionHeart.position; // ������ ��ġ ����

                HeartItem health = _health.GetComponent<HeartItem>();
                if (health != null)
                {
                    // health.healAmount = Random.Range(10, 31); // �̰� ȸ���� ����
                    health.healAmount = 10; // ü�� ȸ���� 10
                }
            }

            // _item�̶�� ���� ������Ʈ ���� ���� + itemPrefab�� �����ؼ� _item�� �Ҵ�
            GameObject _item;

            int dropCount = Random.Range(minDropCount, maxDropCount + 1); // minDropCount���� maxDropCount������ ������ ����
            for (int i = 0; i < dropCount; i++) // dropCount��ŭ �������� �����մϴ�.
            {
                _item = Instantiate(itemPrefab); // ������ ����
                _item.transform.position = dropPosition.position; // ������ ��ġ ����
            }

            if (groupManager != null)
            {
                groupManager.OnMonsterDefeated();
            }

            // 2�� �� �� ���
            meleeArea.enabled = false; // ���Ͱ� �׾��� �� �ڽ� �ݶ��̴� ��Ȱ��ȭ
            EnemyBullet.SetActive(false); // ���Ͱ� �׾��� �� EnemyBullet ��Ȱ��ȭ
            Destroy(gameObject, 1);
        }
    }
}