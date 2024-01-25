using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public int healAmount; // 체력 회복량

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌한 경우
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // 플레이어의 체력을 회복하고 아이템을 파괴
                player.health = Mathf.Min(player.health + healAmount, player.maxHealth);
                Destroy(gameObject);
            }
        }
    }
}