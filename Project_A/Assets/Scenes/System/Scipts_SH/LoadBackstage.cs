using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadBackstage : MonoBehaviour
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
        "�ε��� ���� ���� �� �аڴٰ��?\n���� �߿��� �̾߱�� �����ϴ�.",
        "ȹ���� ��ų�� �ڵ����� �⺻���ݿ� ����˴ϴ�!",
        "�ɼǿ��� ������ �ʱ�ȭ ����� �ֽ��ϴ�."
    };
    private void Start()
    {
        int random = Random.Range(0, gameHoneyTip.Count);
        loadInfo.text = gameHoneyTip[random];
        StartCoroutine(GoToStage());
    }
    IEnumerator GoToStage()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Backstage_0114");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);

                if (loadText.text == "���븦 �����ϴ� ��..." && progressbar.value >= 0.3f)
                {
                    loadText.text = "������ ���� ��...";
                }
                else if (loadText.text == "������ ���� ��..." && progressbar.value >= 0.6f)
                {
                    loadText.text = "�������� �������� ������ ��...";
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
