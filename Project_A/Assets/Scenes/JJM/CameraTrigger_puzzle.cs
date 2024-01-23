using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager_Puzzle : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // ���߾� ī�޶� ����
    public Camera defaultCamera; // �⺻ ī�޶� ����
    public LayerMask triggerLayer; // Ʈ���� ���̾� ����

    private void Start()
    {
        // �ʱ�ȭ: ���߾� ī�޶� ��Ȱ��ȭ�ϰ�, �⺻ ī�޶� Ȱ��ȭ
        virtualCamera.Priority = 0;
        defaultCamera.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���� ���̾ ���� ������Ʈ�� �浹�� ���
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            SwitchCameras(); // ī�޶� ��ȯ �޼ҵ� ȣ��
        }
    }

    private void SwitchCameras()
    {
        // �⺻ ī�޶� ��Ȱ��ȭ, ���߾� ī�޶� Ȱ��ȭ
        defaultCamera.enabled = false;
        virtualCamera.Priority = 10;

        // ���� �ð� �Ŀ� �ٽ� �⺻ ī�޶�� ��ȯ
        StartCoroutine(SwitchBackAfterDelay());
    }

    private IEnumerator SwitchBackAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ���

        // ���߾� ī�޶� ��Ȱ��ȭ, �⺻ ī�޶� Ȱ��ȭ
        virtualCamera.Priority = 0;
        defaultCamera.enabled = true;
    }
}