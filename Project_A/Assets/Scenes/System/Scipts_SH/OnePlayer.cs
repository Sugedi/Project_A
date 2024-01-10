using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 오브젝트를 살린 채로 맵을 왔다갔다 하니까 캐릭터가 무한 증식됨
// 이를 막고자, 첫 스타트에는 캐릭터를 생성하되
// 다시 맵으로 돌아올 때에는 캐릭터를 생성하지 않고,
// 살아있는 오브젝트를 캐릭터를 계속 쓰는 것으로 이용

// 1. 게임 오브젝트(Player)를 살려서 맵을 이동하는 방식
// 2. 씬마다 그냥 캐릭터를 지정 위치에서 소환하는 방식
// 근데 어차피 캐릭터 정보는 easy save로 불러와서 굳이 게임 오브젝트 살릴 필요 없을 듯

public class OnePlayer : MonoBehaviour
{
    public static bool PlayerGet = false;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerGet == false)
        {
            PlayerGet = true;
            Debug.Log("처음이니까 캐릭터를 소환");
        }

        else
        {
            Player.gameObject.SetActive(false);
            Debug.Log("이미 나왔으니까 캐릭터 그만 소환");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
