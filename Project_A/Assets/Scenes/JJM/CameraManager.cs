using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera defaultVirtualCamera; // �⺻ ���߾� ī�޶� ���� (�켱���� 10)
    public CinemachineVirtualCamera switchVirtualCamera; // ��ȯ ���߾� ī�޶� ���� (�켱���� 1)
    public MonoBehaviour MatchingPuzzle; // �浹�� ������ ��ũ��Ʈ ����

    private void Start()
    {
        // �ʱ�ȭ: ���߾� ī�޶� ��Ȱ��ȭ�ϰ�, �⺻ ī�޶� Ȱ��ȭ
        // �ʱ�ȭ: �⺻ ���߾� ī�޶� Ȱ��ȭ�ϰ�, ��ȯ ���߾� ī�޶� ��Ȱ��ȭ
        defaultVirtualCamera.Priority = 10;
        switchVirtualCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� Ư�� ��ũ��Ʈ�� ����Ǿ� �ִ� ���
        if (other.GetComponent(MatchingPuzzle.GetType()) != null)
        {
            SwitchCameras(); // ī�޶� ��ȯ �޼ҵ� ȣ��
        }
    }

    private void SwitchCameras()
    {

        // ���� Ÿ���� �Ͻ����� (Time.timeScale = 0)
        //Time.timeScale = 0;

        // �⺻ ���߾� ī�޶� ��Ȱ��ȭ, ��ȯ ���߾� ī�޶� Ȱ��ȭ
        defaultVirtualCamera.Priority = 0;
        switchVirtualCamera.Priority = 9;

        // ���� �ð� �Ŀ� �ٽ� ��ȯ ���߾� ī�޶� ��Ȱ��ȭ�ϰ� �⺻ ���߾� ī�޶� Ȱ��ȭ
        StartCoroutine(SwitchBackAfterDelay());


    }


    private IEnumerator SwitchBackAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ���

        // �÷��̾� ������ �簳 (Time.timeScale = 1)
       // Time.timeScale = 1;

        // ��ȯ ���߾� ī�޶� ��Ȱ��ȭ, �⺻ ���߾� ī�޶� Ȱ��ȭ
        switchVirtualCamera.Priority = 0;
        defaultVirtualCamera.Priority = 10;
    }
}