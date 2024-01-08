using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRat : MonoBehaviour
{
    public enum Type { A, B, C };
    public Type enemyType; // �� ����
    public int maxHealth; // �ִ� ü��
    public int curHealth; // ���� ü��
    public Transform target; // �÷��̾��� Transform
    public BoxCollider meleeArea; // ���� ���� ���� Collider
    public GameObject bullet; // ���Ÿ� ���ݿ� ���Ǵ� �Ѿ�
    public bool isChase; // �߰� ���� ����
    public bool isAttack; // ���� ���� ����

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
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Invoke("ChaseStart", 1);  // 1�� �ڿ� �߰� ����
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

        // �� ������ ���� Ž�� ���� ����
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
                yield return new WaitForSeconds(0.01f);
                meleeArea.enabled = true;

                // 1�� �� ���� ���� ���� ��Ȱ��ȭ
                yield return new WaitForSeconds(0f);
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
                rigidBullet.velocity = transform.forward * 20;

                // 2�� ���
                yield return new WaitForSeconds(2f);
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

    // �ǰ� �� �߻��ϴ� �浹 �̺�Ʈ ó�� �Լ�
    void OnTriggerEnter(Collider other)
    {
        // ���� �浹�� ������Ʈ�� �±װ� "Melee"�� ���
        if (other.tag == "Melee")  // ���� ������ �޾��� ��
        {
            // �浹�� ������Ʈ���� Weapon ������Ʈ ȹ��
            Weapon weapon = other.GetComponent<Weapon>();

            // ���� ü�¿��� ������ ��������ŭ ����
            curHealth -= weapon.damage;

            // �������� ���� ��ġ ���� ���
            Vector3 reactVec = transform.position - other.transform.position;

            // OnDamage �ڷ�ƾ ����
            StartCoroutine(OnDamage(reactVec));
        }
        // ���� �浹�� ������Ʈ�� �±װ� "Bullet"�� ���
        else if (other.tag == "Bullet") // ���Ÿ� ������ �޾��� ��
        {
            // �浹�� ������Ʈ���� Bullet ������Ʈ ȹ��
            Bullet bullet = other.GetComponent<Bullet>();

            // ���� ü�¿��� �Ѿ��� ��������ŭ ����
            curHealth -= bullet.damage;

            // �������� ���� ��ġ ���� ���
            Vector3 reactVec = transform.position - other.transform.position;

            // �浹�� �Ѿ� ������Ʈ �ı�
            Destroy(other.gameObject);

            // OnDamage �ڷ�ƾ ����
            StartCoroutine(OnDamage(reactVec));
        }
    }

    // �ǰ� �� �߻��ϴ� �ڷ�ƾ �Լ�
    IEnumerator OnDamage(Vector3 reactVec)
    {
        // �ǰ� �� �Ͻ������� ĳ���� ������ ���������� ����
        // mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // ���� ü���� 0���� ū ��� ������ ������� �ǵ���
        if (curHealth > 0)
        {
            // mat.color = Color.white;
            isChase = true;
            // nav.enabled = false;
            anim.SetBool("doGetHit", true);
        }
        // ���� ü���� 0 ������ ���
        else
        {
            // ������ ȸ������ �����ϰ� ���̾ "Dead"�� ����
            // mat.color = Color.gray;
            isChase = true;
            nav.enabled = false;
            anim.SetBool("doGetHit", true);

            gameObject.layer = 14; // ���̾ �����Ͽ� �ٽ� ������ ���� �ʵ��� ����

            // �߰� ����, �׺���̼� ��Ȱ��ȭ, ���� �ִϸ��̼� ���
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");

            // �ǰݵ� ���� ���͸� ����ȭ�ϰ� ���� ���� �̵�������
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // ������ٵ� �ǰ� ���������� ���� ���� ���ϰ�, �߶� �ִϸ��̼� ��� �� 4�� �Ŀ� ���� ������Ʈ �ı�
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            Destroy(gameObject, 4);
        }
    }
}