
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public Player Player; // �÷��̾� ��ũ��Ʈ ����
    public TextMeshProUGUI healthText; // TextMeshProUGUI�� ����

    void Start()
    {
        // �ʱ�ȭ ���� �ڵ�...
    }

    void Update()
    {
        // �÷��̾��� ���� ü�°� �ִ� ü���� TextMeshProUGUI�� ǥ��
        healthText.text = "Hp: " + Player.health.ToString() + " / " + Player.maxHealth.ToString();
    }
}
