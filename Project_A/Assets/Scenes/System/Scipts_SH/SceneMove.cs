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

        //SceneManager.LoadScene("Stage"); // ����� ������ �̵�

        // �ε�â �׽�Ʈ
        SceneManager.LoadScene("LoadingStage");

    }
}
