
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Unity Inspector���� ������ Slider
    public Player player; // Player ��ũ��Ʈ ����

    void Start()
    {
        // Unity Inspector���� Slider �� Player�� �Ҵ��ؾߵ�.
        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider�� �Ҵ����ּ���!");
        }

        if (player == null)
        {
            player = GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player ��ũ��Ʈ�� ã�� �� �����ϴ�!");
            }
        }

        // �ʱ�ȭ: HP ���� �ִ밪�� �÷��̾��� �ִ� ü������ ����
        healthSlider.maxValue = player.maxHealth;
    }

    void Update()
    {
        if (player != null && healthSlider != null)
        {
            // HP ���� value ���� �÷��̾��� ���� ü������ ����
            healthSlider.value = player.health;
        }
    }
}