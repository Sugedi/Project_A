using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public Datas datas;

    public void Portal()
    {
        // 지금 내가 뭘 쓰고 있는지 모르겠음 나중에 수정 필요
        Vector3 lastPos = datas.savePos;
        string lastScene = datas.saveScene.name;
        SceneManager.LoadScene("Stage_0114"); // 저장된 씬으로 이동
        Debug.Log("씬 이동 완료");
        //GameObject.Find("Player").transform.position = datas.savePos; // 지금은 스테이지에 데이터 매니저가 없어서 0,0,0으로 안 가요
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
