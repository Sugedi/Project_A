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
        string lastScene = GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName;
        
        SceneManager.LoadScene(lastScene); // ����� ������ �̵�
        Debug.Log("�� �̵� �Ϸ�");
        //GameObject.Find("Player").transform.position = datas.savePos; // ������ ���������� ������ �Ŵ����� ��� 0,0,0���� �� ����



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
