using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGemUI : MonoBehaviour
{
    public Player Player; // 플레이어 스크립트 참조
    public TextMeshProUGUI GetText; // TextMeshProUGUI로 변경

    void Start()
    {
        // 초기화 등의 코드...
    }

    void Update()
    {
        // 플레이어의 gem을 TextMeshProUGUI에 표시
        GetText.text = Player.gem.ToString();
    }
}

