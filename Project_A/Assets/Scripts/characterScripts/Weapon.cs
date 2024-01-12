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

    // 샷건1 스킬에 대한 속성
    public bool isShotGun1Active = false; // 샷건1 스킬 활성화 여부
    public int shotGun1Bullets = 2; // 한 번에 발사되는 총알의 수
    public float shotGun1SpreadAngle = 45f; // 총알 사이의 각도

    // 샷건2 스킬 적용을 위한 속성
    public bool isShotGun2Active = false; // 샷건1 스킬 활성화 여부
    public int shotGun2Bullets = 3; // 한 번에 발사되는 총알의 수
    public float shotGun2SpreadAngle = 45f; // 총알 사이의 각도

    // 샷건1 스킬에 대한 속성
    public bool isShotGun3Active = false; // 샷건1 스킬 활성화 여부
    public int shotGun3Bullets = 4; // 한 번에 발사되는 총알의 수
    public float shotGun3SpreadAngle = 45f; // 총알 사이의 각도

    // 샷건1 스킬에 대한 속성
    public bool isShotGun4Active = false; // 샷건1 스킬 활성화 여부
    public int shotGun4Bullets = 5; // 한 번에 발사되는 총알의 수
    public float shotGun4SpreadAngle = 45f; // 총알 사이의 각도

    public float bulletSpeed = 25f; // 총알 속도 기본값 설정
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
            defaultCapacity: 50, // 기본 용량
            maxSize: 120 // 최대 용량
        );
    }

    public void Use()
    // 무기를 사용하는 메서드입니다. 원거리 공격을 실행합니다.
    {

        // 무기를 사용하는 메서드입니다. 원거리 공격을 실행합니다.
        if (type == Type.Range)
        {
            // 스킬이 활성화되었는지 확인하고 총알 개수를 결정합니다.
            int totalBullets = 1;
            if (isShotGun1Active) totalBullets = shotGun1Bullets;
            if (isShotGun2Active) totalBullets = shotGun2Bullets;
            if (isShotGun3Active) totalBullets = shotGun3Bullets;
            if (isShotGun4Active) totalBullets = shotGun4Bullets;

            // 현재 탄약 수가 발사할 총알 수보다 적은 경우, 남은 탄약만큼만 발사합니다.
            int bulletsToFire = Mathf.Min(totalBullets, curAmmo);

            // 탄약을 소모합니다.
            curAmmo -= bulletsToFire;

            // Shot 코루틴을 시작합니다.
            StartCoroutine(Shot(bulletsToFire));
        }
    }

        IEnumerator Shot(int bulletsToFire)
    {
        // 스킬이 활성화되었는지 확인합니다.
        
        float spreadAngle = 0f;

        // 가장 최근에 활성화된 스킬을 기준으로 spreadAngle을 설정합니다.
        if (isShotGun4Active)
        {
            spreadAngle = shotGun4SpreadAngle;
        }
        else if (isShotGun3Active)
        {
            spreadAngle = shotGun3SpreadAngle;
        }
        else if (isShotGun2Active)
        {
            spreadAngle = shotGun2SpreadAngle;
        }
        else if (isShotGun1Active)
        {
            spreadAngle = shotGun1SpreadAngle;
        }


        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject instantBullet = bulletPool.Get();
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();

            // 총알 충돌을 일시적으로 비활성화
            Collider bulletCollider = instantBullet.GetComponent<Collider>();
            if (bulletCollider)
            {
                bulletCollider.enabled = false;
            }
            bulletScript.SetPool(bulletPool);
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier;
            bulletScript.lifeTime = 1f; // 총알의 생명 시간 설정

            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            Quaternion spreadRotation = Quaternion.identity;

            // 총알 발사 각도를 계산합니다.
            if (bulletsToFire > 1)
            {
                float angle = -spreadAngle / 2 + spreadAngle * (i / (float)(bulletsToFire - 1));
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }

            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * bulletSpeed;
            
            // 총알 발사 후 충돌을 다시 활성화
            StartCoroutine(EnableColliderAfterDelay(bulletCollider));
        }

        // 공격 속도 배율을 고려하여 다음 총알 발사까지 대기합니다.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));

    }
    // 충돌을 활성화하는 코루틴
    IEnumerator EnableColliderAfterDelay(Collider bulletCollider)
    {
        // 총알이 일정 거리 이동한 후에 충돌을 활성화합니다.
        yield return new WaitForSeconds(0.1f); // 0.1초 대기=
        if (bulletCollider)
        {
            bulletCollider.enabled = true;
        }
    }
    
}