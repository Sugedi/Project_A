using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range }; // ���� ����: ���� �Ǵ� ���Ÿ�
    public Type type; // ���� ������ ����
    public float damage; // ������ ���ݷ�
    public float baseAttackSpeed; // ������ �⺻ ���� �ӵ�    
    public float damageMultiplier = 1f; // ������ ����
    public float attackSpeedMultiplier = 1f; // ���� �ӵ� ����

    public int maxAmmo; // �ִ� ź�� ��
    public int curAmmo; // ���� ź�� ��

    // ��ź ��� ��ų�� ���� �Ӽ�
    public bool isBuckShotActive = false; // ��ź ��� ��ų Ȱ��ȭ ����
    public int buckShotBullets = 3; // �� ���� �߻�Ǵ� �Ѿ��� ��
    public float buckShotSpreadAngle = 30f; // �Ѿ� ������ ����

    public BoxCollider meleeArea; // ���� ������ �浹 ����
    public TrailRenderer trailEffect; // ���� ������ ���� ȿ��
    public Transform bulletPos; // �Ѿ� �߻� ��ġ
    public GameObject bullet; // �Ѿ� ������
    public Transform bulletCasePos; // ź�� ���� ��ġ
    public GameObject bulletCase; // ź�� ���� ��ġ
    public Player player; // ���⸦ �����ϰ� �ִ� �÷��̾��� �����Դϴ�.

    public void Use()
    // ���⸦ ����ϴ� �޼����Դϴ�. ���� �Ǵ� ���Ÿ� ������ �����մϴ�.
    {
        if (type == Type.Melee) // ���� ������ ���
        {
            StopCoroutine("Swing"); // Swing �ڷ�ƾ ����
            StartCoroutine("Swing"); // Swing �ڷ�ƾ ����
        }
        else if (type == Type.Range && curAmmo > 0) // ���Ÿ� �����̰� ź���� �����ִ� ���
        {
            curAmmo--; // ź�� ����
            StartCoroutine(Shot()); // Shot �ڷ�ƾ ����
        }
    }

    // ���� ���� �ڷ�ƾ
    IEnumerator Swing()
    {
        // 1. ���� �ð� ��� �� ���� ���� Ȱ��ȭ �� ���� ȿ�� Ȱ��ȭ
        yield return new WaitForSeconds(0.1f); // 0.1�� ���
        meleeArea.enabled = true; // ���� �浹 ������ Ȱ��ȭ�մϴ�.
        trailEffect.enabled = true; // ���� ȿ���� Ȱ��ȭ�մϴ�.

        // 2. ���� �ð� ��� �� ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(0.3f); // 0.3�� ���
        meleeArea.enabled = false;

        // 3. ���� �ð� ��� �� ���� ȿ�� ��Ȱ��ȭ
        yield return new WaitForSeconds(0.3f); // 0.3�� ���
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // ��ź ��� ��ų Ȱ��ȭ ���ο� ���� �߻��� �Ѿ��� ���� �����մϴ�.
        int totalBullets = isBuckShotActive ? buckShotBullets : 1;

        // �߻��� �Ѿ��� ����ŭ �ݺ��մϴ�.
        for (int i = 0; i < totalBullets; i++)
        {
            // �Ѿ��� �����ϰ� �������� ������ �����մϴ�.

            GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Bullet bulletScript = instantBullet.GetComponent<Bullet>();
            bulletScript.damage *= damageMultiplier; //������ ������ �����մϴ�.        

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

        // ���� �ӵ� ������ ����Ͽ� ���� �Ѿ� �߻���� ����մϴ�.
        yield return new WaitForSeconds(1f / (baseAttackSpeed * attackSpeedMultiplier));

        // #2 ź�� ����
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
    // Use() ���η�ƾ -> Swing() �����ƾ -> Use() ���η�ƾ
    // Use() ���η�ƾ + Swing() �ڷ�ƾ (Co-Op)
}