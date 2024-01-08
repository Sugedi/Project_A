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
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // �Է°����� �̵� ���� ����
                                                           // normalized�� �밢������ �̵� �ÿ��� ���� �ӵ��� ���� ���� �ۼ�
     
        /*if(isSwap) // �����̸鼭 ���� ������ �� �ϰ� ������ �� ���
        {
            moveVec = Vector3.zero;
        }
        */
        if (isDodge)
            moveVec = dodgeVec; // ȸ���ϸ鼭 ���ÿ� ���� �� �ٲٰ� ��

        rigid.MovePosition(rigid.position + moveVec);

     
        if (wDown) // �ȱ� �ӵ� ���� (�ٴ°Ŷ� �ȴ°Ŷ� �ӵ� ���� ������ �ȵǴϱ�)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime; // �ȱ� ���� ��� �ӵ� ����
        else
        {
            transform.position += moveVec * speed * Time.deltaTime; // �ȱⰡ �ƴ� ��� ���� �ӵ��� �̵�
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
