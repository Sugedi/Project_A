using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // ī�޶� ���󰡾� �� Ÿ��
    public Vector3 offset; // ī�޶� ��ġ�� ���� ���� �����ϱ� ���� ���Ǵ� offset�� public ������ ����
                           // ī�޶� ��ġ ž�ٿ��̴ϱ� �����ϸ� inspector â���� offset�� �� �Ȱ��� �ٲ��ּ���!

    /* ����Ƽ â���� ���̾��Ű �κ� WorldSpace���ٰ� �ٴ�~�� �� ���� ����:
     *      ȭ�� �þ߸� �簢������ �״�� �ΰ� ���� �ʹ� '����'�̰� ������
     *      ���� ��Ÿ�ϸ����ϰ� 45�� ������ Ʋ���� �� - ����Ż�� �ǰ�
     */


    void Update() // Update �Լ��� �� �����Ӹ��� ȣ��Ǹ�, �ش� ������Ʈ(ī�޶�)�� ��ġ�� Ÿ���� ��ġ�� �������� ���� ������ ������Ʈ�Ѵ�.
    {
        transform.position = target.position + offset; // ���� ������Ʈ(ī�޶�)�� ��ġ�� Ÿ���� ��ġ�� �������� ���� ������ �����Ѵ�.
    }
}
