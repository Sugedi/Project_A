using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TitleBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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


    public Datas datas;
    Transform curPlayerPos;

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
        // �׽�Ʈ

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

    }

    // UI ��ư ��Ŭ���� ������ ��
    // ��ư�� ������Ʈ�� on click�� �����ϸ� �ǰ���?

    public string transferMapName;
    private Player player;


    public void OnBtnClick()
    {
        
        switch (currentType)
        {
            // MainUI���� New, Continue�� GameStart�� �����ϰ�, ���⿡�� �ݿ� ��ŵ�ϴ�. 
            // �������� �ָ�, TMPro�� (!ES3.FileExists(fileName))�� ���� ��ŸƮ���� �����մϴ�.
            case ButtonType.New:

                // �� ������ ���� ������ ���� �ٽ� ������ �� ���� ������. ��������� �� ������ �ù� �ä����̤�����Ƥ��ۤǹ³�
                // ù ���������� ��ġ�� �̵�
                // DontDestroy ������ �÷��̾ �� �о�ͼ� �߰������ ��.


                // 1�� ���
                //GameObject.Find("Player ").transform.position = datas.savePos; //new Vector3(10, 10, 10);
                //SceneManager.LoadScene("Backstage_0114");

                //2�� ���
                Vector3 lastPos = datas.savePos;
                string lastScene = datas.saveSceneName;
                SceneManager.LoadScene(lastScene); // ����� ������ �̵�
                GameObject.Find("Player").transform.position = lastPos;
                
                // �� ��ġ ���� ��, ���̺� ����ġ���� �ش� �ڵ� �ֱ�
                // string a = SceneManager.GetActiveScene().name;

                break;

            case ButtonType.Continue:

                // DataManager.instance.DataLoad(); -> ������ �Ŵ����� �׻� �ε��մϴ�. ����������.
                // ��� ������ ������ �Ŵ����� �Բ� �մϴ�.
                
                SceneManager.LoadScene(datas.saveScene.name);
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
