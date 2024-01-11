using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��ư�� Ÿ���� �����ߴµ�... MainUI�� �ִ� �������� ����� �� ����? �ű���
    public ButtonType currentType;
    public Transform buttonScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup ContinueGroup;
    Vector3 defaltScale;
    bool isSound;

    public Datas datas;
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    private void Start()
    {
        defaltScale = buttonScale.localScale;
        ES3.LoadInto(KeyName, datas);

        // ������ ������, �̾��ϱ� ���� ���� �ʴ´�.
        if (!ES3.FileExists(fileName))
        {
            CanvasGroupOff(ContinueGroup);
            // ���� ���� �� �� ����.
        }
    }

    // UI ��ư ��Ŭ���� ������ ��
    // ��ư�� ������Ʈ�� on click�� �����ϸ� �ǰ���?
    public void OnBtnClick()
    {
        
        switch (currentType)
        {
            case ButtonType.New:

                // GameObject initialPlayerStat = GameObject.Find("DataManager");
                // �� ������ ������ �׳� �����͸Ŵ������� �⺻���� �ҷ����� ��
                if (ES3.FileExists(fileName))
                {
                    // �˾�â - "����� �����Ͱ� �����˴ϴ�. �׷��� ���� �����Ͻðڽ��ϱ�?
                    // �� - �˾�â ��Ȱ��
                    // �ƴϿ� - ������
                }
                ES3.DeleteFile(fileName); // easy save ���� ������ ������ ��� �ۿ��ϴ��� �𸣰���. �� ��.
                //ES3.Save(KeyName, datas);
                //ES3.LoadInto(KeyName, datas);
                SceneManager.LoadScene("SaveTest");

                // ������ �Ŵ����� ��� ���Ŀ��� - �̷��� ������ �����
                DataManager.instance.DataSave();

                break;
            case ButtonType.Continue:

                ES3.LoadInto(KeyName, datas);
                SceneManager.LoadScene("SaveTest");
                // �̾��ϱ��� ������ ����� �����͸� �ҷ��ͼ� ����

                break;
            case ButtonType.Option:

                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("�ɼ�");
                break;
            case ButtonType.Sound:
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("���� OFF");
                }
                else
                {
                    isSound = true;
                    Debug.Log("���� ON");
                }
                break;
            case ButtonType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                Debug.Log("�ڷ� ����");
                break;
            case ButtonType.Quit:
                Application.Quit();
                Debug.Log("���� ����");
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaltScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaltScale;
    }
}
