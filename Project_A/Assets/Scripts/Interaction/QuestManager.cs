using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =============================================================================================================================
// NPC ������Ʈ�� ���̴� ��ũ��Ʈ
// Main Quest ���� ������ ����Ǳ� ���ؼ� �ʿ��� ��ũ��Ʈ (�߿�!)
// =============================================================================================================================


public class QuestManager : MonoBehaviour
{
    int mainQuest = 0;
    int subQuest = 0;


    public void Start()
    {
        if (mainQuest == 0) // ��Ȳ: ����Ʈ �ޱ� ��
        {

        }

        if (mainQuest == 1) // ��Ȳ: ����Ʈ�� �޾����� �̿Ϸ�
        {

        }

        if (mainQuest == 2) // ��Ȳ: ����Ʈ�� �Ϸ������� ���� �ޱ� ��
        {

        }

        if (mainQuest == 3) // ��Ȳ: ���� ���� ��
        {

        }


        // ��ư���� ����� switch������ ��������
    //    public class QuestManager : MonoBehaviour
    //{
    //    public int mainQuest = 0;
    //    public int subQuest = 0;

    //    // ����Ʈ ���¸� �����ϴ� �޼���
    //    public void UpdateMainQuestStatus(int newStatus)
    //    {
    //        mainQuest = newStatus;
    //        ProcessQuestStatus();
    //    }

    //    // ����Ʈ ���¿� ���� ó���ϴ� �޼���
    //    private void ProcessQuestStatus()
    //    {
    //        switch (mainQuest)
    //        {
    //            case 0: // ��Ȳ: ����Ʈ �ޱ� ��
    //                    // ����Ʈ �ޱ� ���� �ؾ� �� �� ó��
    //                break;
    //            case 1: // ��Ȳ: ����Ʈ�� �޾����� �̿Ϸ�
    //                    // ����Ʈ ���� �߿� �ؾ� �� �� ó��
    //                break;
    //            case 2: // ��Ȳ: ����Ʈ�� �Ϸ������� ���� �ޱ� ��
    //                    // ����Ʈ �Ϸ� �� ������ �ޱ� ���� �ؾ� �� �� ó��
    //                break;
    //            case 3: // ��Ȳ: ���� ���� ��
    //                    // ������ ���� �Ŀ� �ؾ� �� �� ó��
    //                break;
    //            default: // ��Ȳ: 4 ����
    //                     // ����Ʈ�� ������ ���� �Ŀ� �ؾ� �� �� ó��
    //                break;
    //        }
    //    }
    }




}
