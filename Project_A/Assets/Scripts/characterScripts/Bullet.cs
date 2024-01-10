using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // �Ѿ��� ���ݷ�
    public bool isMelee; // ���� �������� ���θ� ��Ÿ���� �÷���

    

    // �浹 �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // �ٴڰ� �浹�� ���
        {
            Destroy(gameObject, 3); // 3�� �Ŀ� �Ѿ� ����
        }
        else if (collision.gameObject.tag == "Wall") // ���� �浹�� ���
        {
            Destroy(gameObject); // �Ѿ� ��� ����
        }
    }

    // Ʈ���� �浹 �� ȣ��Ǵ� �޼���
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") // ���Ÿ� �����̸鼭 ���� �浹�� ���
        {
            Destroy(gameObject); // �Ѿ� ��� ����
        }
    }
}