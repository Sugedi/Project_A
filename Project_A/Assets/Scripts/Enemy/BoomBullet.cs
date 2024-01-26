using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : MonoBehaviour
{
    public int damage; // 총알의 공격력
    public bool isMelee; // 근접 공격인지 여부를 나타내는 플래그

    public GameObject explosionPrefab; // 폭발 이펙트 프리팹

    // 충돌 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // 바닥과 충돌한 경우
        {
            // 폭발 이펙트를 생성합니다.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject, 3); // 3초 후에 총알 제거
        }
        else if (collision.gameObject.tag == "Wall") // 벽과 충돌한 경우
        {
            // 폭발 이펙트를 생성합니다.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject); // 총알 즉시 제거
        }
        else if (collision.gameObject.tag == "Player")
        {
            // 폭발 이펙트를 생성합니다.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // 투사체를 제거합니다.
            Destroy(gameObject);
        }
    }

    // 트리거 충돌 시 호출되는 메서드
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") // 원거리 공격이면서 벽과 충돌한 경우
        {
            // 폭발 이펙트를 생성합니다.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject); // 총알 즉시 제거
        }
        // 플레이어나 벽과 충돌했을 경우
        if (!isMelee && other.gameObject.tag == "Player")
        {
            // 폭발 이펙트를 생성합니다.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // 투사체를 제거합니다.
            Destroy(gameObject);
        }
    }
}