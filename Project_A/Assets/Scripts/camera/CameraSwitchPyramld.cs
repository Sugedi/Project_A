using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


//�������� �װ� ���� ���� ������ ���� ������ ���� (�켱����50��) -> 2/3�� ���帮��Ʈ ī�޶�(�켱����47��) on
//�׷��� ���ڱ� ���ٲ��� ���ƿ��� ������ �� Ʈ���ſ� ������� 2/3�� ���� ����Ʈī�޶� -> ����ī�޶�
// �� �ѹ� Ʈ���ſ� ���̸� ����->2/3�� ���帮��Ʈ ī�޶� on
//Ʈ���� �ѹ� ���� ������ 2/3�� ���� ����Ʈī�޶� -> ����ī�޶�


//�ݴ�� �Ƕ�̵� ���ο��� ������ ������ �Ƕ�̵� �̿� ���� ��Ŀ����� �� Ʈ���Ÿ� �Ƕ�̵� �� �ٷ� �տ� ��ġ
//�� �ٷ� �տ� ����ִ� Ʈ���ſ� ���� ������ 3/4�� ���� ī�޶� on



public class CameraSwitchPyramld : MonoBehaviour
{
    public CinemachineVirtualCamera mainVirtualCamera;         // �켱 ������ ���� ���� ���� ī�޶�
    public CinemachineBlendListCamera blendCamera;             // �켱 ������ ���� ���� ī�޶� (2/3��)
    private bool hasEnteredTrigger = false;                    // Ʈ���ſ� �����ߴ��� ���θ� ��Ÿ���� �÷���




    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���ſ� �������� ��
        if (other.CompareTag("Player"))
        {
            // �̹� Ʈ���ſ� ������ �������
            if (hasEnteredTrigger)
            {
                // ���� ī�޶� ��Ȱ��ȭ�ϰ� ���� ī�޶� Ȱ��ȭ
                blendCamera.enabled = false;
                mainVirtualCamera.enabled = true;
            }
            else
            {
                // Ʈ���ſ� ������ ������ �ƴ϶��
                // ���� ī�޶� Ȱ��ȭ�ϰ� ���� ī�޶� ��Ȱ��ȭ
                blendCamera.enabled = true;
                mainVirtualCamera.enabled = false;

                hasEnteredTrigger = true; // Ʈ���ſ� ������ ���·� �÷��� ����
            }
        }
    }
}