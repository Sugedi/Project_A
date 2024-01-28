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
            blendListCamera.enabled = true;
            mainVirtualCamera.enabled = false;

            // �ڷ�ƾ ����
            StartCoroutine(PlayDoorAnimationWithDelay(doorOpenAnimation, 2f));

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

    IEnumerator PlayDoorAnimationWithDelay(AnimationClip animationClip, float leverDoorDelay)
    {

        // Set the specified animation clip to the Animation component
        GetComponent<Animation>().clip = animationClip;

        // Play the animation once
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();

        yield return new WaitForSeconds(leverDoorDelay); // 2�� ���

        mainVirtualCamera.enabled = true;
        blendListCamera.enabled = false; // ���� ����Ʈ ī�޶� ��Ȱ��ȭ
    }
}