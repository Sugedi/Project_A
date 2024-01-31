using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    private double videoLength;
    private float time;
    void Start()
    {
        videoLength = GameObject.Find("VideoPlay").GetComponent<VideoPlayer>().clip.length;
        Debug.Log(videoLength);
        StartCoroutine(GoToStage());
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
    IEnumerator GoToStage()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Backstage_0114");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (videoLength <= time)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
