using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    Transform a;

    public void MoveCharacterTo()
    {
        // ���� ���� �� ���� �ִ��� �𸣰��� ���߿� ���� �ʿ�
        Vector3 lastPos = Datas.playerPos;
        SceneManager.LoadScene("SaveTest"); // ������ �ϴ� ��
        a = GameObject.Find("Player_SH").GetComponent<Transform>();
        a.position = lastPos;
        
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
