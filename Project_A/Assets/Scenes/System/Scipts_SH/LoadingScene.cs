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
    private void Start()
    {
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
