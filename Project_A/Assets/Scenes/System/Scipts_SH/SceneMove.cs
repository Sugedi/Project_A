using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public Datas datas;

    public void Portal()
    {
        // ���� ���� �� ���� �ִ��� �𸣰��� ���߿� ���� �ʿ�
        Vector3 lastPos = datas.savePos;
        string lastScene = datas.saveScene.name;
        SceneManager.LoadScene("Stage_0114"); // ����� ������ �̵�
        Debug.Log("�� �̵� �Ϸ�");
        //GameObject.Find("Player").transform.position = datas.savePos; // ������ ���������� ������ �Ŵ����� ��� 0,0,0���� �� ����
        GameObject.Find("Player").transform.position = new Vector3(0,0,0);


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
