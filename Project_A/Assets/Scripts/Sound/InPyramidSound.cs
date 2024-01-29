using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPyramidSound : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Ư�� ������Ʈ�� �÷��̾� �±װ� Ʈ���ŵǸ� ���� ��� ����
        if (other.CompareTag("Player"))
        {
            // ���� ��� ���� ��������� �����մϴ� (�ʿ��� ���).
            SoundManager.instance.StopAudio("BGM");

            // Pyramid BGM�� ����մϴ�.
            SoundManager.instance.PlayAudio("Pyramid", "BGM");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pyramid BGM�� �����մϴ�.
            SoundManager.instance.StopAudio("BGM");

            // ������ ��������� �ٽ� ����մϴ� (���� BGM �̸��� �˾ƾ� ��).
            SoundManager.instance.PlayAudio("Stage", "BGM");
        }
    }
}
