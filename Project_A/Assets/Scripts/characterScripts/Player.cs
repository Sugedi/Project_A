using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float moveSpeed;
    public GameObject[] weapons; // ���� �迭
    public bool[] hasWeapons; // ������ ���� ���� �迭

    public Camera playerCamera;

    public int ammo; // ���� �Ѿ� ����
    public int health; // ���� ü��

    public int maxAmmo; // �ִ� �Ѿ� ����
    public int maxHealth; // �ִ� ü��

    float hAxis; // ���� �Է°�
    float vAxis;

    bool wDown;
    bool jDown;
    bool fDown; // ���� �Է� ����

    bool isDodge;
    bool isDamage;

    GunController gunController;
    Rigidbody rigid;
    Animator anim;
    SkinnedMeshRenderer[] meshs;

    Vector3 moveVec;
    Vector3 dodgeVec;

    GameObject nearObject; // �ֺ� ������Ʈ

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        meshs = GetComponents<SkinnedMeshRenderer>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gunController = GetComponent<GunController>();

    }


    void Update()
    {
        GetInput();
        Move();
        Dodge();
        

        if (moveVec.sqrMagnitude == 0)
            return;

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
        
        if (fDown)
        {
            gunController.Shoot(); // fDown�� true�� �� ���� �޼��带 ���������� ȣ���մϴ�.
        }
    }

    void GetInput()
    {
        hAxis = joy.Horizontal; // ���� �Է°� �޾ƿ���
        vAxis = joy.Vertical; // ���� �Է°� �޾ƿ���
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        
    }
    void Move()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Project the camera vectors onto the horizontal plane
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize the vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on camera orientation
        moveVec = (cameraForward * vAxis) + (cameraRight * hAxis);

        if (isDodge)
            moveVec = dodgeVec; // ȸ���ϸ鼭 ���ÿ� ���� �� �ٲٰ� ��

        rigid.MovePosition(rigid.position + moveVec);

     
        if (wDown) // �ȱ� �ӵ� ���� (�ٴ°Ŷ� �ȴ°Ŷ� �ӵ� ���� ������ �ȵǴϱ�)
            transform.position += moveVec * moveSpeed * 0.3f * Time.deltaTime; // �ȱ� ���� ��� �ӵ� ����
        else
        {
            transform.position += moveVec * moveSpeed * Time.deltaTime; // �ȱⰡ �ƴ� ��� ���� �ӵ��� �̵�
        }

        anim.SetBool("isRun", moveVec != Vector3.zero); // �޸��� ���� �ִϸ��̼� ����
        anim.SetBool("isWalk", wDown); // �ȱ� ���� �ִϸ��̼� ����

        // LookAt : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        transform.LookAt(transform.position + moveVec); // �츮�� ���ư��� �������� �ٶ󺸰� ����� (���� ��ġ+���� ����)
    }
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec;
            moveSpeed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        moveSpeed *= 0.5f;
        isDodge = false;
    }

    public void OnFireButtonDown()
    {
        fDown = true;
    }

    public void OnFireButtonUp()
    {
         fDown = false;
    }
    

    // Ʈ���ſ� �浹���� �� ȣ��Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
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
            }

            // �ı��ϴ� ��
            Destroy(item.gameObject);
        }

        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);
                StartCoroutine(OnDamage());
            }
        }

        else if (other.tag == "Weapon")
        {
            if (nearObject != null && !isDodge)
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
    }
 
    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach(SkinnedMeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }

        yield return new WaitForSeconds(1f);

        isDamage = false;
        foreach(SkinnedMeshRenderer mesh in meshs)
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