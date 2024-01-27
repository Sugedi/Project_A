using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �浹�� ���
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Destroy(gameObject);
            }
        }
    }
}