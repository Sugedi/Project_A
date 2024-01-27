using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌한 경우
        if (other.gameObject.CompareTag("Player"))
        {            
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.hasThreadItem = true;
                //player.IncreaseItemValue();
                Debug.Log("Player has collected the item.");
                Destroy(gameObject);
            }            
        }
        
    }
}