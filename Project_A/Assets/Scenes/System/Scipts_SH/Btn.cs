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
    //public CanvasGroup newGroup;
    //public CanvasGroup continueGroup; // �̾��ϱ� ��ư ������ ĵ���� �׷� ���������.
    Vector3 defaltScale;
    bool isSound;

    // instance�� �ҷ����鼭 �ʿ� ������. Ȥ�� ��� ���� �� �� ���� ����Ͽ� ���������
    // public Datas datas;
    // private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    private void Start()
    {
        defaltScale = buttonScale.localScale;

        // ������ �Ŵ����� ��� ���Ŀ��� - �̷��� ������ �����

        // ������ ������, �̾��ϱ� ���� ���� �ʴ´�.
        if (!ES3.FileExists(fileName))
        {
            // �ʱ� ���� ������ ����
            DataManager.instance.DataSave();
            //CanvasGroupOff(continueGroup);
            // ���� ���� �� �� ����.
        }
        else
        {
            //CanvasGroupOff(newGroup);
        }
    }

    // UI ��ư ��Ŭ���� ������ ��
    // ��ư�� ������Ʈ�� on click�� �����ϸ� �ǰ���?
    public void OnBtnClick()
    {
        
        switch (currentType)
        {
            // MainUI���� New, Continue�� GameStart�� �����ϰ�, ���⿡�� �ݿ� ��ŵ�ϴ�. 
            // �������� �ָ�, TMPro�� (!ES3.FileExists(fileName))�� ���� ��ŸƮ���� �����մϴ�.
            case ButtonType.New:

                // �� ������ ���� ������ ���� �ٽ� ������ �� ���� ������. ��������� �� ������ �ù� �ä����̤�����Ƥ��ۤǹ³�
                // ù ���������� ��ġ�� �̵�
                SceneManager.LoadScene("SaveTest");
                break;

            case ButtonType.Continue:

                // DataManager.instance.DataLoad(); -> ������ �Ŵ����� �׻� �ε��մϴ�. ����������.
                // ��� ������ ������ �Ŵ����� �Բ� �մϴ�.
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
