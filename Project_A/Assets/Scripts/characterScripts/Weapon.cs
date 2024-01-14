using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; // ObjectPool�� ����ϱ� ���� ���ӽ����̽� �߰�

public class Weapon : MonoBehaviour
{
    public enum Type { Range }; // ���� ����: ���Ÿ�
    public Type type; // ���� ������ ����    
    public float baseAttackSpeed; // ������ �⺻ ���� �ӵ�    
    public float damageMultiplier = 1f; // ������ ����
    public float attackSpeedMultiplier = 1f; // ���� �ӵ� ����

    public int baseMaxAmmo = 30; // �⺻ �ִ� ź�� ��
    public int maxAmmo; // �ִ� ź�� ��
    public int curAmmo; // ���� ź�� ��

    // ����1 ��ų�� ���� �Ӽ�
    public bool isShotGun1Active = false; // ����1 ��ų Ȱ��ȭ ����
    public int shotGun1Bullets = 2; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float shotGun1SpreadAngle = 45f; // �Ѿ� ������ ����

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

    public float bulletSpeed = 30f; // �Ѿ� �ӵ� �⺻�� ����
    public Transform bulletPos; // �Ѿ� �߻� ��ġ
    public Transform bulletPosLeft; // �Ѿ� �߻� ��ġ
    public Transform bulletPosRight; // �Ѿ� �߻� ��ġ
    public GameObject bullet; // �Ѿ� ������    
    public Player player; // ���⸦ �����ϰ� �ִ� �÷��̾��� �����Դϴ�.

    // �Ѿ� ������Ʈ Ǯ�� �߰��մϴ�.
    private ObjectPool<GameObject> bulletPool;

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

        // ���� �ֱٿ� Ȱ��ȭ�� ��ų�� �������� spreadAngle�� �����մϴ�.
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
            bulletScript.SetPool(bulletPool);
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

            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * bulletSpeed;
            
            // �Ѿ� �߻� �� �浹�� �ٽ� Ȱ��ȭ
            StartCoroutine(EnableColliderAfterDelay(bulletCollider));
        }
        // ���̵弦 ��ų�� Ȱ��ȭ�� ���
        if (isSideShotActive)
        {
            // ���̵弦 �Ѿ��� ���� �Ѿ��� ����ŭ �������� �߻��մϴ�.
            FireSideShot(bulletPosLeft, bulletsToFire); // ���� �߻� ��ġ���� �Ѿ� �߻�
            FireSideShot(bulletPosRight, bulletsToFire); // ������ �߻� ��ġ���� �Ѿ� �߻�
        }

        // ���� �ӵ� ������ ����Ͽ� ���� �Ѿ� �߻���� ����մϴ�.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));

    }
    void FireSideShot(Transform sideShotPos, int bulletsToFire)
    {
        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject instantBullet = bulletPool.Get();
            instantBullet.transform.position = sideShotPos.position;
            instantBullet.transform.rotation = sideShotPos.rotation;

            // �Ѿ� �浹�� �Ͻ������� ��Ȱ��ȭ
            Collider bulletCollider = instantBullet.GetComponent<Collider>();
            if (bulletCollider)
            {
                bulletCollider.enabled = false;
                bulletCollider.isTrigger = isPierceShotActive; // ���뼦 Ȱ��ȭ ���ο� ���� isTrigger ����
            }
            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.isPenetrating = isPierceShotActive; // ���뼦 ���� ����

            // BoomShot ��ų ����
            bulletScript.isBoomShotActive = isBoomShotActive;
            bulletScript.boomShotRadius = boomShotRadius;
            bulletScript.boomShotDamage = boomShotDamage;

            bulletScript.SetPool(bulletPool);
            // �Ѿ��� ������ ����
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier;

            // �Ѿ��� �����ֱ� ����
            bulletScript.lifeTime = 1f;

            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = sideShotPos.forward * bulletSpeed;



            // �Ѿ� �浹�� ��Ȱ��ȭ�ϰ� ���� �� �ٽ� Ȱ��ȭ�ϴ� �ڷ�ƾ ȣ��
            StartCoroutine(EnableColliderAfterDelay(instantBullet.GetComponent<Collider>()));
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