using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{

    // =============================================================================================================
    // ���� ���� �� CSV ���Ͽ��� ��ȭ �����͸� �о�ͼ� UI�� ǥ���ϴ� �⺻���� ����
    // =============================================================================================================

    List<Dictionary<string, object>> data_Dialog; // CSV ���Ͽ��� �о�� ��ȭ �����͸� ������ ����Ʈ

    // UI ��ҵ��� ������ TextMeshProUGUI ������
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI name;

    public Button dialogPopupBtn;

    public GameObject joystick;
    public GameObject dialogPopup;
    
    int id; // ���� ��ȭ�� ID

    // ������
    public List<string> names = new List<string>();
    public List<string> dialogs = new List<string> ();


    private void Awake() // ���� ���� �� ȣ��Ǵ� Awake �Լ�
    {
        // "dialogue" �κ��� Reasources ������ �־�� CSV ������ �̸��� �־��ָ� ��
        data_Dialog = CSVReader.Read("dialogue"); // CSVReader�� ����Ͽ� ��ȭ �����͸� �о��
    }

    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        id = 0;
    }

    // ��ȭ�� ǥ���ϴ� �޼���
    public void CangeTalkId(int _id)
    {
        id = _id;
        dialogCount = 0;
        DialogSetting();
    }
    public void SettingUI(bool type) // ��ư Ű��
    {
        dialogPopupBtn.gameObject.SetActive(type);
        
    }

    public void ShowDialog()
    {
        name.text = names[id];
        dialog.text = dialogs[id];

        dialogPopup.SetActive(true);
        joystick.SetActive(false);
    }


    void DialogSetting()
    {
        names = new List<string>();
        dialogs = new List<string>();

        for(int i = 0; i < data_Dialog.Count; i++)
        {
            if ((int)data_Dialog[i]["talkID"] == id)
            {
                names.Add(data_Dialog[i]["chaName"].ToString());
                dialogs.Add(data_Dialog[i]["description"].ToString());
            }
        }
    }

    public int dialogCount = 0;

    public void NextDialog()
    {
        dialogCount++;
        if(dialogCount >= names.Count)
        {
            dialogPopup.SetActive(false);
            dialogCount = 0;
        }
        else
        {
            name.text = names[dialogCount];
            dialog.text = dialogs[dialogCount];
        }
    }


}