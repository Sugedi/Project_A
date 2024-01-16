using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon, Gem }; // ������ ���� ������
    public Type type; // ������ ����
    public int value; // ������ ��

    Rigidbody rigid; // Rigidbody ������Ʈ
    SphereCollider sphereCollider; // SphereCollider ������Ʈ

    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ �Ҵ�
        sphereCollider = GetComponent<SphereCollider>(); // SphereCollider ������Ʈ �Ҵ�
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime); // ������ ȸ��
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // �浹�� ������Ʈ�� �±װ� "Floor"�� ��
        {
            rigid.isKinematic = true; // �������� ������ ���� �ʵ��� Kinematic Ȱ��ȭ
            sphereCollider.enabled = false; // SphereCollider ��Ȱ��ȭ
        }
    }
}