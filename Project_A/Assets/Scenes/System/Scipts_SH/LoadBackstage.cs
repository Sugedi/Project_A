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
        "맵 곳곳에는 숨겨진 스킬이 존재합니다.\n새로운 스킬을 획득하고 더욱 강해져보세요!",
        "재장전 중에 이동 외에 다른 행동을 하면 장전이 취소됩니다.",
        "죽지 않도록 조심하세요.\n사망 시에는 보유한 젬의 절반을 잃습니다.",
        "관객들은 당신이 다시 인간이 되길 기대하고 있습니다.",
        "혼란의 신은 참 장난꾸러기네요.",
        "몬스터들이 너무 강력하다면 \n백스테이지에서 능력을 강화해보세요!",
        "로딩이 빨라서 글을 못 읽겠다고요?\n별로 중요한 이야기는 없습니다.",
        "획득한 스킬은 자동으로 기본공격에 적용됩니다!",
        "옵션에는 데이터 초기화 기능이 있습니다."
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

                if (loadText.text == "무대를 정리하는 중..." && progressbar.value >= 0.3f)
                {
                    loadText.text = "조명을 끄는 중...";
                }
                else if (loadText.text == "조명을 끄는 중..." && progressbar.value >= 0.6f)
                {
                    loadText.text = "관객들이 공연장을 나가는 중...";
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
