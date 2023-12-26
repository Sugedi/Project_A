using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ� 
    void Update()
    {
        // �̵�
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // �̵� ���͸� ����ȭ�Ͽ� �밢�� �̵� �� ������ �ӵ��� �̵��ϵ��� �մϴ�.
        movement.Normalize();

        // �̵� ���͸� ������� ���� �̵��� �����մϴ�.
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}

