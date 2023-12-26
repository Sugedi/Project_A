using UnityEngine;

// UI ���� �ڵ�

// �÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LivingEntity
{
    private Animator animator;
    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip hitClip; // �ǰ� �Ҹ�


    private void Awake()
    {
        // ����� ������Ʈ�� ��������
        playerAudioPlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();
        UpdateUI();
    }

    // ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ���� (ü�� ����)
        base.RestoreHealth(newHealth);
        // ü�� ����
        UpdateUI();
    }

    private void UpdateUI()
    {
        // UIManager.Instance.UpdateHealthText(dead ? 0f : health); //�Ŀ� �ּ� Ǯ��
    }

    // ������ ó��
    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

     //   EffectManager.Instance.PlayHitEffect(damageMessage.hitPoint, damageMessage.hitNormal, transform, EffectManager.EffectType.Flesh); //�Ŀ� �ּ� Ǯ��
        playerAudioPlayer.PlayOneShot(hitClip);

        // LivingEntity�� OnDamage() ����(������ ����)
        // ���ŵ� ü���� ü�� �����̴��� �ݿ�
        UpdateUI();
        return true;
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();

        // ü�� �����̴� ��Ȱ��ȭ
        UpdateUI();
        // ����� ���
        playerAudioPlayer.PlayOneShot(deathClip);
        // �ִϸ������� Die Ʈ���Ÿ� �ߵ����� ��� �ִϸ��̼� ���
        animator.SetTrigger("Die");
    }
}