using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage; // 총알의 기본 공격력
    public float damage; // 총알의 공격력  
    public float lifeTime; // 총알의 최대 생명 시간 (사거리 제한)
    private float lifeTimer; // 현재까지의 생명 시간을 추적하는 타이머
    private ObjectPool<GameObject> pool;
    
    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }
    
    void OnEnable()
    {
        // 총알이 활성화될 때마다 기본 데미지로 초기화합니다.
        damage = baseDamage;
        lifeTimer = lifeTime; // 타이머를 최대 생명 시간으로 초기화합니다.
    }

    // 오브젝트 풀에 반환하는 메서드를 추가합니다.
    
    public void ReturnToPool()
    {
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
            ReturnToPool();
        }
    }

    // 충돌 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            ReturnToPool();
        }
    }

    // 트리거 충돌 시 호출되는 메서드
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall") // 원거리 공격이면서 벽과 충돌한 경우
        {
            ReturnToPool(); // 총알 즉시 제거
        }
    }
}