using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : MonoBehaviour
{
    public int damage; // �Ѿ��� ���ݷ�
    public bool isMelee; // ���� �������� ���θ� ��Ÿ���� �÷���

    public GameObject explosionPrefab; // ���� ����Ʈ ������

    // �浹 �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // �ٴڰ� �浹�� ���
        {
            // ���� ����Ʈ�� �����մϴ�.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject, 3); // 3�� �Ŀ� �Ѿ� ����
        }
        else if (collision.gameObject.tag == "Wall") // ���� �浹�� ���
        {
            // ���� ����Ʈ�� �����մϴ�.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject); // �Ѿ� ��� ����
        }
        else if (collision.gameObject.tag == "Player")
        {
            // ���� ����Ʈ�� �����մϴ�.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // ����ü�� �����մϴ�.
            Destroy(gameObject);
        }
    }

    // Ʈ���� �浹 �� ȣ��Ǵ� �޼���
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") // ���Ÿ� �����̸鼭 ���� �浹�� ���
        {
            // ���� ����Ʈ�� �����մϴ�.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject); // �Ѿ� ��� ����
        }
        // �÷��̾ ���� �浹���� ���
        if (!isMelee && other.gameObject.tag == "Player")
        {
            // ���� ����Ʈ�� �����մϴ�.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // ����ü�� �����մϴ�.
            Destroy(gameObject);
        }
    }
}