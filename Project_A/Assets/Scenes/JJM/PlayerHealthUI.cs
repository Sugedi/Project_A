using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Player player; // 플레이어 스크립트 참조
    public Text healthText; // 체력을 표시할 Text UI

    void Start()
    {
        UpdateHealthUI(); // 초기화 시 UI 업데이트
    }

    // 플레이어의 체력이 변경될 때마다 호출되는 메서드
    public void UpdateHealthUI()
    {
        // 플레이어의 현재 체력을 Text에 표시
        healthText.text = "hp: " + player.health.ToString();
    }
}