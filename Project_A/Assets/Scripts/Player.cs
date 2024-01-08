using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;
    public GameObject[] weapons; // ���� �迭
    public bool[] hasWeapons; // ������ ���� ���� �迭

    public Camera playerCamera; // ���� ī�޶�

    public int ammo; // ���� �Ѿ� ����
    public int health; // ���� ü��

    public int maxAmmo; // �ִ� �Ѿ� ����
    public int maxHealth; // �ִ� ü��

    float hAxis; // ���� �Է°�
    float vAxis;

    bool wDown;
    bool jDown;
    
    bool sDown1; // ���� ����1 �Է� ����
    bool sDown2; // ���� ����2 �Է� ����

    bool isDodge;
    

    Rigidbody rigid;
    Animator anim;

    Vector3 moveVec;
    Vector3 dodgeVec;

    GameObject nearObject; // �ֺ� ������Ʈ
    Weapon equipWeapon; // ���� ������ ����
    

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
       
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
        // Get the camera's forward and right vectors
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
            moveVec = dodgeVec; // Dodge overrides the movement direction

        // Move the player using Rigidbody
        rigid.MovePosition(rigid.position + moveVec * speed * Time.deltaTime);

        // Set the player's rotation to face the movement direction
        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
            rigid.MoveRotation(moveQuat);
        }

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
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
