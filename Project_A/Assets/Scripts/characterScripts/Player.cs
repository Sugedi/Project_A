using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾� �Ӽ���

    public float speed; // �̵� �ӵ�
    public GameObject[] weapons; // ���� �迭
    public bool[] hasWeapons; // ������ ���� ���� �迭
    public GameObject[] grenades; // ����ź �迭
    public int hasGrenade; // ������ ����ź ����
    public Camera followCamera; // ī�޶�

    public int ammo; // ���� �Ѿ� ����
    public int coin; // ���� ���� ����
    public int health; // ���� ü��

    public int maxAmmo; // �ִ� �Ѿ� ����
    public int maxCoin; // �ִ� ���� ����
    public int maxHealth; // �ִ� ü��
    public int maxHasGrenade; // �ִ� ����ź ����

    // ���� ����

    float hAxis; // ���� �� �Է°�
    float vAxis; // ���� �� �Է°�

    bool wDown; // �ȱ� �Է� ����
    bool jDown; // ���� �Է� ����
    bool fDown; // ���� �Է� ����
    bool rDown; // ������ �Է� ����
    bool iDown; // ��ȣ�ۿ� �Է� ����
    bool sDown1; // ���� ����1 �Է� ����
    bool sDown2; // ���� ����2 �Է� ����
    bool sDown3; // ���� ����3 �Է� ����

    // ���� ����

    bool isJump; // ���� ���� ����
    bool isDodge; // ȸ�� ���� ����
    bool isSwap; // ���� ���� ���� ����
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
        Swap();
        Interation();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("JangJungMi"); // ���� �� �Է°� �ޱ�
        vAxis = Input.GetAxisRaw("Gang"); // ���� �� �Է°� �ޱ�
        wDown = Input.GetButton("Walk"); // �ȱ� �Է� ���� �ޱ�
        jDown = Input.GetButtonDown("Jump"); // ���� �Է� ���� �ޱ�
        fDown = Input.GetButton("Fire1"); // ���� �Է� ���� �ޱ�
        rDown = Input.GetButtonDown("Reload"); // ������ �Է� ���� �ޱ�
        iDown = Input.GetButtonDown("Interation"); // ��ȣ�ۿ� �Է� ���� �ޱ�
        sDown1 = Input.GetButtonDown("Swap1"); // ���� ����1 �Է� ���� �ޱ�
        sDown2 = Input.GetButtonDown("Swap2"); // ���� ����2 �Է� ���� �ޱ�
        sDown3 = Input.GetButtonDown("Swap3"); // ���� ����3 �Է� ���� �ޱ�
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

        if (isSwap || isReload || !isFireReady)
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

    // ���� ��~ ���� ��~

    // �÷��̾� ���� ó��
    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay; // ���� ������ üũ

        if (fDown && isFireReady && !isDodge && !isSwap)
        {
            equipWeapon.Use(); // ���� ���
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot"); // ���� ������ ���� �ִϸ��̼� ����
            fireDelay = 0; // ������ �ʱ�ȭ
        }
    }

    // ������
    void Reload()
    {
        if (equipWeapon == null)
            return;

        if (equipWeapon.type == Weapon.Type.Melee)
            return;

        if (ammo == 0)
            return;

        if (rDown && !isJump && !isDodge && !isSwap && isFireReady)
        {
            anim.SetTrigger("doReload"); // ������ �ִϸ��̼� Ȱ��ȭ
            isReload = true; // ������ �� ���� ����

            Invoke("ReloadOut", 3f); // 3�� �� ������ �Ϸ� ó��
        }
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
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge && !isSwap)
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

    // ���� ����
    void Swap()
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if ((sDown1 || sDown2 || sDown3) && !isJump && !isDodge)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false); // ���� ������ ���� ��Ȱ��ȭ

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>(); // ���ο� ���� ����
            equipWeapon.gameObject.SetActive(true); // ���ο� ���� Ȱ��ȭ

            anim.SetTrigger("doSwap"); // ���� ���� �ִϸ��̼� Ȱ��ȭ

            isSwap = true; // ���� ���� �� ���� ����

            Invoke("SwapOut", 0.4f); // 0.4�� �� ���� ���� �Ϸ� ó��
        }
    }

    // ���� �Ϸ�
    void SwapOut()
    {
        isSwap = false; // ���� ���� �Ϸ� �� ���� ����
    }

    // ���� ��ȣ�ۿ�
    void Interation()
    {
        if (iDown && nearObject != null && !isJump && !isDodge)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true; // ���� ȹ�� ó��

                Destroy(nearObject); // �ֺ� ������Ʈ �ı�
            }
        }
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
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
                case Item.Type.Coin:
                    // ���� �������� ���, ���� ���� ����
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;
                case Item.Type.Heart:
                    // ��Ʈ �������� ���, ü�� ����
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
                case Item.Type.Grenade:
                    // ����ź �������� ���, ����ź ���� ����
                    grenades[hasGrenade].SetActive(true);
                    hasGrenade += item.value;
                    if (hasGrenade > maxHasGrenade)
                        hasGrenade = maxHasGrenade;
                    break;
            }
            // �ı��ϴ� ��
            Destroy(item.gameObject);
        }
        // �� �Ѿ˰� �浹���� �� ó��
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                // �� �Ѿ˿� �¾��� �� ü�� ���� �� �Ѿ� �ı�
                Bullet enemyBullet = other.GetComponent<Bullet>();
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