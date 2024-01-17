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
        //string lastScene = GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName;
        
        SceneManager.LoadScene("Stage"); // 저장된 씬으로 이동
        Debug.Log("씬 이동 완료");
        


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
