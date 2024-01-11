using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // �Ѿ��� ���ݷ�
    public bool isMelee; // ���� �������� ���θ� ��Ÿ���� �÷���
    public float lifeTime; // �Ѿ��� �ִ� ���� �ð� (��Ÿ� ����)
    private float lifeTimer; // ��������� ���� �ð��� �����ϴ� Ÿ�̸�

    void Start()
    {
        lifeTimer = lifeTime; // Ÿ�̸Ӹ� �ִ� ���� �ð����� �ʱ�ȭ�մϴ�.
    }

    void Update()
    {
        // �� �����Ӹ��� Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0) // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� �Ѿ��� �ı��մϴ�.
        {
            Destroy(gameObject);
        }
    }

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