using System;
using UnityEngine;


// �÷��̾� + �� AI ��� ��� '����ü'�� ������Ʈ���� �����ϴ� ��ݵ��� ������ Ŭ����
//�÷��̾�, �� AI ��� ����ü ������Ʈ���� ������ ��ɵ��� �����Ϸ���
//�� Ŭ������ ��� �޾� �� �����ٰ� �ڽŸ��� ��ɵ��� �����̱⸸ �ϸ� �ȴ�

//��� ����ü(�÷��̾� + ��AI)�� �������� ���� �� �����Ƿ� IDamageable�� ��� �޴´�
//IDamageable �������̽��� ���� ���� �Լ��� ApplyDamage �Լ��� �ݵ�� �����ؾ� �Ѵ�



public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f; //����ü���� 100���� ����
    public float health { get; protected set; } //���� ü���� ����
    public bool dead { get; protected set; } //�׾������� ���θ� ��

    public event Action OnDeath; //�׾����� �׼�
                                 // Action�̶� C#���� �̸� ������� �����ϴ� ���� Ÿ�԰�
                                 // �Ű������� ���� �Լ�(*void F()����*)�� ����� ���� ��������Ʈ

    private const float minTimeBetDamaged = 0.1f; //���ݰ� ���ݻ����� �ּ� ��� �ð�
    private float lastDamagedTime;  //�ֱٿ� ������ ���� ����

    protected bool IsInvulnerabe  //����.���� ���� �� �ִ� �������� ����
                                  // true�� ����. false�� ������ ���� �� �ִ� ����
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged) return false; //�ΰ��� ���� �ð����� ������ ���� ���״� ������ ���̴�.

            return true;
        }
    }




    // ����ü ���� ����
    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;




    }
    //IDamgagealbe�� �ڽ����μ� �ݵ�� �����ؾ� �� �Լ�
    public virtual bool ApplyDamage(DamageMessage damageMessage) //������ �ޱ�
    {
        if (IsInvulnerabe || damageMessage.damager == gameObject || dead) return false; //�������� ���� �ʾҴٸ� false ����

        lastDamagedTime = Time.time; //lastDamagedTime�� ���� �ð����� ������Ʈ
        health -= damageMessage.amount; //�� heath�� ������ �� ������ ��ŭ ����

        if (health <= 0) Die(); // ü���� 0�� �ǰų� ���Ϸ� �������� Die() �Լ� ȣ��

        return true;
    }




    //�ڱ� �ڽ��� ü�� ȸ��
    public virtual void RestoreHealth(float newHealth) //�߰��� ��ŭ�� ü���� �μ��� ����
    {
        if (dead) return; //�̹� �׾��ٸ� ���� x

        health += newHealth; //ü�� ȸ��
    }



    //��� ���·� ó��
    public virtual void Die()
    {
        if (OnDeath != null) OnDeath(); //OnDeath �̺�Ʈ�� �ּ� �ϳ� �̻��� �Լ��� ��� �Ǿ� �ִٸ� OnDeath()�Լ� ����

        dead = true; //����� ���·� ����
    }
}