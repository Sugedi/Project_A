using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� �� �����̴� �ž�?
// ��ġ�ڳ� �������̤����̤ä��Ҵ�;����;����̤�
// ���� ��������

public class Move_SH : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(moveAmount);

        // ��ȣ�ۿ� Ű
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceFunction();
        }
    }

    void SpaceFunction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // "monster" �±׸� ���� ������Ʈ�� ��ȣ�ۿ��ϴ� �ڵ� �߰�
                SceneManager.LoadScene("Title");
            }
        }
    }
}
