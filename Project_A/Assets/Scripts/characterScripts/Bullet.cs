using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage; // �Ѿ��� �⺻ ���ݷ�
    public float damage; // �Ѿ��� ���ݷ�  
    public float lifeTime = 0.7f; // �Ѿ��� �ִ� ���� �ð� (��Ÿ� ����)
    private float lifeTimer; // ��������� ���� �ð��� �����ϴ� Ÿ�̸�
    public float acceleration = 8; // �Ѿ��� ���ӵ�
    public float maxRange = 8f; // �ִ� ��Ÿ�
    private float traveledDistance; // �Ѿ��� �̵��� �Ÿ�
    private Rigidbody bulletRigidbody; // �Ѿ��� Rigidbody ����

    public GameObject explosionPrefab; // ���� ȿ�� ������
    public GameObject hitEffectPrefab;

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

    // ���� ��ų ���� �Ӽ�
    public bool isLightningActive = false;
    public ParticleSystem lightningEffectPrefab;
    public float lightningDamage;
    public float spawnHeight;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }
    public void Initialize()
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
        isLightningActive = false;
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
    void OnEnable()
    {
        Initialize();
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
        isLightningActive = false;
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
        bulletRigidbody.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

        // �̵��� �Ÿ��� ������Ʈ�մϴ�.
        float moveDistance = bulletRigidbody.velocity.magnitude * Time.fixedDeltaTime;
        traveledDistance += moveDistance;

        // �̵��� �Ÿ��� �ִ� ��Ÿ��� �ʰ��ߴ��� Ȯ���մϴ�.
        if (traveledDistance >= maxRange)
        {
            // �ִ� ��Ÿ��� �����ϸ� �Ѿ��� ��Ȱ��ȭ�ϰų� �ı��մϴ�.
            ReturnToPool();
        }
    }
   
    // �浹 �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        // �Ѿ��� ���� �浹���� ���
        if (!isPenetrating && collision.gameObject.tag == "Enemy")
        {
            // ���뼦�� �ƴ� ���� ������ �浹�� ó���մϴ�.
            HandleCollisionWithEnemy(collision.gameObject, collision.contacts[0].point);
        }
        else if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            HandleCollisionWithEnvironment();
        }
    }

    // Ʈ���� �浹 �� ȣ��Ǵ� �޼���
    void OnTriggerEnter(Collider other)
    {
        if (isPenetrating && other.CompareTag("Enemy"))
        {
            // ���뼦�� ���� ������ �浹�� ó���մϴ�.
            HandleCollisionWithEnemy(other.gameObject, other.ClosestPointOnBounds(transform.position));
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Floor"))
        {
            // ���뼦�� ���̳� �ٴڰ� �浹���� ���� ó���մϴ�.
            HandleCollisionWithEnvironment();
        }
    }

    // �浹 �� ���� ȿ���� �ߵ��ϴ� �޼���
    void TriggerLightningEffect(Vector3 hitPoint)
    {
        // ���� ��ƼŬ ȿ�� ���� �� ���
        ParticleSystem lightningEffect = Instantiate(lightningEffectPrefab, hitPoint, Quaternion.identity);
        lightningEffect.Play();
        Destroy(lightningEffect.gameObject, lightningEffect.main.duration);
    }

    void HandleCollisionWithEnemy(GameObject enemyObject, Vector3 hitPoint)
    {
        Debug.Log("HandleCollisionWithEnemy called"); // ����� �α� �߰�

        // ���� ȿ�� �ߵ�
        if (isLightningActive)
        {
            TriggerLightningEffect(hitPoint);
        }

        // �ǰ� ����Ʈ �ߵ�
        TriggerHitEffect(hitPoint);

        // ������ �������� �����մϴ�.
        ApplyDamageToEnemy(enemyObject, this, hitPoint);

        // ���� ȿ�� �� �Ѿ� ȸ�� ����
        if (isBoomShotActive)
        {
            Explode();
        }
        else if (!isPenetrating)
        {
            ReturnToPool();
        }
    }
    void TriggerHitEffect(Vector3 position)
    {
        // �ǰ� ����Ʈ ��ƼŬ�� �����ϰų� ����ϴ� �ڵ�
        // ���� ���, �ǰ� ����Ʈ ��ƼŬ �������� �ν��Ͻ��� �����ϰ� ��ġ�� �����մϴ�.
        GameObject hitEffectInstance = Instantiate(hitEffectPrefab, position, Quaternion.identity);
        ParticleSystem hitParticles = hitEffectInstance.GetComponent<ParticleSystem>();
        if (hitParticles && !hitParticles.isPlaying)
        {
            hitParticles.Play();
        }

        Destroy(hitEffectInstance, hitParticles.main.duration); // ��ƼŬ ����� ������ ������Ʈ �ı�
    }

    void HandleCollisionWithEnvironment()
    {
        if (isBoomShotActive)
        {
            Explode();
        }

        ReturnToPool();
    }
    void ApplyDamageToEnemy(GameObject enemyObject, Bullet bullet, Vector3 hitPoint)
    {
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(bullet, hitPoint);
            return;
        }

        EnemyWorm enemyWorm = enemyObject.GetComponent<EnemyWorm>();
        if (enemyWorm != null)
        {            
            enemyWorm.TakeDamage(bullet, hitPoint);
            return;
        }

        EnemyBoss enemyBoss = enemyObject.GetComponent<EnemyBoss>();
        if (enemyBoss != null)
        {            
            enemyBoss.bTakeDamage(bullet, hitPoint);
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