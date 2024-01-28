using UnityEngine;
using Cinemachine;
using System.Collections;

public class leverDoorOpen : MonoBehaviour
{
    public GameObject lever1;  // ���� 1 ���� ������Ʈ
    public GameObject lever2;  // ���� 2 ���� ������Ʈ
    public GameObject lever3;  // ���� 3 ���� ������Ʈ
    public GameObject lever4;  // ���� 4 ���� ������Ʈ

    public AnimationClip doorOpenAnimation;  // �� ���� �ִϸ��̼� Ŭ��

    private bool hasOpened = false;  // ���� ���ȴ��� ���θ� �����ϴ� ����

    public CinemachineVirtualCamera mainVirtualCamera; // ����ī�޶�
    public CinemachineBlendListCamera blendListCamera;  // ���� ����Ʈ ī�޶�

    void Start()
    {
        // �⺻�����δ� ����ī�޶� Ȱ��ȭ
        if (mainVirtualCamera != null)
        {
            mainVirtualCamera.enabled = true;
        }

        // ���� ī�޶�� ��Ȱ��ȭ ���·� ����
        if (blendListCamera != null)
        {
            blendListCamera.enabled = false;
        }
    }

    void Update()
    {
        // ��� ������ ���� ���� ���߾����� Ȯ��
        bool allLeversActivated = CheckAllLevers();

        // ���� ���� �ùٸ��� ���� ���� ������ �ʾ����� �� ���� �ִϸ��̼��� ���
        if (allLeversActivated && !hasOpened)
        {
            // ���� ������ ���� ���� ����Ʈ ī�޶� Ȱ��ȭ�ϰ� ���� ī�޶� ��Ȱ��ȭ
            mainVirtualCamera.enabled = false;
            blendListCamera.enabled = true;

            // 2�� �Ŀ� �� ���� �ִϸ��̼��� ����ϱ� ���� �ڷ�ƾ ȣ��
            StartCoroutine(PlayDoorAnimationWithDelay(2f));

            hasOpened = true;  // ���� ���� ���·� ǥ��
        }
    }

    bool CheckAllLevers()
    {
        // �� ������ ���� ���� �����ͼ� ��� ������ Ȱ��ȭ�Ǿ����� Ȯ��
        bool lever1Activated = lever1.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever2Activated = lever2.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever3Activated = lever3.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever4Activated = lever4.GetComponent<LeverController>().isPuzzleAnswer;

        return lever1Activated && lever2Activated && lever3Activated && lever4Activated;
    }

    // �� ���� �ִϸ��̼��� ����ϴ� �ڷ�ƾ
    IEnumerator PlayDoorAnimationWithDelay(float delay)
    {
        // ��� �ð� �Ŀ� �� ���� �ִϸ��̼��� ���
        yield return new WaitForSeconds(delay);

        // ������ �ִϸ��̼� Ŭ���� Animation ������Ʈ�� ����
        GetComponent<Animation>().clip = doorOpenAnimation;
        // �ִϸ��̼��� �� �� ���
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();

        // ���� ������ ���� ���� ����Ʈ ī�޶� ��Ȱ��ȭ�ϰ� ���� ī�޶� Ȱ��ȭ
        yield return new WaitForSeconds(doorOpenAnimation.length); // �ִϸ��̼� ��� �ð���ŭ ���
        mainVirtualCamera.enabled = true;
        blendListCamera.enabled = false;
    }
}
