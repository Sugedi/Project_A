using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 사랑하는 누님께

// Patrol(); 와리가리 퍼블릭으로 받아와서 좌표값만 넣으면 움직이게 할 것
// 부채꼴 구현할 것
// 그리고 전투용 몬스터 테이블 만들어서 체력 공격력 등등 csv로 불러와서
// 닿았을 때 공격 하는 걸로 CombatManager 하나 만들어 놓을 것
// 범수 형이랑 상의해서 태그 정하고 전투 시뮬 한번 해볼 것
// 범수 형한테 몬스터 총 쏘는 것도 만들어달라고 할 것
// 꼼꼼하게 주석 읽고, 궁금한 점 있으면 연락할 것
// 밥 잘 챙겨먹고 알바할 것

// 추신, 챗지피티 개빡대가리니까 참고용으로만 쓸 것, 틀 잡는 용으로는 나쁘지 않을 듯
// 기능 물어보면 대답 잘 함



// 설계부터 해보자
/*
[구현 단계]
1. 인지거리 내에 플레이어가 들어오면 플레이어를 추적 및 공격                         ***** 구현 완료
2. + 일정 구간을 지속적으로 움직임                                                   ***** 구현 완료
3. 부채꼴 인식 범위 내 플레이어가 들어오면 플레이어를 추적 및 공격                   ***** 구현 완료
4. 부채꼴 범위 표시(걍 이건 몬스터 프리팹 정면에 달면 될듯
5. 부채꼴 시야각 색칠, 발견시 빨간색으로 변경
6. 인식한 이후에는 사격 시작
7. 벽 사이를 두고는 인지하지 못하도록 해야 함.                                       ***** 구현 완료
8. 추적 중에 벽 사이로 숨으면, 벽을 돌아서 플레이어 추적      
9. 순찰 목적지를 다중 설정                                            -> 이거 고양이에 있다고 정미누나가 그랬음
10. 부채꼴 시야각 색칠, 발견시 빨간색으로 변경
11. (시간 남으면) 공격력은 약하지만, 사이렌을 울려 주변 몬스터를 부르는 몬스터


[1차 목표 세부 AI 사이클]
1. 가만히 있는 몬스터가 플레이어 태그를 가진 사람과 계속 거리 계산을 업데이트
2. 


[완성 단계 AI 사이클]
1. 일정 범위를 순찰
2. 플레이어가 인지 범위 내 진입 시 -> 추격
3. 추격 중 플레이어가 추격 범위(부채꼴 반지름)를 벗어나면 -> 해당 위치에 정지 및 두리번 2초
4. 2초 두리번 후, 제 자리로 복귀하여 범위 순찰 재시작

*/
public class MonsterAI : MonoBehaviour
{

    public float detectionRange = 5f;       // 플레이어 감지 범위
    public float chaseRange = 6f;           // 추격 범위
    public float idleDuration = 2f;         // 정지 및 두리번하는 시간
    public Color _color;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    // 마스크 2종
    public LayerMask targetMask, obstacleMask;

    public Vector3 destinationPos;          // 순찰 목적지 좌표를 벡터로 받아옴(아마 탑다운이면 y는 0으로 놓게 될듯)
    // [문제 발생] player랑 enemy를 복붙해서 만들었고, 둘다 같은 리지드바디 박았는데, enemy y 좌표가 1.583333으로 설정되는 버그 ->
    //          1.5로 맞추든지. 레벨 기본 높이를 0으로 하고 int 단위로 올라가게 하면 좋을 듯

    public Transform player;               // 플레이어의 위치
    private NavMeshAgent navMeshAgent;      // NavMeshAgent 컴포넌트 참조
    private Vector3 originalPosition;       // 몬스터의 초기 위치
    private bool isChasing = false;         // 추격 중 여부

    public bool playerCheck;

    // public ViewEffect viewEffect; // viewEffect와 연결됨. 다른 거랑 합쳐도 이 부분 써주기

    void Start()
    {
        // 플레이어 태그 단 놈을 찾아와
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //navMesh를 쓰기 위해서 인스펙터창 컴포넌트 불러오는 거
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 몬스터의 초기 위치 저장(나중에 추격 중단 후 돌아오는 위치 용)
        originalPosition = transform.position;

        // 0.2초 간격으로 코루틴 호출
        StartCoroutine(FindTargetsWithDelay(0.2f));

        // 초기 순찰 시작
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

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    playerCheck = true;
                    //DrawMonView viewColor = GameObject.Find("Enemy").GetComponent<DrawMonView>();
                    //viewColor._color = Color.red;
                    //색을 계속 바꿔주고 싶었는데 일단 보류 ㅈ됨 그냥 ㅈ됨 ㅈ도믿자림넝래면ㅇ피ㅏㄴ어리만어루
                }

            }
        }
    }


    void Update()
    {
        // viewEffect.SetAimDirection(transform.forward); // viewEffect와 연결됨. 다른 거랑 합쳐도 이 부분 써주기
        // viewEffect.SetOrigin(transform.position); // viewEffect와 연결됨. 다른 거랑 합쳐도 이 부분 써주기
        // 순찰 시작점 혹은 순찰 목적지에 있으면 다시 왔다갔다하는 거
        if (transform.position == originalPosition || transform.position == destinationPos)
        {
            Patrol();

        }

        // 여기를 뜯어 고치자 ㅅㅂ
        if (!isChasing)
        {
            // 순찰 중 플레이어 감지
            if (playerCheck == true)
            {
                StartChase();
            }
        }
        else
        {
            // 추격 중인 경우
            if (Vector3.Distance(transform.position, player.position) < chaseRange)
            {
                // 플레이어가 추격 범위 내에 있으면 추격 계속
                ChasePlayer();
            }
            else
            {
                // 플레이어가 추격 범위를 벗어나면 일정 시간 동안 정지 후 순찰 재개
                StopChase();
            }
        }
    }

    // 순찰 시작
    void Patrol()
    {

        // 내가 지금 이걸 왔다 갔다 숫자로 해놨는데, 좌표값을 퍼블릭으로 받아와서 일정 위치 반복 시키면 될듯
        // 지금 직선 와리가리는 되는데, 코너 주행은 어케 할지 모르겠네. 맵을 보고 실험해봐야할듯
        // 네비메쉬 좌표 받아올 수 있음.

        // 처음 위치와 똑같을 경우 -> 순찰 목적지로 보냄


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
        // 플레이어와 몬스터 사이의 거리를 측정하는 거 -> 1. 감지 범위, 2. 추격 포기 범위 두 개에 모두 사용되는 거임
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

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
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

        // 플레이어 추격 시작
        navMeshAgent.SetDestination(player.position);
    }

    void ChasePlayer()
    {
        // 플레이어 계속 추격 (이미 추격 중인데, 포기 범위 내면 계속 따라간다.)
        navMeshAgent.SetDestination(player.position);
    }

    void StopChase()
    {
        // 추격 중 여부 초기화
        isChasing = false;
        playerCheck = false;

        // 그냥 목적지 삭제
        navMeshAgent.ResetPath();

        // 2초간 두리번거리기 - 두리번은 고정 시간 줘도 될듯. 변수 굳이 줄 필요 없어보이긴 하네
        Invoke("ResumePatrol", idleDuration);
    }

    // 순찰 재개
    void ResumePatrol()
    {
        // 시작 위치로 돌아가기
        navMeshAgent.SetDestination(originalPosition);

    }

    // y축 오일러 각을 3차원 방향 벡터로 변환한다.
    // 원본과 구현이 살짝 다름에 주의. 결과는 같다.
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}



