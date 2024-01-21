using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage; // �Ѿ��� �⺻ ���ݷ�
    public float damage; // �Ѿ��� ���ݷ�  
    public float lifeTime; // �Ѿ��� �ִ� ���� �ð� (��Ÿ� ����)
    private float lifeTimer; // ��������� ���� �ð��� �����ϴ� Ÿ�̸�
    public float acceleration; // �Ѿ��� ���ӵ�
    private Rigidbody bulletRigidbody; // �Ѿ��� Rigidbody ����

    public GameObject explosionPrefab; // ���� ȿ�� ������

    private ObjectPool<GameObject> pool;

    public bool isPenetrating; // ���뼦 ����
    
    // BoomShot ��ų �Ӽ�
    public bool isBoomShotActive;
    public float boomShotRadius; // �ռ� ���� �ݰ�
    public float boomShotDamage; // �ռ� ������
    public bool isExplosion = false;

    // SideShot ��ų �Ӽ�
    public bool isHoming; // ���� ��� Ȱ��ȭ ����
    public Transform target; // ������ ���
    public float homingSpeed = 20f; // ���� �ӵ�
    public float homingRotateSpeed = 200f; // ���� ȸ�� �ӵ�

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }
    
    void OnEnable()
    {
        // �Ѿ��� Ȱ��ȭ�� ������ �⺻ �������� �ʱ�ȭ�մϴ�.
        damage = baseDamage;
        lifeTimer = lifeTime; // Ÿ�̸Ӹ� �ִ� ���� �ð����� �ʱ�ȭ�մϴ�.
        isExplosion = false; // ���� ���θ� false�� �缳��
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
        }
        isHoming = false;
        target = null;
        isPenetrating = false;
        isBoomShotActive = false;

        // ��ƼŬ �ý����� �ִٸ� ��Ȱ��ȭ�ߴٰ� �ٽ� Ȱ��ȭ

        if (explosionPrefab != null)
        {
            ParticleSystem ps = explosionPrefab.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }        
    }

    // ������Ʈ Ǯ�� ��ȯ�ϴ� �޼��带 �߰��մϴ�.
    
    public void ReturnToPool()
    {
        // ������Ʈ�� Ȱ��ȭ ������ ��쿡�� Ǯ�� ��ȯ�մϴ�.
        if (gameObject.activeInHierarchy)
        {
            if (pool != null)
            {
                // ������Ʈ�� ��Ȱ��ȭ�ϰ� Ǯ�� ��ȯ�մϴ�.
                gameObject.SetActive(false);
                pool.Release(gameObject);
            }
            else
            {
                // Ǯ�� �����Ǿ� ���� �ʴٸ�, �⺻���� Destroy�� ȣ���մϴ�.
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        // �� �����Ӹ��� Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0) // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� �Ѿ��� �ı��մϴ�.
        {
            if (isBoomShotActive) // �ռ��� Ȱ��ȭ�Ǿ� ������ �����մϴ�.
            {
                Explode();
            }
            else // �׷��� �ʴٸ� �Ѿ��� ��ȯ�մϴ�.
            {
                ReturnToPool();
            }
        }
        else if (isPenetrating)
        {
            // �Ǿ���� �� �Ѿ˿� ���ӵ��� �����մϴ�.
            bulletRigidbody.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }       

    }
    void OnDisable()
    {
        // Rigidbody�� �ӵ��� ���ӵ��� �ʱ�ȭ�մϴ�.
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
        }

        // �Ѿ��� �ٸ� �Ӽ����� �⺻������ �缳���մϴ�.
        isPenetrating = false;
        isBoomShotActive = false;
        isExplosion = false;
        isHoming = false;
        target = null;

        // ���� ȿ���� �ִٸ� �ʱ�ȭ�մϴ�.
        if (explosionPrefab != null)
        {
            ParticleSystem ps = explosionPrefab.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }        
    }
    void FixedUpdate()
    {        
        // ���� ����� Ȱ��ȭ�Ǿ� �ְ�, Ÿ���� �����Ǿ� �ִٸ� ���� ������ �����մϴ�.
        if (isHoming && target != null)
        {
            // Ÿ�� �������� �Ѿ��� ������ �ε巴�� ȸ����ŵ�ϴ�.
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Quaternion.Slerp�� ����Ͽ� ���� �ε巯�� ȸ���� �����մϴ�.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, homingRotateSpeed * Time.deltaTime);

            // �Ѿ��� �ӵ��� ���� ������ŵ�ϴ�.
            bulletRigidbody.velocity += transform.forward * (homingSpeed * Time.fixedDeltaTime);
            bulletRigidbody.velocity = Vector3.ClampMagnitude(bulletRigidbody.velocity, homingSpeed); // �ִ� �ӵ��� �����մϴ�.
        }
    }

    // �浹 �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        // �Ѿ��� ���� �浹���� ���
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // ������ �������� �����ϴ�.
                enemy.TakeDamage(this, collision.contacts[0].point);

                // �ռ��� Ȱ��ȭ�Ǿ� �ְ� ���뼦�� Ȱ��ȭ�Ǿ� ���� �ʴٸ� �����մϴ�.
                if (isBoomShotActive && !isPenetrating)
                {
                    Explode();
                }
                else if (!isPenetrating) // ���뼦�� Ȱ��ȭ�Ǿ� ���� �ʴٸ� �Ѿ��� ��ȯ�մϴ�.
                {
                    ReturnToPool();
                }
            }            
        }
        else if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            // �ռ��� Ȱ��ȭ�Ǿ� ������ �����մϴ�.
            if (isBoomShotActive)
            {
                Explode();
            }
            else // �ռ��� Ȱ��ȭ�Ǿ� ���� �ʴٸ� �Ѿ��� ��ȯ�մϴ�.
            {
                ReturnToPool();
            }
        }
    }

    // Ʈ���� �浹 �� ȣ��Ǵ� �޼���
    void OnTriggerEnter(Collider other)
    {
        // �Ѿ��� ���� �浹���� ���
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // ������ �������� �����ϴ�.
                enemy.TakeDamage(this, other.ClosestPointOnBounds(transform.position));

                // �ռ��� Ȱ��ȭ�Ǿ� �ְ� ���뼦�� Ȱ��ȭ�Ǿ� ���� �ʴٸ� �����մϴ�.
                if (isBoomShotActive && !isPenetrating)
                {
                    Explode();
                }
            }
            else if (!isPenetrating) // ���뼦�� Ȱ��ȭ�Ǿ� ���� �ʴٸ� �Ѿ��� ��ȯ�մϴ�.
            {
                ReturnToPool();
            }
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Floor"))
        {
            // �ռ��� Ȱ��ȭ�Ǿ� ������ �����մϴ�.
            if (isBoomShotActive)
            {
                Explode();
            }
            else // �ռ��� Ȱ��ȭ�Ǿ� ���� �ʴٸ� �Ѿ��� ��ȯ�մϴ�.
            {
                ReturnToPool();
            }
        }
    }

    // ���� ó�� �޼���
    private void Explode()
    {
        isExplosion = true; // ���� �߻� �� true�� ����

        // ���� ���� ���� ��� �ݶ��̴��� ã���ϴ�.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, boomShotRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {                   
                    enemy.TakeDamage(this, transform.position); // ���߷� ���� ������ ����
                }
            }
        }

        // ���� ȿ�� ��ƼŬ�� �����մϴ�.
        if (explosionPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffect, 1f); // ��ƼŬ�� ����� ��ġ�� �ڵ����� �ı��ǵ��� ����
        }
        ReturnToPool(); // ���� �� �Ѿ��� ������Ʈ Ǯ�� ��ȯ
    }
    
}