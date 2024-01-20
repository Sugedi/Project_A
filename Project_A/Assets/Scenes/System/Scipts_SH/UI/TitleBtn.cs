using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleBtn : MonoBehaviour
{
    // ��ư�� Ÿ���� �����ߴµ�... MainUI�� �ִ� �������� ����� �� ����? �ű���
    public ButtonType currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup DataResetGroup;
    public CanvasGroup NO1;
    public CanvasGroup NO2;
    bool isSound;

    // instance�� �ҷ����鼭 �ʿ� ������. Ȥ�� ��� ���� �� �� ���� ����Ͽ� ���������
    // public Datas datas;
    // private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    public Datas datas;
    private Player player;

    private void Start()
    {

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

    }

    // UI ��ư ��Ŭ���� ������ ��


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
                //Vector3 lastPos = datas.savePos;
                string lastScene = GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName;
                SceneManager.LoadScene(lastScene); // ����� ������ �̵�
                
                // �� ��ġ ���� ��, ���̺� ����ġ���� �ش� �ڵ� �ֱ�
                // string a = SceneManager.GetActiveScene().name;

                break;

            case ButtonType.Option:

                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("�ɼ�");
                break;

            case ButtonType.Sound:
                // �� ���� ��� ����, ���� ���� �����̵带 ���� ��
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

            case ButtonType.DataResetUI:
                CanvasGroupOn(DataResetGroup); 

                break;
            case ButtonType.DataReset:
                // 1- ��¥ ����?
                // 2- ���� �Ǿ����ϴ�.
                if (NO1.alpha == 1) // 1�� ����, 2�� ����
                {
                    CanvasGroupOn(NO2);
                    CanvasGroupOff(NO1);
                    DataManager.instance.DataRemove();
                }
                else if (NO1.alpha == 0) // �����Ǿ���, ���� â ����
                {
                    CanvasGroupOff(DataResetGroup);
                    CanvasGroupOff(NO2);
                    CanvasGroupOn(NO1);
                }


                break;
            case ButtonType.DataResetBack:
                CanvasGroupOff(DataResetGroup);
                CanvasGroupOff(NO2);
                CanvasGroupOn(NO1);

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

}
