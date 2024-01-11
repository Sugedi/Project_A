using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾� �Ӽ���

    public float speed; // �̵� �ӵ�
    public GameObject[] weapons; // ���� �迭
    public bool[] hasWeapons; // ������ ���� ���� �迭    
    public Camera followCamera; // ī�޶�
    public int ammo; // ���� �Ѿ� ����    
    public int health; // ���� ü��
    public int maxAmmo; // �ִ� �Ѿ� ����    
    public int maxHealth; // �ִ� ü��

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

    Vector3 moveVec; // �̵� ����
    Vector3 dodgeVec; // ȸ�� ����

    // ���� ���

    Rigidbody rigid; // Rigidbody ������Ʈ
    Animator anim; // Animator ������Ʈ
    MeshRenderer[] meshs; // MeshRenderer �迭

    // ��� ��

    GameObject nearObject; // �ֺ� ������Ʈ
    Weapon equipWeapon; // ���� ������ ����
    int equipWeaponIndex = -1; // ���� ������ ������ �ε���
    float fireDelay; // ���� ������

    // �ʱ�ȭ ��Ű�� ��
    void Awake()
    {              
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
        anim = GetComponentInChildren<Animator>(); // Animator ������Ʈ ��������
        meshs = GetComponentsInChildren<MeshRenderer>(); // MeshRenderer �迭 ��������
    }

    void Update()
    {
        // �Է� ó��
        GetInput();
        // �÷��̾� �׼�
        Move();
        Turn();
        Attack();
        Reload();
        Dodge();
        
    }
    
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ���� �� �Է°� �ޱ�
        vAxis = Input.GetAxisRaw("Vertical"); // ���� �� �Է°� �ޱ�
        wDown = Input.GetButton("Walk"); // �ȱ� �Է� ���� �ޱ�
        jDown = Input.GetButtonDown("Jump"); // ���� �Է� ���� �ޱ�
        fDown = Input.GetButton("Fire1"); // ���� �Է� ���� �ޱ�
        rDown = Input.GetButtonDown("Reload"); // ������ �Է� ���� �ޱ�                
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

        moveVec = (cameraForward * vAxis) + (cameraRight * hAxis); // �Է°��� ����ȭ�Ͽ� �̵� ���� ����

        if (isDodge)
            moveVec = dodgeVec; // ȸ�� ���̸� �̵� ���͸� ȸ�� ���ͷ� ����

        if (isReload || !isFireReady)
            moveVec = Vector3.zero; // ���� ����, ������, ���� �Ұ� ������ �� �̵� ����

        if (!isBorder)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // �̵� ���͸� �̿��� �÷��̾� �̵�

        rigid.MovePosition(rigid.position + moveVec * speed * Time.deltaTime);

        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
            rigid.MoveRotation(moveQuat);
        }
        anim.SetBool("isRun", moveVec != Vector3.zero); // �̵� ������ �ִϸ����Ϳ� ����
        anim.SetBool("isWalk", wDown); // �ȱ� �Է� ���θ� �ִϸ����Ϳ� ����
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
            equipWeapon.Use(); // ���� ���
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot"); // ���� ������ ���� �ִϸ��̼� ����
            fireDelay = 0; // ������ �ʱ�ȭ
        }
    }

    // ������
    void Reload()
    {
        if (equipWeapon == null || equipWeapon.type == Weapon.Type.Melee || ammo == 0)
            return;

        if (rDown && !isDodge && isFireReady)
        {            
            isReload = true; // ������ �� ���� ����
            float reloadTime = 3f * GetReloadTimeMultiplier(); // ������ �ð��� ����մϴ�.
            Invoke("ReloadOut", reloadTime); // 3�� �� ������ �Ϸ� ó��
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
    }

    // ȸ�� ����
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec; // ȸ�� ���� ����
            speed *= 2; // �̵� �ӵ� ����
            anim.SetTrigger("doDodge"); // ȸ�� �ִϸ��̼� Ȱ��ȭ
            isDodge = true; // ȸ�� �� ���� ����

            Invoke("DodgeOut", 0.5f); // 0.5�� �� ȸ�� �Ϸ� ó��
        }
    }

    // ȸ�� �Ϸ� ó��
    void DodgeOut()
    {
        speed *= 0.5f; // �̵� �ӵ� ������� ����
        isDodge = false; // ȸ�� �Ϸ� �� ���� ����
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

            Destroy(other.gameObject); // �ֺ� ������Ʈ �ı�
        }

        // �� �Ѿ˰� �浹���� �� ó��
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                // �� �Ѿ˿� �¾��� �� ü�� ���� �� �Ѿ� �ı�
                EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
                health -= enemyBullet.damage;
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

                // ������ ǥ�� �ڷ�ƾ ����
                StartCoroutine(OnDamage());
            }
        }
    }

    // ������ ǥ�� �ڷ�ƾ
    IEnumerator OnDamage()
    {
        // �ǰ� ���¸� Ȱ��ȭ�ϰ� �޽����� ������ �Ķ������� ����
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.blue;
        }

        // 1�� ���� ���
        yield return new WaitForSeconds(1f);

        // �ǰ� ���¸� ��Ȱ��ȭ�ϰ� �޽����� ������ ������� ����
        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
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
        bool buckShotActive = false; // ��ź ��� ��ų Ȱ��ȭ ����
        int buckShotBullets = 0; // ��ź ��� �� �߻�� �߰� �Ѿ� ��
        float buckShotAngle = 0f; // ��ź ����� �Ѿ� �� ����

        // Ȱ��ȭ�� ��ų���� ��ȸ�ϸ� ������ �� ���� �ӵ� ������ ����մϴ�.
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                totalDamageMultiplier *= skill.damageMultiplier;
                totalAttackSpeedMultiplier *= skill.attackSpeedMultiplier;

                // ��ź ��� ��ų Ȱ��ȭ ���θ� üũ�ϰ� ���� ���� ������
                if (skill.isBuckShot)
                {
                    buckShotActive = true;
                    buckShotBullets = skill.buckShotCount;
                    buckShotAngle = skill.buckShotSpreadAngle;
                }
            }
        }

        // ���� ������ ������ ���⿡ �����մϴ�.
        equipWeapon.damageMultiplier = totalDamageMultiplier;
        equipWeapon.attackSpeedMultiplier = totalAttackSpeedMultiplier;

        // ��ź ��� ��ų�� Ȱ��ȭ�Ǿ��ٸ�, ������ ���⿡ ��ź ��� ������ ������
        equipWeapon.isBuckShotActive = buckShotActive;
        if (buckShotActive)
        {
            equipWeapon.buckShotBullets = buckShotBullets; // �߻�� �Ѿ� ��
            equipWeapon.buckShotSpreadAngle = buckShotAngle; // �Ѿ� ���� ����
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