using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VillainFSM : MonoBehaviour
{
    // 빌런 상태 상수
    enum VillainState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // 빌런 상태 변수
    VillainState v_state;

    //플레이어 게임 오브젝트
    GameObject player;

    // 플레이어 발견 범위
    public float findDistance = 8.0f;

    // 캐릭터 컨트롤러
    CharacterController cc;

    // 이동속도
    public float moveSpeed = 5.0f;

    // 공격 가능 범위
    public float attackDistance = 2.0f;

    // 누적시간
    float currentTime = 0;

    // 공격 딜레이 시간
    float attackDelay = 2.0f;

    // 빌런 공격력
    public int attackPower = 3;

    // 초기 위치 저장용 변수
    Vector3 originPos;
    Quaternion originRot;

    // 이동 가능 범위
    public float moveDistance = 20.0f;

    // 최대 체력
    public int maxHp = 10;

    //현재 체력 변수
    int currentHp;

    // 슬라이더 변수
    public Slider hpSlider;

    //네비게이션 메쉬 에이전트
    NavMeshAgent smith;

    void Start()
    {
        // 초기 빌런 상태는 idle
        v_state = VillainState.Idle;

        // 플레이어 검색
        player = GameObject.Find("Player");

        // 캐릭터 컨트롤러 가져오기
        cc = GetComponent<CharacterController>();

        // 초기 위치, 회전 저장
        originPos = transform.position;
        originRot = transform.rotation;

        //현재 체력 설정
        currentHp = maxHp;

        //네브메쉬 에이전트 컴포넌트 가져오기
        smith = GetComponent<NavMeshAgent>();
        smith.speed = moveSpeed;
        smith.stoppingDistance = attackDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // 현재 상태 체크 >> 해당 상태별로 정해진 기능 수행
        switch (v_state)
        {
            case VillainState.Idle:
                Idle();
                break;
            case VillainState.Move:
                Move();
                break;
            case VillainState.Attack:
                Attack();
                break;
            case VillainState.Return:
                Return();
                break;
            case VillainState.Damaged:
                break;
            case VillainState.Die:
                break;

        }

        // hp 슬라이더 값에 체력 비율 적용 
        hpSlider.value = (float)currentHp / (float)maxHp;
    }

    void Idle()
    {
        // 플레이어와의 거리가 발견 범위이내 >> move
        if (Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            //이동 상태로 변경
            v_state = VillainState.Move;
            print("Villain 상태전환: Idle -> Move");            
        }
    }

    void Move()
    {

        // 현재 위치가 이동 가능 범위밖
        if (Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // 현재 상태를 복귀로 전환
            v_state = VillainState.Return;
            print("Villain 상태 전환: Move -> Return");
        }

        // 플레이어와의 거리가 공격 범위보다 멀 경우 >> 플레이어를 향해 이동
        else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            //네브메쉬 에이전트를 이용하여 타겟 방향으로 이동
            smith.SetDestination(player.transform.position);
            smith.stoppingDistance = attackDistance;
        }

        // 공격 범위 안
        else
        {
            //공격 상태로 변경
            v_state = VillainState.Attack;
            print("Villain 상태 전환: Move -> Attack");

            // 누적 시간을 공격 딜레이 시간만큼 미리 진행
            currentTime = attackDelay;

            //이동 멈추고, 타겟 초기화
            smith.isStopped = true;
            smith.ResetPath();
        }
    }

    //공격
    void Attack()
    {
        // 플레이어가 공격 범위 내에 존재 >> 플레이어 공격
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            //현재 대기시간이 공격 대기 시간을 넘어갔다면
            if (currentTime >= attackDelay)
            {
                currentTime = 0;
                //플레이어 공격
                print("Villain의 공격!");
                HitEvent();
            }

            //// 일정한 시간마다 플레이어 공격
            //currentTime += Time.deltaTime;
            //if (currentTime > attackDelay)
            //{
            //    player.GetComponent<PlayerController>().DamageAction(attackPower);
            //    currentTime = 0;
            //}
            else
            {
                //시간을 누적
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            //이동 상태로 전환
            v_state = VillainState.Move;
            print("Villain 상태 전환: Attack -> Move");
        }
    }

    //플레이어에게 데미지를 주는 함수
    public void HitEvent()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.DamageAction(attackPower);
    }

    void Return()
    {

        Vector3 dist = originPos - transform.position;
        dist.y = 0;

        // 초기 위치에서의 거리가 0.1f 초과면 초기 위치 쪽으로 이동
        //if (Vector3.Distance(transform.position, originPos) > 1.0f)
        if (dist.magnitude > 0.9f)
        {
            //print("return");
            //Vector3 dir = (originPos - transform.position).normalized;
            Vector3 dir = dist.normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // 방향을 복귀 지점으로 전환
            transform.forward = dir;
        }
        // 그렇지 않다면 자신의 위치를 초기 위치로 조정 >> 대기 상태 전환
        else
        {
            // 위치, 회전 값을 초기 상태로 변환
            transform.position = originPos;
            transform.rotation = originRot;

            //대기 상태로 전환
            v_state = VillainState.Idle;
            print("Villain 상태 전환: Return -> Idle");
        }
    }

    void Damaged()
    {
        // 피격 상태 처리 코루틴 실행
        StartCoroutine(DamageProcess());
    }

    // 데미지 처리용 코루틴 함수
    IEnumerator DamageProcess()
    {
        // 피격 모션만큼 기다리기
        yield return new WaitForSeconds(0.5f);

        // 현재 상태를 이동 상태로 전환
        v_state = VillainState.Move;
    }

    void Die()
    {
        // 진행중인 피격 코루틴 중지
        StopAllCoroutines();

        // 죽음 상태 처리 코루틴 실행
        StartCoroutine(DieProcess());
        VaccineManager.instance.DropVaccineToPosition(transform.position, 1);
    }

    IEnumerator DieProcess()
    {
        // 캐릭터 컨트롤러 컴포넌트 비활성화
        cc.enabled = false;

        // 2초동안 기다린 후 자기 자신 제거
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // 데미지 처리 함수
    public void HitVillain(int value)
    {
        // 이미 피격, 사망, 복귀 상태라면 함수 종료
        if (v_state == VillainState.Damaged || v_state == VillainState.Die || v_state == VillainState.Return)
        {
            return;
        }

        // 플레이어의 공격력만큼 에너미 체력 감소
       currentHp -= value;

        // 빌런 hp가 0보다 크면 
        if (currentHp > 0)
        {
            //피격 상태로 전환
            v_state = VillainState.Damaged;
            print("Villain 상태 전환: Any state -> Damaged");
            Damaged();
        }
        // 그렇지 않다면
        else
        {
            //죽음 상태로 전환
            v_state = VillainState.Die;
            print("Villain 상태 전환: Any state -> Die");
            Die();
        }
    }
}
