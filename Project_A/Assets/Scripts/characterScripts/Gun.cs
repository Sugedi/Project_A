using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // 총구의 위치를 나타내는 Transform입니다.
    public Transform muzzle;

    // 발사할 프로젝타일(탄환 등)을 나타냅니다.
    public Projectile projectile;

    // 발사 간격을 밀리초 단위로 나타냅니다. 예를 들어, 100이면 1초에 10번 발사합니다.
    public float msBetweenShots = 10;

    // 총알이 나가는 속도를 나타냅니다.
    public float muzzleVelocity = 35;

    // 다음 발사 가능 시간을 나타냅니다.
    float nextShotTime;

    // 발사를 처리하는 메소드입니다.
    public void Shoot()
    {
        // 현재 시간이 다음 발사 가능 시간보다 클 경우에만 발사를 처리합니다.
        if (Time.time > nextShotTime)
        {
            // 다음 발사 가능 시간을 현재 시간에 발사 간격을 더한 값으로 설정합니다.
            nextShotTime = Time.time + msBetweenShots / 1000;

            // 새로운 프로젝타일을 생성하고, 총구의 위치와 방향으로 설정합니다.
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            
            // 프로젝타일의 속도를 설정합니다.
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
