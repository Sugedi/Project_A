using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaSound : MonoBehaviour
{
    public string bossBGMName; // Elite boss BGM�� �̸�
    public string exitBGMName = "Stage"; // �⺻������ Stage BGM�� ����

    void OnTriggerEnter(Collider other) 
    {
        // Ư�� ������Ʈ�� �÷��̾� �±װ� Ʈ���ŵǸ� ���� ��� ����
        if (other.CompareTag("Player")) 
        {
            // ���� ��� ���� ��������� �����մϴ� (�ʿ��� ���).
            SoundManager.instance.StopAudio("BGM");

            // Elite boss BGM�� ����մϴ�.
            SoundManager.instance.PlayAudio("Boss", "BGM");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Boss BGM�� �����մϴ�.
            SoundManager.instance.StopAudio("BGM");

            // ������ ��������� �ٽ� ����մϴ� (���� BGM �̸��� �˾ƾ� ��).
            SoundManager.instance.PlayAudio(exitBGMName, "BGM");
        }
    }
}
