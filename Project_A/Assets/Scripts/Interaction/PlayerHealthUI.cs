
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public Player Player; // 플레이어 스크립트 참조
    public TextMeshProUGUI healthText; // TextMeshProUGUI로 변경

    void Start()
    {
        // 초기화 등의 코드...
    }

    void Update()
    {
        // 플레이어의 현재 체력과 최대 체력을 TextMeshProUGUI에 표시
        healthText.text = "Hp: " + Player.health.ToString() + " / " + Player.maxHealth.ToString();
    }
}
