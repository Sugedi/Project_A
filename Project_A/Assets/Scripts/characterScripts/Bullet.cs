using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage; // 총알의 기본 공격력
    public float damage; // 총알의 공격력  
    public float lifeTime; // 총알의 최대 생명 시간 (사거리 제한)
    private float lifeTimer; // 현재까지의 생명 시간을 추적하는 타이머
    public float acceleration; // 총알의 가속도
    private Rigidbody bulletRigidbody; // 총알의 Rigidbody 참조

    public GameObject explosionPrefab; // 폭발 효과 프리팹

    private ObjectPool<GameObject> pool;

    public bool isPenetrating; // 관통샷 여부
    
    // BoomShot 스킬 속성
    public bool isBoomShotActive;
    public float boomShotRadius; // 붐샷 폭발 반경
    public float boomShotDamage; // 붐샷 데미지
    public bool isExplosion = false;

    // SideShot 스킬 속성
    public bool isHoming; // 추적 기능 활성화 여부
    public Transform target; // 추적할 대상
    public float homingSpeed = 20f; // 추적 속도
    public float homingRotateSpeed = 200f; // 추적 회전 속도

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
        // 총알이 활성화될 때마다 기본 데미지로 초기화합니다.
        damage = baseDamage;
        lifeTimer = lifeTime; // 타이머를 최대 생명 시간으로 초기화합니다.
        isExplosion = false; // 폭발 여부를 false로 재설정
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
        }
        isHoming = false;
        target = null;
        isPenetrating = false;
        isBoomShotActive = false;

        // 파티클 시스템이 있다면 비활성화했다가 다시 활성화
       
        if (explosionPrefab != null)
        {
            ParticleSystem ps = explosionPrefab.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }        
    }

    // 오브젝트 풀에 반환하는 메서드를 추가합니다.
    
    public void ReturnToPool()
    {
        // 오브젝트가 활성화 상태인 경우에만 풀로 반환합니다.
        if (gameObject.activeInHierarchy)
        {
            if (pool != null)
            {
                // 오브젝트를 비활성화하고 풀로 반환합니다.
                gameObject.SetActive(false);
                pool.Release(gameObject);
            }
            else
            {
                // 풀이 설정되어 있지 않다면, 기본적인 Destroy를 호출합니다.
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        // 매 프레임마다 타이머를 감소시킵니다.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0) // 타이머가 0 이하가 되면 총알을 파괴합니다.
        {
            if (isBoomShotActive) // 붐샷이 활성화되어 있으면 폭발합니다.
            {
                Explode();
            }
            else // 그렇지 않다면 총알을 반환합니다.
            {
                ReturnToPool();
            }
        }
        else if (isPenetrating)
        {
            // 피어스샷일 때 총알에 가속도를 적용합니다.
            bulletRigidbody.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }       

    }
    void OnDisable()
    {
        // Rigidbody의 속도와 각속도를 초기화합니다.
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
        }

        // 총알의 다른 속성들을 기본값으로 재설정합니다.
        isPenetrating = false;
        isBoomShotActive = false;
        isExplosion = false;
        isHoming = false;
        target = null;

        // 폭발 효과가 있다면 초기화합니다.
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
        // 추적 기능이 활성화되어 있고, 타겟이 설정되어 있다면 추적 로직을 수행합니다.
        if (isHoming && target != null)
        {
            // 타겟 방향으로 총알의 방향을 부드럽게 회전시킵니다.
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Quaternion.Slerp를 사용하여 보다 부드러운 회전을 구현합니다.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, homingRotateSpeed * Time.deltaTime);

            // 총알의 속도를 점점 증가시킵니다.
            bulletRigidbody.velocity += transform.forward * (homingSpeed * Time.fixedDeltaTime);
            bulletRigidbody.velocity = Vector3.ClampMagnitude(bulletRigidbody.velocity, homingSpeed); // 최대 속도를 제한합니다.
        }
    }

    // 충돌 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        // 총알이 적과 충돌했을 경우
        if (!isPenetrating && collision.gameObject.tag == "Enemy")
        {
            // 관통샷이 아닐 때만 적과의 충돌을 처리합니다.
            HandleCollisionWithEnemy(collision.gameObject, collision.contacts[0].point);
        }
        else if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            HandleCollisionWithEnvironment();
        }
    }

    // 트리거 충돌 시 호출되는 메서드
    void OnTriggerEnter(Collider other)
    {
        if (isPenetrating && other.CompareTag("Enemy"))
        {
            // 관통샷일 때만 적과의 충돌을 처리합니다.
            HandleCollisionWithEnemy(other.gameObject, other.ClosestPointOnBounds(transform.position));
        }
    }
    void HandleCollisionWithEnemy(GameObject enemyObject, Vector3 hitPoint)
    {
        Debug.Log("HandleCollisionWithEnemy called"); // 디버그 로그 추가
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(this, hitPoint);

            if (isBoomShotActive)
            {
                Explode();
            }
            else if (!isPenetrating)
            {
                ReturnToPool();
            }
        }
    }

    void HandleCollisionWithEnvironment()
    {
        if (isBoomShotActive)
        {
            Explode();
        }
        else
        {
            ReturnToPool();
        }
    }
    // 폭발 처리 메서드
    private void Explode()
    {
        isExplosion = true; // 폭발 발생 시 true로 설정

        // 폭발 범위 내의 모든 콜라이더를 찾습니다.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, boomShotRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {                   
                    enemy.TakeDamage(this, transform.position); // 폭발로 인한 데미지 적용
                }
            }
        }

        // 폭발 효과 파티클을 생성합니다.
        if (explosionPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffect, 1f); // 파티클이 재생을 마치면 자동으로 파괴되도록 설정
        }
        ReturnToPool(); // 폭발 후 총알을 오브젝트 풀로 반환
    }
    
}