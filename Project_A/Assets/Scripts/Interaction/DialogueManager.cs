using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class DialogueManager : MonoBehaviour
{
    // =============================================================================================================
    // 
    // =============================================================================================================

    public GameObject dialogPopupBtn; // Attack Button���� �����Ǿ� ����
    public GameObject joystick; // ���̽�ƽ UI
    public GameObject dialogPopup; // ��ȭ �˾�â UI

    public GameObject questWindowCanvas; // Hierarchy â�� �ִ� QuestWindow Canvas�� ���� ����


    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        // ���� ���� ��ü���� �⺻ ��(���� ����)�� ����ٰ� ���� ����

        // ���� �߻�(24-01-10) �߾��µ� ��ħ: �� ���� ����� ����
        // 1. �Ʒ��� ���� ������ ���۵� �� ���ʷ� 1�� ȣ��Ǵ� Start �޼ҵ忡, ��ȭ�˾�â�� �⺻������ false ���·� �����س���
        // 2. ���� �� ���̵� ���̾��Ű â���� �θ� ��ü�� talk canvas�� ���ΰ�, �ڽ� ��ü�� dialogue�� �ν�����â���� Ȱ��ȭ �������ѳ���
        dialogPopup.SetActive(false); // ��ȭ �˾��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
    }

}
