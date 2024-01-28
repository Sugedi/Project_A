using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;      // �켱 ���� 10���� ���߾� ī�޶�
    public CinemachineBlendListCamera blendingCamera;   // �켱 ���� 2���� ���� ī�޶�
    public float blendingDuration = 3f; // ���� ī�޶� Ȱ��ȭ �Ⱓ (��)

    private bool hasTriggered = false;   // Ʈ���Ű� �� �� �߻��ߴ��� ���θ� ��Ÿ���� �÷���
    private bool isBlending = false;     // ���� ������ ���θ� ��Ÿ���� �÷���

    public string bossBGMName; // Elite boss BGM�� �̸�

    void Start()
    {
        // �⺻�����δ� �켱 ���� 10���� ���߾� ī�޶� Ȱ��ȭ
        if (virtualCamera != null)
        {
            virtualCamera.enabled = true;
        }

        // ���� ī�޶�� ��Ȱ��ȭ ���·� ����
        if (blendingCamera != null)
        {
            blendingCamera.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �� �� Ʈ���Ű� �̹� �߻������� �� �̻� �������� ����
        if (hasTriggered)
        {
            return;
        }

        // Ư�� ������Ʈ�� �÷��̾� �±װ� Ʈ���ŵǸ� ������ BGM ��� ����
        if (other.CompareTag("Player"))
        {
            // ���� ��� ���� ��������� �����մϴ� (�ʿ��� ���).
            SoundManager.instance.StopAudio("BGM");

            // Elite boss BGM�� ����մϴ�.
            SoundManager.instance.PlayAudio("Boss", "BGM");

            StartCoroutine(SwitchToBlendingCamera());
            hasTriggered = true; // Ʈ���Ű� �߻������� ǥ��
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
            if (blendingCamera != null)
            {
                blendingCamera.enabled = true;
                yield return new WaitForSeconds(blendingDuration);
            }

            // �켱 ���� 2���� ���� ī�޶� ��Ȱ��ȭ
            if (blendingCamera != null)
            {
                blendingCamera.enabled = false;
            }

            // �켱 ���� 10���� ���߾� ī�޶� Ȱ��ȭ
            if (virtualCamera != null)
            {
                virtualCamera.enabled = true;
            }

            isBlending = false;
        }
    }
}
