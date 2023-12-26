using System;
using UnityEngine;


// 플레이어 + 적 AI 등등 모든 '생명체'인 오브젝트들이 공유하는 기반들을 정리한 클래스
//플레이어, 적 AI 등등 생명체 오브젝트들은 각각의 기능들을 구현하려면
//이 클래스를 상속 받아 이 위에다가 자신만의 기능들을 덧붙이기만 하면 된다

//모든 생명체(플레이어 + 적AI)는 데미지를 입을 수 있으므로 IDamageable을 상속 받는다
//IDamageable 인터페이스의 순수 가상 함수인 ApplyDamage 함수를 반드시 구현해야 한다



public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f; //시작체력을 100으로 설정
    public float health { get; protected set; } //현재 체력을 관리
    public bool dead { get; protected set; } //죽었는지의 여부를 냄

    public event Action OnDeath; //죽었을때 액션
                                 // Action이란 C#에서 미리 만들어져 제공하는 리턴 타입과
                                 // 매개변수가 없는 함수(*void F()같은*)의 등록을 위한 델리게이트

    private const float minTimeBetDamaged = 0.1f; //공격과 공격사이의 최소 대기 시간
    private float lastDamagedTime;  //최근에 공격을 당한 시점

    protected bool IsInvulnerabe  //무적.공격 받을 수 있는 상태인지 여부
                                  // true면 무적. false면 공격을 받을 수 있는 상태
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged) return false; //두개를 더한 시간내에 공격을 당한 경우네는 무시할 것이다.

            return true;
        }
    }




    // 생명체 상태 리셋
    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;




    }
    //IDamgagealbe의 자식으로서 반드시 구현해야 할 함수
    public virtual bool ApplyDamage(DamageMessage damageMessage) //데미지 받기
    {
        if (IsInvulnerabe || damageMessage.damager == gameObject || dead) return false; //데미지를 받지 않았다면 false 리턴

        lastDamagedTime = Time.time; //lastDamagedTime을 현재 시간으로 업데이트
        health -= damageMessage.amount; //내 heath를 데미지 양 받은것 만큼 차감

        if (health <= 0) Die(); // 체력이 0이 되거나 이하로 떨어지면 Die() 함수 호출

        return true;
    }




    //자기 자신의 체력 회복
    public virtual void RestoreHealth(float newHealth) //추가할 만큼의 체력을 인수로 받음
    {
        if (dead) return; //이미 죽었다면 실행 x

        health += newHealth; //체력 회복
    }



    //사망 상태로 처리
    public virtual void Die()
    {
        if (OnDeath != null) OnDeath(); //OnDeath 이벤트에 최소 하나 이상의 함수가 등록 되어 있다면 OnDeath()함수 실행

        dead = true; //사망한 상태로 변경
    }
}