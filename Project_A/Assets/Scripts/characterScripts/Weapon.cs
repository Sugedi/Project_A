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

    public int maxAmmo; // �ִ� ź�� ��
    public int curAmmo; // ���� ź�� ��

    // ��ź ��� ��ų�� ���� �Ӽ�
    public bool isBuckShotActive = false; // ��ź ��� ��ų Ȱ��ȭ ����
    public int buckShotBullets = 3; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float buckShotSpreadAngle = 30f; // �Ѿ� ������ ����

    // �ٸ��μ��� ��ų ������ ���� �Ӽ�
    public bool isLegBreakActive = false;
    public int legBreakBullets = 5;
    public float legBreakSpreadAngle = 45f;


    public Transform bulletPos; // �Ѿ� �߻� ��ġ
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
            defaultCapacity: 30, // �⺻ �뷮
            maxSize: 120 // �ִ� �뷮
        );
    }

    public void Use()
    // ���⸦ ����ϴ� �޼����Դϴ�. ���Ÿ� ������ �����մϴ�.
    {

        if (type == Type.Range && curAmmo > 0) // ���Ÿ� �����̰� ź���� �����ִ� ���
        {
            curAmmo--; // ź�� ����
            StartCoroutine(Shot()); // Shot �ڷ�ƾ ����
        }
    }
 
    IEnumerator Shot()
    {
        // ��ź ��� ��ų Ȱ��ȭ ���ο� ���� �߻��� �Ѿ��� ���� �����մϴ�.
        int totalBullets = isBuckShotActive ? buckShotBullets : 1;
        int totalBullets2 = isLegBreakActive ? legBreakBullets : 1;

        // �߻��� �Ѿ��� ����ŭ �ݺ��մϴ�.
        for (int i = 0; i < totalBullets; i++)
        {
            // �Ѿ��� �����ϰ� �������� ������ �����մϴ�.

            // Instantiate ��� Ǯ���� �Ѿ��� �����ɴϴ�.
            GameObject instantBullet = bulletPool.Get();
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.SetPool(bulletPool); // Ǯ�� �����մϴ�.
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier; //������ ������ �����մϴ�.        

            // ��Ÿ�(���� �ð�) ������ �߰��մϴ�.
            bulletScript.lifeTime = 0.5f;

            // #1. �Ѿ� �߻�        
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            Quaternion spreadRotation = Quaternion.identity;

            // ��ź ��� ��ų�� Ȱ��ȭ�� ���, �Ѿ��� �پ��� ������ �߻��մϴ�.
            if (isBuckShotActive && totalBullets > 1)
            {
                float angle = -buckShotSpreadAngle / 2 + buckShotSpreadAngle * (i / (float)(totalBullets - 1));
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }
            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * 50;
        }

        for (int i = 0; i < totalBullets2; i++)
        {
            // �Ѿ��� �����ϰ� �������� ������ �����մϴ�.

            // Instantiate ��� Ǯ���� �Ѿ��� �����ɴϴ�.
            GameObject instantBullet = bulletPool.Get();
            instantBullet.transform.position = bulletPos.position;
            instantBullet.transform.rotation = bulletPos.rotation;

            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.SetPool(bulletPool); // Ǯ�� �����մϴ�.
            bulletScript.damage = bulletScript.baseDamage * damageMultiplier; //������ ������ �����մϴ�.        

            // ��Ÿ�(���� �ð�) ������ �߰��մϴ�.
            bulletScript.lifeTime = 0.5f;

            // #1. �Ѿ� �߻�        
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            Quaternion spreadRotation = Quaternion.identity;
            
            if (isLegBreakActive && totalBullets2 > 1)
            {
                // �Ѿ� ���� ������ ����մϴ�. �߾� �Ѿ��� �������� �¿�� �߻�˴ϴ�.
                float angle = -legBreakSpreadAngle / 2 + legBreakSpreadAngle * (i / (float)(totalBullets2 - 1));
                spreadRotation = Quaternion.Euler(0, angle, 0);
            }
            bulletRigid.velocity = bulletPos.rotation * spreadRotation * Vector3.forward * 50;
        }
        // ���� �ӵ� ������ ����Ͽ� ���� �Ѿ� �߻���� ����մϴ�.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));
       
    }
    // Use() ���η�ƾ -> Swing() �����ƾ -> Use() ���η�ƾ
    // Use() ���η�ƾ + Swing() �ڷ�ƾ (Co-Op)
}