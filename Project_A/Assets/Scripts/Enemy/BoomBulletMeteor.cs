using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBulletMeteor : MonoBehaviour
{
    public int damage; // �Ѿ��� ���ݷ�
    public bool isMelee; // ���� �������� ���θ� ��Ÿ���� �÷���

    public GameObject attackIndicatorPrefab; // ���� ��ġ�� ǥ���� UI ������
    private GameObject attackIndicator; // ������ UI ��ü�� ������ ����

    public GameObject fireEffectPrefab; // �� ȿ�� ������

    void Start()
    {
        // ����ü�� ������ �� UI�� �Բ� �����մϴ�.
        attackIndicator = Instantiate(attackIndicatorPrefab, transform.position, Quaternion.identity);
        attackIndicator.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    // �浹 �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // �ٴڰ� �浹�� ���
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(attackIndicator);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall") // ���� �浹�� ���
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(attackIndicator);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(attackIndicator);
            Destroy(gameObject);
        }
    }

    // Ʈ���� �浹 �� ȣ��Ǵ� �޼���
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") // ���Ÿ� �����̸鼭 ���� �浹�� ���
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(attackIndicator);
            Destroy(gameObject);
        }

        if (!isMelee && other.gameObject.tag == "Floor") // ���Ÿ� �����̸鼭 �ٴڰ� �浹�� ���
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(attackIndicator);
            Destroy(gameObject);
        }

        // �÷��̾ ���� �浹���� ���
        if (!isMelee && other.gameObject.tag == "Player")
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(attackIndicator);
            Destroy(gameObject);
        }
    }
}