using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    //==============================================================
    public GameObject itemprefab; // ��� ������ ������ ���
    [SerializeField] Transform dropPosition; // ��� �������� ���� ��ų ��ġ
    //==============================================================

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

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        // Invoke("ChaseStart", 1);  // 1�� �ڿ� �߰� ����
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    void Update()
    {
        if (isChase)
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
            transform.LookAt(target);
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
                targetRadius = 1f;
                targetRange = 1f;
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
                yield return new WaitForSeconds(0f);
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
                yield return new WaitForSeconds(1.8f);
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
        curHealth -= bullet.damage; // ������ ����

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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isChase = false;
            anim.SetBool("isWalk", false);
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
            isChase = false;
            anim.SetBool("doGetHit", true);
            // nav.enabled = false;
            
        }
        // ���� ü���� 0 ������ ���
        else
        {
            // ������ ȸ������ �����ϰ� ���̾ "Dead"�� ����
            // mat.color = Color.gray;
            StopAllCoroutines();
            isChase = true;
            //nav.enabled = false;
            //anim.SetBool("doGetHit", true);

            // gameObject.layer = 12; // ���̾ �����Ͽ� �ٽ� ������ ���� �ʵ��� ����

            // �߰� ����, �׺���̼� ��Ȱ��ȭ, ���� �ִϸ��̼� ���
            isChase = false;
            //nav.enabled = false;
            anim.SetTrigger("doDie");

            // �ǰݵ� ���� ���͸� ����ȭ�ϰ� ���� ���� �̵�������
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            // ������ٵ� �ǰ� ���������� ���� ���� ���ϰ�
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            // _item�̶�� ���� ������Ʈ ���� ���� + itemprefab�� �����ؼ� _item�� �Ҵ�
            GameObject _item = Instantiate(itemprefab);
            // _item�� ��ġ�� ��� ��ġ�� ���� ��Ŵ
            _item.transform.position = dropPosition.position;

            // 2�� �� �� ���
            Destroy(gameObject, 2);
            
        }
    }
}