using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public Datas datas;

    public void Portal()
    {
        
        //SceneManager.LoadScene("Stage"); // ����� ������ �̵�

        // �ε�â �׽�Ʈ
        SceneManager.LoadScene("LoadingStage");

    }
}
