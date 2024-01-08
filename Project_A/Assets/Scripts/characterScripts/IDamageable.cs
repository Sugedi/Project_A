using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IDamageable 인터페이스는 데미지를 입을 수 있는 오브젝트를 정의합니다.
public interface IDamageable
{
    // TakeHit 메서드는 데미지와 충돌 정보를 받아와서 처리합니다.
    void TakeHit(float damage, RaycastHit hit);

}
