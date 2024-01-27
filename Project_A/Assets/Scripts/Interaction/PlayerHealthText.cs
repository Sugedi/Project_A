using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthText : MonoBehaviour
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
        healthText.text = Player.health.ToString() + " / " + Player.maxHealth.ToString();
    }
}
