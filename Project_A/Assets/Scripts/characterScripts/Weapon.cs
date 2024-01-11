using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; // ObjectPool을 사용하기 위한 네임스페이스 추가

public class Weapon : MonoBehaviour
{
    public enum Type { Range }; // 무기 유형: 원거리
    public Type type; // 현재 무기의 유형    
    public float baseAttackSpeed; // 무기의 기본 공격 속도    
    public float damageMultiplier = 1f; // 데미지 배율
    public float attackSpeedMultiplier = 1f; // 공격 속도 배율

    public int maxAmmo; // 최대 탄약 수
    public int curAmmo; // 현재 탄약 수

    // 산탄 사격 스킬에 대한 속성
    public bool isBuckShotActive = false; // 산탄 사격 스킬 활성화 여부
    public int buckShotBullets = 3; // 한 번에 발사되는 총알의 수
    public float buckShotSpreadAngle = 30f; // 총알 사이의 각도

    // 다리부수기 스킬 적용을 위한 속성
    public bool isLegBreakActive = false;
    public int legBreakBullets = 5;
    public float legBreakSpreadAngle = 45f;


    public Transform bulletPos; // 총알 발사 위치
    public GameObject bullet; // 총알 프리팹    
    public Player player; // 무기를 소유하고 있는 플레이어의 참조입니다.

    // 총알 오브젝트 풀을 추가합니다.
    private ObjectPool<GameObject> bulletPool;

    void Awake()
    {
        // 총알 풀을 초기화합니다.
        bulletPool = new ObjectPool<GameObject>(
            createFunc: () => {
                var newBullet = Instantiate(bullet);
                newBullet.SetActive(false); // 비활성화 상태로 시작합니다.
                return newBullet;
            },
            actionOnGet: (obj) => {
                obj.SetActive(true); // 활성화 상태로 변경합니다.
            },
            actionOnRelease: (obj) => {
                obj.SetActive(false); // 비활성화 상태로 변경합니다.
            },
            actionOnDestroy: (obj) => {
                Destroy(obj); // 오브젝트를 파괴합니다.
            },
            defaultCapacity: 30, // 기본 용량
            maxSize: 120 // 최대 용량
        );
    }

    public void Use()
    // 무기를 사용하는 메서드입니다. 원거리 공격을 실행합니다.
    {

        if (type == Type.Range && curAmmo > 0) // 원거리 무기이고 탄약이 남아있는 경우
        {
            curAmmo--; // 탄약 감소
            StartCoroutine(Shot()); // Shot 코루틴 시작
        }
    }
 
    IEnumerator Shot()
    {
        // 산탄 사격 스킬 활성화 여부에 따라 발사할 총알의 수를 결정합니다.
        int totalBullets = isBuckShotActive ? buckShotBullets : 1;
        int totalBullets2 = isLegBreakActive ? legBreakBullets : 1;

        // 발사할 총알의 수만큼 반복합니다.
        for (int i = 0; i < totalBullets; i++)
        {
            // 총알을 생성하고 데미지에 배율을 적용합니다.

            // Instantiate 대신 풀에서 총알을 가져옵니다.
            GameObject instantBullet = bulletPool.Get();
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.SetPool(bulletPool); // 풀을 설정합니다.
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier; //데미지 배율을 적용합니다.        

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

        for (int i = 0; i < totalBullets2; i++)
        {
            // 총알을 생성하고 데미지에 배율을 적용합니다.

            // Instantiate 대신 풀에서 총알을 가져옵니다.
            GameObject instantBullet = bulletPool.Get();
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.SetPool(bulletPool); // 풀을 설정합니다.
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier; //데미지 배율을 적용합니다.        

            // 사거리(생명 시간) 설정을 추가합니다.
            bulletScript.lifeTime = 0.5f;

            // #1. 총알 발사        
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            Quaternion spreadRotation = Quaternion.identity;
            
            if (isLegBreakActive && totalBullets2 > 1)
            {
                // 총알 간의 각도를 계산합니다. 중앙 총알을 기준으로 좌우로 발사됩니다.
                float angle = -legBreakSpreadAngle / 2 + legBreakSpreadAngle * (i / (float)(totalBullets2 - 1));
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }
            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * 50;
        }
        // 공격 속도 배율을 고려하여 다음 총알 발사까지 대기합니다.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));
       
    }
    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-Op)
}