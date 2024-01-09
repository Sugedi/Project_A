using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float moveSpeed;
    public GameObject[] weapons; // 무기 배열
    public bool[] hasWeapons; // 보유한 무기 여부 배열

    public Camera playerCamera;

    public int ammo; // 현재 총알 수량
    public int health; // 현재 체력

    public int maxAmmo; // 최대 총알 수량
    public int maxHealth; // 최대 체력

    float hAxis; // 수평 입력값
    float vAxis;

    bool wDown;
    bool jDown;
    bool fDown; // 공격 입력 여부

    bool isDodge;
    bool isDamage;

    GunController gunController;
    Rigidbody rigid;
    Animator anim;
    SkinnedMeshRenderer[] meshs;

    Vector3 moveVec;
    Vector3 dodgeVec;

    GameObject nearObject; // 주변 오브젝트

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
            gunController.Shoot(); // fDown이 true일 때 공격 메서드를 지속적으로 호출합니다.
        }
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
            moveVec = dodgeVec; // 회피하면서 동시에 방향 못 바꾸게 함

        rigid.MovePosition(rigid.position + moveVec);

     
        if (wDown) // 걷기 속도 조절 (뛰는거랑 걷는거랑 속도 차이 없으면 안되니까)
            transform.position += moveVec * moveSpeed * 0.3f * Time.deltaTime; // 걷기 중인 경우 속도 감소
        else
        {
            transform.position += moveVec * moveSpeed * Time.deltaTime; // 걷기가 아닌 경우 정상 속도로 이동
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
                    hasWeapons[weaponIndex] = true; // 무기 획득 처리

                    Destroy(nearObject); // 주변 오브젝트 파괴
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