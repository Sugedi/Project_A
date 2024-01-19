using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; // ObjectPool�� ����ϱ� ���� ���ӽ����̽� �߰�

public class Weapon : MonoBehaviour
{
    public enum Type { Range }; // ���� ����: ���Ÿ�
    public Type type; // ���� ������ ����    
    public float baseAttackSpeed = 1.5f; // ������ �⺻ ���� �ӵ�    
    public float damageMultiplier = 1f; // ������ ����
    public float attackSpeedMultiplier = 1f; // ���� �ӵ� ����

    public int baseMaxAmmo = 20; // �⺻ �ִ� ź�� ��
    public int maxAmmo = 20; // �ִ� ź�� ��
    public int curAmmo = 20; // ���� ź�� ��

    // ����1 ��ų�� ���� �Ӽ�
    public bool isShotGun1Active = false; // ����1 ��ų Ȱ��ȭ ����
    public int shotGun1Bullets = 2; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float shotGun1SpreadAngle = 20f; // �Ѿ� ������ ����

    // ����2 ��ų ������ ���� �Ӽ�
    public bool isShotGun2Active = false; // ����1 ��ų Ȱ��ȭ ����
    public int shotGun2Bullets = 3; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float shotGun2SpreadAngle = 45f; // �Ѿ� ������ ����

    // ����1 ��ų�� ���� �Ӽ�
    public bool isShotGun3Active = false; // ����1 ��ų Ȱ��ȭ ����
    public int shotGun3Bullets = 4; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float shotGun3SpreadAngle = 45f; // �Ѿ� ������ ����

    // ����1 ��ų�� ���� �Ӽ�
    public bool isShotGun4Active = false; // ����1 ��ų Ȱ��ȭ ����
    public int shotGun4Bullets = 5; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float shotGun4SpreadAngle = 45f; // �Ѿ� ������ ����

    // ���뼦 ��ų�� ���� �Ӽ�
    public bool isPierceShotActive = false; // ���뼦 ��ų Ȱ��ȭ ����

    // �ռ� ��ų�� ���� �Ӽ�
    public bool isBoomShotActive = false; // �ռ� ��ų Ȱ��ȭ ����
    public float boomShotRadius; // �ռ� ���� �ݰ�
    public float boomShotDamage; // �ռ� ���� �� �ִ� ���ط�

    // ���̵弦 ��ų�� ���� �Ӽ�
    public bool isSideShotActive = false; // ���̵弦 ��ų Ȱ��ȭ ����    

    public float bulletSpeed = 7; // �Ѿ� �ӵ� �⺻�� ����
    public Transform bulletPos; // �Ѿ� �߻� ��ġ
    public Transform bulletPosLeft; // �Ѿ� �߻� ��ġ
    public Transform bulletPosRight; // �Ѿ� �߻� ��ġ
    public GameObject bullet; // �Ѿ� ������   
    public GameObject pierceBullet; // �Ǿ�� �Ѿ� ������
    public GameObject sideBullet; // �Ǿ�� �Ѿ� ������
    public Player player; // ���⸦ �����ϰ� �ִ� �÷��̾��� �����Դϴ�.

    // �Ѿ� ������Ʈ Ǯ�� �߰��մϴ�.
    private ObjectPool<GameObject> bulletPool;
    private ObjectPool<GameObject> pierceBulletPool;
    private ObjectPool<GameObject> sideBulletPool; // ���� �Ѿ��� ���� ������Ʈ Ǯ

    public Weapon weapon;

    void Awake()
    {
        // �Ѿ� Ǯ�� �ʱ�ȭ�մϴ�.
        bulletPool = new ObjectPool<GameObject>(
            createFunc: () => {
                var newBullet = Instantiate(bullet);
                newBullet.SetActive(false); // ��Ȱ��ȭ ���·� �����մϴ�.
                return newBullet;
            },
            actionOnGet: (obj) => {
                obj.SetActive(true); // Ȱ��ȭ ���·� �����մϴ�.
            },
            actionOnRelease: (obj) => {
                obj.SetActive(false); // ��Ȱ��ȭ ���·� �����մϴ�.
            },
            actionOnDestroy: (obj) => {
                Destroy(obj); // ������Ʈ�� �ı��մϴ�.
            },
            defaultCapacity: 60, // �⺻ �뷮
            maxSize: 120 // �ִ� �뷮
        );

        // �Ǿ�� �Ѿ� Ǯ �ʱ�ȭ
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
        // ���̵弦 �Ѿ� Ǯ �ʱ�ȭ
        sideBulletPool = new ObjectPool<GameObject>(
            createFunc: () => {
                var newBullet = Instantiate(sideBullet);
                newBullet.SetActive(false);
                return newBullet;
            },
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: 60, // ���̵弦 �Ѿ��� �⺻ �뷮
            maxSize: 120       // ���̵弦 �Ѿ��� �ִ� �뷮
        );

    }

    void Start()
    {
        weapon.curAmmo = GameObject.Find("DataManager").GetComponent<Weapon>().maxAmmo;
    }

    // ��ų�� ���� ����� �ִ� źâ�� �����ϴ� �޼���
    public void UpdateMaxAmmo(int ammoIncrease)
    {        
        maxAmmo = baseMaxAmmo + ammoIncrease; // �ִ� źâ�� ������Ʈ�մϴ�.
        curAmmo = Mathf.Min(curAmmo, maxAmmo); // ���� ź���� �ִ� źâ�� �ʰ����� �ʵ��� �����մϴ�.        
    }

    public void Use()
    // ���⸦ ����ϴ� �޼����Դϴ�. ���Ÿ� ������ �����մϴ�.
    {

        // ���⸦ ����ϴ� �޼����Դϴ�. ���Ÿ� ������ �����մϴ�.
        if (type == Type.Range)
        {
            // ��ų�� Ȱ��ȭ�Ǿ����� Ȯ���ϰ� �Ѿ� ������ �����մϴ�.
            int totalBullets = 1;
            if (isShotGun1Active) totalBullets = shotGun1Bullets;
            if (isShotGun2Active) totalBullets = shotGun2Bullets;
            if (isShotGun3Active) totalBullets = shotGun3Bullets;
            if (isShotGun4Active) totalBullets = shotGun4Bullets;           

            // ���� ź�� ���� �߻��� �Ѿ� ������ ���� ���, ���� ź�ุŭ�� �߻��մϴ�.
            int bulletsToFire = Mathf.Min(totalBullets, curAmmo);

            // ź���� �Ҹ��մϴ�.
            curAmmo -= bulletsToFire;

            // Shot �ڷ�ƾ�� �����մϴ�.
            StartCoroutine(Shot(bulletsToFire));
        }
    }

        IEnumerator Shot(int bulletsToFire)
    {
        // ��ų�� Ȱ��ȭ�Ǿ����� Ȯ���մϴ�.
        
        float spreadAngle = 0f;
        float currentBulletSpeed = bulletSpeed;

        // ���� �ֱٿ� Ȱ��ȭ�� ��ų�� �������� spreadAngle�� �����մϴ�.
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
            currentBulletSpeed += 2f; // �ν����Ϳ��� ������ �ӵ��� 2�� ���մϴ�.
            spreadAngle = shotGun1SpreadAngle;
        }


        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject instantBullet = isPierceShotActive ? pierceBulletPool.Get() : bulletPool.Get(); // ������ Ǯ���� �Ѿ��� �����ɴϴ�.
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.isPenetrating = isPierceShotActive; // ���뼦 ���� ����                                            

            // BoomShot ��ų ����
            bulletScript.isBoomShotActive = isBoomShotActive;
            bulletScript.boomShotRadius = boomShotRadius;
            bulletScript.boomShotDamage = boomShotDamage;

            // �Ѿ� �浹�� �Ͻ������� ��Ȱ��ȭ
            Collider bulletCollider = instantBullet.GetComponent<Collider>();
            if (bulletCollider)
            {
                bulletCollider.enabled = false;
                bulletCollider.isTrigger = isPierceShotActive; // ���뼦 Ȱ��ȭ ���ο� ���� isTrigger ����
            }
            bulletScript.SetPool(isPierceShotActive ? pierceBulletPool : bulletPool);
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier;
            bulletScript.lifeTime = 1f; // �Ѿ��� ���� �ð� ����

            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            Quaternion spreadRotation = Quaternion.identity;

            // �Ѿ� �߻� ������ ����մϴ�.
            if (bulletsToFire > 1)
            {
                float angle = -spreadAngle / 2 + spreadAngle * (i / (float)(bulletsToFire - 1));
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }

            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * currentBulletSpeed;
            
            // �Ѿ� �߻� �� �浹�� �ٽ� Ȱ��ȭ
            StartCoroutine(EnableColliderAfterDelay(bulletCollider));
        }
        // ���̵弦 ��ų�� Ȱ��ȭ�� ���
        if (isSideShotActive)
        {
            // ���̵弦 �Ѿ��� ���� �Ѿ��� ����ŭ �������� �߻��մϴ�.
            StartCoroutine(FireSideShot(bulletPosLeft, bulletsToFire, spreadAngle)); // ���� �߻� ��ġ���� �Ѿ� �߻�
            StartCoroutine(FireSideShot(bulletPosRight, bulletsToFire, spreadAngle)); // ������ �߻� ��ġ���� �Ѿ� �߻�
        }

        // ���� �ӵ� ������ ����Ͽ� ���� �Ѿ� �߻���� ����մϴ�.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));

    }

    // FindClosestEnemy �޼��忡 �Ű������� �߰��մϴ�.
    Transform FindClosestEnemy(Transform shotPosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = shotPosition.position; // �Ű������� ���� ��ġ�� ����մϴ�.

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
            // ź���� �����ִ� ��쿡�� �Ѿ��� �߻��մϴ�.
            if (curAmmo > 0)
            {
                curAmmo--; // ź�� �Ҹ�

                // ���̵弦 �Ѿ� �������� ����Ͽ� �ν��Ͻ��� �����մϴ�.
                GameObject instantBullet = sideBulletPool.Get();
                instantBullet.transform.position = sideShotPos.position + sideShotPos.forward * 0.5f;

                // ���̵弦 �Ѿ��� �߻� ������ ����ϰ� �����մϴ�.
                float angle = (bulletsToFire > 1) ? (-spreadAngle / 2) + (spreadAngle / (bulletsToFire - 1)) * i : 0f;
                Quaternion sideShotRotation = Quaternion.Euler(0, sideShotPos.eulerAngles.y + angle, 0);
                instantBullet.transform.rotation = sideShotRotation;

                // �Ѿ� ��ũ��Ʈ ����
                Bullet bulletScript = instantBullet.GetComponent<Bullet>();
                bulletScript.isPenetrating = false; // ���뼦 ��Ȱ��ȭ
                bulletScript.isHoming = true; // ���� ��� Ȱ��ȭ
                bulletScript.target = FindClosestEnemy(sideShotPos); // ���� ����� ���� Ÿ������ ����
                bulletScript.SetPool(sideBulletPool); // �ش� ������Ʈ Ǯ ����
                bulletScript.damage = bulletScript.baseDamage * damageMultiplier; // ������ ����
                bulletScript.lifeTime = 3f; // �����ֱ� ����

                // �Ѿ��� Rigidbody ����
                Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
                // �Ѿ˿� �ʱ� �ӵ��� �ο��մϴ�. ���� ������ Bullet ��ũ��Ʈ ������ ó���˴ϴ�.
                bulletRigid.velocity = sideShotPos.forward * bulletSpeed;

                // �Ѿ� �浹�� ��Ȱ��ȭ�ϰ� ���� �� �ٽ� Ȱ��ȭ�ϴ� �ڷ�ƾ ȣ��
                StartCoroutine(EnableColliderAfterDelay(instantBullet.GetComponent<Collider>()));

                yield return new WaitForSeconds(0.2f); // �Ѿ� �߻� �� ����
            }
            else
            {
                break; // ź���� ������ ���� ����
            }
        }
    }
    // �浹�� Ȱ��ȭ�ϴ� �ڷ�ƾ
    IEnumerator EnableColliderAfterDelay(Collider bulletCollider)
    {
        // �Ѿ��� ���� �Ÿ� �̵��� �Ŀ� �浹�� Ȱ��ȭ�մϴ�.
        yield return new WaitForSeconds(0.2f); // 0.2�� ���=
        if (bulletCollider)
        {
            bulletCollider.enabled = true;
        }
    }
    
}