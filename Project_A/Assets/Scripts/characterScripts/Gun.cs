using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // �ѱ��� ��ġ�� ��Ÿ���� Transform�Դϴ�.
    public Transform muzzle;

    // �߻��� ������Ÿ��(źȯ ��)�� ��Ÿ���ϴ�.
    public Projectile projectile;

    // �߻� ������ �и��� ������ ��Ÿ���ϴ�. ���� ���, 100�̸� 1�ʿ� 10�� �߻��մϴ�.
    public float msBetweenShots = 10;

    // �Ѿ��� ������ �ӵ��� ��Ÿ���ϴ�.
    public float muzzleVelocity = 35;

    // ���� �߻� ���� �ð��� ��Ÿ���ϴ�.
    float nextShotTime;

    // �߻縦 ó���ϴ� �޼ҵ��Դϴ�.
    public void Shoot()
    {
        // ���� �ð��� ���� �߻� ���� �ð����� Ŭ ��쿡�� �߻縦 ó���մϴ�.
        if (Time.time > nextShotTime)
        {
            // ���� �߻� ���� �ð��� ���� �ð��� �߻� ������ ���� ������ �����մϴ�.
            nextShotTime = Time.time + msBetweenShots / 1000;

            // ���ο� ������Ÿ���� �����ϰ�, �ѱ��� ��ġ�� �������� �����մϴ�.
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            
            // ������Ÿ���� �ӵ��� �����մϴ�.
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
