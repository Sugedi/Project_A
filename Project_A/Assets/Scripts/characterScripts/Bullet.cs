using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage = 10; // �Ѿ��� �⺻ ���ݷ�
    public float damage; // �Ѿ��� ���ݷ�  
    public float lifeTime; // �Ѿ��� �ִ� ���� �ð� (��Ÿ� ����)
    private float lifeTimer; // ��������� ���� �ð��� �����ϴ� Ÿ�̸�
    public GameObject explosionPrefab; // ���� ȿ�� ������

    private ObjectPool<GameObject> pool;

    public bool isPenetrating; // ���뼦 ����
    
    // BoomShot ��ų �Ӽ�
    public bool isBoomShotActive;
    public float boomShotRadius; // �ռ� ���� �ݰ�
    public float boomShotDamage; // �ռ� ������
    public bool isExplosion = false;

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
        if (!gameObject.activeInHierarchy)
        {
            // ������Ʈ�� �̹� ��Ȱ��ȭ�Ǿ��ٸ� ��ȯ�� �õ����� �ʽ��ϴ�.
            return;
        }

        if (pool != null)
        {
            pool.Release(gameObject);
        }
        else
        {
            // Ǯ�� �����Ǿ� ���� �ʴٸ�, �⺻���� Destroy�� ȣ���մϴ�.
            Destroy(gameObject);
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