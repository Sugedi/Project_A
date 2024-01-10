using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;


    // 汽戚斗研 煽舌馬澗 神崎詮闘研 災君人
    GameObject playerStat;

    // 汽戚斗 広製?聖 煽舌馬澗 因娃 識情
    public Datas datas;

    public int maxHP;
    public float attackDamage;

    private string KeyName = "Datas";

    private void Awake()
    {
        ES3.LoadInto(KeyName, datas);
        // 薄仙 煽舌吉 巴傾戚嬢 什堵聖 災君神壱 粛嬢
        //playerStat = GameObject.Find("DataManager");
        //datas = playerStat.GetComponent<DataManager>().datas;
    }
    private void Start() 
    {
        // 痕呪拭 戚杏 隔生檎 鞠畏陥. 走携陥. 蟹澗 探仙析猿? 盃盃盃盃
        // 照 鞠相? せせせせせせせせせ
        maxHP = datas.maxHP;
        attackDamage = datas.attackDamage;
        Debug.Log(datas.maxHP);
        Debug.Log(datas.attackDamage);
        Debug.Log(maxHP);

    }


    //public StageName stage;
    public bool stage_1 = true;
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(moveAmount);

        // 雌硲拙遂 徹
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceFunction();
        }
    }

    void SpaceFunction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (stage_1 == true)
                {
                    SceneManager.LoadScene("Title");
                    stage_1 = false;
                }

                else if (stage_1 == false)
                {
                    SceneManager.LoadScene("SaveTest");
                    stage_1 = true;
                }
                /*
                switch (stage)
                {
                    case StageName.Stage1:
                        SceneManager.LoadScene("Title");
                        break;
                }
                */
                // "enemy" 殿益研 亜遭 神崎詮闘人 雌硲拙遂馬澗 坪球 蓄亜
                //SceneManager.LoadScene("Title");

            }
        }
    }
}
