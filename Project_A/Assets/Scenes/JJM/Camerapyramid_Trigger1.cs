using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/*
-----------------------------------------------------------

*1�� Ʈ���ſ� �� ����

start
�׻� mainī�޶� true
�׿� ī�޶�� false
 
���� 1 (�� �ʱ�ȭ x + ���� Ʈ���ſ� ���̸�)
ī�޶� - pyramid blendlist camera1(������ ����ī�޶�) �� ��ȯ

���� 2 (�� �ʱ�ȭ x + 2�� Ʈ���ſ� �꿴�� + ���� Ʈ���ſ� ���̸�)
ī�޶� - �ٽ� main camera �� ��ȯ

���� 3 (�� �ʱ�ȭ o + ���� Ʈ���ſ� ���̸�)
ī�޶� - �ٽ� main camera �� ��ȯ
��- ������

-----------------------------------------------------------

    */

public class Camerapyramid_Trigger1 : MonoBehaviour
{
    public CinemachineVirtualCamera mainVirtualCamera; // ���� ���� ī�޶�
    public CinemachineVirtualCamera virtualCamera; // ��ȯ�� ���� ī�޶�

    public bool isMapInitialized = false; // �� �ʱ�ȭ ����
    public bool isTrigger2Entered = false; // 2�� Ʈ���ſ� ��Ҵ��� ����
    public bool isThisTriggerEntered = false; // ���� Ʈ���ſ� ��Ҵ��� ����

    void Start()
    {
        // ���� ���� ī�޶�� �׻� ���� �ֵ��� ����
        if (mainVirtualCamera != null)
        {
            mainVirtualCamera.gameObject.SetActive(true);
        }

        // �Ҵ��� ���� ī�޶�� �ʱ⿡ ��Ȱ��ȭ
        if (virtualCamera != null)
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }



    // Ʈ���ſ� �������� �� ȣ��Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
    {
        // �±װ� "Player"�� ��ü�� Ʈ���ſ� ����� ��
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ Ʈ���ſ� ��ҽ��ϴ�.");

            // �� �ʱ�ȭ �ȵ� �����̰�, ���� Ʈ���ſ� ����� ��
            if (!isMapInitialized && isThisTriggerEntered)
            {
                // ���� ī�޶� Ȱ��ȭ�ϰ� ���� ���� ī�޶� ��Ȱ��ȭ
                if (virtualCamera != null && mainVirtualCamera != null)
                {
                    virtualCamera.gameObject.SetActive(true);
                    mainVirtualCamera.gameObject.SetActive(false);
                    Debug.Log("���� ī�޶�� ��ȯ�Ǿ����ϴ�.");
                }
            }


            // �� �ʱ�ȭ �ȵ� �����̰�, �� ��° Ʈ���ſ� ��Ұ�, ���� Ʈ���ſ� ����� ��
            else if (!isMapInitialized && isTrigger2Entered && isThisTriggerEntered)
            {
                // ���� ���� ī�޶� �ٽ� Ȱ��ȭ�ϰ� ���� ī�޶� ��Ȱ��ȭ
                if (mainVirtualCamera != null && virtualCamera != null)
                {
                    mainVirtualCamera.gameObject.SetActive(true);
                    virtualCamera.gameObject.SetActive(false);
                    Debug.Log("���� ���� ī�޶�� ��ȯ�Ǿ����ϴ�.");
                }
            }


            // �� �ʱ�ȭ�� �����̰�, ���� Ʈ���ſ� ����� ��
            else if (isMapInitialized && isThisTriggerEntered)
            {
                // ���� ���� ī�޶� �ٽ� Ȱ��ȭ�ϰ� ���� ī�޶� ��Ȱ��ȭ
                if (mainVirtualCamera != null && virtualCamera != null)
                {
                    mainVirtualCamera.gameObject.SetActive(true);
                    virtualCamera.gameObject.SetActive(false);
                    Debug.Log("�� �ʱ�ȭ ���¿��� ���� ���� ī�޶�� ��ȯ�Ǿ����ϴ�.");
                }
            }
        }
    }

    // SwitchButton ��ũ��Ʈ���� ȣ��Ǵ� �� �ʱ�ȭ �Լ�
    public void ResetMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
