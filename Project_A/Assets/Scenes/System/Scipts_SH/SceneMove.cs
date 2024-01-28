using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public Datas datas;

    public void Portal()
    {
        SoundManager.instance.PlayAudio("Door2", "SE");

        //SceneManager.LoadScene("Stage"); // 저장된 씬으로 이동

        // 로딩창 테스트
        SceneManager.LoadScene("LoadingStage");

    }
}
