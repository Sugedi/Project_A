using UnityEngine;

public class Player_Puzzle : MonoBehaviour
{
    // �÷��̾�� �� ���� �� ũ��
    public float pushForce = 10f;

    // �浹�� ���۵� �� ȣ��Ǵ� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������ ������ Ȯ��
        if (collision.gameObject.CompareTag("RedTag"))
        {
            // ���� ���� ���� �̵���Ŵ
            Rigidbody rockRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (rockRigidbody != null)
            {
                // �÷��̾��� ���� �������� ���� ����
                rockRigidbody.AddForce(transform.forward * pushForce, ForceMode.Impulse);
            }
        }
    }
}
