using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InMonsterLongAttack : MonoBehaviour
{
    public float maxHealth; // �ִ� ü��
    public float curHealth; // ���� ü��
    public Transform target; // �÷��̾��� Transform
    public GameObject bullet; // ���Ÿ� ���ݿ� ���Ǵ� �Ѿ�
    public bool isChase; // �߰� ���� ����
    public bool isAttack; // ���� ���� ����
    public bool isReturn;
    public float chaseRange;
    public int minDropCount; // ��� �������� �ּ� ����
    public int maxDropCount; // ��� �������� �ִ� ����
    public float targetRange = 0;
    public float targetRadius = 0;
    public float attackDuration = 1f; // ������ ���ӵǴ� �ð�
    private bool isAttackHit = false; // �Ϲ� ������ ���������� �����ߴ��� ����

    public float sightRange = 10f; // Ÿ���� ���� �ν�

    // ü�¹�
    public Slider healthBarSlider;
    public GameObject healthBarUI;
    public Transform headPosition;
    public Vector3 healthBarOffset;

    // ���� �׷�
    public MonsterGroupManager groupManager;

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

    void Awake()
    {
        homePosition = transform.position;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        nav = GetComponent<NavMeshAgent>();
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
        if (healthBarUI != null)
        {
            // ü�¹ٸ� ���� �Ӹ� ���� �̵���Ű��
            healthBarUI.transform.position = headPosition.position + healthBarOffset;

            // ü�¹ٰ� ī�޶� �ٶ󺸵��� �ϱ�
            healthBarUI.transform.rotation = Quaternion.Euler(healthBarUI.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, healthBarUI.transform.rotation.eulerAngles.z);
        }

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

        if (Vector3.Distance(transform.position, homePosition) < 12f)
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

    IEnumerator Attack()
    {
        // �߰� ���� �� ���� ���·� ��ȯ
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // ��ä�� ���·� �Ѿ� 2�� �߻�
            for (int i = 0; i < 2; i++)
            {
                // �Ѿ� �߻� ������ ����մϴ�.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 10;
            }

            // isAttackHit = true; // ������ ���������� �����ߴٰ� ǥ��
            yield return new WaitForSeconds(2f);
        }

        // ���� ���� ����
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

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

        // ü�¹� ������Ʈ
        healthBarSlider.value = curHealth;

        // ü���� 0 �����̸� ü�¹� UI�� ��Ȱ��ȭ�մϴ�.
        if (curHealth <= 0)
        {
            healthBarUI.SetActive(false);  // ü�¹� ��Ȱ��ȭ
        }

        Debug.Log(gameObject.name + "�� �������� �޾ҽ��ϴ�. ������: " + damageToApply + ", ���� ü��: " + curHealth);

        Vector3 reactVec = transform.position - hitPoint;
        StartCoroutine(OnDamage(reactVec));

        if (!bullet.isPenetrating) // ���� �Ѿ��� �ƴϸ� �Ѿ��� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        {
            bullet.ReturnToPool(); // �Ѿ� ��ȯ
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ���Ͱ� �׾����� Ȯ���մϴ�.
        if (gameObject.layer == LayerMask.NameToLayer("Enemydead"))
        {
            return; // ���Ͱ� �׾����� �ƹ��͵� ���� �ʽ��ϴ�.
        }

        if (other.tag == "Player")
        {
            isChase = true;
            anim.SetBool("isWalk", true);
        }       
    }

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
            Destroy(gameObject, 1);
        }
    }
}