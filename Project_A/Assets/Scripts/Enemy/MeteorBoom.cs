using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBoom : MonoBehaviour
{
    public float duration; // 불 효과 지속 시간
    public int damage; // 플레이어에게 입히는 피해량
    private float damageInterval; // 피해를 입히는 간격
    private float lastDamageTime; // 마지막으로 피해를 입힌 시간

    void Start()
    {
        // 지정한 시간 후에 불 효과 제거
        Destroy(gameObject, duration);
    }
    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 처음 닿았을 때
        if (other.CompareTag("Player"))
        {
            // 마지막으로 피해를 입힌 시간을 현재 시간으로 설정            
            lastDamageTime = Time.time;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // 플레이어와 계속 닿아 있을 때
        if (other.CompareTag("Player"))
        {
            // 마지막으로 피해를 입힌 이후 1초가 지났다면 다시 피해를 입히고, 마지막으로 피해를 입힌 시간을 현재 시간으로 설정
            if (Time.time >= lastDamageTime + damageInterval)
            {                
                lastDamageTime = Time.time;
            }
        }
    }
    
}