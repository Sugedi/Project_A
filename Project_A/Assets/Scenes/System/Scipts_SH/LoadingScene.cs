using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public Slider progressbar;
    public TextMeshProUGUI loadText;
    public TextMeshProUGUI loadInfo;

    private List<string> gameHoneyTip = new List<string>()
    {
        "�� �������� ������ ��ų�� �����մϴ�.\n���ο� ��ų�� ȹ���ϰ� ���� ������������!",
        "������ �߿� �̵� �ܿ� �ٸ� �ൿ�� �ϸ� ������ ��ҵ˴ϴ�.",
        "���� �ʵ��� �����ϼ���.\n��� �ÿ��� ������ ���� ������ �ҽ��ϴ�.",
        "�������� ����� �ٽ� �ΰ��� �Ǳ� ����ϰ� �ֽ��ϴ�.",
        "ȥ���� ���� �� �峭�ٷ���׿�.",
        "���͵��� �ʹ� �����ϴٸ� \n�齺���������� �ɷ��� ��ȭ�غ�����!",
        "�ε��� ���� ���� �� �аڴٰ��?\n���� �߿��� �̾߱�� �����ϴ�."
    };


    private void Start()
    {
        int random = Random.Range(0, gameHoneyTip.Count); // 0 ���� ī��Ʈ - 1 ����  �� (0,7) 0���� 6����
        loadInfo.text = gameHoneyTip[random];
        StartCoroutine(LoadScene()); 
    }

    IEnumerator LoadScene()
    {
        yield return null;
        string lastScene = GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName;
        AsyncOperation operation = SceneManager.LoadSceneAsync(lastScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);

                if (loadText.text == "���� ��ǰ�� �غ��ϴ� ��..." && progressbar.value >= 0.3f)
                {
                    loadText.text = "������ �׽�Ʈ�ϴ� ��...";
                }                
                else if (loadText.text == "������ �׽�Ʈ�ϴ� ��..." && progressbar.value >= 0.6f)
                {
                    loadText.text = "�������� �����ϴ� ��...";
                }

            }

            else if(progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            
            if(progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }

    IEnumerator GoToStage()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Stage");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);

                if (loadText.text == "���� ��ǰ�� �غ��ϴ� ��..." && progressbar.value >= 0.3f)
                {
                    loadText.text = "������ �׽�Ʈ�ϴ� ��...";
                }
                else if (loadText.text == "������ �׽�Ʈ�ϴ� ��..." && progressbar.value >= 0.6f)
                {
                    loadText.text = "�������� �����ϴ� ��...";
                }

            }

            else if (progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
