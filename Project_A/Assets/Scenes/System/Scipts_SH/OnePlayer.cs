using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���� ������Ʈ�� �츰 ä�� ���� �Դٰ��� �ϴϱ� ĳ���Ͱ� ���� ���ĵ�
// �̸� ������, ù ��ŸƮ���� ĳ���͸� �����ϵ�
// �ٽ� ������ ���ƿ� ������ ĳ���͸� �������� �ʰ�,
// ����ִ� ������Ʈ�� ĳ���͸� ��� ���� ������ �̿�

// 1. ���� ������Ʈ(Player)�� ����� ���� �̵��ϴ� ���
// 2. ������ �׳� ĳ���͸� ���� ��ġ���� ��ȯ�ϴ� ���
// �ٵ� ������ ĳ���� ������ easy save�� �ҷ��ͼ� ���� ���� ������Ʈ �츱 �ʿ� ���� ��

public class OnePlayer : MonoBehaviour
{
    public static bool PlayerGet = false;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerGet == false)
        {
            PlayerGet = true;
            Debug.Log("ó���̴ϱ� ĳ���͸� ��ȯ");
        }

        else
        {
            Player.gameObject.SetActive(false);
            Debug.Log("�̹� �������ϱ� ĳ���� �׸� ��ȯ");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
