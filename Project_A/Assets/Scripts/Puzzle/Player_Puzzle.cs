using UnityEngine;

public class Player_Puzzle : MonoBehaviour
{
    // 플레이어와 돌 간의 힘 크기
    public float pushForce = 10f;

    // 충돌이 시작될 때 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 상대방이 돌인지 확인
        if (collision.gameObject.CompareTag("RedTag"))
        {
            // 돌에 힘을 가해 이동시킴
            Rigidbody rockRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (rockRigidbody != null)
            {
                // 플레이어의 전방 방향으로 힘을 가함
                rockRigidbody.AddForce(transform.forward * pushForce, ForceMode.Impulse);
            }
        }
    }
}
