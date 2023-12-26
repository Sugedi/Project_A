using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ����ϴ� ���Բ�

// Patrol(); �͸����� �ۺ����� �޾ƿͼ� ��ǥ���� ������ �����̰� �� ��
// ��ä�� ������ ��
// �׸��� ������ ���� ���̺� ���� ü�� ���ݷ� ��� csv�� �ҷ��ͼ�
// ����� �� ���� �ϴ� �ɷ� CombatManager �ϳ� ����� ���� ��
// ���� ���̶� �����ؼ� �±� ���ϰ� ���� �ù� �ѹ� �غ� ��
// ���� ������ ���� �� ��� �͵� �����޶�� �� ��
// �Ĳ��ϰ� �ּ� �а�, �ñ��� �� ������ ������ ��
// �� �� ì�ܸ԰� �˹��� ��

// �߽�, ê����Ƽ �����밡���ϱ� ��������θ� �� ��, Ʋ ��� �����δ� ������ ���� ��
// ��� ����� ��� �� ��



// ������� �غ���
/*
[���� �ܰ�]
1. �����Ÿ� ���� �÷��̾ ������ �÷��̾ ���� �� ����
2. + ���� ������ ���������� ������
3. ��ä�� �ν� ���� �� �÷��̾ ������ �÷��̾ ���� �� ����
4. ��ä�� ���� ǥ��(�� �̰� ���� ������ ���鿡 �޸� �ɵ�
5. (�ð� ������) ���ݷ��� ��������, ���̷��� ��� �ֺ� ���͸� �θ��� ����

[1�� ��ǥ ���� AI ����Ŭ]
1. ������ �ִ� ���Ͱ� �÷��̾� �±׸� ���� ����� ��� �Ÿ� ����� ������Ʈ
2. 


[�ϼ� �ܰ� AI ����Ŭ]
1. ���� ������ ����
2. �÷��̾ ���� ���� �� ���� �� -> �߰�
3. �߰� �� �÷��̾ �߰� ����(��ä�� ������)�� ����� -> �ش� ��ġ�� ���� �� �θ��� 2��
4. 2�� �θ��� ��, �� �ڸ��� �����Ͽ� ���� ���� �����

*/
public class MonsterAI : LivingEntity
{
    public float patrolDistance = 10f;       // ���� �Ÿ�
    public float detectionRange = 5f;       // �÷��̾� ���� ����
    public float chaseRange = 6f;          // �߰� ����
    public float idleDuration = 2f;         // ���� �� �θ����ϴ� �ð�


    private Transform player;               // �÷��̾��� ��ġ
    private NavMeshAgent navMeshAgent;      // NavMeshAgent ������Ʈ ����
    private Vector3 originalPosition;       // ������ �ʱ� ��ġ
    private bool isChasing = false;         // �߰� �� ����

    public float runSpeed = 10f;  // ���� �̵� �ӵ�
    public float damage = 30f;  // ���ݷ�
    public float patrolSpeed = 3f; // ���� ���ƴٴϴ� �Ÿ�(Patrol ������ ��)

    void Start()
    {
        // �÷��̾� �±� �� ���� ã�ƿ�
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //navMesh�� ���� ���ؼ� �ν�����â ������Ʈ �ҷ����� ��
        navMeshAgent = GetComponent<NavMeshAgent>();

        // ������ �ʱ� ��ġ ����(���߿� �߰� �ߴ� �� ���ƿ��� ��ġ ��)
        originalPosition = transform.position;

            
        // �ʱ� ���� ����
        Patrol();
    }

    void Update()
    {
        // ���� ������ Ȥ�� ���� �������� ������ �ٽ� �Դٰ����ϴ� ��
        if (transform.position.x == originalPosition.x || transform.position.x == patrolDistance)
        {
            Patrol();
        }

        if (!isChasing)
        {
            // ���� �� �÷��̾� ����
            if (IsPlayerInRange(detectionRange))
            {
                StartChase();
            }
        }
        else
        {
            // �߰� ���� ���
            if (IsPlayerInRange(chaseRange))
            {
                // �÷��̾ �߰� ���� ���� ������ �߰� ���
                ChasePlayer();
            }
            else
            {
                // �÷��̾ �߰� ������ ����� ���� �ð� ���� ���� �� ���� �簳
                StopChase();
            }
        }
    }

    bool IsPlayerInRange(float range)
    {
        // �÷��̾�� ���� ������ �Ÿ��� �����ϴ� �� -> 1. ���� ����, 2. �߰� ���� ���� �� ���� ��� ���Ǵ� ����
        return Vector3.Distance(transform.position, player.position) < range;
    }

    void StartChase()
    {
        isChasing = true;

        // �÷��̾� �߰� ����
        navMeshAgent.SetDestination(player.position);
    }

    void ChasePlayer()
    {
        // �÷��̾� ��� �߰� (�̹� �߰� ���ε�, ���� ���� ���� ��� ���󰣴�.)
        navMeshAgent.SetDestination(player.position);
    }

    void StopChase()
    {
        // �߰� �� ���� �ʱ�ȭ
        isChasing = false;

        // �� ģ�� ��Ȯ�� �� ������� �𸣰���. �׳� ������ �������� / ���� �� �ʱ� ��ġ�� ��������
        navMeshAgent.ResetPath();

        // 2�ʰ� �θ����Ÿ��� - �θ����� ���� �ð� �൵ �ɵ�. ���� ���� �� �ʿ� ����̱� �ϳ�
        Invoke("ResumePatrol", idleDuration);
    }


    // ���� ����
    void Patrol()
    {

        // ���� ���� �̰� �Դ� ���� ���ڷ� �س��µ�, ��ǥ���� �ۺ����� �޾ƿͼ� ���� ��ġ �ݺ� ��Ű�� �ɵ�
        // ���� ���� �͸������� �Ǵµ�, �ڳ� ������ ���� ���� �𸣰ڳ�. ���� ���� �����غ����ҵ�

        if (transform.position.x == originalPosition.x)
        {
            navMeshAgent.SetDestination(new Vector3(transform.position.x + patrolDistance, transform.position.y, transform.position.z));
        }

        if (transform.position.x == patrolDistance)
        {
            navMeshAgent.SetDestination(new Vector3(transform.position.x - patrolDistance, transform.position.y, transform.position.z));
        }

    }


    private void Awake()
    {
        // ������Ʈ�� ��������
       // agent = GetComponent<NavMeshAgent>(); // Zombie�� NavMeshAgent ������Ʈ ��������
       // animator = GetComponent<Animator>(); // Zombie�� Animator ������Ʈ ��������
       // audioPlayer = GetComponent<AudioSource>(); // Zombie�� AudioSource ������Ʈ ��������
       // skinRenderer = GetComponentInChildren<Renderer>();  // Zombie�� �ڽ� ������Ʈ�� �߿��� Rederer ������Ʈ�� ���� ������Ʈ�� Reneerer Ÿ������ ��������

        // 
       // attackDistance = Vector3.Distance(transform.position, new Vector3(attackRoot.position.x, transform.position.y, attackRoot.position.z)) + attackRadius;

      //  attackDistance += agent.radius;

       // agent.stoppingDistance = attackDistance;
       // agent.speed = patrolSpeed;
    }


    // �� AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    public void Setup(float health, float damage,
        float runSpeed, float patrolSpeed, Color skinColor)
    {
        // ü�� ����
        startingHealth = health; // �ʱ� ���� ü��
        this.health = health;  // ü��

        // ����޽� ������Ʈ�� �̵� �ӵ� ����
        this.runSpeed = runSpeed;
        this.patrolSpeed = patrolSpeed;

        this.damage = damage;  // ���ݷ�

       // agent.speed = patrolSpeed; // ������ ����� patrolSpeed�� �ٽ� ���� //�̰� ���� ��ȣ���� ���ľ��ҰŰ�����.. ����
    }


}



