using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // 총알의 공격력
    public bool isMelee; // 근접 공격인지 여부를 나타내는 플래그

    

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