using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float baseDamage; // �Ѿ��� �⺻ ���ݷ�
    public float damage; // �Ѿ��� ���ݷ�  
    public float lifeTime; // �Ѿ��� �ִ� ���� �ð� (��Ÿ� ����)
    private float lifeTimer; // ��������� ���� �ð��� �����ϴ� Ÿ�̸�
    private ObjectPool<GameObject> pool;
    
    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }
    
    void OnEnable()
    {
        // �Ѿ��� Ȱ��ȭ�� ������ �⺻ �������� �ʱ�ȭ�մϴ�.
        damage = baseDamage;
        lifeTimer = lifeTime; // Ÿ�̸Ӹ� �ִ� ���� �ð����� �ʱ�ȭ�մϴ�.
    }

    // ������Ʈ Ǯ�� ��ȯ�ϴ� �޼��带 �߰��մϴ�.
    
    public void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(gameObject);
        }
        else
        {
            // Ǯ�� �����Ǿ� ���� �ʴٸ�, �⺻���� Destroy�� ȣ���մϴ�.
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // �� �����Ӹ��� Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0) // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� �Ѿ��� �ı��մϴ�.
        {
            ReturnToPool();
        }
    }

    // �浹 �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            ReturnToPool();
        }
    }

    // Ʈ���� �浹 �� ȣ��Ǵ� �޼���
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall") // ���Ÿ� �����̸鼭 ���� �浹�� ���
        {
            ReturnToPool(); // �Ѿ� ��� ����
        }
    }
}