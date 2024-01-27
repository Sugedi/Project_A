using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthText : MonoBehaviour
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
        healthText.text = Player.health.ToString() + " / " + Player.maxHealth.ToString();
    }
}
