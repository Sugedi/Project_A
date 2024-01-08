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
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 입력값으로 이동 벡터 설정
                                                           // normalized는 대각선으로 이동 시에도 같은 속도를 내기 위해 작성
     
        /*if(isSwap) // 움직이면서 무기 스왑을 못 하게 설정할 때 사용
        {
            moveVec = Vector3.zero;
        }
        */
        if (isDodge)
            moveVec = dodgeVec; // 회피하면서 동시에 방향 못 바꾸게 함

        rigid.MovePosition(rigid.position + moveVec);

     
        if (wDown) // 걷기 속도 조절 (뛰는거랑 걷는거랑 속도 차이 없으면 안되니까)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime; // 걷기 중인 경우 속도 감소
        else
        {
            transform.position += moveVec * speed * Time.deltaTime; // 걷기가 아닌 경우 정상 속도로 이동
        }

        anim.SetBool("isRun", moveVec != Vector3.zero); // 달리는 상태 애니메이션 설정
        anim.SetBool("isWalk", wDown); // 걷기 상태 애니메이션 설정

        // LookAt : 지정된 벡터를 향해서 회전시켜주는 함수
        transform.LookAt(transform.position + moveVec); // 우리가 나아가는 방향으로 바라보게 만들기 (현재 위치+가는 방향)
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
