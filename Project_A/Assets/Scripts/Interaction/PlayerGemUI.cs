using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGemUI : MonoBehaviour
{
    public Player Player; // �÷��̾� ��ũ��Ʈ ����
    public TextMeshProUGUI GetText; // TextMeshProUGUI�� ����

    void Start()
    {
        // �ʱ�ȭ ���� �ڵ�...
    }

    void Update()
    {
        // �÷��̾��� gem�� TextMeshProUGUI�� ǥ��
        GetText.text = Player.gem.ToString();
    }
}

