using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class bossKillDoor : MonoBehaviour
{
    public AnimationClip bossDoorOpenAnimation; // ������ ������ ���� ������ �ִϸ��̼�

    public CinemachineVirtualCamera mainVirtualCamera; // ����ī�޶�
    public CinemachineBlendListCamera blendListCamera;  // ���� ����Ʈ ī�޶�

    private void Start()
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

        // ���� '������'�� ó������ Ŭ�����ߴٸ�
        if (DataManager.instance.datas.galioFirstClear == true)
        {
            // �� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            gameObject.SetActive(false);
        }
    }

    public void BossKillDoorOpen()
    {
        // ���� ���� �ִϸ��̼��� �����Ǿ� �ִٸ�
        if (bossDoorOpenAnimation != null)
        {
            // '������ ���̰� ���� ����' �ִϸ��̼��� �����մϴ�.
            if(DataManager.instance.datas.galioFirstClear == false)
            {
                bossPlayDoorAnimation(bossDoorOpenAnimation);
            }

        }
        else
        {
            // ���: ���� ų ���� ��ũ��Ʈ�� �� �ִϸ��̼��� �������� �ʾҽ��ϴ�.
            Debug.LogError("Door Animation not assigned to BossKillDoor script.");
        }
    }

    void bossPlayDoorAnimation(AnimationClip animationClip)
    {
        // ���� ���� �� ���带 ����մϴ�.
        SoundManager.instance.PlayAudio("Gate1", "SE");

        // Animation ������Ʈ�� ������ �ִϸ��̼� Ŭ���� �����մϴ�.
        GetComponent<Animation>().clip = animationClip;

        // �ִϸ��̼��� �� �� ����մϴ�.
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();

        // ���� ������ ���� ���� ����Ʈ ī�޶� Ȱ��ȭ�ϰ� ���� ī�޶� ��Ȱ��ȭ
        mainVirtualCamera.enabled = false;
        blendListCamera.enabled = true;

        // �ִϸ��̼� ��� �Ŀ� ��ũ��Ʈ�� ��Ȱ��ȭ�ϴ� �޼��带 ȣ���մϴ�.
        StartCoroutine(DisableAfterAnimation(animationClip.length));

    }

    IEnumerator DisableAfterAnimation(float duration)
    {
        // �ִϸ��̼��� ���� ������ ��ٸ��ϴ�.
        yield return new WaitForSeconds(duration);

        // ���� ������ ���� ���� ī�޶� Ȱ��ȭ�ϰ� ���� ����Ʈ ī�޶� ��Ȱ��ȭ
        mainVirtualCamera.enabled = true;
        blendListCamera.enabled = false;

        // ��ũ��Ʈ�� ��Ȱ��ȭ�մϴ�.
        DisableScript();
    }

    void DisableScript()
    {
        // '������'�� ó������ Ŭ�����ߴٰ� ǥ���մϴ�.
        DataManager.instance.datas.galioFirstClear = true;

        // �α׿� �޽����� ����ϴ�.
        Debug.Log("�������� �����ƽ��ϴ�!");

        // �� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);

        // �����͸� �����մϴ�.
        DataManager.instance.DataSave();
    }
}
