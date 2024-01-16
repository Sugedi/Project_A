using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon, Gem }; // 아이템 유형 열거형
    public Type type; // 아이템 유형
    public int value; // 아이템 값

    Rigidbody rigid; // Rigidbody 컴포넌트
    SphereCollider sphereCollider; // SphereCollider 컴포넌트

    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 할당
        sphereCollider = GetComponent<SphereCollider>(); // SphereCollider 컴포넌트 할당
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime); // 아이템 회전
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // 충돌한 오브젝트의 태그가 "Floor"일 때
        {
            rigid.isKinematic = true; // 물리적인 영향을 받지 않도록 Kinematic 활성화
            sphereCollider.enabled = false; // SphereCollider 비활성화
        }
    }
}