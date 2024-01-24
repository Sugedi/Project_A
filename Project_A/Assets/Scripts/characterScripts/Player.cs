using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // �÷��̾� �Ӽ���
    // �⺻���� ������� �� ��
    public int gem;
    public float speed = 5; // �̵� �ӵ� - ���� �� ��.
    public GameObject[] weapons; // ���� �迭 - �ϴ� ������(Start���� Equip�ؼ� ����� ������)
    public bool[] hasWeapons; // ������ ���� ���� �迭 - �̰͵� �ָ�
    public Camera followCamera; // ī�޶� - ����
    public int ammo = 9999999; // ���� �Ѿ� ���� - �̰� ���� �Ѿ��ΰ�?
    public int health; // ���� ü�� - ������ �����ϸ� ȸ�� ��
    public int maxAmmo = 9999999; // �ִ� �Ѿ� ���� - ��ų�� ����
    public int maxHealth; // �ִ� ü�� - ���� ��
    public Image gameOverScreen;

    // �÷��̾� ��ų ����
    public List<Skill> activeSkills; // Ȱ��ȭ�� ��ų���� �����ϴ� ����Ʈ   

    // ���� ����
    float hAxis; // ���� �� �Է°�
    float vAxis; // ���� �� �Է°�
    bool wDown; // �ȱ� �Է� ����
    bool jDown; // ���� �Է� ����
    bool fDown; // ���� �Է� ����
    bool rDown; // ������ �Է� ����

    // ���� ����
    bool isDodge; // ȸ�� ���� ����    
    bool isReload; // ������ ���� ����
    bool isFireReady = true; // ���� ���� ����
    bool isBorder; // ���� �浹 ����
    bool isDamage; // �������� ���� ���� ����
    public bool isDead = false;

    Vector3 moveVec; // �̵� ����
    Vector3 dodgeVec; // ȸ�� ����

    // ���� ���
    Rigidbody rigid; // Rigidbody ������Ʈ
    Animator anim; // Animator ������Ʈ
    public SkinnedMeshRenderer[] meshs; // SkinnedMeshRenderer �迭

    // ��� ��
    GameObject nearObject; // �ֺ� ������Ʈ
    public Weapon equipWeapon; // ���� ������ ����
    int equipWeaponIndex = -1; // ���� ������ ������ �ε���
    float fireDelay; // ���� ������

    // ������ �Ŵ������� ����� �����͸� �ҷ����� ���� �ʼ�
    public Datas datas;
    private string KeyName = "Datas";
    public SaveSwitch save;

    // UI ON/OFF ���
    public CanvasGroup SaveCanvas;
    public FloatingJoystick joystick; //0122

    public CanvasGroup joy;


    // �ʱ�ȭ ��Ű�� ��
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
        anim = GetComponentInChildren<Animator>(); // Animator ������Ʈ ��������
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>(); // MeshRenderer �迭 ��������
        //EquipWeapon(0);
    }

    // ��ȣ �߰� ����
    public void SaveHeal()
    {
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        health = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;

    }

    private void Start()
    {
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
        health = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        EquipWeapon(0);


        // ���������� �����ϸ� ��ġ�� ����� ���̺� ��ġ Ȥ�� �ʱ� ��ġ�� ����
        if (SceneManager.GetActiveScene().name == "Stage")
        {
            transform.position = datas.savePos;
            SaveCanvas = GameObject.Find("SaveSwitchCanvas").GetComponent<CanvasGroup>();
        }

        // �齺�������� ���ƿ��� ���� ��ġ�� �齺������ 0,0,0
        else if (SceneManager.GetActiveScene().name == "Backstage_0114")
        {

            GameObject.Find("DataManager").GetComponent<DataManager>().datas.saveSceneName = "Backstage_0114";
            transform.position = new Vector3(0f, 0f, 0f);
            DataManager.instance.DataSave();
        }
    }

    public void ChangeScene()
    {
        // ���� �ٲ���� ��, �÷��̾��� ������ ���������� ����� ������ ����
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
    }

    public void SkillGet()
    {
        ES3.LoadInto(KeyName, datas);
        activeSkills = GameObject.Find("DataManager").GetComponent<DataManager>().datas.skillHave;
        maxHealth = GameObject.Find("DataManager").GetComponent<DataManager>().datas.maxHP;
        gem = GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul;
        EquipWeapon(0);
    }

    void SpaceFunction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.01f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("NPC"))
            {

                // ��Ȱ��ȭ�� ���� ������Ʈ ã�ƿ��� ���� �����?
                //GameObject.Find("Skill").transform.Find("SkillCanvas").gameObject.SetActive(true);
                CanvasGroupOff(joy);
                GameObject.Find("Workbench baked").GetComponent<SkillNPC>().Interact();

                // �̰� ��Ȱ��ȭ ����, ���� �޴����� ��� ĵ���� �׷����� ���� �Ѵ� �� ���� �� �ϴ�.
                // �����ϸ� ��Ȱ��ȭ �� ��Ű�� �� ��������..?
                // ���� �� �𸣰ڳ�
                // + UI â ������ ��, Ű���� ������ �� �ְ� �Ǿ�����. ������� ���� ��������...
                // UI �߸� ���� �̵�, ���� �� ���ϵ��� �ϴ� �� ���� ��

                //GameObject mainUI = GameObject.Find("SaveCanvas");


            }

            if (collider.CompareTag("Door"))
            {
                GameObject.Find("door-house-simple").GetComponent<SceneMove>().Portal();
            }

            if (collider.CompareTag("Switch"))
            {
                CanvasGroupOff(joy);
                //GameObject.Find("Switch").GetComponent<SaveSwitch>().SwitchFunc();
                SaveSwitch.SwitchFunc();
                //GameObject.Find("Switch").GetComponent<SaveSwitch>().SaveData();
                Time.timeScale = 0;
                SaveCanvas.alpha = 1;
                SaveCanvas.interactable = true;
                SaveCanvas.blocksRaycasts = true;
                
            }

            if (collider.CompareTag("Treasure"))
            {
                if (collider.gameObject.name == "Treasure Box_1")
                {
                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }
                if (collider.gameObject.name == "Treasure Box_2")
                {
                    collider.gameObject.GetComponent<TreasureBox>().TreasureFind();
                }


            }

        }

    }


    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }

    //��ȣ �߰� ��


    public void Update()
    {
        if (health <= 0 && !isDead)
        {
            Die();
            return; // ��������Ƿ� ���⼭ Update �޼��带 �����մϴ�.
        }
        if (!isDead) // ������� �ʾ��� ���� �Է°� �׼��� ó���մϴ�.
        {
            // �Է� ó��
            GetInput();
            // �÷��̾� �׼�
            Move();
            Turn();
            
            Reload();
            Dodge();

            // ����� ����

            //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.01f);
            //foreach (Collider collider in hitColliders)
            //{

            //    // ��ȣ - skill NPC ��ȣ�ۿ�
            //    if (collider.CompareTag("NPC") || collider.CompareTag("Door") || collider.CompareTag("Switch") || collider.CompareTag("Treasure"))
            //    {
            //        // ���� �κп� ���� ��ư �̹��� -> ��ȣ�ۿ� ��ư �̹����� �ٲٴ� �� ������ ��.

            //        if (fDown == true)
            //        {
            //            SpaceFunction();

            //        }
            //        fDown = false;
            //    }
            //    else if(!collider.CompareTag("Switch"))
            //    {
            //        //Debug.Log(!collider.CompareTag("NPC") && !collider.CompareTag("Door") && !collider.CompareTag("Switch") && !collider.CompareTag("Treasure"));
            //    }
            //}

            // PC ����

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceFunction();
            }

            Attack();
        }

    }


    
    void GetInput()
    {
        // PC ���� (����Ƽ �׽�Ʈ������ ���)

        hAxis = Input.GetAxisRaw("Horizontal"); // ���� �� �Է°� �ޱ�
        vAxis = Input.GetAxisRaw("Vertical"); // ���� �� �Է°� �ޱ�
        wDown = Input.GetButton("Walk"); // �ȱ� �Է� ���� �ޱ�
        jDown = Input.GetButtonDown("Jump"); // ���� �Է� ���� �ޱ�
        fDown = Input.GetButton("Fire1"); // ���� �Է� ���� �ޱ�
        rDown = Input.GetButtonDown("Reload"); // ������ �Է� ���� �ޱ�

        // ����� ����

        // ����: 1. �ٷ� �� PC ���� �κ� �ּ� ó�� & ����� ���ۺκ� �ּ� ����
        //         2. �������� ��(����� ���⸸ �־���)���� Joystick ������Ʈ�� Ȱ��ȭ�ϸ� ��.

        //hAxis = joystick.Horizontal;
        //vAxis = joystick.Vertical;

    }

    public void TouchAttack()
    {
        fDown = true;
    }
    public void TouchReload()
    {
        rDown = true;
    }
    public void TouchRoll()
    {
        jDown = true;
    }

    private void LateUpdate()
    {
        fDown = false;
        rDown = false;
        jDown = false;
    }

    // �÷��̾� �̵�
    void Move()
    {
        Vector3 cameraForward = followCamera.transform.forward;
        Vector3 cameraRight = followCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        moveVec = (cameraForward * vAxis) + (cameraRight * hAxis);

        if (isDodge)
            moveVec = dodgeVec;

        else if (!isFireReady)
            moveVec = Vector3.zero;

        if (!isBorder)
        {
            // Calculate the ray positions for top, middle, and bottom of the player collider
            List<Vector3> rayPositions = new List<Vector3>();
            rayPositions.Add(transform.position + Vector3.up * 0.9f); // Top
            rayPositions.Add(transform.position + Vector3.up * 0.5f); // Middle
            //rayPositions.Add(transform.position + Vector3.up * 0.1f); // Bottom

            // Check if any of the rays hit a wall within a certain scope
            if (CheckHitWall(rayPositions, moveVec))
                moveVec = Vector3.zero;
        }

        // Rest of the Move method remains unchanged
        rigid.MovePosition(rigid.position + moveVec * speed * Time.deltaTime);

        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
            rigid.MoveRotation(moveQuat);
        }

        anim.SetBool("isRun", moveVec != Vector3.zero && !isReload);
        anim.SetBool("isWalk", wDown);
    }

    public float Wallscope = 1f;
    bool CheckHitWall(List<Vector3> rayPositions, Vector3 movement)
    {


        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * Wallscope, Color.red);

            if (Physics.Raycast(pos, movement, out RaycastHit hit, Wallscope))
            {
                if (hit.collider.CompareTag("Wall"))
                    return true;
            }
        }

        return false;
    }


    // �� ������~
    void Turn()
    {
        // #1. Ű���忡 ���� ȸ��
        transform.LookAt(transform.position + moveVec); // �̵� �������� �÷��̾� ȸ��

        // #2. ���콺�� ���� ȸ��
        if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec); // ���콺 ��ġ�� �÷��̾� ȸ��
            }
        }
    }

    // �÷��̾� ���� ó��
    void Attack()
    {
        // ���� ������ ���Ⱑ ������ ������ �������� �ʽ��ϴ�.
        if (equipWeapon == null)
            return;

        // ���� �����̸� ������ŵ�ϴ�.
        fireDelay += Time.deltaTime;

        // ���� �����̰� ���� �ӵ��� �Ѿ����� Ȯ���մϴ�. �Ѿ��ٸ� ������ �غ� �� ���Դϴ�.
        isFireReady = (1f / (equipWeapon.baseAttackSpeed * equipWeapon.attackSpeedMultiplier)) < fireDelay; // ���� ������ üũ

        if (fDown && isFireReady && !isDodge)
        {
            if (isReload) // ������ ���̸� �������� ����մϴ�.
            {
                CancelReload();
            }
            equipWeapon.Use(); // ���� ���
            anim.SetTrigger("doShot"); // ���� ������ ���� �ִϸ��̼� ����
            fireDelay = 0; // ������ �ʱ�ȭ
        }
    }
    void CancelReload()
    {
        if (isReload)
        {
            anim.SetBool("isReload", false); // ������ �ִϸ��̼� ��Ȱ��ȭ
            
            isReload = false; // ������ ���� ����
                              // �������� ��ҵǸ� ĳ������ �ӵ��� ������� �����մϴ�.
            speed = 5;
            CancelInvoke("ReloadOut"); // �̹� ����� ReloadOut ȣ���� ����մϴ�.
        }
    }
    // ������
    public void Reload()
    {
        if (equipWeapon == null || ammo == 0 || isReload || equipWeapon.curAmmo == equipWeapon.maxAmmo)
            return;

        if (rDown && !isDodge && isFireReady)
        {
            anim.SetBool("isReload", true);
            
            isReload = true; // ������ �� ���� ����
            speed = 2;
            float reloadTime = 2f * GetReloadTimeMultiplier(); // ������ �ð��� ����մϴ�.
            Invoke("ReloadOut", reloadTime); // 2�� �� ������ �Ϸ� ó��
        }
    }
    float GetReloadTimeMultiplier()
    {
        // �⺻�����δ� 1 (��, ������ �ð��� ��ȭ�� ����).
        float reloadMultiplier = 1f;
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                reloadMultiplier *= skill.reloadTimeMultiplier;
            }
        }
        return reloadMultiplier;
    }
    // ������ ó�� �Ϸ�
    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo;
        ammo -= reAmmo;
        isReload = false; // ������ �Ϸ� �� ���� ����
        anim.SetBool("isReload", false);

        speed = 5;
        anim.SetBool("isRun", moveVec != Vector3.zero);


    }

    // ȸ�� ����
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isDodge)
        {
            // ȸ�� ��ο� ���̳� "RedTag" �±װ� �ִ��� ����ĳ��Ʈ�� Ȯ��
            if (Physics.Raycast(transform.position, moveVec, out RaycastHit hit, 1f))
            {
                // �� �Ǵ� "RedTag" �±װ� �ִٸ� ȸ�� ���
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("RedTag"))
                {
                    return;
                }
            }
            // ���� �Ǵ� ������ ���¸� ����մϴ�.
            if (isFireReady)
            {
                isFireReady = false; // ���� ������ �ʱ�ȭ
                anim.ResetTrigger("doShot"); // ���� �ִϸ��̼� Ʈ���� ����

            }

            if (isReload)
            {
                CancelReload();
            }

            dodgeVec = moveVec; // ȸ�� ���� ����
            speed *= 1.6f; // �̵� �ӵ� ����
            anim.SetTrigger("doDodge"); // ȸ�� �ִϸ��̼� Ȱ��ȭ
            isDodge = true; // ȸ�� �� ���� ����
            isDamage = true; // ���� ���� Ȱ��ȭ

            Invoke("DodgeOut", 0.5f); // 0.5�� �� ȸ�� �Ϸ� ó��
            Invoke("EndInvulnerability", 0.2f); // 0.2�� �� ���� ���� ����
        }
    }
    // ���� ���� ����
    void EndInvulnerability()
    {
        isDamage = false;
    }
    // ȸ�� �Ϸ� ó��
    void DodgeOut()
    {        
        speed /= 1.6f; // �̵� �ӵ� ������� ����
        isDodge = false; // ȸ�� �Ϸ� �� ���� ����
                         
        // ���� ���°� ���� �������� �ʾҴٸ� ����
        if (isDamage)
            isDamage = false;
    }

    // ȸ�� ���� �Լ�
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    // ������ ���߰� �ϴ� �Լ�
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        // ���� �����Ǹ� isBorder�� true�� ����
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    // ���� ������Ʈ�� ����ϴ� �Լ�
    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    // ���� ���� �޼���
    void EquipWeapon(int weaponIndex)
    {
        if (weapons[weaponIndex] != null)
        {
            // ���� ������ ���Ⱑ ������ ��Ȱ��ȭ
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }

            // �� ���� ����
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            equipWeaponIndex = weaponIndex;

            // ���� ���� ó��
            hasWeapons[weaponIndex] = true;

            // ���⿡ ���� ��ų ����
            ApplySkills();

            // ���� ź���� �ִ� ź������ ����
            equipWeapon.curAmmo = equipWeapon.maxAmmo;

        }
    }

    // Ʈ���ſ� �浹���� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �����۰� �浹���� �� ó��
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            // ������ ������ ���� ó��
            switch (item.type)
            {
                case Item.Type.Ammo:
                    // �Ѿ� �������� ���, �Ѿ� ���� ����
                    ammo += item.value;

                    // �Ѿ� ���� �ִ�ġ�� ���� �ʵ��� �մϴ�.
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;

                case Item.Type.Gem:
                    gem += item.value;
                    GameObject.Find("DataManager").GetComponent<DataManager>().datas.soul = gem;
                    //DataManager.instance.DataSave();
                    break;
            }
            // �ı��ϴ� ��
            Destroy(item.gameObject);
        }
        else if (other.tag == "Weapon")
        {
            Item item = other.GetComponent<Item>();
            int weaponIndex = item.value;
            // ���� ���� ó���� �մϴ�.
            hasWeapons[weaponIndex] = true; // ���� ȹ�� ó��

            // �̹� ������ ���Ⱑ �ִٸ� ��Ȱ��ȭ�մϴ�.
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }
            // ���ο� ���⸦ �����մϴ�.
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            equipWeaponIndex = weaponIndex;

            ApplySkills(); // ���⸦ ���������Ƿ�, ��ų�� �����մϴ�.

            // ���� ź���� �ִ� ź������ �����մϴ�.
            equipWeapon.curAmmo = equipWeapon.maxAmmo;

            Destroy(other.gameObject); // �ֺ� ������Ʈ �ı�
        }

        // �� �Ѿ˰� �浹���� �� ó��
        else if (other.tag == "EnemyBullet")
        {
            if (isDead) return;

            if (!isDamage)
            {
                // �� �Ѿ˿� �¾��� �� ü�� ���� �� �Ѿ� �ı�
                EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
                health -= enemyBullet.damage;
                if (health < 0)
                {
                    health = 0;
                    Die();
                }
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

                // ������ ǥ�� �ڷ�ƾ ����
                StartCoroutine(OnDamage());


            }
        }

        // �� �Ѿ˰� �浹���� �� ó��
        else if (other.tag == "EnemyLong")
        {
            if (isDead) return;

            if (!isDamage)
            {
                // �� �Ѿ˿� �¾��� �� ü�� ���� �� �Ѿ� �ı�
                EnemyLongAttack enemyLong = other.GetComponent<EnemyLongAttack>();
                health -= enemyLong.damage;
                if (health < 0)
                {
                    health = 0;
                    Die();
                }
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

                // ������ ǥ�� �ڷ�ƾ ����
                StartCoroutine(OnDamage());
            }
        }
    }
    // �˹�� �Բ� ���� ó���� ���� ���ط�(damage) ���ڸ� �߰��մϴ�.
    public void GetKnockedBack(Vector3 direction, float force, int damage)
    {
        if (isDead) return;
        // ���� ���ظ� �����մϴ�.
        if (!isDamage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
                Die();
            }
            StartCoroutine(OnDamage());

            if (anim != null)
            {
                anim.SetTrigger("doSlip");
            }
        }

        // �� �Ŀ� �˹� ȿ���� �����մϴ�.
        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }

    // ���� ó�� �޼���
    void Die()
    {
        // ��� ���¸� true�� �����մϴ�.
        isDead = true;

        // ��� �ִϸ��̼� ���
        anim.SetTrigger("Die");

        StartCoroutine(DieSequence());

    }

    IEnumerator DieSequence()
    {
        // ��� �ִϸ��̼��� ����Ǵ� ���� ���
        yield return new WaitForSeconds(3f);

        // ���� ���� ȭ���� Ȱ��ȭ�ϰ� ȸ������ ����ϴ�.
        CanvasGroupOff(joy);
        gameOverScreen.gameObject.SetActive(true);

        DataManager.instance.datas.soul /= 2;

        // ���� ��� �������� ����ϴ�. (�ʿ��� ���)
        Time.timeScale = 0;
        
    }
    // ������ ǥ�� �ڷ�ƾ
    IEnumerator OnDamage()
    {
        // �ǰ� ���¸� Ȱ��ȭ�ϰ� �޽����� ������ �Ķ������� ����
        isDamage = true; // �ǰ� ���� Ȱ��ȭ

        // ������ ȿ���� ���� ���� ����
        float blinkDuration = 1f; // �����̴� �� �ð�
        float blinkRate = 0.1f; // ������ ����
        float elapsedTime = 0f; // ��� �ð�

        // �����̴� ������ �ݺ� ����
        while (elapsedTime < blinkDuration)
        {
            // �޽����� ������ �Ķ������� ����
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.blue;
            }

            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkRate);

            // �޽����� ������ ������� ����
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }

            // �ٽ� ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkRate);

            // ��� �ð� ������Ʈ
            elapsedTime += blinkRate * 9;
        }

        // �ǰ� ���¸� ��Ȱ��ȭ�ϰ� �޽����� ������ ������� ����
        isDamage = false;
        foreach (SkinnedMeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
    }

    // ��ų�� �����ϴ� �޼����Դϴ�.
    public void ApplySkills()
    {
        // ������ ���Ⱑ ���ų�, Ȱ��ȭ�� ��ų�� ���� ��� ��ų�� �������� �ʽ��ϴ�.
        if (equipWeapon == null || activeSkills == null || activeSkills.Count == 0)
        {
            return;
        }

        // �������� ���� �ӵ� ������ �⺻ 1�� �����մϴ�.
        float totalDamageMultiplier = 1f;
        float totalAttackSpeedMultiplier = 1f;
        bool shotGun1Active = false; // ����1 ��ų Ȱ��ȭ ����
        bool shotGun2Active = false; // ����2 ��ų Ȱ��ȭ ����
        bool shotGun3Active = false; // ����3 ��ų Ȱ��ȭ ����
        bool shotGun4Active = false; // ����4 ��ų Ȱ��ȭ ����
        bool pierceShotActive = false; // ���뼦 ��ų Ȱ��ȭ ����
        bool boomShotActive = false; // �ռ� ��ų Ȱ��ȭ ����
        bool sideShotActive = false; // ���̵弦 ��ų Ȱ��ȭ ����
        bool lightningActive = false; // ����Ʈ�� ��ų Ȱ��ȭ ����
        float lightningDamage = 0f;

        int shotGun1Bullets = 0; // ����1 �� �߻�� �߰� �Ѿ� ��
        float shotGun1Angle = 0f; // ����1�� �Ѿ� �� ����
        int shotGun2Bullets = 0; // ����2 ��ų �� �߻�� �߰� �Ѿ� ��
        float shotGun2Angle = 0f; // ����2 ��ų�� �Ѿ� �� ����
        int shotGun3Bullets = 0; // ����3 ��ų �� �߻�� �߰� �Ѿ� ��
        float shotGun3Angle = 0f; // ����3 ��ų�� �Ѿ� �� ����
        int shotGun4Bullets = 0; // ����4 ��ų �� �߻�� �߰� �Ѿ� ��
        float shotGun4Angle = 0f; // ����4 ��ų�� �Ѿ� �� ����
        int totalAmmoIncrease = 0; // �ִ� źâ �������� �⺻ 0���� �����մϴ�.
        float boomShotRadius = 0f; // �ռ� ��ų ���� ����
        float boomShotDamage = 0f; // �ռ� ��ų ���ط�


        // Ȱ��ȭ�� ��ų���� ��ȸ�ϸ� ������ �� ���� �ӵ� ������ ����մϴ�.
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                totalDamageMultiplier *= skill.damageMultiplier;
                totalAttackSpeedMultiplier *= skill.attackSpeedMultiplier;

                // ����1 ��ų Ȱ��ȭ ���θ� üũ�ϰ� ���� ���� ������
                if (skill.isShotGun1)
                {
                    shotGun1Active = true;
                    shotGun1Bullets = skill.shotGun1Count;
                    shotGun1Angle = skill.shotGun1SpreadAngle;
                }

                if (skill.isShotGun2)
                {
                    shotGun2Active = true; // ����2 ��ų�� Ȱ��ȭ ���·� ����
                    shotGun2Bullets = skill.shotGun2Count; // �߻�� �Ѿ� ��
                    shotGun2Angle = skill.shotGun2SpreadAngle; // �Ѿ� ���� ����
                }

                if (skill.isShotGun3)
                {
                    shotGun3Active = true; // ����3 ��ų�� Ȱ��ȭ ���·� ����
                    shotGun3Bullets = skill.shotGun3Count; // �߻�� �Ѿ� ��
                    shotGun3Angle = skill.shotGun3SpreadAngle; // �Ѿ� ���� ����
                }

                if (skill.isShotGun4)
                {
                    shotGun4Active = true; // ����4 ��ų�� Ȱ��ȭ ���·� ����
                    shotGun4Bullets = skill.shotGun4Count; // �߻�� �Ѿ� ��
                    shotGun4Angle = skill.shotGun4SpreadAngle; // �Ѿ� ���� ����
                }

                // �Ǿ�� ��ų ���� ����
                if (skill.isPierceShot)
                {
                    pierceShotActive = true;
                }                

                // �ռ� ��ų ���� ����
                if (skill.isBoomShot)
                {
                    boomShotActive = true;
                    boomShotRadius = skill.boomShotRadius;
                    boomShotDamage = skill.boomShotDamage;
                }
                // SideShot ��ų ���� ����
                if (skill != null && skill.isSideShot)
                {
                    sideShotActive = true;
                }
                totalAmmoIncrease += skill.ammoIncrease;

                if (skill.isLightning)
                {
                    lightningActive = true;
                    lightningDamage = skill.lightningDamage;
                }
            }
            equipWeapon.UpdateMaxAmmo(totalAmmoIncrease);
        }

        // ���� �ִ� źâ �������� ������ ������ baseMaxAmmo�� �����մϴ�.
        // �̸� ���� Weapon Ŭ������ baseMaxAmmo�� UpdateMaxAmmo �޼��尡 ���ǵǾ� �־�� �մϴ�.
        equipWeapon.UpdateMaxAmmo(totalAmmoIncrease);
        // ���� ������ ������ ���⿡ �����մϴ�.
        equipWeapon.damageMultiplier = totalDamageMultiplier;
        equipWeapon.attackSpeedMultiplier = totalAttackSpeedMultiplier;

        // ����1 ��ų�� Ȱ��ȭ�Ǿ��ٸ�, ������ ���⿡ ����1 ������ ������
        equipWeapon.isShotGun1Active = shotGun1Active;
        equipWeapon.isShotGun2Active = shotGun2Active;
        equipWeapon.isShotGun3Active = shotGun3Active;
        equipWeapon.isShotGun4Active = shotGun4Active;

        // ���뼦 ��ų�� Ȱ��ȭ ���θ� ���⿡ ����
        equipWeapon.isPierceShotActive = pierceShotActive;

        // ������ ���⿡ �ռ� ��ų �Ӽ� ����
        equipWeapon.isBoomShotActive = boomShotActive;
        equipWeapon.boomShotRadius = boomShotRadius;
        equipWeapon.boomShotDamage = boomShotDamage;

        // ������ ���⿡ SideShot ��ų �Ӽ� ����
        equipWeapon.isSideShotActive = sideShotActive;

        // ������ ���⿡ Lightning ��ų �Ӽ� ����
        equipWeapon.isLightningActive = lightningActive;
        equipWeapon.lightningDamage = lightningDamage;

        if (shotGun1Active)
        {
            equipWeapon.shotGun1Bullets = shotGun1Bullets; // �߻�� �Ѿ� ��
            equipWeapon.shotGun1SpreadAngle = shotGun1Angle; // �Ѿ� ���� ����
        }
        else
        {   // ����1 ��ų�� ��Ȱ��ȭ�Ǹ� �⺻������ �缳���մϴ�.         
            equipWeapon.shotGun1Bullets = 0;
            equipWeapon.shotGun1SpreadAngle = 0f;
        }

        if (shotGun2Active)
        {
            equipWeapon.shotGun2Bullets = shotGun2Bullets;
            equipWeapon.shotGun2SpreadAngle = shotGun2Angle;
        }
        else
        {
            // ����2 ��ų�� ��Ȱ��ȭ�Ǹ� �⺻������ �缳���մϴ�.
            equipWeapon.shotGun2Bullets = 0;
            equipWeapon.shotGun2SpreadAngle = 0f;
        }

        if (shotGun3Active)
        {
            equipWeapon.shotGun3Bullets = shotGun3Bullets;
            equipWeapon.shotGun3SpreadAngle = shotGun3Angle;
        }
        else
        {
            // ����2 ��ų�� ��Ȱ��ȭ�Ǹ� �⺻������ �缳���մϴ�.
            equipWeapon.shotGun3Bullets = 0;
            equipWeapon.shotGun3SpreadAngle = 0f;
        }

        if (shotGun4Active)
        {
            equipWeapon.shotGun4Bullets = shotGun4Bullets;
            equipWeapon.shotGun4SpreadAngle = shotGun4Angle;
        }
        else
        {
            // ����2 ��ų�� ��Ȱ��ȭ�Ǹ� �⺻������ �缳���մϴ�.
            equipWeapon.shotGun4Bullets = 0;
            equipWeapon.shotGun4SpreadAngle = 0f;
        }

    }

    // ���ο� ��ų�� �߰��ϴ� �޼����Դϴ�.
    public void AddSkill(Skill newSkill)
    {
        activeSkills.Add(newSkill); // ��ų ����Ʈ�� �� ��ų�� �߰��մϴ�.
        ApplySkills(); // ��ų �߰� �� ��ų�� �����մϴ�.

    }

    // ��ų�� �����ϴ� �޼����Դϴ�.
    public void RemoveSkill(Skill skillToRemove)
    {
        activeSkills.Remove(skillToRemove); // ��ų ����Ʈ���� ������ ��ų�� �����մϴ�.
        ApplySkills(); // ��ų ���� �� ��ų�� �ٽ� �����մϴ�.
    }

    // Ʈ���ſ� �ӹ��� �� ȣ��Ǵ� �Լ�
    void OnTriggerStay(Collider other)
    {
        // ����� Ʈ���ſ� �ӹ����ִ� ���, ����� ������Ʈ�� ����
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    // Ʈ���ſ��� ��� �� ȣ��Ǵ� �Լ�
    void OnTriggerExit(Collider other)
    {
        // ���� Ʈ���ſ��� ��� ���, ����� ������Ʈ�� null�� ����
        if (other.tag == "Weapon")
            nearObject = null;
    }

}