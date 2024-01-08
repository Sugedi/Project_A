using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    // 발사체가 충돌할 수 있는 레이어를 정의합니다.
    public LayerMask collisionMask;
	float speed = 10; // 발사 속도
	float damage = 1; // 데미지

    // 발사체의 속도를 설정하는 메서드입니다. 외부에서 호출하여 속도를 변경할 수 있습니다.
    public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
	
	void Update () {

        // 이동 거리를 계산합니다. 속도에 프레임 당 시간을 곱하여 얻습니다
        float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance); // 충돌 검사를 수행합니다.

        // 발사체를 전방으로 moveDistance만큼 이동시킵니다
        transform.Translate (Vector3.forward * moveDistance); 
	}

    // 발사체의 이동 경로상에 충돌이 있는지 확인하는 메서드입니다.
    void CheckCollisions (float moveDistance)
	{
        // 현재 위치에서 전방으로 레이를 생성합니다.
        Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit; // 레이캐스트에 의해 충돌된 정보를 저장할 변수입니다.

        // 레이캐스트가 충돌을 감지하면 OnHitObject 메서드를 호출합니다.
        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
		{
			OnHitObject(hit); // 충돌 처리 메서드를 호출합니다
        }
	}

	// 충돌한 객체에 대한 처리를 하는 메서드
	void OnHitObject(RaycastHit hit)
	{
        // 충돌한 객체에서 IDamageable 인터페이스를 구현하고 있는 컴포넌트를 가져옵니다.
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
		if (damageableObject != null)
		{
            // 데미지 처리 메서드를 호출합니다.
            damageableObject.TakeHit(damage, hit);
		}
        // 발사체 게임 오브젝트를 파괴합니다.
        GameObject.Destroy(gameObject);
	}
}
