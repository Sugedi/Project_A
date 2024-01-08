using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range }; // ���� ����: ���� �Ǵ� ���Ÿ�
    public Type type; // ���� ������ ����
    public int damage; // ������ ���ݷ�
    public float rate; // ������ ���� �ӵ�
    public int maxAmmo; // �ִ� ź�� ��
    public int curAmmo; // ���� ź�� ��

    public BoxCollider meleeArea; // ���� ������ �浹 ����
    public TrailRenderer trailEffect; // ���� ������ ���� ȿ��
    public Transform bulletPos; // �Ѿ� �߻� ��ġ
    public GameObject bullet; // �Ѿ� ������
    public Transform bulletCasePos; // ź�� ���� ��ġ
    public GameObject bulletCase; // ź�� ���� ��ġ

    public void Use()
    {
        if (type == Type.Melee) // ���� ������ ���
        {
            StopCoroutine("Swing"); // Swing �ڷ�ƾ ����
            StartCoroutine("Swing"); // Swing �ڷ�ƾ ����
        }
        else if (type == Type.Range && curAmmo > 0) // ���Ÿ� �����̰� ź���� �����ִ� ���
        {
            curAmmo--; // ź�� ����
            StartCoroutine("Shot"); // Shot �ڷ�ƾ ����
        }
    }

    // ���� ���� �ڷ�ƾ
    IEnumerator Swing()
    {
        // 1. ���� �ð� ��� �� ���� ���� Ȱ��ȭ �� ���� ȿ�� Ȱ��ȭ
        yield return new WaitForSeconds(0.1f); // 0.1�� ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        // 2. ���� �ð� ��� �� ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(0.3f); // 0.3�� ���
        meleeArea.enabled = false;

        // 3. ���� �ð� ��� �� ���� ȿ�� ��Ȱ��ȭ
        yield return new WaitForSeconds(0.3f); // 0.3�� ���
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // #1. �Ѿ� �߻�
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
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