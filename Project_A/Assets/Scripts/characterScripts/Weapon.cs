using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range }; // 무기 유형: 근접 또는 원거리
    public Type type; // 현재 무기의 유형
    public float damage; // 무기의 공격력
    public float baseAttackSpeed; // 무기의 기본 공격 속도    
    public float damageMultiplier = 1f; // 데미지 배율
    public float attackSpeedMultiplier = 1f; // 공격 속도 배율

    public int maxAmmo; // 최대 탄약 수
    public int curAmmo; // 현재 탄약 수

    // 산탄 사격 스킬에 대한 속성
    public bool isBuckShotActive = false; // 산탄 사격 스킬 활성화 여부
    public int buckShotBullets = 3; // 한 번에 발사되는 총알의 수
    public float buckShotSpreadAngle = 30f; // 총알 사이의 각도

    public BoxCollider meleeArea; // 근접 무기의 충돌 영역
    public TrailRenderer trailEffect; // 근접 무기의 궤적 효과
    public Transform bulletPos; // 총알 발사 위치
    public GameObject bullet; // 총알 프리팹
    public Transform bulletCasePos; // 탄피 배출 위치
    public GameObject bulletCase; // 탄피 배출 위치
    public Player player; // 무기를 소유하고 있는 플레이어의 참조입니다.

    public void Use()
    // 무기를 사용하는 메서드입니다. 근접 또는 원거리 공격을 실행합니다.
    {
        if (type == Type.Melee) // 근접 무기인 경우
        {
            StopCoroutine("Swing"); // Swing 코루틴 중지
            StartCoroutine("Swing"); // Swing 코루틴 시작
        }
        else if (type == Type.Range && curAmmo > 0) // 원거리 무기이고 탄약이 남아있는 경우
        {
            curAmmo--; // 탄약 감소
            StartCoroutine(Shot()); // Shot 코루틴 시작
        }
    }

    // 근접 공격 코루틴
    IEnumerator Swing()
    {
        // 1. 일정 시간 대기 후 근접 영역 활성화 및 궤적 효과 활성화
        yield return new WaitForSeconds(0.1f); // 0.1초 대기
        meleeArea.enabled = true; // 근접 충돌 영역을 활성화합니다.
        trailEffect.enabled = true; // 궤적 효과를 활성화합니다.

        // 2. 일정 시간 대기 후 근접 영역 비활성화
        yield return new WaitForSeconds(0.3f); // 0.3초 대기
        meleeArea.enabled = false;

        // 3. 일정 시간 대기 후 궤적 효과 비활성화
        yield return new WaitForSeconds(0.3f); // 0.3초 대기
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // 산탄 사격 스킬 활성화 여부에 따라 발사할 총알의 수를 결정합니다.
        int totalBullets = isBuckShotActive ? buckShotBullets : 1;

        // 발사할 총알의 수만큼 반복합니다.
        for (int i = 0; i < totalBullets; i++)
        {
            // 총알을 생성하고 데미지에 배율을 적용합니다.

            GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.damage *= damageMultiplier; //데미지 배율을 적용합니다.        

            // 사거리(생명 시간) 설정을 추가합니다.
            bulletScript.lifeTime = 0.5f;

            // #1. 총알 발사        
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            Quaternion spreadRotation = Quaternion.identity;

            // 산탄 사격 스킬이 활성화된 경우, 총알을 다양한 각도로 발사합니다.
            if (isBuckShotActive && totalBullets > 1)
            {
                float angle = -buckShotSpreadAngle / 2 + buckShotSpreadAngle * (i / (float)(totalBullets - 1));
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }
            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * 50;
        }        

        // 공격 속도 배율을 고려하여 다음 총알 발사까지 대기합니다.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));

        // #2 탄피 배출
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-Op)
}