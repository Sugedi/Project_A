using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 
    void Update()
    {
        // 이동
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // 이동 벡터를 정규화하여 대각선 이동 시 일정한 속도로 이동하도록 합니다.
        movement.Normalize();

        // 이동 벡터를 기반으로 실제 이동을 수행합니다.
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}

