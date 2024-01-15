using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public Datas datas;
    Transform a;

    public void MoveCharacterTo()
    {
        // 지금 내가 뭘 쓰고 있는지 모르겠음 나중에 수정 필요
        Vector3 lastPos = datas.savePos;
        string lastScene = datas.saveScene.name;
        SceneManager.LoadScene(lastScene); // 저장된 씬으로 이동
        a = GameObject.Find("Player_SH").GetComponent<Transform>();
        a.position = lastPos;

        GameObject.Find("Player").transform.position = datas.savePos;


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
