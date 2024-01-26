using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyDragon : MonoBehaviour
{
    public float maxHealth; // �ִ� ü��
    public float curHealth; // ���� ü��
    public Transform target; // �÷��̾��� Transform
    public GameObject bullet; // ���Ÿ� ���ݿ� ���Ǵ� �Ѿ�
    public bool isChase; // �߰� ���� ����
    public bool isAttack; // ���� ���� ����   
    public int minDropCount; // ��� �������� �ּ� ����
    public int maxDropCount; // ��� �������� �ִ� ����
    public float targetRange = 0;
    public float targetRadius = 0;
    public float attackDuration = 1f; // ������ ���ӵǴ� �ð�
    private bool isAttackHit = false; // �Ϲ� ������ ���������� �����ߴ��� ����
    public float attackHitCooldown = 0.5f; // ���� �Ϲ� ������ ������ �� �ִ� ��ٿ� �ð�
    public Camera followCamera;

    public float sightRange = 10f; // Ÿ���� ���� �ν�

    //==============================================================
    public GameObject itemPrefab; // ��� ������ ������ ���
    [SerializeField] Transform dropPosition; // ��� �������� ���� ��ų ��ġ
    //==============================================================
    public Slider healthBarSlider;
    public GameObject healthBarUI;
    public float HealthBarRange = 15f; // ü�¹ٰ� Ȱ��ȭ�� �÷��̾���� �Ÿ�
    Rigidbody rigid; // Rigidbody ������Ʈ
    BoxCollider boxCollider; // BoxCollider ������Ʈ
    Material mat; // Material ������Ʈ
    NavMeshAgent nav; // NavMeshAgent ������Ʈ
    Animator anim; // Animator ������Ʈ

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        anim = GetComponentInChildren<Animator>();

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        nav = GetComponent<NavMeshAgent>();
        healthBarSlider.maxValue = maxHealth; // Slider�� �ִ밪�� ������ �ִ� ü������ �����մϴ�.
        healthBarSlider.value = curHealth; // Slider�� ���簪�� ������ ���� ü������ �����մϴ�.
        healthBarUI.SetActive(false);
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
        if (curHealth > 0 && distanceToPlayer <= HealthBarRange)
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

        isChase = false;
        anim.SetBool("isAttack4", false);
        anim.SetBool("isWalk", false);
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
        /*

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // �÷��̾ ���� ���� ���� �ȿ� ������ Attack �ڷ�ƾ ȣ��
        if (distanceToPlayer <= targetRadius)
        {
            if (!isAttack)
            {
                StartCoroutine(Attack());
            }
        }
        */

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
    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = followCamera.transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            followCamera.transform.position = new Vector3(followCamera.transform.position.x + x, followCamera.transform.position.y + y, originalPosition.z);


            elapsedTime += Time.deltaTime;

            yield return null;
        }

        followCamera.transform.position = originalPosition;
    }

    // IEnumerator�� ����� Attack �ڷ�ƾ �Լ�
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

            // ��ä�� ���·� �Ѿ� 15�� �߻�
            for (int i = 0; i < 15; i++)
            {
                // �Ѿ� �߻� ������ ����մϴ�.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;
            }

            isAttackHit = true; // ������ ���������� �����ߴٰ� ǥ��
            StartCoroutine(ResetAttackHit()); // ���� ���� ���� �ʱ�ȭ �ڷ�ƾ ����
            yield return new WaitForSeconds(1.5f);
        }

        // ���� ���� ����
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);

        // �߰� ���� �� ���� ���·� ��ȯ
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack2", true);

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // ��ä�� ���·� �Ѿ� 25�� �߻�
            for (int i = 0; i < 25; i++)
            {
                // �Ѿ� �߻� ������ ����մϴ�.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 10), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;

                // ���� �Ѿ� �߻� ���� ��� ���
                // yield return new WaitForSeconds(0.3f); // �� ���� �����Ͽ� �Ѿ� �߻� ������ ������ �� �ֽ��ϴ�.
            }

            isAttackHit = true; // ������ ���������� �����ߴٰ� ǥ��
            StartCoroutine(ResetAttackHit()); // ���� ���� ���� �ʱ�ȭ �ڷ�ƾ ����
            yield return new WaitForSeconds(1.5f);
        }

        // ���� ���� ����
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack2", false);

        // �߰� ���� �� ���� ���·� ��ȯ
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack3", true);

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // �Ѿ� 60���� �߻��ϱ� ���� ī�޶� ���ϴ�.
            StartCoroutine(ShakeCamera(0.5f, 0.5f));

            // ��ä�� ���·� �Ѿ� 50�� �߻�
            for (int i = 0; i < 50; i++)
            {
                // �Ѿ� �߻� ������ ����մϴ�.
                Quaternion bulletRotation = Quaternion.Euler(0, -30 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;

                // ���� �Ѿ� �߻� ���� ��� ���
                yield return new WaitForSeconds(0.15f); // �� ���� �����Ͽ� �Ѿ� �߻� ������ ������ �� �ֽ��ϴ�.
            }

            isAttackHit = true; // ������ ���������� �����ߴٰ� ǥ��
            StartCoroutine(ResetAttackHit()); // ���� ���� ���� �ʱ�ȭ �ڷ�ƾ ����
            yield return new WaitForSeconds(1.5f);
        }

        // ���� ���� ����
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack3", false);

        // �߰� ���� �� ���� ���·� ��ȯ
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack4", true);

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(attackDuration);

        if (curHealth > 0 && !isAttackHit)
        {
            // ��ä�� ���·� �Ѿ� 100�� �߻�
            for (int i = 0; i < 100; i++)
            {
                // �Ѿ� �߻� ������ ����մϴ�.
                Quaternion bulletRotation = Quaternion.Euler(0, -20 + (i * 20), 0) * transform.rotation;
                GameObject instantBullet = Instantiate(bullet, transform.position, bulletRotation);

                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = instantBullet.transform.forward * 15;
            }

            isAttackHit = true; // ������ ���������� �����ߴٰ� ǥ��
            StartCoroutine(ResetAttackHit()); // ���� ���� ���� �ʱ�ȭ �ڷ�ƾ ����
            yield return new WaitForSeconds(1.5f);
        }

        // ���� ���� ����
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack4", false);
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

    void OnCollisionEnter(Collision collision)
    {

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

            // ���� �ʾ��� ���� ü�¹ٸ� Ȱ��ȭ�մϴ�.
            if (curHealth > 0)
            {
                healthBarUI.SetActive(true);
            }
            else if (other.tag == "Bullet") // "Bullet"�� �÷��̾��� �Ѿ˿� ���� �±׸� �Է����ּ���.
            {
                TakeDamage(other.GetComponent<Bullet>(), other.transform.position);
            }
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
            // ü�¹ٸ� 0���� ����
            healthBarSlider.value = 0;

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
            // rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item�̶�� ���� ������Ʈ ���� ���� + itemPrefab�� �����ؼ� _item�� �Ҵ�
            GameObject _item;
            int dropCount = Random.Range(minDropCount, maxDropCount + 1); // minDropCount���� maxDropCount������ ������ ����
            for (int i = 0; i < dropCount; i++) // dropCount��ŭ �������� �����մϴ�.
            {
                _item = Instantiate(itemPrefab); // ������ ����
                _item.transform.position = dropPosition.position; // ������ ��ġ ����
            }

            // 2�� �� �� ���
            Destroy(gameObject, 10);
        }
    }
}