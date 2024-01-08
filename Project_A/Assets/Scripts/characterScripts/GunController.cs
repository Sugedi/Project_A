using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    // 무기를 들고 있을 위치를 나타내는 Transform 타입의 변수입니다.
    public Transform weaponPoint;

    // 시작할 때 플레이어에게 주어질 기본 총을 나타내는 변수입니다.
    public Gun startingGun;

    // 현재 장착된 총을 나타내는 변수입니다.
    Gun equippedGun;

	void Start() {

        // 만약 시작 총이 할당되어 있다면, 그 총을 장착합니다.
        if (startingGun != null) {
			EquipGun(startingGun);
		}
	}

    // 총을 장착하는 메서드입니다. Gun 타입의 인자를 받습니다.
    public void EquipGun(Gun gunToEquip) {

        // 만약 이미 총이 장착되어 있다면, 그 오브젝트를 파괴합니다.
        if (equippedGun != null) {
			Destroy(equippedGun.gameObject);
		}

        // 새 총을 weaponPoint 위치와 회전으로 생성하고, Gun 타입으로 캐스팅합니다.
        equippedGun = Instantiate (gunToEquip, weaponPoint.position, weaponPoint.rotation) as Gun;

        // 생성된 총의 부모를 weaponPoint로 설정하여, 캐릭터가 움직일 때 총도 함께 움직이게 합니다.
        equippedGun.transform.parent = weaponPoint;
	}

    // 총을 발사하는 메서드입니다.
    public void Shoot()
	{
        // 장착된 총이 있다면, 그 총의 Shoot 메서드를 호출하여 발사합니다.
        if (equippedGun != null)
		{
			equippedGun.Shoot();
		}
	}
}