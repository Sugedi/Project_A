using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    Transform a;

    public void MoveCharacterTo()
    {
        // 지금 내가 뭘 쓰고 있는지 모르겠음 나중에 수정 필요
        Vector3 lastPos = Datas.playerPos;
        SceneManager.LoadScene("SaveTest"); // 가고자 하는 씬
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
