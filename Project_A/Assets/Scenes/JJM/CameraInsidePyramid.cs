using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�
using Cinemachine;

public class CameraInsidePyramid : MonoBehaviour
{
    public CinemachineVirtualCamera mainvirtualCamera;          // �켱 ���� 10���� ���߾� ī�޶�
    public CinemachineBlendListCamera outsidevirtualCameraPyramid;   // �켱 ���� 2���� ���� ī�޶�
    public Button attackButton;                                  // ���� ��ư
    private bool isInsideCollider = false;                      // �ݶ��̴� �ȿ� �ִ��� ����

    void OnTriggerEnter(Collider other)
    {
        // Ư�� ������Ʈ�� �ݶ��̴��� ���� ��
        if (other.CompareTag("Player"))
        {
            isInsideCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Ư�� ������Ʈ�� �ݶ��̴����� ������ ��
        if (other.CompareTag("Player"))
        {
            isInsideCollider = false;
        }
    }

    void Start()
    {
        // ���� ��ư�� onClick �̺�Ʈ �߰�
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackButtonClick);
        }
    }

    void OnAttackButtonClick()
    {
        // Ư�� ������Ʈ�� �ݶ��̴� �ȿ� �ְ�, ��ư�� Ŭ������ ��
        if (isInsideCollider)
        {
            // mainvirtualCamera�� Ȱ��ȭ�ϰ� outsidevirtualCameraPyramid�� ��Ȱ��ȭ
            if (mainvirtualCamera != null && outsidevirtualCameraPyramid != null)
            {
                mainvirtualCamera.enabled = true;
                outsidevirtualCameraPyramid.enabled = false;
            }
        }
    }
}