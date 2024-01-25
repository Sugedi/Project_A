using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchPyramld : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;      // �켱 ���� 10���� ���߾� ī�޶�
    public CinemachineClearShot clearShotCamera;   // �켱 ���� 2���� ���� ī�޶�
    public float blendingDuration = 5f; // ���� ī�޶� Ȱ��ȭ �Ⱓ (��)

    //private bool hasTriggered = false;   // Ʈ���Ű� �� �� �߻��ߴ��� ���θ� ��Ÿ���� �÷���
    private bool isBlending = false;     // ���� ������ ���θ� ��Ÿ���� �÷���

    void Start()
    {
        // �⺻�����δ� �켱 ���� 10���� ���߾� ī�޶� Ȱ��ȭ
        if (virtualCamera != null)
        {
            virtualCamera.enabled = true;
        }

        // ���� ī�޶�� ��Ȱ��ȭ ���·� ����
        if (clearShotCamera != null)
        {
            clearShotCamera.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {


        // Ư�� ������Ʈ�� �÷��̾� �±װ� Ʈ���ŵǸ� ���� ����
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SwitchToBlendingCamera());
            //hasTriggered = true; // Ʈ���Ű� �߻������� ǥ��
        }
    }

    IEnumerator SwitchToBlendingCamera()
    {
        // ���� ���� �ƴ� ���� ����
        if (!isBlending)
        {
            isBlending = true;

            // �켱 ���� 10���� ���߾� ī�޶� ��Ȱ��ȭ
            if (virtualCamera != null)
            {
                virtualCamera.enabled = false;
            }

            // �켱 ���� 2���� ���� ī�޶� Ȱ��ȭ
            if (clearShotCamera != null)
            {
                clearShotCamera.enabled = true;
                yield return new WaitForSeconds(blendingDuration);
            }

            // �켱 ���� 2���� ���� ī�޶� ��Ȱ��ȭ
            if (clearShotCamera != null)
            {
                clearShotCamera.enabled = false;
            }

            // �켱 ���� 10���� ���߾� ī�޶� Ȱ��ȭ
            if (virtualCamera != null)
            {
                virtualCamera.enabled = true;
            }

            isBlending = false;
        }
       // yield break; // ���� ��ȯ���� ������ ��������� ǥ��
    }
}

