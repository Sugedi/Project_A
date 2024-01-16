using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Player player; // �÷��̾� ��ũ��Ʈ ����
    public Text healthText; // ü���� ǥ���� Text UI

    void Start()
    {
        UpdateHealthUI(); // �ʱ�ȭ �� UI ������Ʈ
    }

    // �÷��̾��� ü���� ����� ������ ȣ��Ǵ� �޼���
    public void UpdateHealthUI()
    {
        // �÷��̾��� ���� ü���� Text�� ǥ��
        healthText.text = "hp: " + player.health.ToString();
    }
}