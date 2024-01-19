using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; // ObjectPool을 사용하기 위한 네임스페이스 추가

public class Weapon : MonoBehaviour
{
    public enum Type { Range }; // 무기 유형: 원거리
    public Type type; // 현재 무기의 유형    
    public float baseAttackSpeed = 1.5f; // 무기의 기본 공격 속도    
    public float damageMultiplier = 1f; // 데미지 배율
    public float attackSpeedMultiplier = 1f; // 공격 속도 배율

    public int baseMaxAmmo = 20; // 기본 최대 탄약 수
    public int maxAmmo = 20; // 최대 탄약 수
    public int curAmmo = 20; // 현재 탄약 수

    // 샷건1 스킬에 대한 속성
    public bool isShotGun1Active = false; // 샷건1 스킬 활성화 여부
    public int shotGun1Bullets = 2; // 한 번에 발사되는 총알의 수
    public float shotGun1SpreadAngle = 20f; // 총알 사이의 각도

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

    // 관통샷 스킬에 대한 속성
    public bool isPierceShotActive = false; // 관통샷 스킬 활성화 여부

    // 붐샷 스킬에 대한 속성
    public bool isBoomShotActive = false; // 붐샷 스킬 활성화 여부
    public float boomShotRadius; // 붐샷 폭발 반경
    public float boomShotDamage; // 붐샷 폭발 때 주는 피해량

    // 사이드샷 스킬에 대한 속성
    public bool isSideShotActive = false; // 사이드샷 스킬 활성화 여부    

    public float bulletSpeed = 7; // 총알 속도 기본값 설정
    public Transform bulletPos; // 총알 발사 위치
    public Transform bulletPosLeft; // 총알 발사 위치
    public Transform bulletPosRight; // 총알 발사 위치
    public GameObject bullet; // 총알 프리팹   
    public GameObject pierceBullet; // 피어스샷 총알 프리팹
    public GameObject sideBullet; // 피어스샷 총알 프리팹
    public Player player; // 무기를 소유하고 있는 플레이어의 참조입니다.

    // 총알 오브젝트 풀을 추가합니다.
    private ObjectPool<GameObject> bulletPool;
    private ObjectPool<GameObject> pierceBulletPool;
    private ObjectPool<GameObject> sideBulletPool; // 추적 총알을 위한 오브젝트 풀

    public Weapon weapon;

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
            defaultCapacity: 60, // 기본 용량
            maxSize: 120 // 최대 용량
        );

        // 피어스샷 총알 풀 초기화
        pierceBulletPool = new ObjectPool<GameObject>(
            createFunc: () => {
                var newBullet = Instantiate(pierceBullet);
                newBullet.SetActive(false);
                return newBullet;
            },
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: 60,
            maxSize: 120
        );
        // 사이드샷 총알 풀 초기화
        sideBulletPool = new ObjectPool<GameObject>(
            createFunc: () => {
                var newBullet = Instantiate(sideBullet);
                newBullet.SetActive(false);
                return newBullet;
            },
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: 60, // 사이드샷 총알의 기본 용량
            maxSize: 120       // 사이드샷 총알의 최대 용량
        );

    }

    void Start()
    {
        weapon.curAmmo = GameObject.Find("DataManager").GetComponent<Weapon>().maxAmmo;
    }

    // 스킬에 의해 변경된 최대 탄창을 적용하는 메서드
    public void UpdateMaxAmmo(int ammoIncrease)
    {        
        maxAmmo = baseMaxAmmo + ammoIncrease; // 최대 탄창을 업데이트합니다.
        curAmmo = Mathf.Min(curAmmo, maxAmmo); // 현재 탄약이 최대 탄창을 초과하지 않도록 조정합니다.        
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
        float currentBulletSpeed = bulletSpeed;

        // 가장 최근에 활성화된 스킬을 기준으로 spreadAngle을 설정합니다.
        if (isShotGun4Active)
        {
            currentBulletSpeed += 2f;
            spreadAngle = shotGun4SpreadAngle;
        }
        else if (isShotGun3Active)
        {
            currentBulletSpeed += 2f;
            spreadAngle = shotGun3SpreadAngle;
        }
        else if (isShotGun2Active)
        {
            currentBulletSpeed += 2f;
            spreadAngle = shotGun2SpreadAngle;
        }
        else if (isShotGun1Active)
        {
            currentBulletSpeed += 2f; // 인스펙터에서 설정한 속도에 2를 더합니다.
            spreadAngle = shotGun1SpreadAngle;
        }


        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject instantBullet = isPierceShotActive ? pierceBulletPool.Get() : bulletPool.Get(); // 적절한 풀에서 총알을 가져옵니다.
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.isPenetrating = isPierceShotActive; // 관통샷 여부 설정                                            

            // BoomShot 스킬 적용
            bulletScript.isBoomShotActive = isBoomShotActive;
            bulletScript.boomShotRadius = boomShotRadius;
            bulletScript.boomShotDamage = boomShotDamage;

            // 총알 충돌을 일시적으로 비활성화
            Collider bulletCollider = instantBullet.GetComponent<Collider>();
            if (bulletCollider)
            {
                bulletCollider.enabled = false;
                bulletCollider.isTrigger = isPierceShotActive; // 관통샷 활성화 여부에 따라 isTrigger 설정
            }
            bulletScript.SetPool(isPierceShotActive ? pierceBulletPool : bulletPool);
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

            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * currentBulletSpeed;
            
            // 총알 발사 후 충돌을 다시 활성화
            StartCoroutine(EnableColliderAfterDelay(bulletCollider));
        }
        // 사이드샷 스킬이 활성화된 경우
        if (isSideShotActive)
        {
            // 사이드샷 총알을 정면 총알의 수만큼 양쪽으로 발사합니다.
            StartCoroutine(FireSideShot(bulletPosLeft, bulletsToFire, spreadAngle)); // 왼쪽 발사 위치에서 총알 발사
            StartCoroutine(FireSideShot(bulletPosRight, bulletsToFire, spreadAngle)); // 오른쪽 발사 위치에서 총알 발사
        }

        // 공격 속도 배율을 고려하여 다음 총알 발사까지 대기합니다.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));

    }

    // FindClosestEnemy 메서드에 매개변수를 추가합니다.
    Transform FindClosestEnemy(Transform shotPosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = shotPosition.position; // 매개변수로 받은 위치를 사용합니다.

        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTarget = potentialTarget.transform;
            }
        }       

        return closestTarget;
    }
    IEnumerator FireSideShot(Transform sideShotPos, int bulletsToFire, float spreadAngle)
    {
        for (int i = 0; i < bulletsToFire; i++)
        {
            // 탄약이 남아있는 경우에만 총알을 발사합니다.
            if (curAmmo > 0)
            {
                curAmmo--; // 탄약 소모

                // 사이드샷 총알 프리팹을 사용하여 인스턴스를 생성합니다.
                GameObject instantBullet = sideBulletPool.Get();
                instantBullet.transform.position = sideShotPos.position + sideShotPos.forward * 0.5f;

                // 사이드샷 총알의 발사 각도를 계산하고 적용합니다.
                float angle = (bulletsToFire > 1) ? (-spreadAngle / 2) + (spreadAngle / (bulletsToFire - 1)) * i : 0f;
                Quaternion sideShotRotation = Quaternion.Euler(0, sideShotPos.eulerAngles.y + angle, 0);
                instantBullet.transform.rotation = sideShotRotation;

                // 총알 스크립트 설정
                Bullet bulletScript = instantBullet.GetComponent<Bullet>();
                bulletScript.isPenetrating = false; // 관통샷 비활성화
                bulletScript.isHoming = true; // 추적 기능 활성화
                bulletScript.target = FindClosestEnemy(sideShotPos); // 가장 가까운 적을 타겟으로 설정
                bulletScript.SetPool(sideBulletPool); // 해당 오브젝트 풀 설정
                bulletScript.damage = bulletScript.baseDamage * damageMultiplier; // 데미지 설정
                bulletScript.lifeTime = 3f; // 생명주기 설정

                // 총알의 Rigidbody 설정
                Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
                // 총알에 초기 속도를 부여합니다. 추적 로직은 Bullet 스크립트 내에서 처리됩니다.
                bulletRigid.velocity = sideShotPos.forward * bulletSpeed;

                // 총알 충돌을 비활성화하고 지연 후 다시 활성화하는 코루틴 호출
                StartCoroutine(EnableColliderAfterDelay(instantBullet.GetComponent<Collider>()));

                yield return new WaitForSeconds(0.2f); // 총알 발사 간 지연
            }
            else
            {
                break; // 탄약이 없으면 루프 종료
            }
        }
    }
    // 충돌을 활성화하는 코루틴
    IEnumerator EnableColliderAfterDelay(Collider bulletCollider)
    {
        // 총알이 일정 거리 이동한 후에 충돌을 활성화합니다.
        yield return new WaitForSeconds(0.2f); // 0.2초 대기=
        if (bulletCollider)
        {
            bulletCollider.enabled = true;
        }
    }
    
}