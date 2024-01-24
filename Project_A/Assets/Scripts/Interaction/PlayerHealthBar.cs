
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Unity Inspector에서 연결할 Slider
    public Player player; // Player 스크립트 참조

    void Start()
    {
        // Unity Inspector에서 Slider 및 Player를 할당해야돼.
        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider를 할당해주세요!");
        }

        if (player == null)
        {
            player = GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player 스크립트를 찾을 수 없습니다!");
            }
        }

        // 초기화: HP 바의 최대값을 플레이어의 최대 체력으로 설정
        healthSlider.maxValue = player.maxHealth;
    }

    void Update()
    {
        if (player != null && healthSlider != null)
        {
            // HP 바의 value 값을 플레이어의 현재 체력으로 설정
            healthSlider.value = player.health;
        }
    }
}