using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;
    public GameObject[] weapons; // 무기 배열
    public bool[] hasWeapons; // 보유한 무기 여부 배열

    public Camera playerCamera; // 메인 카메라

    public int ammo; // 현재 총알 수량
    public int health; // 현재 체력

    public int maxAmmo; // 최대 총알 수량
    public int maxHealth; // 최대 체력

    float hAxis; // 수평 입력값
    float vAxis;

    bool wDown;
    bool jDown;
    
    bool sDown1; // 무기 스왑1 입력 여부
    bool sDown2; // 무기 스왑2 입력 여부

    bool isDodge;
    

    Rigidbody rigid;
    Animator anim;

    Vector3 moveVec;
    Vector3 dodgeVec;

    GameObject nearObject; // 주변 오브젝트
    Weapon equipWeapon; // 현재 장착된 무기
    

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
        hAxis = joy.Horizontal; // 수평 입력값 받아오기
        vAxis = joy.Vertical; // 수직 입력값 받아오기
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
    

    // 트리거와 충돌했을 때 호출되는 함수
    void OnTriggerEnter(Collider other)
    {
        // 아이템과 충돌했을 때 처리
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            // 아이템 종류에 따라 처리
            switch (item.type)
            {
                case Item.Type.Ammo:
                    // 총알 아이템인 경우, 총알 수량 증가
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
            }

            // 파괴하는 거
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
                    hasWeapons[weaponIndex] = true; // 무기 획득 처리

                    Destroy(nearObject); // 주변 오브젝트 파괴
                }
            }
        }
    }



    // 트리거에 머무를 때 호출되는 함수
    void OnTriggerStay(Collider other)
    {
        // 무기와 트리거에 머물러있는 경우, 가까운 오브젝트로 설정
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    // 트리거에서 벗어날 때 호출되는 함수
    void OnTriggerExit(Collider other)
    {
        // 무기 트리거에서 벗어날 경우, 가까운 오브젝트를 null로 설정
        if (other.tag == "Weapon")
            nearObject = null;
    }


}
