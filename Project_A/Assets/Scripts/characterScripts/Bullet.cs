using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage = 10; // 총알의 기본 공격력
    public float damage; // 총알의 공격력  
    public float lifeTime; // 총알의 최대 생명 시간 (사거리 제한)
    private float lifeTimer; // 현재까지의 생명 시간을 추적하는 타이머
    public GameObject explosionPrefab; // 폭발 효과 프리팹

    private ObjectPool<GameObject> pool;

    public bool isPenetrating; // 관통샷 여부
    
    // BoomShot 스킬 속성
    public bool isBoomShotActive;
    public float boomShotRadius; // 붐샷 폭발 반경
    public float boomShotDamage; // 붐샷 데미지
    public bool isExplosion = false;

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
        if (!gameObject.activeInHierarchy)
        {
            // 오브젝트가 이미 비활성화되었다면 반환을 시도하지 않습니다.
            return;
        }

        if (pool != null)
        {
            pool.Release(gameObject);
        }
        else
        {
            // 풀이 설정되어 있지 않다면, 기본적인 Destroy를 호출합니다.
            Destroy(gameObject);
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
    }

    // 충돌 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        // 총알이 적과 충돌했을 경우
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 적에게 데미지를 입힙니다.
                enemy.TakeDamage(this, collision.contacts[0].point);

                // 붐샷이 활성화되어 있고 관통샷이 활성화되어 있지 않다면 폭발합니다.
                if (isBoomShotActive && !isPenetrating)
                {
                    Explode();
                }
                else if (!isPenetrating) // 관통샷이 활성화되어 있지 않다면 총알을 반환합니다.
                {
                    ReturnToPool();
                }
            }            
        }
        else if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            // 붐샷이 활성화되어 있으면 폭발합니다.
            if (isBoomShotActive)
            {
                Explode();
            }
            else // 붐샷이 활성화되어 있지 않다면 총알을 반환합니다.
            {
                ReturnToPool();
            }
        }
    }

    // 트리거 충돌 시 호출되는 메서드
    void OnTriggerEnter(Collider other)
    {
        // 총알이 적과 충돌했을 경우
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 적에게 데미지를 입힙니다.
                enemy.TakeDamage(this, other.ClosestPointOnBounds(transform.position));

                // 붐샷이 활성화되어 있고 관통샷이 활성화되어 있지 않다면 폭발합니다.
                if (isBoomShotActive && !isPenetrating)
                {
                    Explode();
                }
            }
            else if (!isPenetrating) // 관통샷이 활성화되어 있지 않다면 총알을 반환합니다.
            {
                ReturnToPool();
            }
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Floor"))
        {
            // 붐샷이 활성화되어 있으면 폭발합니다.
            if (isBoomShotActive)
            {
                Explode();
            }
            else // 붐샷이 활성화되어 있지 않다면 총알을 반환합니다.
            {
                ReturnToPool();
            }
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