using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_SH : MonoBehaviour
{
    // 災痕馬澗 巴傾戚嬢 什堵
    public float moveSpeed = 5f; // 戚疑 紗亀 煽舌 照敗
    public float interactionDistance = 2f;

    // 汽戚斗 古艦煽拭辞 災君神澗 巴傾戚嬢 什堵
    public int maxHP;
    public float attackDamage;

    // 汽戚斗 古艦煽拭辞 汽戚斗研 災君神奄 是廃 段汐
    public Datas datas;
    private string KeyName = "Datas";

    public List<Skill> activeSkills;


    private void Start()
    {
        // 惟績 獣拙 獣, 蝶遣斗亜 煽舌吉 切重税 什堵聖 災君身
        ES3.LoadInto(KeyName, datas);
        maxHP = datas.maxHP;
        Debug.Log(datas.maxHP);
        Debug.Log(maxHP);

        activeSkills = datas.skillHave;

        // 薦拭降 薦降 什迭 戚係惟 隔澗 暗食虞
        // 操鳶鎌倉? せせせせせせせせせせせ 陥 吉陥 戚暗醤
        // 軒什闘拭 馬蟹馬蟹 蓄亜馬澗 言 耕弾陥.

        /* 什迭 諮脊 砺什闘
        Skill skillName = Resources.Load<Skill>("SpeedUp1");
        activeSkills.Add(skillName);
        Skill skillName1 = Resources.Load<Skill>("SpeedUp2");
        activeSkills.Add(skillName1);
        */
    }

    //public StageName stage; 更醤? 鎧亜 彰 爽汐 焼観汽 戚暗
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

            if (collider.CompareTag("NPC"))
            {

                // 搾醗失鉢吉 惟績 神崎詮闘 達焼神奄 �幼� 嬢形崇?
                //GameObject.Find("Skill").transform.Find("SkillCanvas").gameObject.SetActive(true);

                GameObject.Find("SkillNPC").GetComponent<SkillNPC>().Interact();

                // 戚暗 搾醗失鉢 源壱, 五昔 五敢拭辞 処揮 諜獄什 益血生稽 屋陥 佃澗 惟 蟹聖 牛 馬陥.
                // 摺幻馬檎 搾醗失鉢 照 獣徹澗 惟 疏聖走亀..?
                // 焼送 設 乞牽畏革
                // + UI 但 佃然聖 凶, 徹原澗 崇送析 呪 赤惟 鞠嬢赤製. 乞郊析精 案舛 蒸畏走幻...
                // UI 襟檎 政煽 戚疑, 因維 去 公馬亀系 馬澗 惟 疏聖 牛

                //GameObject mainUI = GameObject.Find("SaveCanvas");
            }
        }

        // 雌硲拙遂 獣原陥 汽戚斗研 災君人 巴傾戚嬢 倖拭 旋遂背層陥. 鯵蝦~
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;

    }

    public void ChangeScene()
    {
        // 樟戚 郊餓醸聖 凶, 巴傾戚嬢税 什堵聖 原走厳生稽 煽舌吉 葵生稽 痕井
        ES3.LoadInto(KeyName, datas);

        maxHP = datas.maxHP;

    }
    
    public void SkillGet()
    {
        ES3.LoadInto(KeyName, datas);
        Debug.Log(datas.skillHave[0]);
        activeSkills = datas.skillHave;
    }
    
}
