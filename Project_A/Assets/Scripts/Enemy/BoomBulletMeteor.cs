using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBulletMeteor : MonoBehaviour
{
    public int damage; // 총알의 공격력
    public bool isMelee; // 근접 공격인지 여부를 나타내는 플래그

    public GameObject fireEffectPrefab; // 불 효과 프리팹

    // 충돌 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // 바닥과 충돌한 경우
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall") // 벽과 충돌한 경우
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    // 트리거 충돌 시 호출되는 메서드
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") // 원거리 공격이면서 벽과 충돌한 경우
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if (!isMelee && other.gameObject.tag == "Floor") // 원거리 공격이면서 바닥과 충돌한 경우
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        // 플레이어나 벽과 충돌했을 경우
        if (!isMelee && other.gameObject.tag == "Player")
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}