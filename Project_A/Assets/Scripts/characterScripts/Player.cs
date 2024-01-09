using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 속성값

    public float speed; // 이동 속도
    public GameObject[] weapons; // 무기 배열
    public bool[] hasWeapons; // 보유한 무기 여부 배열
    public GameObject[] grenades; // 수류탄 배열
    public int hasGrenade; // 보유한 수류탄 개수
    public Camera followCamera; // 카메라

    public int ammo; // 현재 총알 수량
    public int coin; // 현재 코인 수량
    public int health; // 현재 체력

    public int maxAmmo; // 최대 총알 수량
    public int maxCoin; // 최대 코인 수량
    public int maxHealth; // 최대 체력
    public int maxHasGrenade; // 최대 수류탄 개수

    // 투입 변수

    float hAxis; // 가로 축 입력값
    float vAxis; // 세로 축 입력값

    bool wDown; // 걷기 입력 여부
    bool jDown; // 점프 입력 여부
    bool fDown; // 공격 입력 여부
    bool rDown; // 재장전 입력 여부
    bool iDown; // 상호작용 입력 여부
    bool sDown1; // 무기 스왑1 입력 여부
    bool sDown2; // 무기 스왑2 입력 여부
    bool sDown3; // 무기 스왑3 입력 여부

    // 상태 변수

    bool isJump; // 점프 상태 여부
    bool isDodge; // 회피 상태 여부
    bool isSwap; // 무기 스왑 상태 여부
    bool isReload; // 재장전 상태 여부
    bool isFireReady = true; // 공격 가능 여부
    bool isBorder; // 벽과 충돌 여부
    bool isDamage; // 데미지를 받은 상태 여부

    Vector3 moveVec; // 이동 벡터
    Vector3 dodgeVec; // 회피 벡터

    // 구성 요소

    Rigidbody rigid; // Rigidbody 컴포넌트
    Animator anim; // Animator 컴포넌트
    MeshRenderer[] meshs; // MeshRenderer 배열

    // 장비 값

    GameObject nearObject; // 주변 오브젝트
    Weapon equipWeapon; // 현재 장착된 무기
    int equipWeaponIndex = -1; // 현재 장착된 무기의 인덱스
    float fireDelay; // 공격 딜레이

    // 초기화 시키는 거
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        anim = GetComponentInChildren<Animator>(); // Animator 컴포넌트 가져오기
        meshs = GetComponentsInChildren<MeshRenderer>(); // MeshRenderer 배열 가져오기
    }

    void Update()
    {
        // 입력 처리
        GetInput();
        // 플레이어 액션
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
        hAxis = Input.GetAxisRaw("JangJungMi"); // 가로 축 입력값 받기
        vAxis = Input.GetAxisRaw("Gang"); // 세로 축 입력값 받기
        wDown = Input.GetButton("Walk"); // 걷기 입력 여부 받기
        jDown = Input.GetButtonDown("Jump"); // 점프 입력 여부 받기
        fDown = Input.GetButton("Fire1"); // 공격 입력 여부 받기
        rDown = Input.GetButtonDown("Reload"); // 재장전 입력 여부 받기
        iDown = Input.GetButtonDown("Interation"); // 상호작용 입력 여부 받기
        sDown1 = Input.GetButtonDown("Swap1"); // 무기 스왑1 입력 여부 받기
        sDown2 = Input.GetButtonDown("Swap2"); // 무기 스왑2 입력 여부 받기
        sDown3 = Input.GetButtonDown("Swap3"); // 무기 스왑3 입력 여부 받기
    }

    // 플레이어 이동
    void Move()
    {
        Vector3 cameraForward = followCamera.transform.forward;
        Vector3 cameraRight = followCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        moveVec = (cameraForward * vAxis) + (cameraRight * hAxis); // 입력값을 정규화하여 이동 벡터 생성

        if (isDodge)
            moveVec = dodgeVec; // 회피 중이면 이동 벡터를 회피 벡터로 설정

        if (isSwap || isReload || !isFireReady)
            moveVec = Vector3.zero; // 무기 스왑, 재장전, 공격 불가 상태일 때 이동 멈춤

        if (!isBorder)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // 이동 벡터를 이용해 플레이어 이동

        rigid.MovePosition(rigid.position + moveVec * speed * Time.deltaTime);

        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
            rigid.MoveRotation(moveQuat);
        }
        anim.SetBool("isRun", moveVec != Vector3.zero); // 이동 중인지 애니메이터에 전달
        anim.SetBool("isWalk", wDown); // 걷기 입력 여부를 애니메이터에 전달
    }

    // 다 돌려놔~
    void Turn()
    {
        // #1. 키보드에 의한 회전
        transform.LookAt(transform.position + moveVec); // 이동 방향으로 플레이어 회전

        // #2. 마우스에 의한 회전
        if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec); // 마우스 위치로 플레이어 회전
            }
        }
    }

    // 점핑 예~ 점핑 예~

    // 플레이어 공격 처리
    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay; // 공격 딜레이 체크

        if (fDown && isFireReady && !isDodge && !isSwap)
        {
            equipWeapon.Use(); // 무기 사용
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot"); // 무기 종류에 따라 애니메이션 설정
            fireDelay = 0; // 딜레이 초기화
        }
    }

    // 재장전
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
            anim.SetTrigger("doReload"); // 재장전 애니메이션 활성화
            isReload = true; // 재장전 중 상태 설정

            Invoke("ReloadOut", 3f); // 3초 후 재장전 완료 처리
        }
    }

    // 재장전 처리 완료
    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo;
        ammo -= reAmmo;
        isReload = false; // 재장전 완료 후 상태 설정
    }

    // 회피 구현
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            dodgeVec = moveVec; // 회피 방향 설정
            speed *= 2; // 이동 속도 증가
            anim.SetTrigger("doDodge"); // 회피 애니메이션 활성화
            isDodge = true; // 회피 중 상태 설정

            Invoke("DodgeOut", 0.5f); // 0.5초 후 회피 완료 처리
        }
    }

    // 회피 완료 처리
    void DodgeOut()
    {
        speed *= 0.5f; // 이동 속도 원래대로 감소
        isDodge = false; // 회피 완료 후 상태 설정
    }

    // 무기 스왑
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
                equipWeapon.gameObject.SetActive(false); // 현재 장착된 무기 비활성화

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>(); // 새로운 무기 장착
            equipWeapon.gameObject.SetActive(true); // 새로운 무기 활성화

            anim.SetTrigger("doSwap"); // 무기 스왑 애니메이션 활성화

            isSwap = true; // 무기 스왑 중 상태 설정

            Invoke("SwapOut", 0.4f); // 0.4초 후 무기 스왑 완료 처리
        }
    }

    // 스왑 완료
    void SwapOut()
    {
        isSwap = false; // 무기 스왑 완료 후 상태 설정
    }

    // 무기 상호작용
    void Interation()
    {
        if (iDown && nearObject != null && !isJump && !isDodge)
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

    // 회전 고정 함수
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    // 벽에서 멈추게 하는 함수
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        // 벽이 감지되면 isBorder를 true로 설정
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    // 물리 업데이트를 담당하는 함수
    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }



    // 트리거와 충돌했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
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
                case Item.Type.Coin:
                    // 코인 아이템인 경우, 코인 수량 증가
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;
                case Item.Type.Heart:
                    // 하트 아이템인 경우, 체력 증가
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
                case Item.Type.Grenade:
                    // 수류탄 아이템인 경우, 수류탄 수량 증가
                    grenades[hasGrenade].SetActive(true);
                    hasGrenade += item.value;
                    if (hasGrenade > maxHasGrenade)
                        hasGrenade = maxHasGrenade;
                    break;
            }
            // 파괴하는 거
            Destroy(item.gameObject);
        }
        // 적 총알과 충돌했을 때 처리
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                // 적 총알에 맞았을 때 체력 감소 및 총알 파괴
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

                // 데미지 표시 코루틴 실행
                StartCoroutine(OnDamage());
            }
        }
    }

    // 데미지 표시 코루틴
    IEnumerator OnDamage()
    {
        // 피격 상태를 활성화하고 메쉬들의 색상을 파란색으로 변경
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.blue;
        }

        // 1초 동안 대기
        yield return new WaitForSeconds(1f);

        // 피격 상태를 비활성화하고 메쉬들의 색상을 흰색으로 변경
        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
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