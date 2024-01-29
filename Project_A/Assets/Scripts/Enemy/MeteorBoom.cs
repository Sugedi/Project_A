using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBoom : MonoBehaviour
{
    public float duration; // �� ȿ�� ���� �ð�
    public int damage; // �÷��̾�� ������ ���ط�
    private float damageInterval; // ���ظ� ������ ����
    private float lastDamageTime; // ���������� ���ظ� ���� �ð�

    void Start()
    {
        // ������ �ð� �Ŀ� �� ȿ�� ����
        Destroy(gameObject, duration);
    }
    void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� ó�� ����� ��
        if (other.CompareTag("Player"))
        {
            // ���������� ���ظ� ���� �ð��� ���� �ð����� ����            
            lastDamageTime = Time.time;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // �÷��̾�� ��� ��� ���� ��
        if (other.CompareTag("Player"))
        {
            // ���������� ���ظ� ���� ���� 1�ʰ� �����ٸ� �ٽ� ���ظ� ������, ���������� ���ظ� ���� �ð��� ���� �ð����� ����
            if (Time.time >= lastDamageTime + damageInterval)
            {                
                lastDamageTime = Time.time;
            }
        }
    }
    
}