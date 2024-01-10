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
1. �����Ÿ� ���� �÷��̾ ������ �÷��̾ ���� �� ����                         ***** ���� �Ϸ�
2. + ���� ������ ���������� ������                                                   ***** ���� �Ϸ�
3. ��ä�� �ν� ���� �� �÷��̾ ������ �÷��̾ ���� �� ����                   ***** ���� �Ϸ�
4. ��ä�� ���� ǥ��(�� �̰� ���� ������ ���鿡 �޸� �ɵ�
5. ��ä�� �þ߰� ��ĥ, �߽߰� ���������� ����
6. �ν��� ���Ŀ��� ��� ����
7. �� ���̸� �ΰ�� �������� ���ϵ��� �ؾ� ��.                                       ***** ���� �Ϸ�
8. ���� �߿� �� ���̷� ������, ���� ���Ƽ� �÷��̾� ����      
9. ���� �������� ���� ����                                            -> �̰� ����̿� �ִٰ� ���̴����� �׷���
10. ��ä�� �þ߰� ��ĥ, �߽߰� ���������� ����
11. (�ð� ������) ���ݷ��� ��������, ���̷��� ��� �ֺ� ���͸� �θ��� ����


[1�� ��ǥ ���� AI ����Ŭ]
1. ������ �ִ� ���Ͱ� �÷��̾� �±׸� ���� ����� ��� �Ÿ� ����� ������Ʈ
2. 


[�ϼ� �ܰ� AI ����Ŭ]
1. ���� ������ ����
2. �÷��̾ ���� ���� �� ���� �� -> �߰�
3. �߰� �� �÷��̾ �߰� ����(��ä�� ������)�� ����� -> �ش� ��ġ�� ���� �� �θ��� 2��
4. 2�� �θ��� ��, �� �ڸ��� �����Ͽ� ���� ���� �����

*/
public class MonsterAI : MonoBehaviour
{

    public float detectionRange = 5f;       // �÷��̾� ���� ����
    public float chaseRange = 6f;           // �߰� ����
    public float idleDuration = 2f;         // ���� �� �θ����ϴ� �ð�
    public Color _color;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    // ����ũ 2��
    public LayerMask targetMask, obstacleMask;

    public Vector3 destinationPos;          // ���� ������ ��ǥ�� ���ͷ� �޾ƿ�(�Ƹ� ž�ٿ��̸� y�� 0���� ���� �ɵ�)
    // [���� �߻�] player�� enemy�� �����ؼ� �������, �Ѵ� ���� ������ٵ� �ھҴµ�, enemy y ��ǥ�� 1.583333���� �����Ǵ� ���� ->
    //          1.5�� ���ߵ���. ���� �⺻ ���̸� 0���� �ϰ� int ������ �ö󰡰� �ϸ� ���� ��

    public Transform player;               // �÷��̾��� ��ġ
    private NavMeshAgent navMeshAgent;      // NavMeshAgent ������Ʈ ����
    private Vector3 originalPosition;       // ������ �ʱ� ��ġ
    private bool isChasing = false;         // �߰� �� ����

    public bool playerCheck;

    // public ViewEffect viewEffect; // viewEffect�� �����. �ٸ� �Ŷ� ���ĵ� �� �κ� ���ֱ�

    void Start()
    {
        // �÷��̾� �±� �� ���� ã�ƿ�
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //navMesh�� ���� ���ؼ� �ν�����â ������Ʈ �ҷ����� ��
        navMeshAgent = GetComponent<NavMeshAgent>();

        // ������ �ʱ� ��ġ ����(���߿� �߰� �ߴ� �� ���ƿ��� ��ġ ��)
        originalPosition = transform.position;

        // 0.2�� �������� �ڷ�ƾ ȣ��
        StartCoroutine(FindTargetsWithDelay(0.2f));

        // �ʱ� ���� ����
        Patrol();

    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Sight();
        }
    }

    public void Sight()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length > 0)
        {
            Transform target = targetsInViewRadius[0].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float targetAngle = Vector3.Angle(dirToTarget, transform.forward);

            if (targetAngle < viewAngle * 0.5f)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    playerCheck = true;
                    //DrawMonView viewColor = GameObject.Find("Enemy").GetComponent<DrawMonView>();
                    //viewColor._color = Color.red;
                    //���� ��� �ٲ��ְ� �;��µ� �ϴ� ���� ���� �׳� ���� �������ڸ��շ��餷�Ǥ���������
                }

            }
        }
    }


    void Update()
    {
        // viewEffect.SetAimDirection(transform.forward); // viewEffect�� �����. �ٸ� �Ŷ� ���ĵ� �� �κ� ���ֱ�
        // viewEffect.SetOrigin(transform.position); // viewEffect�� �����. �ٸ� �Ŷ� ���ĵ� �� �κ� ���ֱ�
        // ���� ������ Ȥ�� ���� �������� ������ �ٽ� �Դٰ����ϴ� ��
        if (transform.position == originalPosition || transform.position == destinationPos)
        {
            Patrol();

        }

        // ���⸦ ��� ��ġ�� ����
        if (!isChasing)
        {
            // ���� �� �÷��̾� ����
            if (playerCheck == true)
            {
                StartChase();
            }
        }
        else
        {
            // �߰� ���� ���
            if (Vector3.Distance(transform.position, player.position) < chaseRange)
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

    // ���� ����
    void Patrol()
    {

        // ���� ���� �̰� �Դ� ���� ���ڷ� �س��µ�, ��ǥ���� �ۺ����� �޾ƿͼ� ���� ��ġ �ݺ� ��Ű�� �ɵ�
        // ���� ���� �͸������� �Ǵµ�, �ڳ� ������ ���� ���� �𸣰ڳ�. ���� ���� �����غ����ҵ�
        // �׺�޽� ��ǥ �޾ƿ� �� ����.

        // ó�� ��ġ�� �Ȱ��� ��� -> ���� �������� ����


        if (transform.position == originalPosition)
        {
            navMeshAgent.SetDestination(destinationPos);
        }

        if (transform.position == destinationPos)
        {
            navMeshAgent.SetDestination(originalPosition);
        }

    }
    /*
    public bool IsPlayerInRange(bool check)
    {
        // �÷��̾�� ���� ������ �Ÿ��� �����ϴ� �� -> 1. ���� ����, 2. �߰� ���� ���� �� ���� ��� ���Ǵ� ����
        //return Vector3.Distance(transform.position, player.position) < range;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length > 0)
        {
            Transform target = targetsInViewRadius[0].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float targetAngle = Vector3.Angle(dirToTarget, transform.position);

            if (targetAngle < viewAngle * 0.5f)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    return playerCheck = true;
                }
            }

            else
            {
                return playerCheck = false;
            }
            return false;

        }

        else
        {
            return playerCheck = false;
        }
    }
        */
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
        playerCheck = false;

        // �׳� ������ ����
        navMeshAgent.ResetPath();

        // 2�ʰ� �θ����Ÿ��� - �θ����� ���� �ð� �൵ �ɵ�. ���� ���� �� �ʿ� ����̱� �ϳ�
        Invoke("ResumePatrol", idleDuration);
    }

    // ���� �簳
    void ResumePatrol()
    {
        // ���� ��ġ�� ���ư���
        navMeshAgent.SetDestination(originalPosition);

    }

    // y�� ���Ϸ� ���� 3���� ���� ���ͷ� ��ȯ�Ѵ�.
    // ������ ������ ��¦ �ٸ��� ����. ����� ����.
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}



