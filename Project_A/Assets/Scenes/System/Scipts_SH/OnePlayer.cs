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
    public static bool playerGet = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (playerGet == false)
        {
            playerGet = true;
            Debug.Log("ó���̴ϱ� ĳ���͸� ��ȯ");
        }

        else
        {
            player.gameObject.SetActive(false);
            //Destroy(player); // ���� �۵��� ���� ����
            Debug.Log("�̹� �������ϱ� ĳ���� �׸� ��ȯ");
        }
        
    }

}
