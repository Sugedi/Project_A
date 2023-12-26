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
1. 인지거리 내에 플레이어가 들어오면 플레이어를 추적 및 공격
2. + 일정 구간을 지속적으로 움직임
3. 부채꼴 인식 범위 내 플레이어가 들어오면 플레이어를 추적 및 공격
4. 부채꼴 범위 표시(걍 이건 몬스터 프리팹 정면에 달면 될듯
5. (시간 남으면) 공격력은 약하지만, 사이렌을 울려 주변 몬스터를 부르는 몬스터

[1차 목표 세부 AI 사이클]
1. 가만히 있는 몬스터가 플레이어 태그를 가진 사람과 계속 거리 계산을 업데이트
2. 


[완성 단계 AI 사이클]
1. 일정 범위를 순찰
2. 플레이어가 인지 범위 내 진입 시 -> 추격
3. 추격 중 플레이어가 추격 범위(부채꼴 반지름)를 벗어나면 -> 해당 위치에 정지 및 두리번 2초
4. 2초 두리번 후, 제 자리로 복귀하여 범위 순찰 재시작

*/
public class MonsterAI : LivingEntity
{
    public float patrolDistance = 10f;       // 순찰 거리
    public float detectionRange = 5f;       // 플레이어 감지 범위
    public float chaseRange = 6f;          // 추격 범위
    public float idleDuration = 2f;         // 정지 및 두리번하는 시간


    private Transform player;               // 플레이어의 위치
    private NavMeshAgent navMeshAgent;      // NavMeshAgent 컴포넌트 참조
    private Vector3 originalPosition;       // 몬스터의 초기 위치
    private bool isChasing = false;         // 추격 중 여부

    public float runSpeed = 10f;  // 좀비 이동 속도
    public float damage = 30f;  // 공격령
    public float patrolSpeed = 3f; // 좀비가 돌아다니는 거리(Patrol 상태일 때)

    void Start()
    {
        // 플레이어 태그 단 놈을 찾아와
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //navMesh를 쓰기 위해서 인스펙터창 컴포넌트 불러오는 거
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 몬스터의 초기 위치 저장(나중에 추격 중단 후 돌아오는 위치 용)
        originalPosition = transform.position;

            
        // 초기 순찰 시작
        Patrol();
    }

    void Update()
    {
        // 순찰 시작점 혹은 순찰 목적지에 있으면 다시 왔다갔다하는 거
        if (transform.position.x == originalPosition.x || transform.position.x == patrolDistance)
        {
            Patrol();
        }

        if (!isChasing)
        {
            // 순찰 중 플레이어 감지
            if (IsPlayerInRange(detectionRange))
            {
                StartChase();
            }
        }
        else
        {
            // 추격 중인 경우
            if (IsPlayerInRange(chaseRange))
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

    bool IsPlayerInRange(float range)
    {
        // 플레이어와 몬스터 사이의 거리를 측정하는 거 -> 1. 감지 범위, 2. 추격 포기 범위 두 개에 모두 사용되는 거임
        return Vector3.Distance(transform.position, player.position) < range;
    }

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

        // 이 친구 정확히 뭔 기능인지 모르겠음. 그냥 목적지 삭제인지 / 삭제 후 초기 위치로 복귀인지
        navMeshAgent.ResetPath();

        // 2초간 두리번거리기 - 두리번은 고정 시간 줘도 될듯. 변수 굳이 줄 필요 없어보이긴 하네
        Invoke("ResumePatrol", idleDuration);
    }


    // 순찰 시작
    void Patrol()
    {

        // 내가 지금 이걸 왔다 갔다 숫자로 해놨는데, 좌표값을 퍼블릭으로 받아와서 일정 위치 반복 시키면 될듯
        // 지금 직선 와리가리는 되는데, 코너 주행은 어케 할지 모르겠네. 맵을 보고 실험해봐야할듯

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
        // 컴포넌트들 가져오기
       // agent = GetComponent<NavMeshAgent>(); // Zombie의 NavMeshAgent 컴포넌트 가져오기
       // animator = GetComponent<Animator>(); // Zombie의 Animator 컴포넌트 가져오기
       // audioPlayer = GetComponent<AudioSource>(); // Zombie의 AudioSource 컴포넌트 가져오기
       // skinRenderer = GetComponentInChildren<Renderer>();  // Zombie의 자식 오브젝트들 중에서 Rederer 컴포넌트를 가진 오브젝트를 Reneerer 타입으로 가져오기

        // 
       // attackDistance = Vector3.Distance(transform.position, new Vector3(attackRoot.position.x, transform.position.y, attackRoot.position.z)) + attackRadius;

      //  attackDistance += agent.radius;

       // agent.stoppingDistance = attackDistance;
       // agent.speed = patrolSpeed;
    }


    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float health, float damage,
        float runSpeed, float patrolSpeed, Color skinColor)
    {
        // 체력 설정
        startingHealth = health; // 초기 시작 체력
        this.health = health;  // 체력

        // 내비메쉬 에이전트의 이동 속도 설정
        this.runSpeed = runSpeed;
        this.patrolSpeed = patrolSpeed;

        this.damage = damage;  // 공격력

       // agent.speed = patrolSpeed; // 위에서 변경된 patrolSpeed로 다시 적용 //이게 뭔지 승호꺼랑 합쳐야할거같은데.. 뭘까
    }


}



