using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public int healAmount; // ü�� ȸ����

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �浹�� ���
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // �÷��̾��� ü���� ȸ���ϰ� �������� �ı�
                player.health = Mathf.Min(player.health + healAmount, player.maxHealth);
                Destroy(gameObject);
            }
        }
    }
}