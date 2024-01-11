using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // 총알의 공격력
    public bool isMelee; // 근접 공격인지 여부를 나타내는 플래그
    public float lifeTime; // 총알의 최대 생명 시간 (사거리 제한)
    private float lifeTimer; // 현재까지의 생명 시간을 추적하는 타이머

    void Start()
    {
        lifeTimer = lifeTime; // 타이머를 최대 생명 시간으로 초기화합니다.
    }

    void Update()
    {
        // 매 프레임마다 타이머를 감소시킵니다.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0) // 타이머가 0 이하가 되면 총알을 파괴합니다.
        {
            Destroy(gameObject);
        }
    }

    // 충돌 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // 바닥과 충돌한 경우
        {
            Destroy(gameObject, 3); // 3초 후에 총알 제거
        }
        else if (collision.gameObject.tag == "Wall") // 벽과 충돌한 경우
        {
            Destroy(gameObject); // 총알 즉시 제거
        }
    }

    // 트리거 충돌 시 호출되는 메서드
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") // 원거리 공격이면서 벽과 충돌한 경우
        {
            Destroy(gameObject); // 총알 즉시 제거
        }
    }
}